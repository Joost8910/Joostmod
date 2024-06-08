using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Thrown
{
    public class Tornade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tornade");
        }
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = 2;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 200;
            AIType = ProjectileID.Shuriken;
        }
        public override void AI()
        {
            Projectile.ai[1] = -1;
            Projectile.ai[2] = -1;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.ai[1] = target.whoAmI;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Projectile.ai[2] = target.whoAmI;
        }
        public override void OnKill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y - 15, 0, 0, ModContent.ProjectileType<Nado>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, Projectile.ai[1], Projectile.ai[2]);
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        }
    }
}

