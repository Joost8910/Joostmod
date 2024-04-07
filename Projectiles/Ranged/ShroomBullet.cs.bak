using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Ranged
{
    public class ShroomBullet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shroomite Bullet");
        }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 420;
            Projectile.alpha = 5;
            Projectile.extraUpdates = 1;
            Projectile.scale = 1.5f;
            AIType = ProjectileID.Bullet;
        }

        public override void AI()
        {
            if (Projectile.localAI[0] == 0)
                Projectile.localAI[0] = Main.rand.Next(10) + 1;
            if ((Projectile.timeLeft + Projectile.localAI[0]) % 10 == 0 && Main.myPlayer == Projectile.owner)
            {
                Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, ProjectileID.Mushroom, (int)(Projectile.damage * 0.5f), 0, Projectile.owner).DamageType = Projectile.DamageType;
            }
        }

    }
}

