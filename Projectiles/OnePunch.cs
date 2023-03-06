using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class OnePunch : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("ONE PAAAUUUUUWWWWNNNCCHH");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.ai[0] < 1)
            {
                Projectile.ai[0] += 0.05f / player.GetAttackSpeed(DamageClass.Melee);
                if (Projectile.ai[0] > 1)
                {
                    Projectile.ai[0] = 1;
                }
            }
            Projectile.scale = Projectile.ai[0];
            Projectile.width = (int)((float)28 * Projectile.scale);
            Projectile.height = (int)((float)28 * Projectile.scale);
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            if (!player.noItems && !player.CCed)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    float scaleFactor = 1f;
                    if (player.inventory[player.selectedItem].shoot == Projectile.type)
                    {
                        scaleFactor = player.inventory[player.selectedItem].shootSpeed * Projectile.scale * ((Projectile.ai[1] / 2) + 0.4f);
                    }
                    Vector2 dir = Main.MouseWorld - vector;
                    dir.Normalize();
                    if (dir.HasNaNs())
                    {
                        dir = Vector2.UnitX * (float)player.direction;
                    }
                    dir *= scaleFactor;
                    if (dir.X != Projectile.velocity.X || dir.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    Projectile.velocity = dir;
                }
            }
            else
            {
                Projectile.Kill();
            }
        
            if (player.channel)
            {
                if (Projectile.ai[0] >= 1 && Projectile.soundDelay >= 0)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        int dust = Dust.NewDust(player.position, player.width, player.height, 90);
                        Main.dust[dust].noGravity = true;
                    }
                    SoundEngine.PlaySound(SoundID.Trackable, Projectile.position);
                    Projectile.soundDelay = -1;
                }
            }
            else
            {
                if (Projectile.ai[1] <= 0)
                {
                    SoundEngine.PlaySound(SoundID.Trackable, Projectile.position);
                }
                Projectile.ai[1] += 0.15f;
                if (player.velocity.X * Projectile.velocity.X <= 0)
                {
                    player.velocity.X = Projectile.velocity.X * Projectile.ai[0] * 2f;
                }
                if (player.velocity.Y * Projectile.velocity.Y <= 0)
                {
                    player.velocity.Y = Projectile.velocity.Y * Projectile.ai[0] * 2f;
                }
                player.velocity += Projectile.velocity * Projectile.ai[0] * 0.2f;
                if (player.velocity.Y > 10 || (player.gravDir == -1 && player.velocity.Y < -10))
                {
                    player.portalPhysicsFlag = true;
                }
                else
                {
                    player.portalPhysicsFlag = false;
                }
            }
            if (Projectile.ai[1] > 2f)
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
            if (Projectile.scale >= 1)
            {
                Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
                for (int k = 0; k < Projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                    Color color2 = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, (TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]) * Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]));
                    sb.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, rect, color2, Projectile.oldRot[k], drawOrigin, Projectile.scale, effects, 0f);
                }
            }
            Color color = Lighting.GetColor((int)(Projectile.Center.X / 16), (int)(Projectile.Center.Y / 16.0));
			sb.Draw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, new Vector2(tex.Width/2, tex.Height/2), Projectile.scale, effects, 0f);
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
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
            knockback = knockback *Projectile.scale;
            damage += (int)(target.life * Projectile.scale) + target.defense;
            if (Projectile.scale >= 1)
            {
                SoundEngine.PlaySound(SoundID.Item100, Projectile.position);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.scale >= 1)
            {
                SoundEngine.PlaySound(SoundID.Item100, player.position);
                if (target.type != NPCID.TargetDummy)
                {
                    target.velocity = Projectile.velocity / 5 * knockback;
                }
            }
            else
            {
                target.velocity += Projectile.velocity / 10 * knockback * target.knockBackResist * Projectile.scale;
            }
            for (int i = 0; i < (int)(Projectile.scale * 40); i++)
            {
                Dust.NewDust(target.position, target.width, target.height, 5, target.velocity.X, target.velocity.Y);
            }
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage += (int)(target.statLife * Projectile.scale) + target.statDefense;
            if (Projectile.scale >= 1)
            {
                SoundEngine.PlaySound(SoundID.Item100, Projectile.position);
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.scale >= 1)
            {
                SoundEngine.PlaySound(SoundID.Item100, player.position);
                target.velocity = Projectile.velocity / 5 * Projectile.knockBack;
            }
            else if (!target.noKnockback)
            {
                target.velocity += Projectile.velocity / 10 * Projectile.knockBack * Projectile.scale;
            }
            for (int i = 0; i < (int)(Projectile.scale * Projectile.scale * 40); i++)
            {
                Dust.NewDust(target.position, target.width, target.height, 5, target.velocity.X, target.velocity.Y);
            }
        }
    }
}