using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Thrown
{
    public class Boomerain : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boomerain");
        }
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1200;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 15;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            Projectile.aiStyle = 3;
            //projectile.tileCollide = false;
            if (Projectile.timeLeft % 8 == 0)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center.X + Main.rand.Next(-Projectile.width / 2, Projectile.width / 2), Projectile.Center.Y, 0, 7, ModContent.ProjectileType<Rain>(), Projectile.damage, 0, Projectile.owner);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }

    }
}

