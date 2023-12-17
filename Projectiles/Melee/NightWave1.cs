using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class NightWave1 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Night's Wrath");
        }
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.Bullet;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 2;
            height = 2;
            fallThrough = false;
            return true;
        }
        public override void Kill(int timeLeft)
        {
            Vector2 posi = new Vector2(Projectile.position.X, Projectile.position.Y + 4 * Projectile.ai[0]);
            Point pos = posi.ToTileCoordinates();
            Tile tileSafely = Framing.GetTileSafely(pos.X, pos.Y);
            if (tileSafely.HasTile)
            {
                Tile tileSafely2 = Framing.GetTileSafely(pos.X, pos.Y - (int)Projectile.ai[0]);
                if (!tileSafely2.HasTile || !Main.tileSolid[tileSafely2.TileType] || Main.tileSolidTop[tileSafely2.TileType])
                {
                    Dust dust = Main.dust[WorldGen.KillTile_MakeTileDust(pos.X, pos.Y, tileSafely)];
                    dust.velocity.Y = (dust.velocity.Y - 5 * Projectile.ai[0]) * Main.rand.NextFloat();
                    SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
                    int offset = Projectile.ai[0] == -1 ? 24 : 8;
                    Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y + offset, 0, 0, ModContent.ProjectileType<NightWave2>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.ai[0]);
                }
            }
        }
    }
}
