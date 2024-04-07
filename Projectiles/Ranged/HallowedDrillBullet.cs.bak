using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Ranged
{
    public class HallowedDrillBullet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hallowed Drill Bullet");
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.aiStyle = 1;
            AIType = ProjectileID.Bullet;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 0;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.velocity.Normalize();
            Projectile.ai[0] = 5;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = target.defense / 2 + damage / 40;
        }
        public override void AI()
        {
            if (Projectile.timeLeft >= 300)
            {
                Projectile.penetrate = Projectile.damage;
                if (Projectile.penetrate > 40)
                {
                    Projectile.penetrate = 40;
                }
                Projectile.knockBack = 0;
                Projectile.ai[1] = Projectile.velocity.Length();
            }
            Projectile.ai[0]--;
            if (Projectile.ai[0] < 0)
            {
                Projectile.velocity.Normalize();
                Projectile.velocity *= Projectile.ai[1];
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 2)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % 3;
            }
            if (Projectile.timeLeft % 20 == 0)
            {
                SoundEngine.PlaySound(SoundID.Item22, Projectile.Center);
            }
            float x = (int)(Projectile.Center.X / 16);
            float y = (int)(Projectile.Center.Y / 16);
            if (Main.tile[(int)Math.Round(x), (int)Math.Round(y)].HasTile)
            {
                SoundEngine.PlaySound(SoundID.Item23, Projectile.Center);
                Main.player[Projectile.owner].PickTile((int)Math.Round(x), (int)Math.Round(y), 200);
                Projectile.Kill();
            }
        }
    }
}
