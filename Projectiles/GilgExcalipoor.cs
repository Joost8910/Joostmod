using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class GilgExcalipoor : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Excalipoor... somehow");
        }
		public override void SetDefaults()
		{
			projectile.width = 102;
			projectile.height = 102;
			projectile.aiStyle = 2;
			projectile.hostile = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 300;
			aiType = ProjectileID.Shuriken;
        }
        public override void AI()
        {
            projectile.damage = 1;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(3, projectile.Center, 4);
            Item.NewItem(projectile.Center, mod.ItemType("BrokenExcalipoor"));
            Vector2 dir = projectile.velocity * 0.5f;
            dir.Y *= -1;
            Gore.NewGore(projectile.position, dir, mod.GetGoreSlot("Gores/Excalipoor1"), 1f);
            dir.X *= -1;
            Gore.NewGore(projectile.position, dir, mod.GetGoreSlot("Gores/Excalipoor2"), 1f);
        }
    }
}
