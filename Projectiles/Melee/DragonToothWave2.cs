using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class DragonToothWave2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Plunging Attack");
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            AIType = ProjectileID.Bullet;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 50;
        }
        public override void AI()
        {
            if (Projectile.velocity.Y < 20)
            {
                Projectile.velocity.Y += 0.3f;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.knockBackResist > 0 && Projectile.velocity.Y < 0)
            {
                target.velocity.Y -= Projectile.knockBack * target.knockBackResist;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)/* tModPorter Note: Removed. Use OnHitPlayer and check info.PvP */
        {
            Player player = Main.player[Projectile.owner];
            if (!target.noKnockback && Projectile.velocity.Y < 0)
            {
                target.velocity.Y -= Projectile.knockBack;
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int d = 0; d < 4; d++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 1);
            }
        }
    }
}
