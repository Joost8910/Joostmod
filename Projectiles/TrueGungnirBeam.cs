using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
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
			projectile.width = 42;
			projectile.height = 42;
            projectile.scale = 1.2f;
            projectile.aiStyle = 27;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 1200;
			projectile.alpha = 55;
			projectile.light = 0.5f;
			projectile.extraUpdates = 1;
			projectile.tileCollide = true;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 22;
            height = 22;
            return true;
        }
        public override void AI()
        {
            if ((projectile.timeLeft % 2) == 0)
            {
                //say you wanted to add particles that stay mostly still to leave a trail behind a projectile
                int num1 = Dust.NewDust(
                         projectile.position,
                         projectile.width,
                         projectile.height,
                         72, //Dust ID
                         projectile.velocity.X,
                         projectile.velocity.Y,
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
                         projectile.position,
                         projectile.width,
                         projectile.height,
                         72, //Dust ID
                         projectile.velocity.X,
                         projectile.velocity.Y,
                         100, //alpha goes from 0 to 255
                         default(Color),
                         1f
                         );

                Main.dust[dust].noGravity = true;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
            Color color = lightColor * ((255f - projectile.alpha) / 255f);
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, projectile.rotation, drawOrigin, projectile.scale, effects, 0f);
            return false;
        }

    }
}
