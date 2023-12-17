using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Ranged
{
    public class BlazingArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Volcanic Arrow");
        }
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.arrow = true;
            AIType = ProjectileID.WoodenArrowFriendly;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 600);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 600);
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 6, -Projectile.velocity.X, -Projectile.velocity.Y, 0, default, 2f);
        }
        public override void AI()
        {
            Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 6, 0, 2, 0, default, 2f).noGravity = true;
            if (Main.rand.NextBool(15))
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, new Vector2(0, 1), ModContent.ProjectileType<BlazingDroplet>(), Projectile.damage / 3, 0, Projectile.owner);
            }
        }
    }
}

