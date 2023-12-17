using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class SaxWave1 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SA-X");
        }
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.aiStyle = 1;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.Bullet;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 6;
            height = 2;
            fallThrough = false;
            return true;
        }
        public override void Kill(int timeLeft)
        {
            Vector2 posi = new Vector2(Projectile.position.X, Projectile.position.Y + 4);
            Point pos = posi.ToTileCoordinates();
            Tile tileSafely = Framing.GetTileSafely(pos.X, pos.Y);
            if (tileSafely.HasTile)
            {
                Tile tileSafely2 = Framing.GetTileSafely(pos.X, pos.Y - 1);
                if (!tileSafely2.HasTile || !Main.tileSolid[tileSafely2.TileType] || Main.tileSolidTop[tileSafely2.TileType])
                {
                    Dust dust = Main.dust[WorldGen.KillTile_MakeTileDust(pos.X, pos.Y, tileSafely)];
                    dust.velocity.Y = (dust.velocity.Y - 5) * Main.rand.NextFloat();
                    SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
                    Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y + 8, 0, 0, ModContent.ProjectileType<SaxWave2>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                }
            }
        }

    }
}
