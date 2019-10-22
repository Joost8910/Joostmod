using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class UnicornLance : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Unicorn Lance");
		}
        public override void SetDefaults()
        {
            projectile.width = 50;
            projectile.height = 50;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.light = 0.20f;
            projectile.ownerHitCheck = true;
            projectile.extraUpdates = 1;
        }

        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            //float num = 1.57079637f;          
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            bool channeling = player.channel && !player.noItems && !player.CCed;
            if (channeling)
            {
                if (Main.myPlayer == projectile.owner)
                {
                    //projectile.ai[0]++;
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
            else
            {
                projectile.Kill();
            }
        
        
            projectile.position = (projectile.velocity + vector) - projectile.Size / 2f;
            projectile.rotation = projectile.velocity.ToRotation() + 2.355f;
            //projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 10;
            player.itemAnimation = 10;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
            return false;
        }
		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[projectile.owner];
            double speed = player.velocity.Length();
			damage = (int)(damage * speed  * speed / 30);
		}
  		public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)
		{
			Player player = Main.player[projectile.owner];
            if (projectile.velocity.X * player.velocity.X > 0 && Math.Abs(projectile.velocity.X) > 3)
            {
                player.velocity.X *= -1;
            }
            if (projectile.velocity.Y * player.velocity.Y > 0 && Math.Abs(projectile.velocity.Y) > 3)
            {
		        player.velocity.Y *= -1;
            }
		}
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            Player player = Main.player[projectile.owner];
            double speed = player.velocity.Length();
            damage = (int)(damage * speed * speed / 30);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (projectile.velocity.X * player.velocity.X > 0 && Math.Abs(projectile.velocity.X) > 3)
            {
                player.velocity.X *= -1;
            }
            if (projectile.velocity.Y * player.velocity.Y > 0 && Math.Abs(projectile.velocity.Y) > 3)
            {
                player.velocity.Y *= -1;
            }
        }

    }
}