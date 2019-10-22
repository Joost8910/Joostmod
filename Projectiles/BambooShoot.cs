using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class BambooShoot : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mighty Bamboo Shoot");
            Main.projFrames[projectile.type] = 6;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.width = 100;
            projectile.height = 12;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ranged = true;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.ownerHitCheck = true;
            projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 26;
            projectile.extraUpdates = 1;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.ai[0] == 0)
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            if (projectile.ai[0] == 0)
            {
                return base.CanHitPvp(target);
            }
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (projectile.localAI[1] > -21)
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
            if (projectile.localAI[1] <= -21)
            {
                damage = (int)(damage * 1.2f);
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (!target.noKnockback)
            {
                if (projectile.localAI[1] > -21)
                {
                    target.velocity.Y -= projectile.knockBack * player.gravDir;
                }
                else
                {
                    target.velocity.Y += projectile.knockBack * player.gravDir;
                }
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projectile.ai[0] == 0)
            {
                Player player = Main.player[projectile.owner];
                Vector2 unit = projectile.velocity;
                Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
                float point = 0f;
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), vector, vector + unit * projectile.width, projectile.height, ref point))
                {
                    return true;
                }
            }
            return false;
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            float speed = ((21f / player.inventory[player.selectedItem].useTime) / player.meleeSpeed) / 2;
            projectile.localNPCHitCooldown = (int)(21f / speed);
            projectile.scale = player.inventory[player.selectedItem].scale;
            projectile.width = (int)(projectile.scale * 100);
            projectile.height = (int)(projectile.scale * 12);
            bool channeling = !player.dead && ((player.controlUseItem && projectile.ai[0] == 0 && (!player.controlUseTile || projectile.localAI[1] == 0)) || (player.controlUseTile && projectile.ai[0] > 0) || (projectile.localAI[1] != 0 && !(projectile.localAI[1] <= -20 && projectile.localAI[1] > -26) && projectile.ai[0] == 0)) && !player.noItems && !player.CCed;
            if (channeling)
            {
                if (Main.myPlayer == projectile.owner)
                {
                    Vector2 vector13 = Main.MouseWorld - vector;
                    vector13.Normalize();
                    if (vector13.HasNaNs())
                    {
                        vector13 = Vector2.UnitX * (float)player.direction;
                    }
                    if (vector13.X != projectile.velocity.X || vector13.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.velocity = vector13;
                }
            }
            else
            {
                projectile.Kill();
            }
            if (projectile.velocity.X < 0)
            {
                projectile.direction = -1;
            }
            else
            {
                projectile.direction = 1;
            }
            if (projectile.ai[0] == 0)
            {
                projectile.velocity.X = projectile.direction * 4;
                if (projectile.soundDelay >= 0 && (projectile.localAI[1] == 0 || projectile.localAI[1] <= -26))
                {
                    Main.PlaySound(42, (int)projectile.Center.X, (int)projectile.Center.Y, 213);
                    projectile.soundDelay = -1;
                }
                if (projectile.localAI[1] > -26)
                {
                    projectile.soundDelay = 1;
                }
                projectile.localAI[1] -= speed;
                if (projectile.localAI[1] >= -21)
                {
                    projectile.velocity.Y = (projectile.localAI[1] + 8) * player.gravDir * 2;
                }
                else
                {
                    projectile.velocity.Y = (-14 - (projectile.localAI[1] + 21)) * player.gravDir * 2;
                }
                if (projectile.localAI[1] <= -42)
                {
                    projectile.Kill();
                }
            }
            else
            {
                if (projectile.localAI[0] < 5)
                {
                    projectile.localAI[1] += speed * player.meleeSpeed;
                }
                if (projectile.localAI[1] > 30 && player.inventory.Any(item => item.ammo == AmmoID.Dart && item.stack > 0))
                {
                    projectile.localAI[1] = 0;
                    projectile.localAI[0]++;
                    Main.PlaySound(2, projectile.Center, 65);
                    if ((int)projectile.ai[1] == ProjectileID.Seed)
                    {
                        player.ConsumeItem(ItemID.Seed);
                    }
                    if ((int)projectile.ai[1] == ProjectileID.CrystalDart)
                    {
                        player.ConsumeItem(ItemID.CrystalDart);
                    }
                    if ((int)projectile.ai[1] == ProjectileID.CursedDart)
                    {
                        player.ConsumeItem(ItemID.CursedDart);
                    }
                    if ((int)projectile.ai[1] == ProjectileID.IchorDart)
                    {
                        player.ConsumeItem(ItemID.IchorDart);
                    }
                    if ((int)projectile.ai[1] == ProjectileID.PoisonDartBlowgun)
                    {
                        player.ConsumeItem(ItemID.PoisonDart);
                    }
                    projectile.frame++;
                }
            }
            projectile.velocity.Normalize();
            projectile.position = (vector + (projectile.velocity * projectile.width * 0.5f)) - (projectile.Size / 2f);
            projectile.rotation = projectile.velocity.ToRotation() + (projectile.direction == -1 ? 3.14f : 0);
            projectile.spriteDirection = projectile.direction;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                effects = SpriteEffects.None;
            }
            Color color = Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16));
            Texture2D texture = Main.projectileTexture[projectile.type];
            Rectangle rectangle = new Rectangle(0, projectile.frame * (texture.Height / Main.projFrames[projectile.type]), texture.Width, (texture.Height / Main.projFrames[projectile.type]));
            Vector2 vector = new Vector2((texture.Width / 2f), ((texture.Height / Main.projFrames[projectile.type]) / 2f));
            if (projectile.ai[0] == 0)
            {
                for (int k = 0; k < projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + new Vector2(projectile.width / 2, projectile.height / 2);
                    Color color2 = color * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                    spriteBatch.Draw(texture, drawPos, rectangle, color2, projectile.oldRot[k], vector, projectile.scale, effects, 0f);
                }
            }
            spriteBatch.Draw(texture, new Vector2(projectile.position.X - Main.screenPosition.X + (float)(projectile.width / 2) - (float)(texture.Width) / 2f + vector.X, projectile.position.Y - Main.screenPosition.Y + (4 + (7 * projectile.scale)) - (texture.Height / Main.projFrames[projectile.type]) + vector.Y * 1.5f), new Rectangle?(rectangle), color, projectile.rotation, vector, projectile.scale, effects, 0f);
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            if (projectile.ai[0] > 0)
            {
                projectile.velocity.Normalize();
                float shootSpeed = player.inventory[player.selectedItem].shootSpeed;
                for (int i = 0; i < projectile.frame; i++)
                {
                    Vector2 vel = projectile.velocity * (shootSpeed + (i * shootSpeed / 11));
                    Projectile.NewProjectile(vector, vel, (int)projectile.ai[1], projectile.damage, projectile.knockBack / 2f, projectile.owner);
                    Main.PlaySound(2, projectile.Center, 63);
                }
            }
        }
    }
}