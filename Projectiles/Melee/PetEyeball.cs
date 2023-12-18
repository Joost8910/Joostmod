using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class PetEyeball : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pet Eyeball");
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 44;
            Projectile.height = 44;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Width = (int)(44f * Projectile.scale);
            hitbox.Height = (int)(44f * Projectile.scale);
            hitbox.X -= (int)((44f * Projectile.scale - 44f) * 0.5f);
            hitbox.Y -= (int)((44f * Projectile.scale - 44f) * 0.5f);
        }
        public override bool? CanHitNPC(NPC target)
        {
            return !target.friendly && Projectile.ai[1] >= 10;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = (int)(30f * Projectile.scale);
            height = (int)(30f * Projectile.scale);
            return true;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Projectile.Center.X > Main.player[Projectile.owner].Center.X)
            {
                hitDirection = 1;
            }
            else
            {
                hitDirection = -1;
            }
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 playerPos = player.RotatedRelativePoint(player.MountedCenter, true);
            Projectile.localAI[1] = 1;
            bool channeling = player.channel && !player.noItems && !player.CCed;
            if (channeling)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    float scaleFactor = 1f;
                    if (player.inventory[player.selectedItem].shoot == Projectile.type)
                    {
                        Projectile.scale = player.inventory[player.selectedItem].scale;
                        Projectile.localAI[1] = 10f / player.inventory[player.selectedItem].useTime / player.GetAttackSpeed(DamageClass.Melee);
                        scaleFactor = player.inventory[player.selectedItem].shootSpeed;
                    }
                    Vector2 dir = Main.MouseWorld - Projectile.Center;
                    dir = dir.RotatedByRandom(MathHelper.ToRadians(40));
                    dir.Normalize();
                    if (dir.HasNaNs())
                    {
                        dir = Vector2.UnitX * player.direction;
                    }
                    if (Projectile.Distance(Main.MouseWorld) < 72)
                    {
                        scaleFactor = Projectile.Distance(Main.MouseWorld) / 2f;
                    }
                    if (dir.X * scaleFactor != Projectile.velocity.X || dir.Y * scaleFactor != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    Projectile.velocity = dir * scaleFactor;
                }
                if (Collision.SolidCollision(Projectile.Center + new Vector2(-4, -4), 8, 8))
                {
                    Projectile.velocity = Projectile.DirectionTo(playerPos) * 9;
                    Projectile.position += Projectile.velocity;
                }
            }
            else
            {
                Projectile.velocity = Projectile.DirectionTo(playerPos) * 18;
                Projectile.tileCollide = false;
                if (Projectile.Distance(playerPos) < 20 || Projectile.Distance(playerPos) > 800)
                {
                    Projectile.Kill();
                }
            }
            Projectile.localNPCHitCooldown = (int)(10f / Projectile.localAI[1]);

            if (Projectile.ai[1] == 0)
            {
                Projectile.ai[0] = 0;
            }
            Projectile.ai[1] = Projectile.ai[1] < 15 ? Projectile.ai[1] + Projectile.localAI[1] : 0;
            if (Projectile.ai[1] < 5)
            {
                Projectile.frame = 0;
            }
            else if (Projectile.ai[1] < 10)
            {
                Projectile.frame = 1;
            }
            else
            {
                Projectile.frame = 2;
                if (Projectile.soundDelay <= 0)
                {
                    Projectile.soundDelay = (int)(10f / Projectile.localAI[1]);
                    SoundEngine.PlaySound(SoundID.NPCHit2.WithVolumeScale(0.275f * Projectile.scale).WithPitchOffset(0.3f - 0.5f * (Projectile.scale - 1)), Projectile.Center);
                    for (int i = 0; i < 6 * Projectile.scale; i++)
                    {
                        int dust = Dust.NewDust(Projectile.Center - new Vector2(12 * Projectile.scale, 12 * Projectile.scale), (int)(24 * Projectile.scale), (int)(24 * Projectile.scale), 247, Projectile.velocity.X * 0.3f * Projectile.scale, Projectile.velocity.Y * 0.3f * Projectile.scale, 150, Color.LightBlue);
                        Main.dust[dust].noGravity = true;
                    }
                }
            }
            if (Projectile.Distance(playerPos) > 250)
            {
                Projectile.position = playerPos + Projectile.DirectionFrom(playerPos) * 250 - Projectile.Size / 2f;
            }
            if (Projectile.Center.X < playerPos.X)
            {
                Projectile.direction = -1;
            }
            else
            {
                Projectile.direction = 1;
            }
            for (int i = 0; i < Main.item.Length; i++)
            {
                if (Main.item[i].active)
                {
                    Item I = Main.item[i];
                    if (Projectile.Hitbox.Intersects(I.Hitbox))
                    {
                        I.velocity = Projectile.velocity;
                        I.position = Projectile.Center - I.Size / 2;
                    }
                }
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.direction == -1 ? 3.14f : 0);
            Projectile.spriteDirection = Projectile.direction;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 10;
            player.itemAnimation = 10;
            Vector2 direction = player.DirectionTo(Projectile.Center);
            player.itemRotation = (float)Math.Atan2((double)(direction.Y * Projectile.direction), (double)(direction.X * Projectile.direction));
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Projectiles/PetEyeball_Chain");

            Vector2 position = Projectile.Center;
            Player player = Main.player[Projectile.owner];
            Vector2 mountedCenter = player.RotatedRelativePoint(player.MountedCenter, true);
            Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            float num1 = texture.Height;
            Vector2 vector2_4 = mountedCenter - position;
            float rotation = (float)Math.Atan2(vector2_4.Y, vector2_4.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                flag = false;
            while (flag)
            {
                if ((double)vector2_4.Length() < (double)num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    vector2_4 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16.0));
                    color2 = Projectile.GetAlpha(color2);
                    Main.EntitySpriteDraw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0);
                }
            }

            return true;
        }
    }
}