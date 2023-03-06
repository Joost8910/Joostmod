using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class BitterEndFriendly : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bitter End");
            Main.projFrames[Projectile.type] = 18;
        }
        public override void SetDefaults()
        {
            Projectile.width = 124;
            Projectile.height = 124;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
            Projectile.tileCollide = false;
            Projectile.light = 0.95f;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 10)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % 18;
                SoundEngine.PlaySound(SoundID.Item15, Projectile.position);
            }
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void Kill(int timeLeft)
        {
            int shootNum = 36;
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
                Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), Mod.Find<ModProjectile>("BitterEndFriendly2").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
            SoundEngine.PlaySound(SoundID.Item74, Projectile.position);
        }

    }
}
