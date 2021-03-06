using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class TerraSpear : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Terra Spear");
		}
		public override void SetDefaults()
		{
			projectile.width = 54;
			projectile.height = 54;
			projectile.scale = 1.2f;
			projectile.aiStyle = 19;
			projectile.timeLeft = 90;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.light = 0.5f;
			projectile.ownerHitCheck = true;
			projectile.hide = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 6;
        }
		
		public override void AI()
        {
            Player player = Main.player[projectile.owner];
            player.direction = projectile.direction;
            player.heldProj = projectile.whoAmI;
            player.itemTime = player.itemAnimation;
            float speed = player.meleeSpeed;
            if (player.inventory[player.selectedItem].shoot == projectile.type)
            {
                projectile.scale = player.inventory[player.selectedItem].scale;
                speed = ((25f / player.inventory[player.selectedItem].useTime)) * projectile.scale;
                projectile.localNPCHitCooldown = (int)(6 / (speed / projectile.scale));
                projectile.width = (int)(54 * projectile.scale);
                projectile.height = (int)(54 * projectile.scale);
                projectile.netUpdate = true;
            }
            projectile.position = player.RotatedRelativePoint(player.MountedCenter) - (projectile.Size / 2);
            projectile.position += projectile.velocity * speed * projectile.ai[0];
            if (projectile.ai[0] == 0f)
			{
				projectile.ai[0] = 3f;
				projectile.netUpdate = true;
			}
			if (player.itemAnimation < player.itemAnimationMax * 2f / 3f)
            {
                if (projectile.ai[1] == 0)
                {
                    Projectile.NewProjectile(projectile.Center, projectile.velocity, mod.ProjectileType("TerraSpearBeam"), projectile.damage, projectile.knockBack / 2, projectile.owner);
                    projectile.ai[1]++;
                }
                projectile.ai[0] -= 1.86f;
			}
			else
			{
				projectile.ai[0] += 3.73f;
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
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition - vel * 110 * projectile.scale, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, projectile.rotation, drawOrigin, projectile.scale, effects, 0f);
            return false;
        }
    }
}