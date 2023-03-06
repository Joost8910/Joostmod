using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace JoostMod.Projectiles
{
	public class PowerBombExplosion2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Power Bomb");
		}
		public override void SetDefaults()
		{
			Projectile.width = 1000;
			Projectile.height = 500;
			Projectile.aiStyle = -1;
			Projectile.timeLeft = 55;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.ignoreWater = true;
			Projectile.usesIDStaticNPCImmunity = true;
			Projectile.idStaticNPCHitCooldown = 10;
		}
		public override void AI()
		{
			Projectile.scale = Projectile.timeLeft*0.036f;
            Projectile.position.X = Projectile.Center.X - (float)((1000 * Projectile.scale) / 2f);
            Projectile.position.Y = Projectile.Center.Y - (float)((500 * Projectile.scale) / 2f);
            Projectile.width = (int)(Math.Round(1000 * Projectile.scale));
            Projectile.height = (int)(Math.Round(500 * Projectile.scale));
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Collides(Projectile.position, Projectile.Size, target.position, target.Size))
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            if (Collides(Projectile.position, Projectile.Size, target.position, target.Size))
            {
                return base.CanHitPlayer(target);
            }
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            if (Collides(Projectile.position, Projectile.Size, target.position, target.Size))
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
        public override bool PreDraw(ref Color lightColor)
		{
			Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
			spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), Color.Black, Projectile.rotation, new Vector2(tex.Width/2, tex.Height/2), Projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}