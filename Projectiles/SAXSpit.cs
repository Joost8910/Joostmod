using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class SAXSpit : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Acidic Saliva");
		}
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 1;
            projectile.alpha = 20;
            projectile.hostile = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 600 + Main.rand.Next(600));
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 600 + Main.rand.Next(600));
            if (Main.expertMode)
            {
                target.AddBuff(BuffID.OgreSpit, 300);
            }
        }
        public override void AI()
        {
            projectile.velocity.X *= 0.98f;
            projectile.velocity.Y = (projectile.velocity.Y < 10 ? projectile.velocity.Y + 0.3f : projectile.velocity.Y);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(4, projectile.Center, 9);
            int numberProjectiles = 6;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(projectile.velocity.X / 2, projectile.velocity.Y / 2).RotatedByRandom(MathHelper.ToRadians(360));
                perturbedSpeed *= 1f - (Main.rand.NextFloat() * .3f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SAXSpit2"), projectile.damage / 2, 0, projectile.owner);
            }
        }
	}
}

