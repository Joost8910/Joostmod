using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Nado : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tornade");
			Main.projFrames[Projectile.type] = 6;
		}
		public override void SetDefaults()
		{
			Projectile.width = 56;
			Projectile.height = 56;
			Projectile.aiStyle = 0;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 150;
			Projectile.tileCollide = false;
			AIType = ProjectileID.Bullet;
			Projectile.usesIDStaticNPCImmunity = true;
			Projectile.idStaticNPCHitCooldown = 12;
		}

		public override void AI()
		{
			if (Projectile.timeLeft % 5 == 0)
			{
				SoundEngine.PlaySound(SoundID.Item7, Projectile.position);
				int num1 = Dust.NewDust(
				Projectile.position,
				Projectile.width,
				Projectile.height,
				51, //Dust ID
				Main.rand.Next(5) - 2,
				Main.rand.Next(5) - 2,
				100, //alpha goes from 0 to 255
				default(Color),
				1f
				);
			Main.dust[num1].noGravity = true;
			}
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 4)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 6;
			}
			if (Projectile.timeLeft < 60)
			{
				Projectile.scale = Projectile.timeLeft*0.016f;
				Projectile.position.X = Projectile.Center.X - ((int)((float)56 * Projectile.scale) / 2);
				Projectile.position.Y = Projectile.Center.Y - ((int)((float)56 * Projectile.scale) / 2);
				Projectile.width = (int)((float)56 * Projectile.scale);
				Projectile.height = (int)((float)56 * Projectile.scale);
			}
		}
		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
			Color color = Lighting.GetColor((int)(Projectile.Center.X / 16), (int)(Projectile.Center.Y / 16.0));
			sb.Draw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY) + new Vector2(0f, Projectile.height*2.5f), new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[Projectile.type]) * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type])), color, Projectile.rotation, new Vector2(tex.Width/2, tex.Height/2), Projectile.scale, SpriteEffects.None, 0);
			return false;
		}

	}
}
