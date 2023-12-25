using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Ranged
{
    public class DragonBlaster : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Blaster");
        }
        public override void SetDefaults()
        {
            Projectile.width = 44;
            Projectile.height = 44;
            Projectile.scale = 0.8f;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            var source = Projectile.GetSource_FromAI();
            Vector2 origin = player.GetFrontHandPosition(Player.CompositeArmStretchAmount.None, 0);
            float speed = 11;
            float shootSpeed = 13f;
            if (Projectile.ai[0] == 1)
            {
                origin = player.GetBackHandPosition(Player.CompositeArmStretchAmount.None, 0);
                Projectile.scale = 0.75f;
            }
            Vector2 v = Main.OffsetsPlayerHeadgear[player.bodyFrame.Y / player.bodyFrame.Height];
            v.Y -= 2f;
            origin += v * player.gravDir;

            Projectile.width = (int)(44f * Projectile.scale);
            Projectile.height = (int)(44f * Projectile.scale);

            if (player.inventory[player.selectedItem].shoot == Projectile.type)
            {
                shootSpeed = player.inventory[player.selectedItem].shootSpeed;
                speed = player.inventory[player.selectedItem].useTime;
                Projectile.netUpdate = true;
            }
            bool channelling = player.controlUseItem && Projectile.ai[0] == 0 || player.controlUseTile && Projectile.ai[0] == 1;
            if (Main.myPlayer == Projectile.owner && player.ownedProjectileCounts[Projectile.type] < 2 && (Projectile.ai[0] == 0 && player.controlUseTile || Projectile.ai[0] == 1 && player.controlUseItem && Projectile.ai[1] > 1))
            {
                player.ownedProjectileCounts[Projectile.type] = 2;
                Projectile.NewProjectileDirect(source, origin, Projectile.velocity, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner, 1 - Projectile.ai[0]);
            }
            if (Projectile.ai[1] <= 1)
            {
                Projectile.ai[1]++;
            }
            if (Projectile.localAI[1] <= 0 && Main.myPlayer == Projectile.owner)
            {
                Vector2 dir = Main.MouseWorld - origin;
                dir.Normalize();
                player.ChangeDir(Math.Sign(player.DirectionTo(Main.MouseWorld).X));
                if (dir.HasNaNs())
                {
                    dir = Vector2.UnitX * player.direction;
                }
                if (dir.X != Projectile.velocity.X || dir.Y != Projectile.velocity.Y)
                {
                    Projectile.netUpdate = true;
                }
                Projectile.velocity = dir;
            }
            if (player.noItems || player.CCed || player.dead || !player.active || !channelling && Projectile.localAI[1] <= 0 && Projectile.localAI[0] <= 0 && Projectile.ai[1] <= 2)
            {
                Projectile.Kill();
            }
            else if (channelling && Projectile.localAI[0] > 0)
            {
                if (Projectile.localAI[1] <= 0)
                    Projectile.ai[1]++;
                if (Projectile.ai[1] >= 15 && Projectile.ai[1] < 120)
                {
                    if (Projectile.ai[1] % 15 == 0)
                    {
                        SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
                    }
                    if (Main.rand.NextBool(Math.Max(20 - (int)(Projectile.ai[1] / 6), 1)))
                    {
                        Dust.NewDustDirect(Projectile.Center + Projectile.velocity * 30 * Projectile.scale - new Vector2(12, 12), 12, 12, 6).noGravity = true;
                    }
                }
                if (Projectile.ai[1] == 120)
                {
                    SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_phantom_phoenix_shot_1"), Projectile.Center); // 218
                }
                if (Projectile.ai[1] >= 120)
                {
                    player.velocity.X *= 0.99f;
                    Dust.NewDustDirect(Projectile.Center + Projectile.velocity * 30 * Projectile.scale - new Vector2(12, 12), 12, 12, 6).noGravity = true;
                }
            }
            else if (Projectile.localAI[0] <= 0)
            {
                if (Projectile.ai[1] < 120)
                {
                    /*
                    int type = 0;
                    Item item = new Item();
                    bool canShoot = false;
                    bool flag = false;
                    for (int i = 54; i < 58; i++)
                    {
                        if (player.inventory[i].ammo == AmmoID.Bullet && player.inventory[i].stack > 0)
                        {
                            item = player.inventory[i];
                            canShoot = true;
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        for (int j = 0; j < 54; j++)
                        {
                            if (player.inventory[j].ammo == AmmoID.Bullet && player.inventory[j].stack > 0)
                            {
                                item = player.inventory[j];
                                canShoot = true;
                                break;
                            }
                        }
                    }
                    */
                    bool canShoot = player.PickAmmo(player.HeldItem, out int type, out shootSpeed, out int damage, out float knockback, out int bulletID);

                    if (canShoot)
                    {
                        shootSpeed += (int)(Projectile.ai[1] / 30);
                        /*
                        type = item.shoot;
                        if (Projectile.timeLeft < 3600 && item.consumable && ItemLoader.ConsumeAmmo(player.HeldItem, item, player))
                        {
                            player.ConsumeItem(item.type);
                        }
                        */
                        if (Main.myPlayer == Projectile.owner)
                            Projectile.NewProjectile(source, origin, Projectile.velocity * shootSpeed, type, damage + (Projectile.damage * (int)(Projectile.ai[1] / 30)), knockback + (int)(Projectile.ai[1] / 30), Projectile.owner);
                        SoundEngine.PlaySound(SoundID.Item41, Projectile.Center);
                    }
                    else
                    {
                        SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/MissileClick"), Projectile.Center);
                    }
                    Projectile.localAI[1] = speed;
                    Projectile.localAI[0] = 1;
                }
                else
                {
                    Projectile.NewProjectile(source, origin + Projectile.velocity * 30 * Projectile.scale + Projectile.velocity * 24, Projectile.velocity * shootSpeed * 0.5f, ModContent.ProjectileType<DragonBlast>(), Projectile.damage * 7, Projectile.knockBack * 7, Projectile.owner);

                    Vector2 dir = -Projectile.velocity;
                    dir.Normalize();
                    dir = dir * shootSpeed;
                    if (player.velocity.X * dir.X <= 0 || player.velocity.X * dir.X > 0 && Math.Abs(player.velocity.X) < Math.Abs(dir.X))
                    {
                        player.velocity.X = dir.X;
                    }
                    if (player.velocity.Y * dir.Y <= 0 || player.velocity.Y * dir.Y > 0 && Math.Abs(player.velocity.Y) < Math.Abs(dir.Y))
                    {
                        player.velocity.Y = dir.Y;
                    }
                    SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
                    SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_phantom_phoenix_shot_0"), Projectile.Center); // 217
                    Projectile.localAI[1] = speed * 5;
                    Projectile.localAI[0] = 5;
                }
                Projectile.ai[1] = 2;
            }
            if (Projectile.localAI[1] > 0 && Projectile.localAI[0] > 0)
            {
                Projectile.localAI[1]--;
                float len = Projectile.localAI[0] * speed;
                float kick = Projectile.localAI[1] < len * 0.75f ? Projectile.localAI[1] / 3 : len - Projectile.localAI[1];
                float rot = Projectile.velocity.ToRotation() - 7f * 0.0174f * Projectile.direction * kick;
                Projectile.rotation = rot + (player.direction == -1 ? 3.14f : 0);
            }
            else
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + (player.direction == -1 ? 3.14f : 0);
            }
            if (Projectile.localAI[1] <= 0 && !channelling)
            {
                Projectile.localAI[0] = 0;
            }
            float rotate = (float)Math.Atan2((double)(Projectile.velocity.Y * Projectile.direction), (double)(Projectile.velocity.X * Projectile.direction));
            float armRot = rotate - (float)(Math.PI / 2) * Projectile.direction;
            if (Projectile.ai[0] == 0)
            {
                player.heldProj = Projectile.whoAmI;
                player.itemRotation = rotate;
                player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, armRot);
                origin = player.GetFrontHandPosition(Player.CompositeArmStretchAmount.Full, armRot);
                origin += v * player.gravDir;
            }
            else
            {
                player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, armRot);
                origin = player.GetBackHandPosition(Player.CompositeArmStretchAmount.Full, armRot);
                origin += v * player.gravDir;
            }
            Projectile.position = origin + Projectile.velocity * Projectile.scale - Projectile.Size / 2f;
            Projectile.spriteDirection = player.direction;
            if (player.gravDir < 0)
            {
                Projectile.rotation += 3.14f;
                Projectile.spriteDirection = -player.direction;
            }
            //player.ChangeDir(Projectile.direction);
            player.itemTime = 2;
            player.itemAnimation = 2;
            Projectile.timeLeft = 2;
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
            Color color = lightColor;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            return false;
        }
    }
}