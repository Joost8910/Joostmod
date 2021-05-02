using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.Enums;

namespace JoostMod.Projectiles
{
	public class FocusSoulBeam : ModProjectile
	{
		private const int MAX_CHARGE = 30;
		private const float MOVE_DISTANCE = 20f;   
		private int sound = 0;
		public float Distance
		{
			get { return projectile.ai[0]; }
			set { projectile.ai[0] = value; }
		}

		public float Charge
		{
			get { return projectile.localAI[0]; }
			set { projectile.localAI[0] = value; }
		}

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Focus Souls");
		}
		public override void SetDefaults()
		{
			projectile.width = 4;
			projectile.height = 4;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
			projectile.magic = true;
            projectile.timeLeft = 120;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (Charge == MAX_CHARGE)
			{
				Vector2 unit = projectile.velocity;
				DrawLaser(spriteBatch, Main.projectileTexture[projectile.type], 
					projectile.Center, unit, 10, projectile.damage, 
					-1.57f, 1f, 2000f, Color.White, (int)MOVE_DISTANCE);
            }
			return false;

		}

		/// <summary>
		/// The core function of drawing a laser
		/// </summary>
		public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, float maxDist = 1200f, Color color = default(Color), int transDist = 50)
		{
			Vector2 origin = start;
			float r = unit.ToRotation() + rotation;

			#region Draw laser body
			for (float i = transDist + 4; i <= Distance; i += step)
			{
				Color c = Color.White;
				origin = start + i * unit;
				spriteBatch.Draw(texture, origin - Main.screenPosition,
					new Rectangle(0, 26, 16, 26), i < transDist ? Color.Transparent : c, r,
					new Vector2(16 / 2, 26 / 2), scale, 0, 0);
			}
			#endregion

			#region Draw laser tail
			spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition,
				new Rectangle(0, 0, 16, 26), Color.White, r, new Vector2(16 / 2, 26 / 2), scale, 0, 0);
			#endregion

			#region Draw laser head
			spriteBatch.Draw(texture, start + (Distance + step) * unit - Main.screenPosition,
				new Rectangle(0, 52, 16, 26), Color.White, r, new Vector2(16 / 2, 26 / 2), scale, 0, 0);
			#endregion
		}

		/// <summary>
		/// Change the way of collision check of the projectile
		/// </summary>
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (Charge == MAX_CHARGE)
			{
				Vector2 unit = projectile.velocity;
				float point = 0f;
				if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center, projectile.Center + unit * Distance, 10, ref point))
				{
					return true;
				}
			}
			return false;
		}

        /// <summary>
        /// Change the behavior after hit a NPC
        /// </summary>

        /// <summary>
        /// The AI of the projectile
        /// </summary>
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            #region Charging process
            Vector2 offset = projectile.velocity;
            offset *= MOVE_DISTANCE - 20;
            Vector2 pos = projectile.Center - new Vector2(5, 5);
            double rot = projectile.velocity.ToRotation();
            if (Charge == 0)
            {
                projectile.direction = projectile.velocity.X > 0 ? 1 : -1;
                projectile.spriteDirection = projectile.direction;
                rot += 45 * (Math.PI / 180) * projectile.spriteDirection;
            }
            if (Charge < MAX_CHARGE)
            {
                Charge++;
            }
            if (Charge >= MAX_CHARGE)
            {
                //sound++;
                rot -= projectile.spriteDirection * (Math.PI / 180);
            }
            projectile.velocity = new Vector2((float)Math.Cos(rot), (float)Math.Sin(rot));
            projectile.velocity.Normalize();
            /*if (sound >= 15)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 15);
                sound = 0;
            }*/
            int chargeFact = (int)(Charge / 20f);
            Vector2 dustVelocity = Vector2.UnitX * 18f;
            dustVelocity = dustVelocity.RotatedBy(projectile.rotation - 1.57f, default(Vector2));
            Vector2 spawnPos = projectile.Center + dustVelocity;
            for (int k = 0; k < chargeFact + 1; k++)
            {
                Vector2 spawn = spawnPos + ((float)Main.rand.NextDouble() * 6.28f).ToRotationVector2() * (12f - (chargeFact * 2));
                Dust dust = Main.dust[Dust.NewDust(pos, 10, 10, 92, projectile.velocity.X / 2f,
                    projectile.velocity.Y / 2f, 0, default(Color), 1f)];
                dust.velocity = Vector2.Normalize(spawnPos - spawn) * 1.5f * (10f - chargeFact * 2f) / 10f;
                dust.noGravity = true;
                dust.scale = Main.rand.Next(10, 20) * 0.05f;
            }
            #endregion


            #region Set laser tail position and dusts
            if (Charge < MAX_CHARGE) return;
            Vector2 start = projectile.Center;
            Vector2 unit = projectile.velocity;
            unit *= -1;
            for (Distance = MOVE_DISTANCE; Distance <= 2000f; Distance += 5f)
            {
                start = projectile.Center + projectile.velocity * Distance;
                if (!Collision.CanHitLine(projectile.Center, 1, 1, start, 1, 1))
                {
                    Distance -= 5f;
                    break;
                }
            }

            Vector2 dustPos = projectile.Center + projectile.velocity * Distance;
            for (int i = 0; i < 2; ++i)
            {
                float num1 = projectile.velocity.ToRotation() + (Main.rand.Next(2) == 1 ? -1.0f : 1.0f) * 1.57f;
                float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
                Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
                Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, 92, dustVel.X, dustVel.Y, 0, new Color(), 1f)];
                dust.noGravity = true;
                dust.scale = 1.2f;
                /*dust = Main.dust[Dust.NewDust(projectile.Center + unit * 5f, 0, 0, 92, unit.X, unit.Y, 0, new Color(), 1f)];
                dust.fadeIn = 0f;
                dust.noGravity = true;
                dust.scale = 0.88f;*/
            }
            #endregion

            //Add lights
            DelegateMethods.v3_1 = new Vector3(0.1f, 0.8f, 1f);
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
