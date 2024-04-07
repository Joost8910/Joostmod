using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class PlatinumFlail : Flail
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Platinum Flail");
        }
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.aiStyle = 15;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 4800;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            throwSpeed = 11.5f;
            outTime = 13;
            returnSpeed = 8f;
            returnSpeedAfterHeld = 13f;
            swingHitCD = 16;
            swingSpeed = 0.75f;
            chainTex = (Texture2D)ModContent.Request<Texture2D>("JoostMod/Projectiles/Platinum_Chain");
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Width = (int)(34 * Projectile.scale);
            hitbox.Height = (int)(34 * Projectile.scale);
            hitbox.X -= (hitbox.Width - Projectile.width) / 2;
            hitbox.Y -= (hitbox.Height - Projectile.height) / 2;
        }
        public override void CheckStats(ref float speedMult)
        {
            Player player = Main.player[Projectile.owner];
            if (player.HeldItem.shoot == Projectile.type)
            {
                Projectile.scale = player.HeldItem.scale;
                speedMult *= 44f / player.HeldItem.useTime;
            }
            Projectile.width = (int)(26 * Projectile.scale);
            Projectile.height = (int)(26 * Projectile.scale);
        }
    }
}
