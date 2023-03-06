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
            Main.projFrames[Projectile.type] = 9;
        }
        public override void SetDefaults()
        {
            Projectile.width = 192;
            Projectile.height = 192;
            Projectile.aiStyle = 0;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 16;
            Projectile.alpha = 15;
            Projectile.light = 0.2f;
            Projectile.tileCollide = false;
        }
        public override void AI()
        {
            NPC host = Main.npc[(int)Projectile.ai[0]];
            Projectile.position = host.Center + new Vector2(-24 * host.direction, -43) - Projectile.Size / 2;
            Projectile.velocity = host.velocity;
            if (Projectile.timeLeft % 2 == 0)
            {
                Projectile.frame++;
            }
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.direction = (Projectile.velocity.X < 0 ? -1 : 1);
            if (Projectile.ai[1] < 0)
            {
                Projectile.direction *= -1;
            }
            Projectile.spriteDirection = Projectile.direction;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                target.velocity *= 0.3f;
                target.wingTime -= 10;
            }
            if (Main.npc[(int)Projectile.ai[0]].ai[3] >= 6)
            {
                target.immuneTime = Projectile.timeLeft + 10;
            }
        }
    }
}

