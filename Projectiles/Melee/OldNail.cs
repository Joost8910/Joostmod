using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class OldNail : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Old Nail");
            Main.projFrames[Projectile.type] = 9;
        }
        public override void SetDefaults()
        {
            Projectile.width = 58;
            Projectile.height = 58;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            bool channeling = player.itemAnimation > 0 && !player.noItems && !player.CCed;
            /*if(channeling)
            {
                float scaleFactor6 = 1f;
                if (player.inventory[player.selectedItem].shoot == projectile.type)
                {
                    scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                }
                Vector2 vector13 = Main.MouseWorld - vector;
                vector13.Normalize();
                if (vector13.HasNaNs())
                {
                    vector13 = Vector2.UnitX * (float)player.direction;
                }
                vector13 *= scaleFactor6;
                if (vector13.X != projectile.velocity.X || vector13.Y != projectile.velocity.Y)
                {
                    projectile.netUpdate = true;
                }
                projectile.velocity = vector13;
            }
            else
            {
                projectile.Kill();
            }*/
            if (!channeling)
            {
                Projectile.Kill();
            }
            if (player.itemAnimation > (int)(8 * (float)player.itemAnimationMax / 9))
            {
                Projectile.frame = 0;
            }
            else if (player.itemAnimation > (int)(7 * (float)player.itemAnimationMax / 9))
            {
                Projectile.frame = 1;
            }
            else if (player.itemAnimation > (int)(6 * (float)player.itemAnimationMax / 9))
            {
                Projectile.frame = 2;
            }
            else if (player.itemAnimation > (int)(5 * (float)player.itemAnimationMax / 9))
            {
                Projectile.frame = 3;
            }
            else if (player.itemAnimation > (int)(4 * (float)player.itemAnimationMax / 9))
            {
                Projectile.frame = 4;
            }
            else if (player.itemAnimation > (int)(3 * (float)player.itemAnimationMax / 9))
            {
                Projectile.frame = 5;
            }
            else if (player.itemAnimation > (int)(2 * (float)player.itemAnimationMax / 9))
            {
                Projectile.frame = 6;
            }
            else if (player.itemAnimation > (int)((float)player.itemAnimationMax / 9))
            {
                Projectile.frame = 7;
            }
            else
            {
                Projectile.frame = 8;
            }
            Projectile.position = Projectile.velocity + vector - Projectile.Size / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.direction == -1 ? 3.14f : 0);
            Projectile.spriteDirection = Projectile.direction;
            //player.ChangeDir(projectile.direction);
            player.heldProj = Projectile.whoAmI;
            //player.itemTime = 10;
            //player.itemAnimation = 10;
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * Projectile.direction), (double)(Projectile.velocity.X * Projectile.direction));
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.velocity.Y * player.gravDir > 0 && player.velocity.Y * player.gravDir > 0 && Math.Abs(Projectile.velocity.X) < 2)
            {
                player.velocity.Y = Math.Abs(player.velocity.Y) < 5 ? -5 * player.gravDir : -player.velocity.Y;
            }
        }
    }
}