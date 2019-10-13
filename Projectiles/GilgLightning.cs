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
            projectile.CloneDefaults(ProjectileID.CultistBossLightningOrbArc);
            projectile.extraUpdates = 2;
            aiType = ProjectileID.CultistBossLightningOrbArc;
        }
    }
}
