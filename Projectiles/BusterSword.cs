using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class BusterSword : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Buster Sword");
        }
        public override void SetDefaults()
        {
            Projectile.width = 136;
            Projectile.height = 120;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            target.velocity.Y -= knockback * target.knockBackResist * player.gravDir;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (!target.noKnockback)
            {
                target.velocity.Y -= Projectile.knockBack * player.gravDir;
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (Projectile.ai[0] == 0)
            {
                Player player = Main.player[Projectile.owner];
                Vector2 unit = Projectile.velocity;
                Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
                float point = 0f;
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), vector, vector + unit * (int)(Projectile.scale * 172), (int)(Projectile.scale * 30), ref point))
                {
                    return true;
                }
            }
            return false;
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            float speed = 1;
            bool channeling = !player.dead && !player.noItems && !player.CCed;
            if (!channeling)
            {
                Projectile.Kill();
            }
            if (Projectile.velocity.X < 0)
            {
                Projectile.direction = -1;
            }
            else
            {
                Projectile.direction = 1;
            }
            if (Projectile.ai[0] == 0)
            {
                Projectile.velocity.X = Projectile.direction * 4;
                if (Projectile.soundDelay >= 0)
                {
                    SoundEngine.PlaySound(SoundID.Trackable, Projectile.Center);
                    Projectile.soundDelay = -1;
                }
                Projectile.localAI[1] -= speed;
                if (Projectile.localAI[1] >= -20)
                {
                    Projectile.velocity.Y = (Projectile.localAI[1] + 8) * player.gravDir * 2;
                }
                else
                {
                    Projectile.Kill();
                }
                if (Projectile.localAI[1] == -10)
                {
                    Vector2 vel = Projectile.velocity;
                    if (Main.myPlayer == Projectile.owner)
                    {
                        Vector2 vector13 = Main.MouseWorld - vector;
                        vector13.Normalize();
                        if (vector13.HasNaNs())
                        {
                            vector13 = Vector2.UnitX * (float)player.direction;
                        }
                        if (vector13.X != Projectile.velocity.X || vector13.Y != Projectile.velocity.Y)
                        {
                            Projectile.netUpdate = true;
                        }
                        vel = vector13;
                    }
                    Projectile.NewProjectile(source, player.Center, vel * 14f, Mod.Find<ModProjectile>("BusterBeam").Type, Projectile.damage, Projectile.knockBack / 3, Projectile.owner);
                }
            }
            Projectile.velocity.Normalize();
            Projectile.position = (vector - (Projectile.Size / 2f)) + (Projectile.velocity * Projectile.width * 0.7f);
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.direction == -1 ? 1.57f : 0) + 0.785f;
            Projectile.spriteDirection = Projectile.direction;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * (float)Projectile.direction), (double)(Projectile.velocity.X * (float)Projectile.direction));
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            if (Projectile.ai[0] > 0)
            {
                Projectile.velocity.Normalize();
                float shootSpeed = player.inventory[player.selectedItem].shootSpeed;
                for (int i = 0; i < Projectile.frame; i++)
                {
                    Vector2 vel = Projectile.velocity * (shootSpeed + (i * shootSpeed / 11));
                    Projectile.NewProjectile(vector, vel, (int)Projectile.ai[1], Projectile.damage, Projectile.knockBack / 2f, Projectile.owner);
                    SoundEngine.PlaySound(SoundID.Item63, Projectile.Center);
                }
            }
        }
    }
}