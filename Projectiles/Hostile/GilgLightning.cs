using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class GilgLightning : ModProjectile
    {

        public override string Texture => "Terraria/Images/Projectile_466";
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
