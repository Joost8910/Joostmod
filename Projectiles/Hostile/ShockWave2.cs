using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class ShockWave2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Golem");
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 50;
            Projectile.aiStyle = 0;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 70;
            Projectile.tileCollide = false;
            AIType = ProjectileID.Bullet;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            if (Projectile.timeLeft > 60)
            {
                Projectile.frame = 0;
                Projectile.height = 18;
            }
            if (Projectile.timeLeft == 60)
            {
                Projectile.position.Y = Projectile.position.Y - 6;
            }
            if (Projectile.timeLeft <= 60 && Projectile.timeLeft > 50)
            {
                Projectile.frame = 1;
                Projectile.height = 24;
            }
            if (Projectile.timeLeft == 50)
            {
                Projectile.position.Y = Projectile.position.Y - 12;
            }
            if (Projectile.timeLeft <= 50 && Projectile.timeLeft > 40)
            {
                Projectile.frame = 2;
                Projectile.height = 36;
            }
            if (Projectile.timeLeft == 40)
            {
                Projectile.position.Y = Projectile.position.Y - 13;
            }
            if (Projectile.timeLeft <= 40 && Projectile.timeLeft > 30)
            {
                Projectile.frame = 3;
                Projectile.height = 50;
            }
            if (Projectile.timeLeft == 30)
            {
                Projectile.position.Y = Projectile.position.Y + 13;
            }
            if (Projectile.timeLeft <= 30 && Projectile.timeLeft > 20)
            {
                Projectile.frame = 2;
                Projectile.height = 36;
            }
            if (Projectile.timeLeft == 20)
            {
                Projectile.position.Y = Projectile.position.Y + 12;
            }
            if (Projectile.timeLeft <= 20 && Projectile.timeLeft > 10)
            {
                Projectile.frame = 1;
                Projectile.height = 24;
            }
            if (Projectile.timeLeft == 10)
            {
                Projectile.position.Y = Projectile.position.Y + 6;
            }
            if (Projectile.timeLeft <= 10)
            {
                Projectile.frame = 0;
                Projectile.height = 18;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.velocity.Y -= Projectile.knockBack * target.knockBackResist;
            if (target.knockBackResist > 0)
            {
                target.velocity.X = 0;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (!target.noKnockback)
            {
                target.velocity.Y -= Projectile.knockBack;
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            knockback = 0;
        }
    }
}
