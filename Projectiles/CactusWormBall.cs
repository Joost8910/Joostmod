using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;

namespace JoostMod.Projectiles
{
	public class CactusWormBall : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactus Ball");
		}
		public override void SetDefaults()
		{
			projectile.width = 38;
			projectile.height = 38;
			projectile.aiStyle = 1;
			projectile.hostile = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 300;
            projectile.tileCollide = false;
			aiType = ProjectileID.Bullet;
        }
        public override void PostDraw(SpriteBatch sb, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            sb.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), Color.GreenYellow, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale, SpriteEffects.None, 0f);
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 26;
            height = 26;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.X += 4;
            hitbox.Y += 4;
            hitbox.Width = 30;
            hitbox.Height = 30;
        }
        public override void AI()
        {
            if (projectile.velocity.Length() > 0)
            {
                projectile.rotation = projectile.timeLeft * -projectile.direction * 0.0174f * 12;
            }
            if (projectile.timeLeft < 280)
            {
                projectile.tileCollide = true;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity = Vector2.Zero;
            projectile.timeLeft -= 100;
            projectile.damage -= 10;
            return false;
        }
        /*public override void Kill(int timeLeft)
        {
            if (Main.expertMode && Main.netMode != 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    NPC.NewNPC((int)projectile.Center.X, (int)projectile.Center.Y, mod.NPCType("CactusThorn"));
                }
            }
        }*/
    }
}

