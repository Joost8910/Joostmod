using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace JoostMod.Projectiles
{
	public class DarkLaser : ModProjectile
	{
		private const int MAX_CHARGE = 70;
		private const float MOVE_DISTANCE = 50f;       //The distance charge particle from the player center
		private int sound = 0;
		public float Distance
		{
			get { return Projectile.ai[0]; }
			set { Projectile.ai[0] = value; }
		}

		public float Charge
		{
			get { return Projectile.localAI[0]; }
			set { Projectile.localAI[0] = value; }
		}

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Laser of Darkness");
		}
		public override void SetDefaults()
		{
			Projectile.width = 4;
			Projectile.height = 4;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
			Projectile.DamageType = DamageClass.Magic;
		}

		public override bool PreDraw(ref Color lightColor)
        {
            if (Charge == MAX_CHARGE)
            {
                Vector2 unit = Projectile.velocity;
                DrawLaser(spriteBatch, TextureAssets.Projectile[Projectile.type].Value,
                    Main.player[Projectile.owner].Center, unit, 10, Projectile.damage,
                    -1.57f, 1f, 1000f, Color.White, (int)MOVE_DISTANCE);
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
				Main.EntitySpriteDraw(texture, origin - Main.screenPosition,
					new Rectangle(0, 26, 18, 26), i < transDist ? Color.Transparent : c, r,
					new Vector2(18 / 2, 26 / 2), scale, 0, 0);
			}
			#endregion

			#region Draw laser tail
			spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition,
				new Rectangle(0, 0, 18, 26), Color.White, r, new Vector2(18 / 2, 26 / 2), scale, 0, 0);
			#endregion

			#region Draw laser head
			spriteBatch.Draw(texture, start + (Distance + step) * unit - Main.screenPosition,
				new Rectangle(0, 52, 18, 26), Color.White, r, new Vector2(18 / 2, 26 / 2), scale, 0, 0);
			#endregion
		}

		/// <summary>
		/// Change the way of collision check of the projectile
		/// </summary>
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (Charge == MAX_CHARGE)
			{
				Player p = Main.player[Projectile.owner];
				Vector2 unit = Projectile.velocity;
				float point = 0f;
				if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), p.Center, p.Center + unit * Distance, 10, ref point))
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
			Player player = Main.player[Projectile.owner];
            Vector2 mousePos = Main.MouseWorld;

            #region Set projectile position
            if (Projectile.owner == Main.myPlayer) // Multiplayer support
			{
				Vector2 diff = mousePos - player.Center;
				diff.Normalize();
                float home = 20f;
                Projectile.velocity = ((home - 1f) * Projectile.velocity - diff) / home;
                Projectile.velocity.Normalize();
                Projectile.direction = Main.MouseWorld.X > player.position.X ? -1 : 1;
				Projectile.netUpdate = true;
			}
			Projectile.position = (player.Center + Projectile.velocity * MOVE_DISTANCE) - new Vector2(Projectile.width/2, Projectile.height/2);
			Projectile.timeLeft = 2;
			//int dir = projectile.direction;
			//player.ChangeDir(dir);
			//player.heldProj = projectile.whoAmI;
			//player.itemTime = 2;
			//player.itemAnimation = 2;
			//player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * dir, projectile.velocity.X * dir);
			#endregion

			#region Charging process
			// Kill the projectile if the player stops channeling
			if (!player.channel)
			{
				Projectile.Kill();
			}
			else
            {
                if (Main.time % 10 < 1 && !player.CheckMana(player.inventory[player.selectedItem].mana, false))
                {
                    Projectile.Kill();
                }
                Vector2 offset = Projectile.velocity;
				offset *= MOVE_DISTANCE - 20;
				Vector2 pos = player.Center + offset - new Vector2(10, 10);
				if (Charge == 0)
				{
					SoundEngine.PlaySound(SoundID.Item13, Projectile.position);
				}
				if (Charge < MAX_CHARGE)
				{
					Charge++;
				}
				if (Charge >= MAX_CHARGE)
				{
					sound++;
				}
                if (sound >= 15)
                {
                    SoundEngine.PlaySound(SoundID.Item15, Projectile.position);
                    sound = 0;
                }
				int chargeFact = (int)(Charge / 20f);
				Vector2 dustVelocity = Vector2.UnitX * 18f;
				dustVelocity = dustVelocity.RotatedBy(Projectile.rotation - 1.57f, default(Vector2));
				Vector2 spawnPos = Projectile.Center + dustVelocity;
				for (int k = 0; k < chargeFact + 1; k++)
				{
					Vector2 spawn = spawnPos + ((float)Main.rand.NextDouble() * 6.28f).ToRotationVector2() * (12f - (chargeFact * 2));
					Dust dust = Main.dust[Dust.NewDust(pos, 20, 20, 109, Projectile.velocity.X / 2f,
						Projectile.velocity.Y / 2f, 0, default(Color), 1f)];
					dust.velocity = Vector2.Normalize(spawnPos - spawn) * 1.5f * (10f - chargeFact * 2f) / 10f;
					dust.noGravity = true;
					dust.scale = Main.rand.Next(10, 20) * 0.05f;
				}
			}
			#endregion


			#region Set laser tail position and dusts
			if (Charge < MAX_CHARGE) return;
			Vector2 start = player.Center;
			Vector2 unit = Projectile.velocity;
			unit *= -1;
			for (Distance = MOVE_DISTANCE; Distance <= 1200f; Distance += 5f)
			{
				start = player.Center + Projectile.velocity * Distance;
				if (!Collision.CanHitLine(player.Center, 1, 1, start, 1, 1))
				{
					Distance -= 5f;
					break;
				}
			}

			Vector2 dustPos = player.Center + Projectile.velocity * Distance;
			//Imported dust code from source because I'm lazy
			
			for (int i = 0; i < 2; ++i)
			{
				float num1 = Projectile.velocity.ToRotation() + (Main.rand.Next(2) == 1 ? -1.0f : 1.0f) * 1.57f;
				float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
				Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
				Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, 109, dustVel.X, dustVel.Y, 0, new Color(), 1f)];
				dust.noGravity = true;
				dust.scale = 1.2f;
				// At this part, I was messing with the dusts going across the laser beam very fast, but only really works properly horizontally now
				dust = Main.dust[Dust.NewDust(Main.player[Projectile.owner].Center + unit * 5f, 0, 0, 109, unit.X, unit.Y, 0, new Color(), 1f)];
				dust.fadeIn = 0f;
				dust.noGravity = true;
				dust.scale = 0.88f;
			}
			#endregion

			//Add lights
			//DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
			//Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * (Distance - MOVE_DISTANCE), 26, new Utils.PerLinePoint(DelegateMethods.CastLight));
		}

		public override bool ShouldUpdatePosition()
		{
			return false;
		}

		public override void CutTiles()
		{
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Vector2 unit = Projectile.velocity;
			Utils.PlotTileLine(Projectile.Center, Projectile.Center + unit * Distance, (Projectile.width + 16) * Projectile.scale, new Utils.PerLinePoint(DelegateMethods.CutTiles));
		}
	}
}
