using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class CactusWorm : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactus Worm");
            ProjectileID.Sets.DontAttachHideToAlpha[projectile.type] = true;
        }
        public override void SetDefaults()
        {
            projectile.width = 56;
            projectile.height = 56;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.penetrate = 15;
            projectile.timeLeft = 180;
            projectile.tileCollide = false;
            projectile.hide = true;
            projectile.ignoreWater = true;
            aiType = ProjectileID.Bullet;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 22;
        }
        public override void AI()
        {
            projectile.direction = projectile.velocity.X > 0 ? 1 : -1;
            projectile.spriteDirection = projectile.direction;
            projectile.rotation = -projectile.timeLeft * projectile.velocity.X * 0.0174f * 2;
            projectile.velocity.Y = -8;
            bool solid = true;
            for (int i = (int)(projectile.position.X / 16); i < (int)((projectile.position.X + projectile.width) / 16); i++)
            {
                for (int j = (int)((projectile.position.Y + 18) / 16); j < (int)((projectile.position.Y + projectile.height) / 16); j++)
                {
                    Tile tile = Framing.GetTileSafely(i, j);
                    if (!tile.active() || !Main.tileSolid[(int)tile.type] || Main.tileSolidTop[(int)tile.type])
                    {
                        solid = false;
                        break;
                    }
                }
            }
            if (!solid)
            {
                projectile.velocity.Y = 8;
            }
            else if (projectile.timeLeft % 22 == 0)
            {
                Main.PlaySound(15, (int)projectile.position.X, (int)projectile.position.Y, 1);
            }
        }
        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
        {
            drawCacheProjsBehindNPCsAndTiles.Add(index);
        }
    }
}
