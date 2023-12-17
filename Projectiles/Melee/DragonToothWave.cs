using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class DragonToothWave : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plunging Attack");
        }
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.Bullet;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void Kill(int timeLeft)
        {
            int grav = 1;
            if (Projectile.velocity.Y < 0)
            {
                grav = -1;
            }
            Vector2 posi = new Vector2(Projectile.position.X, Projectile.position.Y + 4 * grav);
            Point pos = posi.ToTileCoordinates();
            Tile tileSafely = Framing.GetTileSafely(pos.X, pos.Y);
            if (tileSafely.HasTile)
            {
                Tile tileSafely2 = Framing.GetTileSafely(pos.X, pos.Y - 1 * grav);
                if (!tileSafely2.HasTile || !Main.tileSolid[tileSafely2.TileType] || Main.tileSolidTop[tileSafely2.TileType])
                {
                    for (int d = 0; d < 6; d++)
                    {
                        Dust dust = Main.dust[WorldGen.KillTile_MakeTileDust(pos.X, pos.Y, tileSafely)];
                        dust.velocity.Y = (dust.velocity.Y - 5) * Main.rand.NextFloat() * grav;
                    }
                    Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, pos.Y * 16 - 8 * grav, Projectile.velocity.X * 15, -6 * grav, ModContent.ProjectileType<DragonToothWave2>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                }
            }
        }
    }
}
