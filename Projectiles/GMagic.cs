using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class GMagic : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Magical Barrier");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.width = 60;
			projectile.height = 60;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.tileCollide = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 360;
			projectile.light = 0.5f;
			projectile.alpha = 100;
		} 
public override void AI()
{
    //Making player variable "p" set as the projectile's owner

    //Factors for calculations
    double deg = (double) projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
    double rad = deg * (Math.PI / 180); //Convert degrees to radians
    double dist = 150; //Distance away from the player
 
    /*Position the player based on where the player is, the Sin/Cos of the angle times the /
    /distance for the desired distance away from the player minus the projectile's width   /
    /and height divided by two so the center of the projectile is at the right place.     */
    projectile.position.X = Main.player[projectile.owner].Center.X - (int)(Math.Cos(rad) * dist) - projectile.width/2;
    projectile.position.Y = Main.player[projectile.owner].Center.Y - (int)(Math.Sin(rad) * dist) - projectile.height/2;
 
    //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
    projectile.ai[1] += 5f;
}

	}
}

