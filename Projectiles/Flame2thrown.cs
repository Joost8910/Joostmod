using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Flame2thrown : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flame");
			Main.projFrames[projectile.type] = 4;
		}
		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 18;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.ignoreWater = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 90;
			projectile.tileCollide = false;
			projectile.alpha = 35;
			projectile.light = 0.75f;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 10;
		}
		public override void AI()
		{
			if(Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquid > 80 && (Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquidType() == 0 || Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquidType() == 2))
			{
				projectile.Kill();
			}
			projectile.frameCounter++;
			if (projectile.frameCounter >= 4)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 4;
			}
			if (projectile.timeLeft < 60)
			{
			projectile.scale = projectile.timeLeft*0.016f;
			projectile.position.X = projectile.Center.X - ((int)((float)16 * projectile.scale) / 2);
			projectile.position.Y = projectile.Center.Y - ((int)((float)18 * projectile.scale) / 2);
			projectile.width = (int)((float)16 * projectile.scale);
			projectile.height = (int)((float)18 * projectile.scale);
			}
		}
		public override bool PreDraw(SpriteBatch sb, Color lightColor)
		{
			Texture2D tex = Main.projectileTexture[projectile.type];
			sb.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY) + new Vector2(0f, projectile.height*1.5f), new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[projectile.type]) * projectile.frame, tex.Width, tex.Height / Main.projFrames[projectile.type])), Color.Yellow, projectile.rotation, new Vector2(tex.Width/2, tex.Height/2), projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
		public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
		{
			Player owner = Main.player[projectile.owner];
			n.AddBuff(24, 60);
		}
	}
}


