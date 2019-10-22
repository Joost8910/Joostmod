using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class OnePunch : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("ONE PAAAUUUUUWWWWNNNCCHH");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.friendly = true;
            projectile.penetrate = -1;
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
                projectile.ai[0] += 0.05f / player.meleeSpeed;
                if (projectile.ai[0] > 1)
                {
                    projectile.ai[0] = 1;
                }
            }
            projectile.scale = projectile.ai[0];
            projectile.width = (int)((float)28 * projectile.scale);
            projectile.height = (int)((float)28 * projectile.scale);
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            if (!player.noItems && !player.CCed)
            {
                if (Main.myPlayer == projectile.owner)
                {
                    float scaleFactor = 1f;
                    if (player.inventory[player.selectedItem].shoot == projectile.type)
                    {
                        scaleFactor = player.inventory[player.selectedItem].shootSpeed * projectile.scale * ((projectile.ai[1] / 2) + 0.4f);
                    }
                    Vector2 dir = Main.MouseWorld - vector;
                    dir.Normalize();
                    if (dir.HasNaNs())
                    {
                        dir = Vector2.UnitX * (float)player.direction;
                    }
                    dir *= scaleFactor;
                    if (dir.X != projectile.velocity.X || dir.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.velocity = dir;
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
                        int dust = Dust.NewDust(player.position, player.width, player.height, 90);
                        Main.dust[dust].noGravity = true;
                    }
                    Main.PlaySound(42, (int)projectile.position.X, (int)projectile.position.Y, 212);
                    projectile.soundDelay = -1;
                }
            }
            else
            {
                if (projectile.ai[1] <= 0)
                {
                    Main.PlaySound(42, (int)projectile.position.X, (int)projectile.position.Y, 216);
                }
                projectile.ai[1] += 0.15f;
                if (player.velocity.X * projectile.velocity.X <= 0)
                {
                    player.velocity.X = projectile.velocity.X * projectile.ai[0] * 2f;
                }
                if (player.velocity.Y * projectile.velocity.Y <= 0)
                {
                    player.velocity.Y = projectile.velocity.Y * projectile.ai[0] * 2f;
                }
                player.velocity += projectile.velocity * projectile.ai[0] * 0.2f;
                if (player.velocity.Y > 10 || (player.gravDir == -1 && player.velocity.Y < -10))
                {
                    player.portalPhysicsFlag = true;
                }
                else
                {
                    player.portalPhysicsFlag = false;
                }
            }
            if (projectile.ai[1] > 2f)
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
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
		{
			Texture2D tex = Main.projectileTexture[projectile.type];
            SpriteEffects effects = SpriteEffects.None;
			if (projectile.spriteDirection == -1)
			{
				effects = SpriteEffects.FlipHorizontally;
            }
            if (projectile.scale >= 1)
            {
                Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
                for (int k = 0; k < projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                    Color color2 = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, (Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]) * projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]));
                    sb.Draw(Main.projectileTexture[projectile.type], drawPos, rect, color2, projectile.oldRot[k], drawOrigin, projectile.scale, effects, 0f);
                }
            }
            Color color = Lighting.GetColor((int)(projectile.Center.X / 16), (int)(projectile.Center.Y / 16.0));
			sb.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, projectile.rotation, new Vector2(tex.Width/2, tex.Height/2), projectile.scale, effects, 0f);
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
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
            knockback = knockback *projectile.scale;
            damage += (int)(target.life * projectile.scale) + target.defense;
            if (projectile.scale >= 1)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 100);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (projectile.scale >= 1)
            {
                Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 100);
                if (target.type != NPCID.TargetDummy)
                {
                    target.velocity = projectile.velocity / 5 * knockBack;
                }
            }
            else
            {
                target.velocity += projectile.velocity / 10 * knockBack * target.knockBackResist * projectile.scale;
            }
            for (int i = 0; i < (int)(projectile.scale * 40); i++)
            {
                Dust.NewDust(target.position, target.width, target.height, 5, target.velocity.X, target.velocity.Y);
            }
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage += (int)(target.statLife * projectile.scale) + target.statDefense;
            if (projectile.scale >= 1)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 100);
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (projectile.scale >= 1)
            {
                Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 100);
                target.velocity = projectile.velocity / 5 * projectile.knockBack;
            }
            else if (!target.noKnockback)
            {
                target.velocity += projectile.velocity / 10 * projectile.knockBack * projectile.scale;
            }
            for (int i = 0; i < (int)(projectile.scale * projectile.scale * 40); i++)
            {
                Dust.NewDust(target.position, target.width, target.height, 5, target.velocity.X, target.velocity.Y);
            }
        }
    }
}