using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class TrueGungnirBeam : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Gungnir");
		}
		public override void SetDefaults()
		{
			Projectile.width = 42;
			Projectile.height = 42;
            Projectile.scale = 1.2f;
            Projectile.aiStyle = 27;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 1200;
			Projectile.alpha = 55;
			Projectile.light = 0.5f;
			Projectile.extraUpdates = 1;
			Projectile.tileCollide = true;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 22;
            height = 22;
            return true;
        }
        public override void AI()
        {
            if ((Projectile.timeLeft % 2) == 0)
            {
                //say you wanted to add particles that stay mostly still to leave a trail behind a projectile
                int num1 = Dust.NewDust(
                         Projectile.position,
                         Projectile.width,
                         Projectile.height,
                         72, //Dust ID
                         Projectile.velocity.X,
                         Projectile.velocity.Y,
                         100, //alpha goes from 0 to 255
                         default(Color),
                         1f
                         );

                Main.dust[num1].noGravity = true;
                Main.dust[num1].velocity *= 0.1f;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                int dust = Dust.NewDust(
                         Projectile.position,
                         Projectile.width,
                         Projectile.height,
                         72, //Dust ID
                         Projectile.velocity.X,
                         Projectile.velocity.Y,
                         100, //alpha goes from 0 to 255
                         default(Color),
                         1f
                         );

                Main.dust[dust].noGravity = true;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
            Color color = lightColor * ((255f - Projectile.alpha) / 255f);
            spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0f);
            return false;
        }

    }
}
