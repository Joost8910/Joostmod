using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class AwokenDreamGreatSlash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Awoken Dream Great Slash");
            Main.projFrames[Projectile.type] = 9;
        }
        public override void SetDefaults()
        {
            Projectile.width = 192;
            Projectile.height = 192;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 2;
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            Projectile.ai[0]++;
            bool channeling = Projectile.ai[0] < 25 && !player.noItems && !player.CCed;
            if (!channeling)
            {
                Projectile.Kill();
            }
            if (Projectile.ai[0] < 3)
            {
                Projectile.frame = 0;
            }
            else if (Projectile.ai[0] < 6)
            {
                Projectile.frame = 1;
            }
            else if (Projectile.ai[0] < 9)
            {
                Projectile.frame = 2;
            }
            else if (Projectile.ai[0] < 12)
            {
                Projectile.frame = 3;
            }
            else if (Projectile.ai[0] < 15)
            {
                Projectile.frame = 4;
            }
            else if (Projectile.ai[0] < 18)
            {
                Projectile.frame = 5;
            }
            else if (Projectile.ai[0] < 21)
            {
                Projectile.frame = 6;
            }
            else if (Projectile.ai[0] < 24)
            {
                Projectile.frame = 7;
            }
            else
            {
                Projectile.frame = 8;
            }
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 71, Projectile.velocity.X / 2, Projectile.velocity.Y / 2, 100, default, 0.8f + Main.rand.Next(5) / 10);
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 71, Projectile.velocity.X / 2, Projectile.velocity.Y / 2, 100, default, 0.8f + Main.rand.Next(5) / 10);
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
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.velocity.Y * player.gravDir > 0 && player.velocity.Y * player.gravDir > 0 && Math.Abs(Projectile.velocity.X) < 9)
            {
                player.velocity.Y = Math.Abs(player.velocity.Y) < 12 ? -12 * player.gravDir : -player.velocity.Y;
            }
        }
    }
}