using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class EggSack : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Kraken);
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Egg Sack");
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 20;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 300f;
        }
        public override void AI()
        {
            int SPIDERS = Main.rand.Next(1, 37);
            if (SPIDERS > 35f)
            {

                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, 379, (int)(Projectile.damage * 0.5f), 0, Projectile.owner, 0f, 0f); //Spawning a projectile

            }
        }

    }
}


