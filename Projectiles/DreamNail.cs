using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class DreamNail : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dream Nail");
            Main.projFrames[Projectile.type] = 9;
		}
        public override void SetDefaults()
        {
            Projectile.width = 110;
            Projectile.height = 110;
            //projectile.scale = 1.9f;
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
            bool channeling = (player.itemAnimation > 1 || player.channel) && !player.noItems && !player.CCed;
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
                if (Projectile.ai[0] >= 24 && Projectile.ai[0] < 75)
                {
                    int dust = Dust.NewDust(player.position, player.width, player.height, 71);
                    Main.dust[dust].noGravity = true;
                }
                if (Projectile.ai[0] == 26)
                {
                    SoundEngine.PlaySound(SoundLoader.customSoundType, (int)Projectile.Center.X, (int)Projectile.Center.Y, Mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/hero_dream_nail_charge_start"));
                }
                if (Projectile.ai[0] == 75)
                {
                    SoundEngine.PlaySound(SoundLoader.customSoundType, (int)Projectile.Center.X, (int)Projectile.Center.Y, Mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/hero_dream_nail_charge_complete"));
                }
                if (Projectile.ai[0] > 75)
                {
                    int dust2 = Dust.NewDust(new Vector2(player.Center.X - 4, player.Center.Y + player.height / 2 * player.gravDir), 1, 1, 71, 5, -3 * player.gravDir, 0, default(Color), 1);
                    Main.dust[dust2].noGravity = true;
                    int dust3 = Dust.NewDust(new Vector2(player.Center.X - 4, player.Center.Y + player.height / 2 * player.gravDir), 1, 1, 71, -5, -3 * player.gravDir, 0, default(Color), 1);
                    Main.dust[dust3].noGravity = true;
                }
            }
            else
            {
                Projectile.Kill();
            }
			
			if (player.itemAnimation > (int)(8*(float)player.itemAnimationMax/9))
			{
				Projectile.frame = 0;
			}
            else if (player.itemAnimation > (int)(7*(float)player.itemAnimationMax/9))
			{
				Projectile.frame = 1;
			}
            else if (player.itemAnimation > (int)(6*(float)player.itemAnimationMax/9))
			{
				Projectile.frame = 2;
			}
            else if (player.itemAnimation > (int)(5*(float)player.itemAnimationMax/9))
			{
				Projectile.frame = 3;
			}
            else if (player.itemAnimation > (int)(4*(float)player.itemAnimationMax/9))
			{
				Projectile.frame = 4;
			}
            else if (player.itemAnimation > (int)(3*(float)player.itemAnimationMax/9))
			{
				Projectile.frame = 5;
			}
            else if (player.itemAnimation > (int)(2*(float)player.itemAnimationMax/9))
			{
				Projectile.frame = 6;
			}
            else if (player.itemAnimation > (int)((float)player.itemAnimationMax/9))
			{
				Projectile.frame = 7;
			}
            else
			{
				Projectile.frame = 8;
                player.itemTime = 1;
                player.itemAnimation = 1;
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
			damage += (int)(target.defense / 4);
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage += (int)(target.statDefense / 4);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = Main.player[Projectile.owner];
            if (Projectile.velocity.Y * player.gravDir > 0 && player.velocity.Y * player.gravDir > 0 && Math.Abs(Projectile.velocity.X) < 6)
            {
		        player.velocity.Y = Math.Abs(player.velocity.Y) < 7 ? -7 * player.gravDir : -player.velocity.Y;
            }
		}
        public override void Kill(int timeLeft)
        {
            if(Projectile.ai[0] > 75)
            {
                Player player = Main.player[Projectile.owner];
                Vector2 pos = player.RotatedRelativePoint(player.MountedCenter, true);
                if (player.controlUp || player.controlDown)
                {
                    SoundEngine.PlaySound(SoundLoader.customSoundType, (int)Projectile.Center.X, (int)Projectile.Center.Y, Mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/hero_nail_art_cyclone_slash_1"));
                    Projectile.NewProjectile(pos.X, pos.Y, player.direction, 0, Mod.Find<ModProjectile>("DreamSpinSlash").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
                }
                else if(Math.Abs(player.velocity.X) >= 6 && player.velocity.X * Projectile.velocity.X > 0 && Math.Abs(Projectile.velocity.Y) < 6)
                {
                    SoundEngine.PlaySound(SoundLoader.customSoundType, (int)Projectile.Center.X, (int)Projectile.Center.Y, Mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/hero_dream_nail_slash"));
                    Projectile.NewProjectile(pos.X, pos.Y, player.direction * 60, 0, Mod.Find<ModProjectile>("DreamDashSlash").Type, Projectile.damage * 9, Projectile.knockBack * 7, Projectile.owner);
                    player.velocity.X += 7 * player.direction;
                }
                else
                {
                    SoundEngine.PlaySound(SoundLoader.customSoundType, (int)Projectile.Center.X, (int)Projectile.Center.Y, Mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/hero_dream_nail_slash"));
                    Projectile.NewProjectile(pos.X, pos.Y, Projectile.velocity.X * 2, Projectile.velocity.Y * 2, Mod.Find<ModProjectile>("DreamGreatSlash").Type, Projectile.damage * 9, Projectile.knockBack * 7, Projectile.owner);
                }
            }
            else
            {
                Player player = Main.player[Projectile.owner];
                Vector2 pos = player.RotatedRelativePoint(player.MountedCenter, true);
                SoundEngine.PlaySound(SoundID.Trackable, Projectile.position);
                Projectile.NewProjectile(pos.X, pos.Y, Projectile.velocity.X, Projectile.velocity.Y, Mod.Find<ModProjectile>("DreamNail2").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
                player.itemTime = player.itemAnimationMax;
                player.itemAnimation = player.itemAnimationMax;
                if (player.statLife >= player.statLifeMax2)
                {
                    Projectile.NewProjectile(pos.X, pos.Y, Projectile.velocity.X, Projectile.velocity.Y, Mod.Find<ModProjectile>("DreamBeam").Type, Projectile.damage / 2, 0, Projectile.owner);
                }
            }
        }
    }
}