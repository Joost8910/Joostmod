using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class SAXBeam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Beam");
            Main.projFrames[projectile.type] = 8;
        }
        public override void SetDefaults()
        {
            projectile.width = 80;
            projectile.height = 80;
            projectile.aiStyle = 1;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 1200;
            projectile.extraUpdates = 1;
            projectile.tileCollide = false;
            projectile.light = 0.75f;
            projectile.coldDamage = true;
            aiType = ProjectileID.Bullet;
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.X += 16;
            hitbox.Y += 16;
            hitbox.Width = 48;
            hitbox.Height = 48;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                if (!target.HasBuff(BuffID.Frozen) && Main.rand.Next(4) < 3)
                {
                    target.AddBuff(BuffID.Frozen, 30, true);
                }
            }
            target.AddBuff(BuffID.Frostburn, 300, true);
            target.AddBuff(BuffID.Chilled, 100, true);
        }
        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 8;
            }
        }
    }
}

