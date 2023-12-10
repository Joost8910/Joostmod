using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Magic
{
    public class WaterSplash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Splash");
        }
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = 1;
            Projectile.alpha = 80;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.damage /= 2;
            Projectile.knockBack /= 2;
        }
        public override void AI()
        {
            if (Main.tile[Projectile.Center.ToTileCoordinates().X, Projectile.Center.ToTileCoordinates().Y].LiquidAmount > 50 || Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
            {
                Projectile.tileCollide = false;
                Projectile.penetrate = -1;
                Projectile.timeLeft = 2;
                Projectile.velocity.Y = Projectile.velocity.Y > -10 ? Projectile.velocity.Y - 0.3f : Projectile.velocity.Y;
            }
            else
            {
                Projectile.tileCollide = true;
                Projectile.velocity.Y = Projectile.velocity.Y < 10 ? Projectile.velocity.Y + 0.3f : Projectile.velocity.Y;
            }
            Projectile.velocity.X *= 0.98f;
        }
        public override void Kill(int timeLeft)
        {
            int x = Projectile.Center.ToTileCoordinates().X;
            int y = Projectile.Center.ToTileCoordinates().Y;
            Tile tile = Main.tile[x, y];
            tile.LiquidType = 0;
            tile.LiquidAmount = 255;
            WorldGen.SquareTileFrame(x, y, true);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                NetMessage.sendWater(x, y);
            }
            else
            {
                Liquid.AddWater(x, y);
            }
            SoundEngine.PlaySound(SoundID.SplashWeak, Projectile.position);
        }
    }
}

