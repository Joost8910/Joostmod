using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class ToxicWaterDisperse : ModProjectile
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Toxic Water");
            Main.projFrames[Projectile.type] = 4;
		}
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.aiStyle = 1;
            Projectile.alpha = 80;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 20;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 600 + Main.rand.Next(600));
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 600 + Main.rand.Next(600));
        }
        public override void AI()
        {
            Projectile.alpha += 2;
            if (Projectile.timeLeft % 5 == 1)
            {
                Projectile.frame++;
            }
            if (Main.tile[(int)Projectile.Center.ToTileCoordinates().X, (int)Projectile.Center.ToTileCoordinates().Y].LiquidAmount <= 80)
            {
                Projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            int numberProjectiles = 6;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X / 2, Projectile.velocity.Y / 2).RotatedByRandom(MathHelper.ToRadians(100));
                perturbedSpeed *= 1f - (Main.rand.NextFloat() * .5f);
                Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, Mod.Find<ModProjectile>("ToxicWater2").Type, Projectile.damage, 0, Projectile.owner);
            }
        }
    }
}

