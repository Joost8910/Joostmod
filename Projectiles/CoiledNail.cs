using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class CoiledNail : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Coiled Nail");
            Main.projFrames[projectile.type] = 9;
		}
        public override void SetDefaults()
        {
            projectile.width = 88;
            projectile.height = 88;
            //projectile.scale = 1.5f;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.ownerHitCheck = true;
            projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = -1;
        }
        public override bool? CanHitNPC(NPC target)
		{
            Player player = Main.player[projectile.owner];
			return !target.friendly && player.itemAnimation > 1;
		}
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            projectile.ai[0]++;
            bool channeling = (player.itemAnimation > 1 || player.channel) && !player.noItems && !player.CCed; 
            if (channeling)
            {
                if (player.itemAnimation <= 1)
                {
                    if (Main.myPlayer == projectile.owner)
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
                }
                if (projectile.ai[0] >= 35 && projectile.ai[0] < 100)
                {
                    if (projectile.ai[0] % 15 == 0)
                    {
                        Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 13);
                    }
                    int dust = Dust.NewDust(player.position, player.width, player.height, 261);
                    Main.dust[dust].noGravity = true;
                }
                if (projectile.ai[0] == 100)
                {
                    Main.PlaySound(42, (int)projectile.position.X, (int)projectile.position.Y, 217);
                }
                if (projectile.ai[0] > 100)
                {
                    if (projectile.ai[0] % 6 == 0)
                    {
                        Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 7);
                    }
                    int dust2 = Dust.NewDust(new Vector2(player.Center.X - 4, player.Center.Y + player.height / 2 * player.gravDir), 1, 1, 261, 5, -3 * player.gravDir, 0, default(Color), 1);
                    Main.dust[dust2].noGravity = true;
                    int dust3 = Dust.NewDust(new Vector2(player.Center.X - 4, player.Center.Y + player.height / 2 * player.gravDir), 1, 1, 261, -5, -3 * player.gravDir, 0, default(Color), 1);
                    Main.dust[dust3].noGravity = true;
                }
            }
            else
            {
                projectile.Kill();
            }
			if (player.itemAnimation > (int)(8*(float)player.itemAnimationMax/9))
			{
				projectile.frame = 0;
			}
            else if (player.itemAnimation > (int)(7*(float)player.itemAnimationMax/9))
			{
				projectile.frame = 1;
			}
            else if (player.itemAnimation > (int)(6*(float)player.itemAnimationMax/9))
			{
				projectile.frame = 2;
			}
            else if (player.itemAnimation > (int)(5*(float)player.itemAnimationMax/9))
			{
				projectile.frame = 3;
			}
            else if (player.itemAnimation > (int)(4*(float)player.itemAnimationMax/9))
			{
				projectile.frame = 4;
			}
            else if (player.itemAnimation > (int)(3*(float)player.itemAnimationMax/9))
			{
				projectile.frame = 5;
			}
            else if (player.itemAnimation > (int)(2*(float)player.itemAnimationMax/9))
			{
				projectile.frame = 6;
			}
            else if (player.itemAnimation > (int)((float)player.itemAnimationMax/9))
			{
				projectile.frame = 7;
			}
            else
			{
				projectile.frame = 8;
                player.itemTime = 1;
                player.itemAnimation = 1;
			}
            projectile.position = (projectile.velocity + vector) - projectile.Size / 2f;
            projectile.rotation = projectile.velocity.ToRotation() + (projectile.direction == -1 ? 3.14f : 0);
            projectile.spriteDirection = projectile.direction;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            //player.itemTime = 10;
            //player.itemAnimation = 10;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
            return false;
        }
  		public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)
		{
			Player player = Main.player[projectile.owner];
            if (projectile.velocity.Y * player.gravDir > 0 && player.velocity.Y * player.gravDir > 0 && Math.Abs(projectile.velocity.X) < 4)
            {
		        player.velocity.Y = Math.Abs(player.velocity.Y) < 6 ? -6 * player.gravDir : -player.velocity.Y;
            }
		}
        public override void Kill(int timeLeft)
        {
            if(projectile.ai[0] > 100)
            {
                Player player = Main.player[projectile.owner];
                Vector2 pos = player.RotatedRelativePoint(player.MountedCenter, true);	
                if (Math.Abs(player.velocity.X) >= 6 && player.velocity.X * projectile.velocity.X > 0 && Math.Abs(projectile.velocity.Y) < 4)
                {
                    Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 74);
                    Projectile.NewProjectile(pos.X, pos.Y, player.direction * 40, 0, mod.ProjectileType("DashSlash"), projectile.damage * 9, projectile.knockBack * 5, projectile.owner);
                    player.velocity.X += 7 * player.direction;
                }
                else if (player.controlUp || player.controlDown)
                {
                     Projectile.NewProjectile(pos.X, pos.Y, player.direction, 0, mod.ProjectileType("SpinSlash"), projectile.damage, projectile.knockBack, projectile.owner);
                }
                else
                {
                    Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 74);
                    Projectile.NewProjectile(pos.X, pos.Y, projectile.velocity.X * 2, projectile.velocity.Y * 2, mod.ProjectileType("GreatSlash"), projectile.damage * 9, projectile.knockBack * 5, projectile.owner);
                }
            }
            else
            {
                Player player = Main.player[projectile.owner];
                Vector2 pos = player.RotatedRelativePoint(player.MountedCenter, true);
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 19);
                Projectile.NewProjectile(pos.X, pos.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("CoiledNail2"), projectile.damage, projectile.knockBack, projectile.owner);
                player.itemTime = player.itemAnimationMax;
                player.itemAnimation = player.itemAnimationMax;
            }
        }
    }
}