using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hybrid
{
    public class BambooShoot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mighty Bamboo Shoot");
            Main.projFrames[Projectile.type] = 6;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 26;
            Projectile.extraUpdates = 1;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[0] == 0)
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            if (Projectile.ai[0] == 0)
            {
                return base.CanHitPvp(target);
            }
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.localAI[1] > -21)
            {
                target.velocity.Y -= knockback * target.knockBackResist * player.gravDir;
            }
            else
            {
                target.velocity.Y += knockback * target.knockBackResist * player.gravDir;
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Projectile.localAI[1] <= -21)
            {
                damage = (int)(damage * 1.2f);
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (!target.noKnockback)
            {
                if (Projectile.localAI[1] > -21)
                {
                    target.velocity.Y -= Projectile.knockBack * player.gravDir;
                }
                else
                {
                    target.velocity.Y += Projectile.knockBack * player.gravDir;
                }
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (Projectile.ai[0] == 0)
            {
                Player player = Main.player[Projectile.owner];
                Vector2 unit = Projectile.velocity;
                Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
                float point = 0f;
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), vector, vector + unit * Projectile.width, Projectile.height, ref point))
                {
                    return true;
                }
            }
            return false;
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            float speed = 21f / player.inventory[player.selectedItem].useTime / player.GetAttackSpeed(DamageClass.Melee) / 2;
            Projectile.localNPCHitCooldown = (int)(21f / speed);
            Projectile.scale = player.inventory[player.selectedItem].scale;
            Projectile.width = (int)(Projectile.scale * 100);
            Projectile.height = (int)(Projectile.scale * 12);
            bool channeling = !player.dead && (player.controlUseItem && Projectile.ai[0] == 0 && (!player.controlUseTile || Projectile.localAI[1] == 0) || player.controlUseTile && Projectile.ai[0] > 0 || Projectile.localAI[1] != 0 && !(Projectile.localAI[1] <= -20 && Projectile.localAI[1] > -26) && Projectile.ai[0] == 0) && !player.noItems && !player.CCed;
            if (channeling)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    Vector2 vector13 = Main.MouseWorld - vector;
                    vector13.Normalize();
                    if (vector13.HasNaNs())
                    {
                        vector13 = Vector2.UnitX * player.direction;
                    }
                    if (vector13.X != Projectile.velocity.X || vector13.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    Projectile.velocity = vector13;
                }
            }
            else
            {
                Projectile.Kill();
            }
            if (Projectile.velocity.X < 0)
            {
                Projectile.direction = -1;
            }
            else
            {
                Projectile.direction = 1;
            }
            if (Projectile.ai[0] == 0)
            {
                Projectile.velocity.X = Projectile.direction * 4;
                if (Projectile.soundDelay >= 0 && (Projectile.localAI[1] == 0 || Projectile.localAI[1] <= -26))
                {
                    SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_monk_staff_swing_0"), Projectile.Center); // 213
                    Projectile.soundDelay = -1;
                }
                if (Projectile.localAI[1] > -26)
                {
                    Projectile.soundDelay = 1;
                }
                Projectile.localAI[1] -= speed;
                if (Projectile.localAI[1] >= -21)
                {
                    Projectile.velocity.Y = (Projectile.localAI[1] + 8) * player.gravDir * 2;
                }
                else
                {
                    Projectile.velocity.Y = (-14 - (Projectile.localAI[1] + 21)) * player.gravDir * 2;
                }
                if (Projectile.localAI[1] <= -42)
                {
                    Projectile.Kill();
                }
            }
            else
            {
                if (Projectile.localAI[0] < 5)
                {
                    Projectile.localAI[1] += speed * player.GetAttackSpeed(DamageClass.Melee);
                }
                if (Projectile.localAI[1] > 30 && player.inventory.Any(item => item.ammo == AmmoID.Dart && item.stack > 0))
                {
                    Projectile.localAI[1] = 0;
                    Projectile.localAI[0]++;
                    SoundEngine.PlaySound(SoundID.Item65, Projectile.Center);
                    if ((int)Projectile.ai[1] == ProjectileID.Seed)
                    {
                        player.ConsumeItem(ItemID.Seed);
                    }
                    if ((int)Projectile.ai[1] == ProjectileID.CrystalDart)
                    {
                        player.ConsumeItem(ItemID.CrystalDart);
                    }
                    if ((int)Projectile.ai[1] == ProjectileID.CursedDart)
                    {
                        player.ConsumeItem(ItemID.CursedDart);
                    }
                    if ((int)Projectile.ai[1] == ProjectileID.IchorDart)
                    {
                        player.ConsumeItem(ItemID.IchorDart);
                    }
                    if ((int)Projectile.ai[1] == ProjectileID.PoisonDartBlowgun)
                    {
                        player.ConsumeItem(ItemID.PoisonDart);
                    }
                    Projectile.frame++;
                }
            }
            Projectile.velocity.Normalize();
            Projectile.position = vector + Projectile.velocity * Projectile.width * 0.5f - Projectile.Size / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.direction == -1 ? 3.14f : 0);
            Projectile.spriteDirection = Projectile.direction;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * Projectile.direction), (double)(Projectile.velocity.X * Projectile.direction));
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                effects = SpriteEffects.None;
            }
            Color color = Lighting.GetColor((int)(Projectile.Center.X / 16), (int)(Projectile.Center.Y / 16));
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Rectangle rectangle = new Rectangle(0, Projectile.frame * (texture.Height / Main.projFrames[Projectile.type]), texture.Width, texture.Height / Main.projFrames[Projectile.type]);
            Vector2 vector = new Vector2(texture.Width / 2f, texture.Height / Main.projFrames[Projectile.type] / 2f);
            if (Projectile.ai[0] == 0)
            {
                for (int k = 0; k < Projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + new Vector2(Projectile.width / 2, Projectile.height / 2);
                    Color color2 = color * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                    Main.EntitySpriteDraw(texture, drawPos, rectangle, color2, Projectile.oldRot[k], vector, Projectile.scale, effects, 0);
                }
            }
            Main.EntitySpriteDraw(texture, new Vector2(Projectile.position.X - Main.screenPosition.X + Projectile.width / 2 - texture.Width / 2f + vector.X, Projectile.position.Y - Main.screenPosition.Y + (4 + 7 * Projectile.scale) - texture.Height / Main.projFrames[Projectile.type] + vector.Y * 1.5f), new Rectangle?(rectangle), color, Projectile.rotation, vector, Projectile.scale, effects, 0);
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            if (Projectile.ai[0] > 0)
            {
                Projectile.velocity.Normalize();
                float shootSpeed = player.inventory[player.selectedItem].shootSpeed;
                for (int i = 0; i < Projectile.frame; i++)
                {
                    Vector2 vel = Projectile.velocity * (shootSpeed + i * shootSpeed / 11);
                    Projectile.NewProjectile(Projectile.GetSource_ItemUse_WithPotentialAmmo(player.HeldItem, AmmoID.Dart), vector, vel, (int)Projectile.ai[1], Projectile.damage, Projectile.knockBack / 2f, Projectile.owner);
                    SoundEngine.PlaySound(SoundID.Item63, Projectile.Center);
                }
            }
        }
    }
}