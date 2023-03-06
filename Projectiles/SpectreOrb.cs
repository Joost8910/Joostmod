using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class SpectreOrb : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spectral Orb");
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.alpha = 120;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            //Making player variable "p" set as the projectile's owner

            //Factors for calculations
            double deg = (double)Projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
            double rad = deg * (Math.PI / 180); //Convert degrees to radians
            double dist = (Projectile.ai[1] * -1) / 2; //Distance away from the player
            Projectile.rotation = (float)rad * 1.5f;

            /*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
            Projectile.position.X = Main.player[Projectile.owner].Center.X - (int)(Math.Cos(rad) * dist / 2) - Projectile.width / 2;
            Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (int)(Math.Sin(rad) * dist / 2) - Projectile.height / 2;

            //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
            Projectile.ai[1] += 6f;
        }

    }
}

