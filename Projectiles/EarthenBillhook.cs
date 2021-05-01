using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class EarthenBillhook : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Earthen Billhook");
		}
		public override void SetDefaults()
		{
			projectile.width = 46;
			projectile.height = 46;
			projectile.aiStyle = 19;
			projectile.timeLeft = 90;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
            projectile.ownerHitCheck = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 25;
            projectile.extraUpdates = 1;
		}
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[projectile.owner];
            if ((player.itemAnimation < player.itemAnimationMax / 2) && target.Distance(player.Center + player.velocity) > 60 + knockback + target.width / 2)
            {
                hitDirection = -projectile.direction;
                knockback *= 0.6f;
            }
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 center = player.RotatedRelativePoint(player.position + new Vector2(player.width / 2, 20), true);
            float speed = player.meleeSpeed / 2;
            if (player.inventory[player.selectedItem].shoot == projectile.type)
            {
                projectile.scale = player.inventory[player.selectedItem].scale;
                speed = (((36f / player.inventory[player.selectedItem].useTime) / player.meleeSpeed) / 2) * projectile.scale;
                projectile.localNPCHitCooldown = (int)(25 / (speed / projectile.scale) * 0.667f);
                projectile.width = (int)(46 * projectile.scale);
                projectile.height = (int)(46 * projectile.scale);
                projectile.netUpdate = true;
            }
            projectile.spriteDirection = projectile.direction * (int)player.gravDir;
            player.direction = projectile.direction;
            player.heldProj = projectile.whoAmI;
            player.itemTime = player.itemAnimation;
            projectile.position.X = center.X - (float)(projectile.width / 2);
            projectile.position.Y = center.Y - (float)(projectile.height / 2);
            Vector2 vel = projectile.velocity;
            vel.Normalize();
            projectile.position += vel * 7 * projectile.ai[1];
            if (projectile.ai[1] == 0f)
            {
                projectile.ai[1] = 3f;
                projectile.netUpdate = true;
            }
            if (player.itemAnimation < player.itemAnimationMax / 2)
            {
                projectile.ai[1] -= speed;
                if (player.itemAnimation > player.itemAnimationMax / 3)
                    projectile.velocity = projectile.velocity.RotatedBy(2 * 0.0174f * player.direction * player.gravDir);
            }
            else
            {
                projectile.ai[1] += speed;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.type == mod.ProjectileType("Boulder") && projectile.Distance(p.Center) < 40)
                    {
                        p.velocity = projectile.velocity * 2f;
                        p.damage = (int)(projectile.damage * 3f);
                        p.knockBack = projectile.knockBack * 3f;
                        p.owner = projectile.owner;
                        if (p.timeLeft <= 500)
                        {
                            Main.PlaySound(21, (int)p.Center.X, (int)p.Center.Y, 1, 1, -0.25f);
                            p.timeLeft = 540;
                        }
                        break;
                    }
                }
            }
            if (player.itemAnimation == 0)
            {
                projectile.Kill();
            }
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 2.355f;
            if (projectile.spriteDirection == -1)
            {
                projectile.rotation -= 1.57f;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
            Color color = lightColor;
            Vector2 vel = projectile.velocity;
            vel.Normalize();
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition - vel * 60 * projectile.scale, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, projectile.rotation, drawOrigin, projectile.scale, effects, 0f);
            return false;
        }
    }
}