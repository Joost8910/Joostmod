using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace JoostMod.Projectiles.Hostile
{
    public class CactusNeedle : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("10000 Needles");
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.aiStyle = 1;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 300;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            if (Projectile.timeLeft == 300)
            {
                if (Main.expertMode)
                    Projectile.penetrate++;
                if (Main.masterMode)
                    Projectile.penetrate += 2;
                if (Main.getGoodWorld)
                    Projectile.penetrate++;
            }
        }
        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            //player.GetModPlayer<JoostPlayer>().enemyIgnoreDefenseDamage = 10;
            modifiers.ScalingArmorPenetration += 1f;
            modifiers.SetMaxDamage(10);
            modifiers.ModifyHurtInfo += (ref Player.HurtInfo hurtInfo) =>
            {
                hurtInfo.Dodgeable = false;
            };
            //modifiers.Cancel();
            //target.Hurt(modifiers.inf);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            /*
            info.CooldownCounter = 3;
            info.Dodgeable = false;
            */
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
        }
    }
}

