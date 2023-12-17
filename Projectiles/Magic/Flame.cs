using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Magic
{
    public class Flame : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flame");
        }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 2;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 250;
            Projectile.alpha = 35;
            Projectile.light = 0.15f;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Shuriken;
        }

        public override void AI()
        {
            if (Main.tile[Projectile.Center.ToTileCoordinates().X, Projectile.Center.ToTileCoordinates().Y].LiquidAmount > 80 && (Main.tile[Projectile.Center.ToTileCoordinates().X, Projectile.Center.ToTileCoordinates().Y].LiquidType == 0 || Main.tile[Projectile.Center.ToTileCoordinates().X, Projectile.Center.ToTileCoordinates().Y].LiquidType == 2))
            {
                Projectile.Kill();
            }
        }
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            Player owner = Main.player[Projectile.owner];
            n.AddBuff(24, 60);
        }

        public override void Kill(int timeLeft)
        {
            if (Main.myPlayer == Projectile.owner)
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<Flame2>(), Projectile.damage, 0, Projectile.owner);
        }

    }
}


