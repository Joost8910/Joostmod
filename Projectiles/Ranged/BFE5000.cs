using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Ranged
{
    public class BFE5000 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BFE5000");
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 240;
            AIType = ProjectileID.Bullet;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 34;
            height = 34;
            return true;
        }
        public override void AI()
        {
            if (Projectile.timeLeft % 5 == 0)
            {
                Projectile.frame = (Projectile.frame + 1) % 3;
                Dust.NewDust(Projectile.position - Vector2.Normalize(Projectile.velocity) * 80, Projectile.width, Projectile.height, 127, -Projectile.velocity.X, -Projectile.velocity.Y, 200, default, 1f + (float)Main.rand.Next(10) / 10);
            }
            if (Projectile.timeLeft % 3 == 0)
            {
                Dust.NewDust(Projectile.position - Vector2.Normalize(Projectile.velocity) * 80, Projectile.width, Projectile.height, 6, -Projectile.velocity.X * 1.2f, -Projectile.velocity.Y * 1.2f, 100, default, 2f + (float)Main.rand.Next(10) / 10);
            }
        }
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * 0, Projectile.velocity.Y * 0, Mod.Find<ModProjectile>("Explosion").Type, (int)(Projectile.damage * 1f), Projectile.knockBack, Projectile.owner);
            int shootNum = 3 + Main.rand.Next(4);
            float shootSpread = 360f;
            float spread = shootSpread * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(7f * 7f + 7f * 7f);
            double startAngle = Math.Atan2(7f, 7f) - spread / shootNum;
            double deltaAngle = spread / shootNum;
            double offsetAngle;
            int i;
            for (i = 0; i < shootNum; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Mod.Find<ModProjectile>("Kerbal").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
            SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/MissileExplode"), Projectile.Center);

        }
    }
}

