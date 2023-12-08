using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class TomatoHead : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tomato Head");
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return !target.friendly && Projectile.ai[1] >= 20;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (player.immuneTime < Projectile.localNPCHitCooldown)
            {
                player.immune = true;
                player.immuneTime = Projectile.localNPCHitCooldown;
            }
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Width = (int)(40f * Projectile.scale);
            hitbox.Height = (int)(40f * Projectile.scale);
            hitbox.X -= (int)((40f * Projectile.scale - 40) * 0.5f);
            hitbox.Y -= (int)((40f * Projectile.scale - 40) * 0.5f);
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            Projectile.localAI[1] = 1;
            bool channeling = player.channel && !player.noItems && !player.CCed;
            if (channeling)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    float scaleFactor6 = 1f;
                    if (player.inventory[player.selectedItem].shoot == Projectile.type)
                    {
                        Projectile.scale = player.inventory[player.selectedItem].scale;
                        Projectile.localAI[1] = 30f / player.inventory[player.selectedItem].useTime / player.GetAttackSpeed(DamageClass.Melee);
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * Projectile.scale;
                    }
                    Vector2 vector13 = Main.MouseWorld - vector;
                    vector13.Normalize();
                    if (vector13.HasNaNs())
                    {
                        vector13 = Vector2.UnitX * player.direction;
                    }
                    vector13 *= scaleFactor6;
                    if (vector13.X != Projectile.velocity.X || vector13.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    Projectile.velocity = vector13;
                }
            }
            else
            {
                Projectile.Kill();
            }

            if (Projectile.ai[1] == 0)
            {
                Projectile.ai[0] = 0;
            }
            if (player.velocity.Y == 0)
            {
                Projectile.ai[0] = 1;
            }
            float speed = 8.5f;
            float accel = speed * Projectile.localAI[1] / 30f;
            Projectile.localNPCHitCooldown = (int)(20f / Projectile.localAI[1]);
            Projectile.ai[1] = Projectile.ai[1] < 30 ? Projectile.ai[1] + Projectile.localAI[1] : 0;
            bool halt = player.controlLeft && player.velocity.X > 0 && Projectile.velocity.X > 0 || player.controlRight && player.velocity.X < 0 && Projectile.velocity.X < 0;
            bool zip = player.velocity.X > 0 && Projectile.velocity.X > 0 && player.controlRight || player.velocity.X < 0 && Projectile.velocity.X < 0 && player.controlLeft;
            if (Projectile.ai[1] < 10)
            {
                Projectile.frame = 0;
                if (!zip)
                {
                    player.velocity.X *= 0.9f;
                }
            }
            else if (Projectile.ai[1] < 20)
            {
                Projectile.frame = 1;
                if (!zip)
                {
                    player.velocity.X *= 0.9f;
                }
            }
            else
            {
                if (Projectile.soundDelay <= 0)
                {
                    Projectile.soundDelay = (int)(15f / Projectile.localAI[1]);
                    SoundEngine.PlaySound(SoundID.NPCHit2.WithVolumeScale(0.3f * Projectile.scale).WithPitchOffset(-0.3f - 0.5f * (Projectile.scale - 1)), Projectile.Center);
                    for (int i = 0; i < 6 * Projectile.scale; i++)
                    {
                        int dust = Dust.NewDust(Projectile.Center - new Vector2(12 * Projectile.scale, 12 * Projectile.scale), (int)(24 * Projectile.scale), (int)(24 * Projectile.scale), DustID.PlatinumCoin, Projectile.velocity.X, Projectile.velocity.Y, 150, Color.LightBlue);
                        Main.dust[dust].noGravity = true;
                    }
                }
                Projectile.frame = 2;
                player.velocity.Y = Projectile.ai[0] > 0 && player.velocity.Y > -speed || Projectile.velocity.Y > 0 ? player.velocity.Y + Projectile.velocity.Y * accel : player.velocity.Y;
                player.velocity.X = Math.Abs(player.velocity.X) < speed ? player.velocity.X + Projectile.velocity.X * accel : player.velocity.X;
            }
            if (halt)
            {
                player.velocity.X *= 0.9f;
            }
            Projectile.position = Projectile.velocity + vector - Projectile.Size / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.direction == -1 ? 3.14f : 0);
            Projectile.spriteDirection = Projectile.direction;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 10;
            player.itemAnimation = 10;
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * Projectile.direction), (double)(Projectile.velocity.X * Projectile.direction));
            return false;
        }
    }
}