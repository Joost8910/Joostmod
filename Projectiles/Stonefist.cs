using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class Stonefist : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stone Fist");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.friendly = true;
            Projectile.penetrate = 3;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((short)Projectile.localAI[1]);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Projectile.localAI[1] = reader.ReadInt16();
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.ai[0] < 1)
            {
                Projectile.ai[0] += 0.02f * (55f / player.inventory[player.selectedItem].useTime) / player.GetAttackSpeed(DamageClass.Melee);
                if (Projectile.ai[0] > 1)
                {
                    Projectile.ai[0] = 1;
                }
            }
            Projectile.scale = Projectile.ai[0] * player.inventory[player.selectedItem].scale;
            Projectile.width = (int)((float)64 * Projectile.scale);
            Projectile.height = (int)((float)64 * Projectile.scale);
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            if (!player.noItems && !player.CCed)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    float scaleFactor6 = 1f;
                    if (player.inventory[player.selectedItem].shoot == Projectile.type)
                    {
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * Projectile.scale * (Projectile.ai[1] + 0.75f);
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
        
            if (player.channel)
            {
                if (Projectile.ai[0] >= 1)
                {
                    if (Projectile.soundDelay >= 0)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            int dust = Dust.NewDust(player.position, player.width, player.height, 263);
                            Main.dust[dust].noGravity = true;
                        }
                        SoundEngine.PlaySound(SoundID.Trackable, Projectile.position);
                        Projectile.soundDelay = -1;
                    }

                    if (player.controlUseTile)
                    {
                        Projectile.NewProjectile(vector, Projectile.velocity, Mod.Find<ModProjectile>("Stonefist2").Type, Projectile.damage / 2, Projectile.knockBack / 2, Projectile.owner, 0, 1);
                        Projectile.Kill();
                    }
                }
            }
            else
            {
                if (Projectile.soundDelay <= 0 && Projectile.soundDelay > -10)
                {
                    SoundEngine.PlaySound(SoundID.Trackable, Projectile.position);
                    Projectile.soundDelay = -10;
                }
                Projectile.ai[1] += 0.2f * (55f / player.inventory[player.selectedItem].useTime) / player.GetAttackSpeed(DamageClass.Melee);
                Vector2 dir = Projectile.velocity;
                dir.Normalize();
                dir = dir * 10f * (Projectile.ai[1] + 0.75f) * (55f / player.inventory[player.selectedItem].useTime) / player.GetAttackSpeed(DamageClass.Melee);
                if (Projectile.localAI[1] <= 0)
                {
                    //player.velocity += dir * projectile.ai[0] * projectile.ai[0] * 0.05f;
                    if (player.velocity.X * dir.X <= 0)
                    {
                        player.velocity.X += dir.X * Projectile.ai[0] * 0.05f;
                    }
                    if (player.velocity.Y * dir.Y <= 0)
                    {
                        player.velocity.Y += dir.Y * Projectile.ai[0] * 0.05f;
                    }
                    if (Math.Abs(player.velocity.X + dir.X * Projectile.ai[0] * 0.05f) < 15)
                    {
                        player.velocity.X += dir.X * Projectile.ai[0] * 0.05f;
                    }
                    if (Math.Abs(player.velocity.Y + dir.Y * Projectile.ai[0] * 0.05f) < 15)
                    {
                        player.velocity.Y += dir.Y * Projectile.ai[0] * 0.05f;
                    }
                }
            }
            if (Projectile.ai[1] > 2)
            {
                Projectile.Kill();
            }
            Projectile.position = (Projectile.velocity + vector) - Projectile.Size / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57f + (Projectile.direction * 0.785f);
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;
            player.ChangeDir(Projectile.direction);
            //player.heldProj = projectile.whoAmI;
            player.itemTime = 10;
            player.itemAnimation = 10;
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * (float)Projectile.direction), (double)(Projectile.velocity.X * (float)Projectile.direction));
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
		{
			Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
			if (Projectile.spriteDirection == -1)
			{
				effects = SpriteEffects.FlipHorizontally;
            }
            if (Projectile.ai[0] >= 1)
            {
                Vector2 drawOrigin = new Vector2((tex.Width / 2), (tex.Height / 2));
                for (int k = 0; k < Projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + new Vector2(Projectile.width / 2, Projectile.height / 2);
                    Color color2 = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[Projectile.type]) * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type]));
                    Main.EntitySpriteDraw(tex, drawPos, rect, color2, Projectile.oldRot[k], drawOrigin, Projectile.scale, effects, 0);
                }
            }
            Color color = Lighting.GetColor((int)(Projectile.Center.X / 16), (int)(Projectile.Center.Y / 16.0));
			Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, new Vector2(tex.Width/2, tex.Height/2), Projectile.scale, effects, 0f);
			return false;
		}
        public override bool? CanHitNPC(NPC target)
		{
			return !target.friendly && Projectile.ai[1] > 0;
		}
        public override bool CanHitPvp(Player target)
        {
            if (Projectile.ai[1] > 0)
            {
                return base.CanHitPvp(target);
            }
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
            Player player = Main.player[Projectile.owner];
            if (Projectile.velocity.Y * player.gravDir > 0 && player.velocity.Y * player.gravDir > 0 && Math.Abs(Projectile.velocity.X) < 6*Projectile.scale)
            {
		        player.velocity.Y = Math.Abs(player.velocity.Y) < 15*Projectile.ai[0] ? -15 * Projectile.ai[0] * player.gravDir : -player.velocity.Y;
                Projectile.localAI[1] = 1;
            }
            if (Projectile.ai[0] > 0.7f)
            {
			    SoundEngine.PlaySound(SoundID.Trackable, Projectile.position);
            }
            if (target.knockBackResist > 0 && Projectile.ai[0] >= 1)
            {
                Projectile.NewProjectile(target.Center, target.velocity, Mod.Find<ModProjectile>("GrabThrow").Type, Projectile.damage / 2, Projectile.knockBack, Projectile.owner, target.whoAmI);
            }
            for (int i = 0; i < (int)(Projectile.scale*Projectile.scale*40); i++)
            {
                Dust.NewDust(target.position, target.width, target.height, 1);
            }
			target.velocity += Projectile.velocity/10 * knockback * target.knockBackResist * Projectile.ai[0] * Projectile.ai[0];
		}
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
            knockback = knockback * Projectile.ai[0] * Projectile.ai[0];
			damage = (int)(damage * Projectile.ai[0]);
		}
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.velocity.Y * player.gravDir > 0 && player.velocity.Y * player.gravDir > 0 && Math.Abs(Projectile.velocity.X) < 6 * Projectile.scale)
            {
                player.velocity.Y = Math.Abs(player.velocity.Y) < 15 * Projectile.ai[0] ? -15 * Projectile.ai[0] * player.gravDir : -player.velocity.Y;
                Projectile.localAI[1] = 1;
            }
            if (Projectile.ai[0] > 0.7f)
            {
                SoundEngine.PlaySound(SoundID.Trackable, Projectile.position);
            }
            if (Projectile.ai[0] >= 1)
            {
                Projectile.NewProjectile(target.Center, target.velocity, Mod.Find<ModProjectile>("GrabThrow").Type, Projectile.damage / 2, Projectile.knockBack, Projectile.owner, -1, target.whoAmI);
            }
            for (int i = 0; i < (int)(Projectile.scale * Projectile.scale * 40); i++)
            {
                Dust.NewDust(target.position, target.width, target.height, 1);
            }
            if (!target.noKnockback)
            {
                target.velocity += Projectile.velocity / 10 * Projectile.knockBack * Projectile.ai[0] * Projectile.ai[0];

                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)JoostModMessageType.Playerpos);
                packet.Write((byte)target.whoAmI);
                packet.WriteVector2(target.position);
                packet.WriteVector2(target.velocity + Projectile.velocity / 10 * Projectile.knockBack * Projectile.ai[0] * Projectile.ai[0]);
                ModPacket netMessage = packet;
                netMessage.Send(-1, -1);
            }
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage = (int)(damage * Projectile.ai[0]);
        }
    }
}