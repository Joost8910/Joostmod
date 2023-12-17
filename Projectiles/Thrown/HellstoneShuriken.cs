using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Thrown
{
    public class HellstoneShuriken : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellfire Shuriken");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.aiStyle = 2;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.ignoreWater = false;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 600;
            Projectile.alpha = 5;
            Projectile.light = 0.5f;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Shuriken;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
        }
        public override void AI()
        {
            Projectile.spriteDirection = -Projectile.direction;
            if (Main.myPlayer == Projectile.owner)
            {
                if (Main.tile[Projectile.Center.ToTileCoordinates().X, Projectile.Center.ToTileCoordinates().Y].LiquidAmount > 150 && (Main.tile[Projectile.Center.ToTileCoordinates().X, Projectile.Center.ToTileCoordinates().Y].LiquidType == 0 || Main.tile[Projectile.Center.ToTileCoordinates().X, Projectile.Center.ToTileCoordinates().Y].LiquidType == 2))
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * 0.75f, Projectile.velocity.Y * 0.75f, ModContent.ProjectileType<DousedShuriken>(), Projectile.damage / 2, Projectile.knockBack / 2, Projectile.owner);
                    SoundEngine.PlaySound(SoundID.Item13.WithVolumeScale(0.3f), Projectile.position);
                    Projectile.Kill();
                }
            }
        }
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            Player owner = Main.player[Projectile.owner];
            n.AddBuff(24, 180);
        }

    }
}


