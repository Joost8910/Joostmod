using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class GrognakHammer2 : ModProjectile
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Warhammer of Grognak");
            Main.projFrames[projectile.type] = 12;
		}
        public override void SetDefaults()
        {
            projectile.width = 82;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
            projectile.ownerHitCheck = true;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.scale = player.inventory[player.selectedItem].scale;
            projectile.width = (int)(82 * projectile.scale);
            projectile.height = (int)(30 * projectile.scale);
            projectile.localNPCHitCooldown = (int)(24 * player.meleeSpeed);

            if (projectile.owner == Main.myPlayer)
            {
                Vector2 mousePos = Main.MouseWorld;
                Vector2 diff = mousePos - player.Center;
                diff.Normalize();
                float home = 12f * player.meleeSpeed;
                projectile.velocity = ((home - 1f) * projectile.velocity + diff) / home;
                projectile.velocity.Normalize();
                projectile.spriteDirection = projectile.velocity.X > 0 ? 1 : -1;
                projectile.netUpdate = true;
            }
            projectile.position = player.Center - projectile.Size / 2;
            projectile.rotation = projectile.velocity.ToRotation();
            projectile.timeLeft = 2;

            if (projectile.ai[0] < 12)
            {
                player.heldProj = projectile.whoAmI;
            }
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.direction = (projectile.ai[0] > 5 && projectile.ai[0] < 16) ? projectile.spriteDirection : -projectile.spriteDirection;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
            
            if (projectile.ai[0] == 0)
            {
                Main.PlaySound(42, (int)projectile.Center.X, (int)projectile.Center.Y, 230, 1, -0.2f);
            }

            float speed = 1 / player.meleeSpeed;
            projectile.ai[0] += speed;
            if (projectile.ai[0] >= 4 && projectile.ai[1] == 1)
            {
                projectile.ai[1] = 0;
                Main.PlaySound(2, (int)projectile.Center.X, (int)projectile.Center.Y, 8, 0.5f);
                Projectile.NewProjectile(projectile.Center, projectile.velocity * 6 * speed, mod.ProjectileType("GrognakBeam2"), projectile.damage, projectile.knockBack, projectile.owner);
            }
            if (projectile.ai[0] >= 24)
            {
                if (player.controlUseTile)
                {
                    projectile.Kill();
                }
                else if (player.controlUseItem)
                {
                    projectile.ai[0] = 0;
                    projectile.ai[1] = 1;
                }
                else
                {
                    projectile.Kill();
                }
            }
            projectile.frame = (int)(projectile.ai[0] / 2);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.frame >= 3 && projectile.frame <= 5)
            {
                Player p = Main.player[projectile.owner];
                Vector2 unit = projectile.velocity;
                float point = 0f;
                Vector2 start = p.Center + projectile.velocity * 53;
                Vector2 end = start + unit * 18;
                if (Collision.CheckAABBvLineCollision(target.Hitbox.TopLeft(), target.Hitbox.Size(), start, end, 18, ref point))
                {
                    crit = true;
                    Main.PlaySound(42, (int)target.Center.X, (int)target.Center.Y, 211, 0.3f);
                    for (int i = 0; i < 12; i++)
                    {
                        Dust.NewDustDirect(p.Center + projectile.velocity * 62, 1, 1, 197, 0, 0, 150, new Color(0, 255, 0), 1f).noGravity = true;
                    }
                }
            }
            if (projectile.ai[0] > 10)
            {
                damage = (int)(damage * 0.8f);
                knockback *= 0.8f;
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            if (projectile.frame >= 3 && projectile.frame <= 5)
            {
                Player p = Main.player[projectile.owner];
                Vector2 unit = projectile.velocity;
                float point = 0f;
                Vector2 start = p.Center + projectile.velocity * 54;
                Vector2 end = start + unit * 18;
                if (Collision.CheckAABBvLineCollision(target.Hitbox.TopLeft(), target.Hitbox.Size(), start, end, 18, ref point))
                {
                    crit = true;
                    Main.PlaySound(42, (int)target.Center.X, (int)target.Center.Y, 211, 0.3f);
                    for (int i = 0; i < 12; i++)
                    {
                        Dust.NewDustDirect(p.Center + projectile.velocity * 62, 1, 1, 197, 0, 0, 150, new Color(0, 255, 0), 1f).noGravity = true;
                    }
                }
            }
            if (projectile.ai[0] > 10)
            {
                damage = (int)(damage * 0.8f);
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Player p = Main.player[projectile.owner];
            Vector2 unit = projectile.velocity;
            float point = 0f;
            Vector2 start = p.Center + projectile.velocity * (-79 + (projectile.frame * 19));
            Vector2 end = start + unit * projectile.width;
            if (projectile.ai[0] >= 10)
            {
                start = p.Center + projectile.velocity * -3;
                if (projectile.ai[0] >= 16)
                {
                    start -= projectile.velocity * (20 * (projectile.frame - 7));
                    end = p.Center;
                }
                else
                {
                    end -= projectile.velocity * ((projectile.frame - 4) * 16);
                }
            }
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, projectile.height, ref point))
            {
                return true;
            }
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipVertically;
            }
            else
            {
                effects = SpriteEffects.None;
            }
            Color color = Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16));
            Texture2D texture = Main.projectileTexture[projectile.type];
            Rectangle rectangle = new Rectangle(0, projectile.frame * (texture.Height / Main.projFrames[projectile.type]), texture.Width, (texture.Height / Main.projFrames[projectile.type]));
            Vector2 vector = new Vector2((texture.Width / 2f), ((texture.Height / Main.projFrames[projectile.type]) / 2f));
            spriteBatch.Draw(texture, new Vector2(projectile.position.X - Main.screenPosition.X + (float)(projectile.width / 2) - (float)(texture.Width) / 2f + vector.X, projectile.position.Y - Main.screenPosition.Y + (25) - (texture.Height / Main.projFrames[projectile.type]) + vector.Y * 1.5f), new Rectangle?(rectangle), color, projectile.rotation, vector, projectile.scale, effects, 0f);
            return false;
        }
    }
}