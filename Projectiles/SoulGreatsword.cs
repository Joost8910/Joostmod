using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class SoulGreatsword : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul Greatsword");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
		public override void SetDefaults()
		{
			projectile.width = 160;
			projectile.height = 160;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 104;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.ownerHitCheck = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
            projectile.magic = true;
            projectile.extraUpdates = 1;
        }
        public override void AI()
        {
	        Player player = Main.player[projectile.owner];
            Vector2 center = player.RotatedRelativePoint(player.position + new Vector2(player.width / 2, 20), true);
            projectile.velocity = Vector2.Zero;
            projectile.direction = player.direction * (int)player.gravDir;
            float speed = player.meleeSpeed / 2;
            if (player.inventory[player.selectedItem].shoot == projectile.type)
            {
                projectile.scale = player.inventory[player.selectedItem].scale;
                speed = ((50f / player.inventory[player.selectedItem].useTime) / player.meleeSpeed) / 2;
                projectile.width = (int)(160 * projectile.scale);
                projectile.height = (int)(160 * projectile.scale);
                projectile.netUpdate = true;
            }
            bool channeling = player.channel && !player.noItems && !player.CCed;
            if (channeling && Main.myPlayer == projectile.owner)
            {
                Vector2 vector13 = Main.MouseWorld - center;
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
            player.ChangeDir(projectile.direction * (int)player.gravDir);
            projectile.spriteDirection = projectile.direction;
            if (projectile.localAI[1] > 0 && projectile.ai[1] > 0 && !player.mount.Active)
            {
                projectile.localNPCHitCooldown = (int)(40f * (1f - speed));
                player.fullRotationOrigin = player.Center - player.position;
                if (projectile.ai[1] < 180)
                {
                    player.fullRotation = (float)(projectile.ai[1] * Math.PI / 180 * player.direction * (int)player.gravDir);
                }
                else
                {
                    player.fullRotation = (float)(90 + (player.direction * (int)player.gravDir < 0 ? 90 : 0) + ((20 - projectile.timeLeft) * 25) * Math.PI / 180 * player.direction * (int)player.gravDir);
                }
            }
            double rad = (player.fullRotation - 1.83f) + (projectile.ai[1] * 0.0174f * projectile.direction);
            if (player.gravDir < 0)
            {
                rad += 3.14f;
            }
            projectile.rotation = (float)rad;
            if (projectile.direction == -1)
            {
                rad -= 1.045;
                projectile.rotation = (float)rad - 1.57f;
            }
            double dist = -110 * projectile.scale * projectile.direction;
            projectile.position.X = center.X + (0 * player.direction) - (int)(Math.Cos(rad - 0.785f) * dist) - (projectile.width / 2);
            projectile.position.Y = center.Y + (0) - (int)(Math.Sin(rad - 0.785f) * dist) - (projectile.height / 2);
            projectile.ai[0] += speed;
            if (projectile.ai[0] < 30)
            {
                projectile.alpha -= (int)(7 * speed);
            }
            if (projectile.ai[0] > 30 && projectile.soundDelay >= 0)
            {
                projectile.soundDelay = -60;
                Main.PlaySound(25, projectile.Center);
            }
            if (channeling && projectile.ai[0] > 30)
            {
                projectile.ai[0] = 40;
            }
            if (projectile.ai[0] <= 42)
            {
                projectile.timeLeft = 22;
            }
            if (projectile.ai[0] > 42 && projectile.localAI[0] <= 0)
            {
                if (player.velocity.Y != 0 && player.velocity.X * player.direction > 0)
                {
                    projectile.localAI[1] = 1;
                }
                projectile.localAI[0] = 1;
                Main.PlaySound(42, projectile.Center, 186);
            }
            if (projectile.timeLeft <= 20)
            {
                if (projectile.ai[1] < 180)
                {
                    projectile.timeLeft = 20;
                    projectile.ai[1] += 15 * speed;
                }
                else
                {
                    projectile.alpha += 16;
                    if (projectile.timeLeft <= 1)
                    {
                        player.fullRotation = 0;
                    }
                }
            }
            player.heldProj = projectile.whoAmI;
            player.itemTime = (int)((50f / speed) - ((projectile.ai[1] / 15f) * 4f / speed));
            player.itemAnimation = (int)((50f / speed) - ((projectile.ai[1] / 15f) * 4f / speed));
            if (player.itemTime < 1)
            {
                player.itemTime = 1;
            }
            if (player.itemAnimation < 1)
            {
                player.itemAnimation = 1;
            }
            player.ChangeDir(projectile.direction * (int)player.gravDir);
            /*
            float rot = projectile.rotation - 1.57f + (0.785f * player.direction * (int)player.gravDir);
            Vector2 unit = rot.ToRotationVector2();
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            for (int i = 0; i < 7; i++)
            {
                Dust.NewDustPerfect(vector + unit * i * 32, 20);
            }*/
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projectile.timeLeft <= 20 && (projectile.timeLeft > 4 || projectile.localAI[1] > 0))
            {
                Player player = Main.player[projectile.owner];
                float rot = projectile.rotation - 1.57f + (0.785f * player.direction * (int)player.gravDir);
                Vector2 unit = rot.ToRotationVector2();
                Vector2 vector = player.RotatedRelativePoint(player.position + new Vector2(player.width / 2, 20), true);
                float point = 0f;
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), vector, vector + unit * 224 * projectile.scale, 52 * projectile.scale, ref point))
                {
                    return true;
                }
            }
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            if (projectile.timeLeft <= 20 && projectile.timeLeft > 10)
            {
                return base.CanHitPvp(target);
            }
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.timeLeft <= 20 && projectile.timeLeft > 10)
            {
                return base.CanHitNPC(target);
            }
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
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
            Color color = Color.White * (1f - (projectile.alpha / 255f));
            if (projectile.ai[1] > 0)
            {
                for (int k = 0; k < projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + new Vector2(projectile.width / 2, projectile.height / 2);
                    Color color2 = color * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height));
                    spriteBatch.Draw(tex, drawPos, rect, color2, projectile.oldRot[k], drawOrigin, projectile.scale, effects, 0f);
                }
            }
            //color.A = (byte)projectile.alpha;
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, projectile.rotation, drawOrigin, projectile.scale, effects, 0f);
            return false;
        }
    }
}
