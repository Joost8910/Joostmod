using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class TheRose : Flail
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Rose");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
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
            outTime = 18;
            throwSpeed = 13f;
            returnSpeed = 10f;
            returnSpeedAfterHeld = 15f;
            swingHitCD = 15;
            swingSpeed = 0.8f;
            chainTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_Chain");
        }
        public override void ThrowEffects()
        {
            Player player = Main.player[Projectile.owner];
            if (Main.myPlayer == Projectile.owner && player.ownedProjectileCounts[ModContent.ProjectileType<RoseThorns>()] <= 0)
                Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center, Projectile.velocity, ModContent.ProjectileType<RoseThorns>(), (int)(Projectile.damage * 0.8f), Projectile.knockBack * 0.5f, Projectile.owner, Projectile.whoAmI);
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Width = (int)(30 * Projectile.scale);
            hitbox.Height = (int)(30 * Projectile.scale);
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
    public class RoseThorns : ModProjectile
    {
        public override string Texture => "JoostMod/Projectiles/Melee/TheRose";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Rose's Thorns");
        }
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 4800;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
        public override void AI()
        {
            Projectile owner = Main.projectile[(int)(Projectile.ai[0])];
            if (!owner.active || owner.type != ModContent.ProjectileType<TheRose>() || Projectile.owner != owner.owner)
            {
                Projectile.Kill();
            }
            Projectile.Center = owner.Center;
            Projectile.velocity = owner.velocity;
            Projectile.timeLeft = 2;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            hitDirection = Math.Sign(Projectile.Center.X - Main.player[Projectile.owner].Center.X);
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Player player = Main.player[Projectile.owner];
            Vector2 playerCenter = player.MountedCenter;
            if (player.bodyFrame.Y == player.bodyFrame.Height * 3)
            {
                playerCenter.X += 8 * player.direction;
                playerCenter.Y += 2 * player.gravDir;
            }
            else if (player.bodyFrame.Y == player.bodyFrame.Height * 2)
            {
                playerCenter.X += 6 * player.direction;
                playerCenter.Y += -12 * player.gravDir;
            }
            else if (player.bodyFrame.Y == player.bodyFrame.Height * 4)
            {
                playerCenter.X += 6 * player.direction;
                playerCenter.Y += 8 * player.gravDir;
            }
            else if (player.bodyFrame.Y == player.bodyFrame.Height)
            {
                playerCenter.X += -10 * player.direction;
                playerCenter.Y += -14 * player.gravDir;
            }
            float point = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), playerCenter, Projectile.Center, 8, ref point))
            {
                return true;
            }
            return false;
        }

    }
}