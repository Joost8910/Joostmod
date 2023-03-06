using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class AwokenDreamNail2 : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Awoken Dream Nail");
            Main.projFrames[Projectile.type] = 8;
		}
        public override void SetDefaults()
        {
            Projectile.width = 122;
            Projectile.height = 122;
            //projectile.scale = 2.1f;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
        }
        public override bool? CanHitNPC(NPC target)
		{
            Player player = Main.player[Projectile.owner];
			return !target.friendly && player.itemAnimation > 1;
		}
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            Projectile.ai[0]++;
            bool channeling = player.itemAnimation > 1 && !player.noItems && !player.CCed;
            if (channeling)
            {
                if (player.itemAnimation <= 1)
                {
                    if (Main.myPlayer == Projectile.owner)
                    {
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
                if (Projectile.ai[0] < 20)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 71, Projectile.velocity.X / 3, Projectile.velocity.Y / 3, 100, default(Color), (0.7f + (Main.rand.Next(5) / 10)));
                }
            }
            else
            {
                Projectile.Kill();
            }
            if (player.itemAnimation > (int)(7 * (float)player.itemAnimationMax / 8))
            {
                Projectile.frame = 0;
            }
            else if (player.itemAnimation > (int)(6 * (float)player.itemAnimationMax / 8))
            {
                Projectile.frame = 1;
            }
            else if (player.itemAnimation > (int)(5 * (float)player.itemAnimationMax / 8))
            {
                Projectile.frame = 2;
            }
            else if (player.itemAnimation > (int)(4 * (float)player.itemAnimationMax / 8))
            {
                Projectile.frame = 3;
            }
            else if (player.itemAnimation > (int)(3 * (float)player.itemAnimationMax / 8))
            {
                Projectile.frame = 4;
            }
            else if (player.itemAnimation > (int)(2 * (float)player.itemAnimationMax / 8))
            {
                Projectile.frame = 5;
            }
            else if (player.itemAnimation > (int)((float)player.itemAnimationMax / 8))
            {
                Projectile.frame = 6;
            }
            else
            {
                Projectile.frame = 7;
            }
            Projectile.position = (Projectile.velocity + vector) - Projectile.Size / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.direction == -1 ? 3.14f : 0);
            Projectile.spriteDirection = Projectile.direction;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            //player.itemTime = 10;
            //player.itemAnimation = 10;
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * (float)Projectile.direction), (double)(Projectile.velocity.X * (float)Projectile.direction));
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			damage += (int)(target.defense / 2);
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage += (int)(target.statDefense / 2);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = Main.player[Projectile.owner];
            if (Projectile.velocity.Y * player.gravDir > 0 && player.velocity.Y * player.gravDir > 0 && Math.Abs(Projectile.velocity.X) < 7)
            {
		        player.velocity.Y = Math.Abs(player.velocity.Y) < 8 ? -8 * player.gravDir : -player.velocity.Y;
            }
		}
    }
}