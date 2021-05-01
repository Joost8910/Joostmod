using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class PowerSpiritBlast : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spirit of Power");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 500;
			projectile.height = 500;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.penetrate = -1;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = -1;
			projectile.timeLeft = 14;
			projectile.tileCollide = false;
		}
        public override void AI()
        {
            if (projectile.ai[0] == 0)
            {
                projectile.ai[0] = 1;
                projectile.ai[1] = 14;
            }
            if (projectile.ai[1] == 0)
            {
                projectile.ai[1] = 14;
            }
            if (projectile.localAI[0] == 0)
            {
                projectile.timeLeft = (int)projectile.ai[1];
                projectile.localAI[0] = 1;
            }
            int size = (int)projectile.ai[1] - projectile.timeLeft;
            projectile.scale = size * (1f / projectile.ai[1]) * projectile.ai[0];
            projectile.position.X = projectile.Center.X - (float)((500 * projectile.scale) / 2f);
            projectile.position.Y = projectile.Center.Y - (float)((500 * projectile.scale) / 2f);
            projectile.width = (int)(Math.Round(500 * projectile.scale));
            projectile.height = (int)(Math.Round(500 * projectile.scale));
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Collides(projectile.position, projectile.Size, target.position, target.Size))
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            if (Collides(projectile.position, projectile.Size, target.position, target.Size))
            {
                return base.CanHitPlayer(target);
            }
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            if (Collides(projectile.position, projectile.Size, target.position, target.Size))
            {
                return base.CanHitPvp(target);
            }
            return false;
        }
        public bool Collides(Vector2 ellipsePos, Vector2 ellipseDim, Vector2 boxPos, Vector2 boxDim)
        {
            Vector2 ellipseCenter = ellipsePos + 0.5f * ellipseDim;
            float x = 0f; //ellipse center
            float y = 0f; //ellipse center
            if (boxPos.X > ellipseCenter.X)
            {
                x = boxPos.X - ellipseCenter.X; //left corner
            }
            else if (boxPos.X + boxDim.X < ellipseCenter.X)
            {
                x = boxPos.X + boxDim.X - ellipseCenter.X; //right corner
            }
            if (boxPos.Y > ellipseCenter.Y)
            {
                y = boxPos.Y - ellipseCenter.Y; //top corner
            }
            else if (boxPos.Y + boxDim.Y < ellipseCenter.Y)
            {
                y = boxPos.Y + boxDim.Y - ellipseCenter.Y; //bottom corner
            }
            float a = ellipseDim.X / 2f;
            float b = ellipseDim.Y / 2f;
            return (x * x) / (a * a) + (y * y) / (b * b) < 1; //point collision detection
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = new Color(90, 255, (int)(51 + (Main.DiscoG * 0.75f)));
            color *= (projectile.timeLeft / projectile.ai[1]);
            float scale = (projectile.ai[1] - projectile.timeLeft) * (1f / projectile.ai[1]) * projectile.ai[0];
            sb.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), scale, effects, 0f);
            return false;
        }
    }
}

