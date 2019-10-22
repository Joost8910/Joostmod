using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class AwokenDreamSpinSlash : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Awoken Dream Spin Slash");
            Main.projFrames[projectile.type] = 14;
		}
        public override void SetDefaults()
        {
            projectile.width = 420;
            projectile.height = 96;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.ownerHitCheck = true;
            projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 5;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += (int)(target.defense / 2);
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            projectile.ai[0]++;
            bool channeling = projectile.ai[0] < 120 && (player.controlUseItem || projectile.ai[0] < 30) && !player.noItems && !player.CCed;
            if (!channeling)
            {
                projectile.Kill();
            }
            projectile.frame = (projectile.frame + 1) % 14;
            if(projectile.ai[0] % 7 == 0)
            {
                Main.PlaySound(42, (int)projectile.position.X, (int)projectile.position.Y, 183);
            }
            Dust.NewDust(projectile.position, projectile.width,projectile.height, 71, projectile.velocity.X*20, 0, 100, default(Color), (0.8f + (Main.rand.Next(5)/10)));
            Dust.NewDust(projectile.position, projectile.width,projectile.height, 71, projectile.velocity.X*-20, 0, 100, default(Color), (0.8f + (Main.rand.Next(5)/10)));
            player.velocity.Y *= 0.9f;
            player.fallStart = (int)(player.position.Y / 16f);
            player.ChangeDir(projectile.direction * (projectile.ai[0] % 14 < 7 ? projectile.direction : -projectile.direction));
            projectile.position = vector - projectile.Size / 2f;
            projectile.rotation = 0;
            projectile.spriteDirection = projectile.direction;
            player.heldProj = projectile.whoAmI;
            player.itemTime = 10;
            player.itemAnimation = 10;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
            return false;
        }
    }
}