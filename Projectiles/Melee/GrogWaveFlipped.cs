using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class GrogWaveFlipped : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Warhammer of Grognak");
        }
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100;
            Projectile.tileCollide = false;
            AIType = ProjectileID.Bullet;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        public override void AI()
        {
            if (Projectile.localAI[0] < 10)
            {
                Projectile.direction = Projectile.velocity.X > 0 ? 1 : -1;
                Projectile.spriteDirection = Projectile.direction;
                Projectile.velocity = Vector2.Zero;
                Projectile.localAI[0] = 10;
            }
            Projectile.scale = Projectile.timeLeft * 0.02f;
            Projectile.position.X += Projectile.scale * 16 * Projectile.spriteDirection;
            if (Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
            {
                Projectile.position.Y += 16 * Projectile.scale;
            }
            if (Main.myPlayer == Projectile.owner)
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center.X, Projectile.Center.Y, 0, -15f, ModContent.ProjectileType<GrogWaveFlipped1>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.spriteDirection, Projectile.scale);
        }

    }
}
