using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class Stonefist : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stone Fist");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.width = 64;
            projectile.height = 64;
            projectile.friendly = true;
            projectile.penetrate = 3;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.ownerHitCheck = true;
            projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = -1;
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            if (projectile.ai[0] < 1)
            {
                projectile.ai[0] += 0.02f * (55f / player.inventory[player.selectedItem].useTime) / player.meleeSpeed;
                if (projectile.ai[0] > 1)
                {
                    projectile.ai[0] = 1;
                }
            }
            projectile.scale = projectile.ai[0] * player.inventory[player.selectedItem].scale;
            projectile.width = (int)((float)64 * projectile.scale);
            projectile.height = (int)((float)64 * projectile.scale);
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            if (!player.noItems && !player.CCed)
            {
                if (Main.myPlayer == projectile.owner)
                {
                    float scaleFactor6 = 1f;
                    if (player.inventory[player.selectedItem].shoot == projectile.type)
                    {
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * projectile.scale * (projectile.ai[1] + 0.75f);
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
        
            if (player.channel)
            {
                if (projectile.ai[0] >= 1 && projectile.soundDelay >= 0)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        int dust = Dust.NewDust(player.position, player.width, player.height, 263);
                        Main.dust[dust].noGravity = true;
                    }
                    Main.PlaySound(42, (int)projectile.position.X, (int)projectile.position.Y, 212);
                    projectile.soundDelay = -1;
                }
            }
            else
            {
                if (projectile.soundDelay <= 0 && projectile.soundDelay > -10)
                {
                    Main.PlaySound(42, (int)projectile.position.X, (int)projectile.position.Y, 216);
                    projectile.soundDelay = -10;
                }
                projectile.ai[1] += 0.2f * (55f / player.inventory[player.selectedItem].useTime) / player.meleeSpeed;
                Vector2 dir = projectile.velocity;
                dir.Normalize();
                dir = dir * 10f * (projectile.ai[1] + 0.75f) * (55f / player.inventory[player.selectedItem].useTime) / player.meleeSpeed;
                if (projectile.localAI[1] <= 0)
                {
                    //player.velocity += dir * projectile.ai[0] * projectile.ai[0] * 0.05f;
                    if (player.velocity.X * dir.X <= 0)
                    {
                        player.velocity.X += dir.X * projectile.ai[0] * 0.05f;
                    }
                    if (player.velocity.Y * dir.Y <= 0)
                    {
                        player.velocity.Y += dir.Y * projectile.ai[0] * 0.05f;
                    }
                    if (Math.Abs(player.velocity.X + dir.X * projectile.ai[0] * 0.05f) < 15)
                    {
                        player.velocity.X += dir.X * projectile.ai[0] * 0.05f;
                    }
                    if (Math.Abs(player.velocity.Y + dir.Y * projectile.ai[0] * 0.05f) < 15)
                    {
                        player.velocity.Y += dir.Y * projectile.ai[0] * 0.05f;
                    }
                }
            }
            if (projectile.ai[1] > 2)
            {
                projectile.Kill();
            }
            projectile.position = (projectile.velocity + vector) - projectile.Size / 2f;
            projectile.rotation = projectile.velocity.ToRotation() + 1.57f + (projectile.direction * 0.785f);
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            //player.heldProj = projectile.whoAmI;
            player.itemTime = 10;
            player.itemAnimation = 10;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D tex = Main.projectileTexture[projectile.type];
            SpriteEffects effects = SpriteEffects.None;
			if (projectile.spriteDirection == -1)
			{
				effects = SpriteEffects.FlipHorizontally;
            }
            if (projectile.ai[0] >= 1)
            {
                Vector2 drawOrigin = new Vector2((tex.Width / 2), (tex.Height / 2));
                for (int k = 0; k < projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + new Vector2(projectile.width / 2, projectile.height / 2);
                    Color color2 = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[projectile.type]) * projectile.frame, tex.Width, tex.Height / Main.projFrames[projectile.type]));
                    spriteBatch.Draw(tex, drawPos, rect, color2, projectile.oldRot[k], drawOrigin, projectile.scale, effects, 0f);
                }
            }
            Color color = Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16.0));
			spriteBatch.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, projectile.rotation, new Vector2(tex.Width/2, tex.Height/2), projectile.scale, effects, 0f);
			return false;
		}
        public override bool? CanHitNPC(NPC target)
		{
			return !target.friendly && projectile.ai[1] > 0;
		}
        public override bool CanHitPvp(Player target)
        {
            if (projectile.ai[1] > 0)
            {
                return base.CanHitPvp(target);
            }
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)
		{
            Player player = Main.player[projectile.owner];
            if (projectile.velocity.Y * player.gravDir > 0 && player.velocity.Y * player.gravDir > 0 && Math.Abs(projectile.velocity.X) < 6*projectile.scale)
            {
		        player.velocity.Y = Math.Abs(player.velocity.Y) < 15*projectile.ai[0] ? -15 * projectile.ai[0] * player.gravDir : -player.velocity.Y;
                projectile.localAI[1] = 1;
            }
            if (projectile.ai[0] > 0.7f)
            {
			    Main.PlaySound(42, (int)projectile.position.X, (int)projectile.position.Y, 208);
            }
            for (int i = 0; i < (int)(projectile.scale*projectile.scale*40); i++)
            {
                Dust.NewDust(target.position, target.width, target.height, 1);
            }
			target.velocity += projectile.velocity/10 * knockBack * target.knockBackResist * projectile.ai[0] * projectile.ai[0];
		}
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
            knockback = knockback * projectile.ai[0] * projectile.ai[0];
			damage = (int)(damage * projectile.ai[0]);
		}
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (projectile.velocity.Y * player.gravDir > 0 && player.velocity.Y * player.gravDir > 0 && Math.Abs(projectile.velocity.X) < 6 * projectile.scale)
            {
                player.velocity.Y = Math.Abs(player.velocity.Y) < 15 * projectile.ai[0] ? -15 * projectile.ai[0] * player.gravDir : -player.velocity.Y;
                projectile.localAI[1] = 1;
            }
            if (projectile.ai[0] > 0.7f)
            {
                Main.PlaySound(42, (int)projectile.position.X, (int)projectile.position.Y, 208);
            }
            for (int i = 0; i < (int)(projectile.scale * projectile.scale * 40); i++)
            {
                Dust.NewDust(target.position, target.width, target.height, 1);
            }
            if (!target.noKnockback)
            {
                target.velocity += projectile.velocity / 10 * projectile.knockBack * projectile.ai[0] * projectile.ai[0];
            }
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage = (int)(damage * projectile.ai[0]);
        }
    }
}