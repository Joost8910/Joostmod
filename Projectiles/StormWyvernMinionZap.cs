using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace JoostMod.Projectiles
{
	public class StormWyvernMinionZap : ModProjectile
	{
		private const float MOVE_DISTANCE = 50f;       //The distance charge particle from the player center
        private const float MAX_DISTANCE = 3200f;       //The maximum length of projectile
        private int sound = 0;
		public float Distance
		{
			get { return projectile.ai[1]; }
			set { projectile.ai[1] = value; }
		}

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lightning Strike");
		}
		public override void SetDefaults()
		{
			projectile.width = 4;
			projectile.height = 4;
			projectile.friendly = true;
            projectile.minion = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
            projectile.timeLeft = 20;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 unit = projectile.velocity;
            DrawLaser(spriteBatch, Main.projectileTexture[projectile.type],
                Main.projectile[(int)projectile.ai[0]].Center, unit, 26, projectile.damage,
                -1.57f, 1f, MAX_DISTANCE, Color.White, (int)MOVE_DISTANCE);
            return false;
		}

		/// <summary>
		/// The core function of drawing a laser
		/// </summary>
		public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, float maxDist = 1200f, Color color = default(Color), int transDist = 50)
		{
			Vector2 origin = start;
			float r = unit.ToRotation() + rotation;
            int xFrame = (projectile.timeLeft / 3) % 3;

			#region Draw laser body
			for (float i = transDist; i <= Distance; i += step)
			{
				origin = start + i * unit;
				spriteBatch.Draw(texture, origin - Main.screenPosition,
					new Rectangle(28 * xFrame, 28, 26, 28), i < transDist ? Color.Transparent : color, r,
					new Vector2(26 / 2, 28 / 2), scale, 0, 0);
			}
			#endregion

			#region Draw laser tail
			spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition,
				new Rectangle(28 * xFrame, 0, 26, 28), color, r, new Vector2(26 / 2, 28 / 2), scale, 0, 0);
			#endregion

			#region Draw laser head
			spriteBatch.Draw(texture, start + (Distance) * unit - Main.screenPosition,
				new Rectangle(28 * xFrame, 56, 26, 28), color, r, new Vector2(26 / 2, 28 / 2), scale, 0, 0);
			#endregion
		}
        
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Projectile owner = Main.projectile[(int)projectile.ai[0]];
            Vector2 unit = projectile.velocity;
            float point = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), owner.Center, owner.Center + unit * Distance, 18, ref point))
            {
                return true;
            }
            return false;
		}
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            crit = true;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            crit = true;
        }
        public override void AI()
		{
            Projectile owner = Main.projectile[(int)projectile.ai[0]];
			projectile.position = (owner.Center + projectile.velocity * MOVE_DISTANCE) - new Vector2(projectile.width/2, projectile.height/2);
            
			#region Set laser tail position and dusts
			Vector2 start = owner.Center;
			Vector2 unit = projectile.velocity;
			unit *= -1;
			for (Distance = MOVE_DISTANCE; Distance <= MAX_DISTANCE; Distance += 5f)
			{
				start = owner.Center + projectile.velocity * Distance;
				if (!Collision.CanHit(owner.Center, 1, 1, start, 1, 1))
				{
					Distance -= 5f;
					break;
				}
			}

			Vector2 dustPos = owner.Center + projectile.velocity * Distance;
			//Imported dust code from source because I'm lazy
			for (int i = 0; i < 2; ++i)
			{
				float num1 = projectile.velocity.ToRotation() + (Main.rand.Next(2) == 1 ? -1.0f : 1.0f) * 1.57f;
				float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
				Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
				Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, 55, dustVel.X, dustVel.Y, 0, Color.White, 1f)];
				dust.noGravity = true;
				dust.scale = 1.2f;
				// At this part, I was messing with the dusts going across the laser beam very fast, but only really works properly horizontally now
				dust = Main.dust[Dust.NewDust(owner.Center + unit * 5f, 0, 0, 55, unit.X, unit.Y, 0, Color.White, 1f)];
				dust.fadeIn = 0f;
				dust.noGravity = true;
				dust.scale = 0.88f;
			}
			#endregion

			//Add lights
			DelegateMethods.v3_1 = new Vector3(1f, 0.8f, 0.2f);
			Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * (Distance - MOVE_DISTANCE), 26, new Utils.PerLinePoint(DelegateMethods.CastLight));
		}

		public override bool ShouldUpdatePosition()
		{
			return false;
		}

		public override void CutTiles()
		{
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Vector2 unit = projectile.velocity;
			Utils.PlotTileLine(projectile.Center, projectile.Center + unit * Distance, (projectile.width + 16) * projectile.scale, new Utils.PerLinePoint(DelegateMethods.CutTiles));
		}
	}
}
