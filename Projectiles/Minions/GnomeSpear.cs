using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Minions
{
	public class GnomeSpear : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gnome");
            Main.projFrames[Projectile.type] = 2;
            ProjectileID.Sets.MinionShot[Projectile.type] = true;
        }
		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.aiStyle = -1;
			Projectile.timeLeft = 16;
			Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 9;
		}
		
		public override void AI()
		{
            Projectile owner = Main.projectile[(int)Projectile.ai[0]];
            Projectile.spriteDirection = Projectile.direction;
            Projectile.position += Projectile.velocity * 10f * Projectile.ai[1];
            Projectile.position = owner.Center + new Vector2(0, 9) - (Projectile.Size / 2);
			Projectile.position += Projectile.velocity * Projectile.ai[1];
            if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] = 3f;
				Projectile.netUpdate = true;
			}
			if (Projectile.timeLeft < 10)
            {
                Projectile.ai[1] -= 0.6f;
                Projectile.frame = 0;
            }
			else
			{
				Projectile.ai[1] += 1f;
                Projectile.frame = 1;
                owner.velocity = Projectile.velocity * 2f;
			}
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 2.355f;
			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation -= 1.57f;
			}
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.direction == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = lightColor; 
            Rectangle? rect = new Rectangle?(new Rectangle(0, Projectile.frame * tex.Height / Main.projFrames[Projectile.type], tex.Width, tex.Height / Main.projFrames[Projectile.type]));

            Vector2 vel = Projectile.velocity;
            vel.Normalize();
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition - vel * 22 * Projectile.scale, rect, color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 4), Projectile.scale, effects, 0);
            return false;
        }
    }
}