using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Nado : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tornade");
			Main.projFrames[projectile.type] = 6;
		}
		public override void SetDefaults()
		{
			projectile.width = 56;
			projectile.height = 56;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 150;
			projectile.tileCollide = false;
			aiType = ProjectileID.Bullet;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 12;
		}

		public override void AI()
		{
			if (projectile.timeLeft % 5 == 0)
			{
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 7);
				int num1 = Dust.NewDust(
				projectile.position,
				projectile.width,
				projectile.height,
				51, //Dust ID
				Main.rand.Next(5) - 2,
				Main.rand.Next(5) - 2,
				100, //alpha goes from 0 to 255
				default(Color),
				1f
				);
			Main.dust[num1].noGravity = true;
			}
			projectile.frameCounter++;
			if (projectile.frameCounter >= 4)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 6;
			}
			if (projectile.timeLeft < 60)
			{
				projectile.scale = projectile.timeLeft*0.016f;
				projectile.position.X = projectile.Center.X - ((int)((float)56 * projectile.scale) / 2);
				projectile.position.Y = projectile.Center.Y - ((int)((float)56 * projectile.scale) / 2);
				projectile.width = (int)((float)56 * projectile.scale);
				projectile.height = (int)((float)56 * projectile.scale);
			}
		}
		public override bool PreDraw(SpriteBatch sb, Color lightColor)
		{
			Texture2D tex = Main.projectileTexture[projectile.type];
			Color color = Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16.0));
			sb.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY) + new Vector2(0f, projectile.height*2.5f), new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[projectile.type]) * projectile.frame, tex.Width, tex.Height / Main.projFrames[projectile.type])), color, projectile.rotation, new Vector2(tex.Width/2, tex.Height/2), projectile.scale, SpriteEffects.None, 0f);
			return false;
		}

	}
}
