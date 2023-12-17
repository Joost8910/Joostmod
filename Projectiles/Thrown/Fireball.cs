using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Thrown
{
    public class Fireball : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fireball");
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 200;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.WoodenArrowFriendly;
        }
        public override void AI()
        {
            if (Main.tile[Projectile.Center.ToTileCoordinates().X, Projectile.Center.ToTileCoordinates().Y].LiquidAmount > 80 && (Main.tile[Projectile.Center.ToTileCoordinates().X, Projectile.Center.ToTileCoordinates().Y].LiquidType == 0 || Main.tile[Projectile.Center.ToTileCoordinates().X, Projectile.Center.ToTileCoordinates().Y].LiquidType == 2))
            {
                Projectile.Kill();
            }
            Projectile.rotation = Projectile.timeLeft * 6;
            int num1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 127, Projectile.velocity.X / 10, Projectile.velocity.Y / 10, 100, default, 1f);
            Main.dust[num1].noGravity = true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }
        public override void Kill(int timeLeft)
        {
            int shootNum = 12;
            float shootSpread = 360f;
            float spread = shootSpread * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y);
            double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y) - spread / shootNum;
            double deltaAngle = spread / shootNum;
            double offsetAngle;
            int i;
            for (i = 0; i < shootNum; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), ModContent.ProjectileType<Flamethrown>(), Projectile.damage, 0, Projectile.owner);
            }
            SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
        }
    }
}

