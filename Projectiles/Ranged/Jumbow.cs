using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Ranged
{
    public class Jumbow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jumbow");
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 200;
            Projectile.height = 200;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 20;
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            Projectile.ai[1] = 1;
            if (!player.noItems && !player.CCed)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    float scaleFactor6 = 1f;
                    if (player.inventory[player.selectedItem].shoot == Projectile.type)
                    {
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * Projectile.scale * (player.archery ? 1.2f : 1);
                        Projectile.ai[1] = 40f / player.inventory[player.selectedItem].useTime;
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


            if (player.channel && !player.noItems && !player.CCed)
            {
                if (Projectile.ai[0] < 1)
                {
                    Projectile.ai[0] += Projectile.ai[1] * 0.03f;
                }
                else if (Projectile.localAI[0] == 0)
                {
                    Projectile.localAI[0] = 1;
                    SoundEngine.PlaySound(SoundID.Item39, Projectile.Center);
                    Projectile.ai[0] = 1;
                }
                Projectile.timeLeft = 11;
            }
            if (Projectile.ai[0] < 0.5f || Projectile.timeLeft < 10)
            {
                Projectile.frame = 0;
            }
            else if (Projectile.ai[0] < 1f)
            {
                Projectile.frame = 1;
            }
            else
            {
                Projectile.frame = 2;
            }
            if (!player.channel && Projectile.timeLeft == 10)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Projectile.velocity * Projectile.ai[0], ModContent.ProjectileType<GiantArrow>(), (int)(Projectile.damage * Projectile.ai[0]), Projectile.knockBack * Projectile.ai[0], Projectile.owner);
                SoundEngine.PlaySound(SoundID.Item5, Projectile.Center);
            }

            Vector2 dir = Projectile.velocity;
            dir.Normalize();
            Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f + dir * -30;
            Projectile.rotation = dir.ToRotation() + (Projectile.direction < 0 ? 3.14f : 0);
            Projectile.spriteDirection = Projectile.direction;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 10;
            player.itemAnimation = 10;
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * Projectile.direction), (double)(Projectile.velocity.X * Projectile.direction));
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
    }
}