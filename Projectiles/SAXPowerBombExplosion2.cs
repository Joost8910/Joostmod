using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JoostMod.Projectiles
{
	public class SAXPowerBombExplosion2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Power Bomb");
		}
		public override void SetDefaults()
		{
			projectile.width = 1000;
			projectile.height = 500;
			projectile.aiStyle = -1;
			projectile.timeLeft = 55;
			projectile.hostile = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.ignoreWater = true;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 10;
		}
		public override void AI()
		{
			projectile.scale = projectile.timeLeft*0.036f;
			projectile.position.X = projectile.Center.X - (float)((1000 * projectile.scale) / 2f) + 1;
			projectile.position.Y = projectile.Center.Y - (float)((500 * projectile.scale) / 2f) + 1;
			projectile.width = (int)(1000 * projectile.scale);
			projectile.height = (int)(500 * projectile.scale);
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
			sb.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), Color.Black, projectile.rotation, new Vector2(tex.Width/2, tex.Height/2), projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}