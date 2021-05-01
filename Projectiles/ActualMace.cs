using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class ActualMace : ModProjectile
    {
        public override string Texture =>"JoostMod/Items/Weapons/ActualMace";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Actual Mace");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
		public override void SetDefaults()
		{
			projectile.width = 46;
			projectile.height = 46;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 124;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.ownerHitCheck = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
            projectile.extraUpdates = 1;
        }
        public override void AI()
        {
	        Player player = Main.player[projectile.owner];
            Vector2 center = player.RotatedRelativePoint(player.position + new Vector2(player.width / 2, 20), true);
            projectile.velocity.Y = 0;
            projectile.direction = player.direction * (int)player.gravDir;
            projectile.velocity.X = projectile.direction;
            float speed = player.meleeSpeed / 2;
            if (player.inventory[player.selectedItem].shoot == projectile.type)
            {
                projectile.scale = player.inventory[player.selectedItem].scale;
                speed = ((45f / player.inventory[player.selectedItem].useTime) / player.meleeSpeed) / 2;
                projectile.width = (int)(46 * projectile.scale);
                projectile.height = (int)(46 * projectile.scale);
                projectile.netUpdate = true;
            }
            bool channeling = player.channel && !player.noItems && !player.CCed && !player.dead;
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
            double rad = (player.fullRotation - 1.83f) + ((projectile.ai[1] - 15) * 0.0174f * projectile.direction);
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
            double dist = ((-20 * projectile.scale) - 10) * projectile.direction;
            projectile.position.X = center.X + (-2 * player.direction * projectile.scale) - (int)(Math.Cos(rad - 0.785f) * dist) - (projectile.width / 2);
            projectile.position.Y = center.Y + ((projectile.ai[1] / 30 - 3) * projectile.scale) - (int)(Math.Sin(rad - 0.785f) * dist) - (projectile.height / 2);
            if (projectile.ai[1] < 0)
            {
                projectile.position.Y += player.gravDir * ((projectile.ai[1] / projectile.scale * 0.15f) - 4);
            }
            if (projectile.ai[0] < 30)
            {
                projectile.ai[1] -= speed;
                projectile.localAI[0] += 0.02f * speed;
            }
            if (projectile.ai[0] > 30 && projectile.soundDelay >= 0 && channeling)
            {
                projectile.soundDelay = -60;
                Main.PlaySound(2, projectile.Center, 39);
            }
            if (channeling && projectile.ai[0] > 30)
            {
                projectile.ai[0] = 30;
            }
            if (!channeling && projectile.ai[0] < 30)
            {
                projectile.ai[0] = 30;
            }
            if (projectile.ai[0] <= 32)
            {
                projectile.ai[0] += speed;
                projectile.timeLeft = 122;
            }
            else if (projectile.soundDelay != -10)
            {
                projectile.soundDelay = -10;
                Main.PlaySound(42, (int)projectile.Center.X, (int)projectile.Center.Y, 214, 0.9f, 0.1f);
            }
            if (projectile.timeLeft <= 120)
            {
                if (projectile.ai[1] < 180)
                {
                    projectile.timeLeft = 70;
                    projectile.ai[1] += 10 * speed * (projectile.localAI[0] + 1);
                }
                if (projectile.timeLeft == 68 && projectile.ai[0] < 100)
                {
                    projectile.ai[0] = 100;
                    projectile.timeLeft = (int)(68 / (speed * 2));
                    if (player.velocity.Y == 0)
                    {
                        Main.PlaySound(42, (int)projectile.Center.X, (int)projectile.Center.Y, 210, 0.8f, 0.1f);
                    }
                }
                if (projectile.ai[1] > 180)
                {
                    projectile.ai[1] = 180;
                }
            }
            player.heldProj = projectile.whoAmI;
            if (projectile.ai[1] < 180)
            {
                player.itemTime = (int)((45f / (speed * 2)) - ((projectile.ai[1] / 15f) * 2 / speed));
                player.itemAnimation = (int)((45f / (speed * 2)) - ((projectile.ai[1] / 15f) * 2 / speed));
                if (player.itemTime < 2)
                {
                    player.itemTime = 2;
                }
                if (player.itemAnimation < 2)
                {
                    player.itemAnimation = 2;
                }
            }
            else
            {
                player.itemTime = 2;
                player.itemAnimation = 2;
            }
            player.ChangeDir(projectile.direction * (int)player.gravDir);
        }
        public override bool? CanCutTiles()
        {
            return (projectile.ai[0] > 32 && projectile.ai[0] < 100);
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Player player = Main.player[projectile.owner];
            if (projectile.ai[0] > 32 && (projectile.ai[0] < 100 || player.velocity.Y * player.gravDir > 3))
            {
                float rot = projectile.rotation - 1.57f + (0.785f * player.direction * (int)player.gravDir);
                Vector2 unit = rot.ToRotationVector2();
                Vector2 vector = player.RotatedRelativePoint(player.position + new Vector2(player.width / 2, 20), true);
                float point = 0f;
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), vector, vector + unit * 62 * projectile.scale, 14 * projectile.scale, ref point))
                {
                    return true;
                }
                if (player.velocity.Y * player.gravDir > 3 && projectile.ai[1] > 120 && Collision.CheckAABBvAABBCollision(player.Center - new Vector2((player.width / 2) + 2, 0), new Vector2(player.width + 4, ((player.height / 2) + 4) * player.gravDir), targetHitbox.TopLeft(), targetHitbox.Size()))
                {
                    player.velocity.Y = -4 * player.gravDir;
                    if (player.immuneTime < 4)
                    {
                        player.immune = true;
                        player.immuneNoBlink = true;
                        player.immuneTime = 4;
                    }
                    return true;
                }
            }
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = (int)(damage * (projectile.localAI[0] + 1));
            knockback = knockback * (projectile.localAI[0] + 1);
            if (target.velocity.Y == 0)
                hitDirection = target.Center.X < Main.player[projectile.owner].Center.X ? -1 : 1;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage = (int)(damage * (projectile.localAI[0] + 1));
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.localAI[0] > 0.55f)
            {
                target.AddBuff(BuffID.Ichor, 600);
            }
            Player player = Main.player[projectile.owner];
            if (target.knockBackResist > 0)
            {
                if (projectile.ai[0] >= 100 && projectile.ai[1] > 120)
                {
                    if (target.velocity.Y < 0 && projectile.localAI[0] > 0.55f)
                    {
                        Projectile.NewProjectile(target.Center, target.velocity, mod.ProjectileType("GrabThrow"), projectile.damage, projectile.knockBack, projectile.owner, target.whoAmI);
                    }
                    target.velocity.Y = (knockback + Math.Abs(player.velocity.Y)) * player.gravDir * target.knockBackResist;
                }
                else if (projectile.ai[1] < 90 && player.velocity.Y == 0 && target.velocity.Y != 0 && target.velocity.Y > -knockback)
                {
                    target.velocity.Y = -knockback;
                }
            }
            //target.velocity = player.velocity + knockback * (projectile.rotation + (90 - 45 * projectile.direction) * 0.0174f).ToRotationVector2() * target.knockBackResist;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.localAI[0] > 0.55f)
            {
                target.AddBuff(BuffID.Ichor, 600);
            }
            Player player = Main.player[projectile.owner];
            if (!target.noKnockback)
            {
                if (player.velocity.Y * player.gravDir > 3 && projectile.ai[1] > 120)
                {
                    if (target.velocity.Y < 0 && projectile.localAI[0] > 0.55f)
                    {
                        Projectile.NewProjectile(target.Center, target.velocity, mod.ProjectileType("GrabThrow"), projectile.damage, projectile.knockBack, projectile.owner, 0, target.whoAmI);
                    }
                    target.velocity.Y = (projectile.knockBack + Math.Abs(player.velocity.Y)) * player.gravDir;
                }
                else if (projectile.ai[1] < 90 && player.velocity.Y == 0 && target.velocity.Y != 0 && target.velocity.Y > -projectile.knockBack)
                {
                    target.velocity.Y = -projectile.knockBack;
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = lightColor;
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
            Rectangle? rect = new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height));
            Player player = Main.player[projectile.owner];
            if (projectile.ai[1] > 0 && (projectile.ai[0] < 100 || player.velocity.Y * player.gravDir > 4))
            {
                for (int k = 1; k < projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + new Vector2(projectile.width / 2, projectile.height / 2);
                    Color color2 = color * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                    spriteBatch.Draw(tex, drawPos, rect, color2, projectile.oldRot[k], drawOrigin, projectile.scale, effects, 0f);
                }
            }
            //color.A = (byte)projectile.alpha;
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, rect, color, projectile.rotation, drawOrigin, projectile.scale, effects, 0f);
            return false;
        }
    }
}
