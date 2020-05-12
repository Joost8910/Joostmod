using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class DragonTooth : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dragon Tooth");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
		public override void SetDefaults()
		{
			projectile.width = 110;
			projectile.height = 110;
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
                projectile.width = (int)(110 * projectile.scale);
                projectile.height = (int)(110 * projectile.scale);
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
            double rad = (player.fullRotation - 1.83f) + ((projectile.ai[1] - 20) * 0.0174f * projectile.direction);
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
            double dist = -70 * projectile.scale * projectile.direction;
            projectile.position.X = center.X + (0 * player.direction) - (int)(Math.Cos(rad - 0.785f) * dist) - (projectile.width / 2);
            projectile.position.Y = center.Y + (0) - (int)(Math.Sin(rad - 0.785f) * dist) - (projectile.height / 2);
            if (projectile.ai[1] < 0)
            {
                projectile.position.Y += player.gravDir * ((projectile.ai[1] / projectile.scale * 0.15f) - 4);
            }
            if (projectile.ai[0] < 30)
            {
                projectile.ai[1] -= speed;
                player.velocity.X *= 0.98f;
                projectile.localAI[0] += 0.02f * speed;
            }
            if (projectile.ai[0] > 30 && projectile.soundDelay >= 0 && channeling)
            {
                projectile.soundDelay = -60;
                Main.PlaySound(2, projectile.Center, 39);
            }
            if (channeling && projectile.ai[0] > 30)
            {
                player.velocity.X *= 0.98f;
                projectile.ai[0] = 30;
                if (player.velocity.Y * player.gravDir > player.gravity)
                {
                    projectile.localAI[1] = 1;
                }
                else
                {
                    projectile.localAI[1] = 0;
                }
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
                Main.PlaySound(42, projectile.Center, 214);
            }
            if (projectile.timeLeft <= 120)
            {
                if (projectile.localAI[1] > 0)
                {
                    player.mount.Dismount(player);
                    if (projectile.ai[1] > 150 && projectile.localAI[1] < 10)
                    {
                        if (player.velocity.Y == 0)
                        {
                            if (projectile.ai[0] < 100)
                            {
                                projectile.localAI[1] = 10;
                                Main.PlaySound(42, projectile.Center, 207);
                                int damage = projectile.damage;
                                float knockBack = projectile.knockBack;
                                for (int i = 1; i <= 5 * projectile.scale; i++)
                                {
                                    Projectile.NewProjectile(projectile.Center.X + i * 16, projectile.Center.Y - 60 * player.gravDir, 0.01f * i, 15 * player.gravDir, mod.ProjectileType("DragonToothWave"), damage, knockBack, projectile.owner);
                                    Projectile.NewProjectile(projectile.Center.X - i * 16, projectile.Center.Y - 60 * player.gravDir, -0.01f * i, 15 * player.gravDir, mod.ProjectileType("DragonToothWave"), damage, knockBack, projectile.owner);
                                }
                                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 60 * player.gravDir, 0, 15 * player.gravDir, mod.ProjectileType("DragonToothWave"), damage, knockBack, projectile.owner);
                            }
                        }
                        else
                        {
                            projectile.timeLeft = 110;
                            projectile.ai[1] = 150;
                        }
                    }
                    else if (projectile.ai[1] < 160)
                    {
                        projectile.timeLeft = 110;
                        projectile.ai[1] += 10 * speed * (projectile.localAI[0] + 1);
                    }
                    player.fullRotation = (projectile.ai[1] * 0.00174f * player.direction);
                    player.fullRotationOrigin = player.Center - player.position;
                    if (projectile.localAI[1] >= 10)
                    {
                        player.velocity.X = 0;
                        player.velocity.Y = 10f * player.gravDir;
                        player.controlJump = false;
                        player.controlLeft = false;
                        player.controlRight = false;
                    }
                }
                else
                {
                    if (projectile.ai[1] < 180)
                    {
                        projectile.timeLeft = 70;
                        projectile.ai[1] += 10 * speed * (projectile.localAI[0] + 1);
                    }
                }
                if (projectile.timeLeft == 68 && projectile.ai[0] < 100)
                {
                    projectile.ai[0] = 100;
                    projectile.timeLeft = (int)(68 / (speed * 2));
                    if (player.velocity.Y == 0 && projectile.localAI[1] == 0)
                    {
                        Main.PlaySound(42, projectile.Center, 210);
                    }
                }
                if (projectile.timeLeft <= 1)
                {
                    player.fullRotation = 0;
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
            /*
            float rot = projectile.rotation - 1.57f + (0.785f * player.direction * (int)player.gravDir);
            Vector2 unit = rot.ToRotationVector2();
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            for (int i = 0; i < 7; i++)
            {
                Dust.NewDustPerfect(vector + unit * i * 32, 20);
            }*/
        }
        public override bool? CanCutTiles()
        {
            return (projectile.ai[0] > 32 && projectile.ai[0] < 100);
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projectile.ai[0] > 32 && projectile.ai[0] < 100)
            {
                Player player = Main.player[projectile.owner];
                float rot = projectile.rotation - 1.57f + (0.785f * player.direction * (int)player.gravDir);
                Vector2 unit = rot.ToRotationVector2();
                Vector2 vector = player.RotatedRelativePoint(player.position + new Vector2(player.width / 2, 20), true);
                float point = 0f;
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), vector, vector + unit * 140 * projectile.scale, 40 * projectile.scale, ref point))
                {
                    return true;
                }
            }
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = (int)(damage * (projectile.localAI[0] + 1));
            knockback = knockback * (projectile.localAI[0] + 1);
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage = (int)(damage * (projectile.localAI[0] + 1));
        }
        public override bool CanHitPvp(Player target)
        {
            if (projectile.ai[0] > 32 && projectile.ai[0] < 100)
            {
                return base.CanHitPvp(target);
            }
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.ai[0] > 32 && projectile.ai[0] < 100)
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
            Color color = lightColor;
            if (projectile.ai[1] > 0 && projectile.ai[0] < 100)
            {
                for (int k = 1; k < projectile.oldPos.Length; k++)
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
