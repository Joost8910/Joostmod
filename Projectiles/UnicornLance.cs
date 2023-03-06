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
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.light = 0.20f;
            Projectile.ownerHitCheck = true;
            Projectile.extraUpdates = 1;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            //float num = 1.57079637f;          
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            bool channeling = player.channel && !player.noItems && !player.CCed;
            if (channeling)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    //projectile.ai[0]++;
                    float scaleFactor6 = 1f;
                    if (player.inventory[player.selectedItem].shoot == Projectile.type)
                    {
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * Projectile.scale;
                    }
                    Vector2 vector13 = Main.MouseWorld - vector;
                    vector13.Normalize();
                    if (vector13.HasNaNs())
                    {
                        vector13 = Vector2.UnitX * (float)player.direction;
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
        
        
            Projectile.position = (Projectile.velocity + vector) - Projectile.Size / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + 2.355f;
            //projectile.spriteDirection = projectile.direction;
            Projectile.timeLeft = 2;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 10;
            player.itemAnimation = 10;
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * (float)Projectile.direction), (double)(Projectile.velocity.X * (float)Projectile.direction));
            return false;
        }
		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[Projectile.owner];
            double speed = player.velocity.Length();
			damage = (int)(damage * speed  * speed / 30);
		}
  		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = Main.player[Projectile.owner];
            if (Projectile.velocity.X * player.velocity.X > 0 && Math.Abs(Projectile.velocity.X) > 3)
            {
                player.velocity.X *= -1;
            }
            if (Projectile.velocity.Y * player.velocity.Y > 0 && Math.Abs(Projectile.velocity.Y) > 3)
            {
		        player.velocity.Y *= -1;
            }
		}
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            Player player = Main.player[Projectile.owner];
            double speed = player.velocity.Length();
            damage = (int)(damage * speed * speed / 30);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.velocity.X * player.velocity.X > 0 && Math.Abs(Projectile.velocity.X) > 3)
            {
                player.velocity.X *= -1;
            }
            if (Projectile.velocity.Y * player.velocity.Y > 0 && Math.Abs(Projectile.velocity.Y) > 3)
            {
                player.velocity.Y *= -1;
            }
        }

    }
}