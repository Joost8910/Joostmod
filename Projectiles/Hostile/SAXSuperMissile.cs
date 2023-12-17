using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class SAXSuperMissile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Super Missile");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 1;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 1500;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            int dustIndex = Dust.NewDust(Projectile.Center - Projectile.velocity, 1, 1, 127, 0, 0, 0, default, 2f);
            Main.dust[dustIndex].noGravity = true;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                target.wingTime = 0;
                target.rocketTime = 0;
                target.mount.Dismount(target);
                target.velocity.Y = 10;
            }
            Projectile.Kill();
        }
        public override void Kill(int timeLeft)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * 0, Projectile.velocity.Y * 0, ModContent.ProjectileType<SAXExplosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
            SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/MissileExplode"), Projectile.Center);
        }
    }
}

