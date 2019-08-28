using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class AwokenDreamDashSlash : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Awoken Dream Dash Slash");
            Main.projFrames[projectile.type] = 9;
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
            projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = -1;
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
            if(Main.myPlayer == projectile.owner)
            {
                bool channeling = projectile.ai[0] < 25 && !player.noItems && !player.CCed;
                if (!channeling)
                {
                    projectile.Kill();
                }
			}
			if (projectile.ai[0] < 3)
			{
				projectile.frame = 0;
			}
            else if (projectile.ai[0] < 6)
			{
				projectile.frame = 1;
			}
            else if (projectile.ai[0] < 9)
			{
				projectile.frame = 2;
			}
            else if (projectile.ai[0] < 12)
			{
				projectile.frame = 3;
			}
            else if (projectile.ai[0] < 15)
			{
				projectile.frame = 4;
			}
            else if (projectile.ai[0] < 18)
			{
				projectile.frame = 5;
			}
            else if (projectile.ai[0] < 21)
			{
				projectile.frame = 6;
			}
            else if (projectile.ai[0] < 24)
			{
				projectile.frame = 7;
			}
            else
			{
				projectile.frame = 8;
			}
            Dust.NewDust(projectile.position, projectile.width,projectile.height, 71, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), (1f + (Main.rand.Next(5)/10)));
            projectile.position = (projectile.velocity + vector) - projectile.Size / 2f;
            projectile.rotation = projectile.velocity.ToRotation() + (projectile.direction == -1 ? 3.14f : 0);
            projectile.spriteDirection = projectile.direction;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 10;
            player.itemAnimation = 10;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
            return false;
        }
    }
}