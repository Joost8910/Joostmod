using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class GilgLightning : ModProjectile
    {
        public override string Texture
        {
            get
            {
                return "Terraria/Projectile_466";
            }
        }
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gilgamesh's Wrath");
        }
		public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.CultistBossLightningOrbArc);
            Projectile.extraUpdates = 2;
            AIType = ProjectileID.CultistBossLightningOrbArc;
        }
    }
}
