using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class GilgMasamuneSlash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Masamune");
            Main.projFrames[projectile.type] = 9;
        }
        public override void SetDefaults()
        {
            projectile.width = 192;
            projectile.height = 192;
            projectile.aiStyle = 0;
            projectile.hostile = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 16;
            projectile.alpha = 15;
            projectile.light = 0.2f;
            projectile.tileCollide = false;
        }
        public override void AI()
        {
            NPC host = Main.npc[(int)projectile.ai[0]];
            projectile.position = host.Center + new Vector2(-24 * host.direction, -43) - projectile.Size / 2;
            projectile.velocity = host.velocity;
            if (projectile.timeLeft % 2 == 0)
            {
                projectile.frame++;
            }
            projectile.rotation = projectile.velocity.ToRotation();
            projectile.direction = (projectile.velocity.X < 0 ? -1 : 1);
            if (projectile.ai[1] < 0)
            {
                projectile.direction *= -1;
            }
            projectile.spriteDirection = projectile.direction;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                target.velocity *= 0.3f;
                target.wingTime -= 10;
            }
            if (Main.npc[(int)projectile.ai[0]].ai[3] >= 6)
            {
                target.immuneTime = projectile.timeLeft + 10;
            }
        }
    }
}

