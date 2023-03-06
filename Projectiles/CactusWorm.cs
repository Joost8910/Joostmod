using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class CactusWorm : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactus Worm");
            ProjectileID.Sets.DontAttachHideToAlpha[Projectile.type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.width = 56;
            Projectile.height = 56;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.penetrate = 15;
            Projectile.timeLeft = 200;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.Bullet;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 22;
        }
        public override void AI()
        {
            Projectile.direction = Projectile.velocity.X > 0 ? 1 : -1;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation = -Projectile.timeLeft * Projectile.velocity.X * 0.0174f * 2;
            Projectile.velocity.Y = -8;
            bool solid = true;
            for (int i = (int)(Projectile.position.X / 16); i < (int)((Projectile.position.X + Projectile.width) / 16); i++)
            {
                for (int j = (int)((Projectile.position.Y + 18) / 16); j < (int)((Projectile.position.Y + Projectile.height) / 16); j++)
                {
                    Tile tile = Framing.GetTileSafely(i, j);
                    if (!tile.HasTile || !Main.tileSolid[(int)tile.TileType] || Main.tileSolidTop[(int)tile.TileType])
                    {
                        solid = false;
                        break;
                    }
                }
            }
            if (!solid)
            {
                Projectile.velocity.Y = 8;
            }
            else if (Projectile.timeLeft % 22 == 0)
            {
                SoundEngine.PlaySound(SoundID.WormDig, Projectile.position);
            }
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            drawCacheProjsBehindNPCsAndTiles.Add(index);
        }
    }
}
