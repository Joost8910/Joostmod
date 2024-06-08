using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Thrown
{
    public class PalladiumChainedchainsaw : ChainedChainsaw
    {
        public override void SetDefaults()
        {
            Projectile.width = 56;
            Projectile.height = 56;
            Projectile.aiStyle = 2;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = -1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 8;
            Projectile.timeLeft = 660;
            AIType = ProjectileID.Shuriken;
            chainTex = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Projectiles/Palladium_Chain");
        }
    }
}
