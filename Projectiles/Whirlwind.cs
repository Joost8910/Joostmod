using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class Whirlwind : ModProjectile
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Whirlwind");
			Main.projFrames[projectile.type] = 6;
		}
        public override void SetDefaults()
        {
            projectile.width = 150;
            projectile.height = 100;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.melee = true;
		    projectile.alpha = 80;
            projectile.extraUpdates = 1;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 8;
            projectile.ignoreWater = true;
            projectile.ownerHitCheck = true;
            drawHeldProjInFrontOfHeldItemAndArms = true;
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            bool channeling = player.channel && !player.noItems && !player.CCed;
            if (channeling)
            {
                projectile.ai[0]++;
                player.AddBuff(mod.BuffType("Whirlwind"), 2);
                if (Main.myPlayer == projectile.owner)
                {
                    Vector2 vector13 = Main.MouseWorld - vector;
                    vector13.Normalize();
                    if (vector13.HasNaNs())
                    {
                        vector13 = Vector2.UnitX * (float)player.direction;
                    }
                    if (vector13.X > 0)
                    {
                        projectile.direction = (int)player.gravDir;
                        projectile.netUpdate = true;
                    }
                    else
                    {
                        projectile.direction = -(int)player.gravDir;
                        projectile.netUpdate = true;
                    }
                }
            }
            else
            {
                projectile.Kill();
            }
            if (projectile.ai[0] % 16 <= 0)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 18);
            }
            projectile.frameCounter++;
            if (projectile.frameCounter >= 8)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 6;
            }
            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f;
            projectile.velocity.X = projectile.direction;
            projectile.position.X -= projectile.velocity.X;
            projectile.position.Y -= 11;
            projectile.rotation = 0f;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction * (projectile.ai[0] % 32 <= 16 ? 1 : -1));
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            return false;
        }
  
    }
}