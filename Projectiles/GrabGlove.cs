using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class GrabGlove : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Brawler's Glove");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.ownerHitCheck = true;
            projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 origin = player.RotatedRelativePoint(player.MountedCenter, true);
            Vector2 aim = Vector2.Zero;
            float speed = (12f / player.inventory[player.selectedItem].useTime) / player.meleeSpeed;
            projectile.localNPCHitCooldown = (int)(10 / speed);
            if (projectile.velocity.Y * player.gravDir >= 0)
            {
                origin.Y += 4 * player.gravDir;
            }
            if (projectile.ai[0] == 0)
            {
                if (player.controlUseItem)
                {
                    projectile.ai[1] = 0;
                }
                if (player.controlUseTile)
                {
                    projectile.ai[1] = 1;
                    projectile.localAI[0] = 0;
                    projectile.localAI[1] = -1;
                }
            }
            if (projectile.ai[1] == 0) //Punch
            {
                projectile.timeLeft = 3;
                if (projectile.ai[0] > -15 && projectile.ai[0] <= 0)
                {
                    if (projectile.localAI[0] == 0)
                    {
                        int frame = projectile.ai[0] < -10 ? 10 : 11;
                        player.bodyFrame.Y = frame * player.bodyFrame.Height;
                    }
                    else
                    {
                        player.bodyFrame.Y = 17 * player.bodyFrame.Height;
                    }
                    projectile.ai[0] -= 3.75f * speed;
                }
                if (projectile.ai[0] <= -15)
                {
                    projectile.ai[0] = 0.1f;
                    if (projectile.localAI[0] == 0)
                    {
                        Main.PlaySound(SoundID.Item19, projectile.Center);
                    }
                    else
                    {
                        Main.PlaySound(SoundID.Item18, projectile.Center);
                    }
                    if (((player.controlRight && player.direction > 0) || (player.controlLeft && player.direction < 0)) && player.velocity.X * player.direction < projectile.knockBack)
                    {
                        player.velocity.X = player.direction * projectile.knockBack * speed;
                    }
                    if (player.controlDown && (-projectile.velocity.Y * player.gravDir > 0) && player.velocity.Y * player.gravDir < projectile.knockBack)
                    {
                        player.velocity.Y = player.gravDir * projectile.knockBack * speed;
                    }
                }
                if (projectile.ai[0] > 0)
                {
                    projectile.ai[0] += 3.75f * speed;
                    if (projectile.localAI[0] == 0)
                    {
                        if (projectile.ai[0] < 15f)
                        {
                            int frame = projectile.ai[0] > 7.5f ? 17 : 11;
                            player.bodyFrame.Y = frame * player.bodyFrame.Height;
                        }
                    }
                    else
                    {
                        int frame = projectile.ai[0] > 15f ? 11 : 17;
                        player.bodyFrame.Y = frame * player.bodyFrame.Height;
                    }
                }
                if (projectile.ai[0] >= 30)
                {
                    if (player.channel)
                    {
                        projectile.ai[0] = 0;
                        if (projectile.localAI[0] == 0)
                        {
                            projectile.localAI[0] = 1;
                        }
                        else
                        {
                            projectile.localAI[0] = 0;
                        }
                    }
                    else
                    {
                        projectile.Kill();
                    }
                }
            }
            if (!player.noItems && !player.CCed && player.inventory[player.selectedItem].type == mod.ItemType("GrabGlove"))
            {
                if (Main.myPlayer == projectile.owner && projectile.ai[1] >= 0)
                {
                    float scaleFactor6 = projectile.ai[0] * 0.5f;
                    aim = Main.MouseWorld - origin;
                    aim.Normalize();
                    if (aim.HasNaNs())
                    {
                        aim = Vector2.UnitX * (float)player.direction;
                    }
                    if (aim.X > 0)
                    {
                        player.ChangeDir(1);
                    }
                    if (aim.X < 0)
                    {
                        player.ChangeDir(-1);
                    }
                    projectile.direction = player.direction;
                    if (aim.X * scaleFactor6 != projectile.velocity.X || aim.Y * scaleFactor6 != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.velocity = aim * scaleFactor6;
                }
                if (player.ownedProjectileCounts[mod.ProjectileType("MobHook")] + player.ownedProjectileCounts[mod.ProjectileType("EnchantedMobHook")] <= 0)
                {
                    Rectangle rect = new Rectangle((int)(player.position.X + player.velocity.X - 2), (int)(player.position.Y + player.velocity.Y - 2), player.width + 4, player.height + 4);
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC target = Main.npc[i];
                        if (rect.Intersects(target.getRect()) && target.active && !target.friendly && projectile.ai[1] != 1 && (projectile.ai[1] == 3 || i != (int)projectile.localAI[1]))
                        {
                            if ((player.velocity.Y < 0 && projectile.velocity.Y < 0 && player.Center.Y >= target.Center.Y) ||
                                (player.velocity.Y > 0 && projectile.velocity.Y > 0 && player.Center.Y <= target.Center.Y) ||
                                (player.velocity.X >= 0 && player.direction > 0 && player.Center.X <= target.Center.X) ||
                                (player.velocity.X <= 0 && player.direction < 0 && player.Center.X >= target.Center.X))
                            {
                                bool incoming = false;
                                if ((target.velocity.Y >= 0 && player.Center.Y >= target.Center.Y) ||
                                    (target.velocity.Y <= 0 && player.Center.Y <= target.Center.Y) ||
                                    (target.velocity.X >= 0 && player.Center.X >= target.Center.X) ||
                                    (target.velocity.X <= 0 && player.Center.X <= target.Center.X))
                                {
                                    incoming = true;
                                }
                                Vector2 dir = target.Center - player.Center;
                                dir.Normalize();
                                if (target.knockBackResist > 0)
                                {
                                    target.velocity = dir + player.velocity;
                                    if (!target.noTileCollide)
                                    {
                                        Vector2 push = new Vector2(player.Center.X + rect.Width / 2, target.position.Y);
                                        if (projectile.direction < 0)
                                        {
                                            push.X = (player.Center.X - rect.Width / 2) - target.width;
                                        }
                                        if (dir.Y > 0.7f)
                                        {
                                            push.Y = target.position.Y + 16;
                                        }
                                        if (dir.Y < -0.7f)
                                        {
                                            push.Y = target.position.Y - 16;
                                        }
                                        Vector2 pos = push + player.velocity;
                                        if (Collision.SolidCollision(pos, target.width, target.height))
                                        {
                                            player.velocity = -dir;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    player.velocity = -dir + (incoming ? target.velocity : Vector2.Zero);
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                projectile.Kill();
            }
            if (projectile.localAI[0] == 0)
            {
                player.heldProj = projectile.whoAmI;
            }
            projectile.rotation = projectile.velocity.ToRotation() + (projectile.direction < 0 ? 3.14f : 0);
            projectile.spriteDirection = projectile.direction;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * projectile.direction, projectile.velocity.X * projectile.direction);
            if (projectile.ai[1] == 1) //Grab
            {
                projectile.timeLeft = 3;
                float rot = 90;
                if (projectile.ai[0] > -15 && projectile.ai[0] <= 0)
                {
                    if (projectile.localAI[0] == 0)
                    {
                        int frame = projectile.ai[0] < -10 ? 10 : 11;
                        player.bodyFrame.Y = frame * player.bodyFrame.Height;
                    }
                    else
                    {
                        player.bodyFrame.Y = 17 * player.bodyFrame.Height;
                    }
                    projectile.ai[0] -= 1.5f * speed;
                }
                if (projectile.ai[0] <= -15)
                {
                    projectile.ai[0] = 0.1f;
                    Main.PlaySound(42, (int)projectile.Center.X, (int)projectile.Center.Y, 216, 1f, -0.2f);
                }
                if (projectile.ai[0] > 0)
                {
                    projectile.ai[0] += 9f * speed;
                    player.velocity.X = player.direction * projectile.knockBack * speed;
                    rot = (90 - projectile.ai[0]) * projectile.direction * 0.0174f + (player.direction < 0 ? 3.14f : 0);
                }
                if (projectile.ai[0] > 135)
                {
                    projectile.ai[1] = -1;
                    projectile.ai[0] = -1;
                    projectile.timeLeft = 15;
                }
                projectile.velocity = rot.ToRotationVector2() * 10;
                player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * projectile.direction, projectile.velocity.X * projectile.direction);
                projectile.rotation = projectile.velocity.ToRotation() - 1.57f;
            }
            if (projectile.ai[1] == 2) //NPC
            {
                projectile.localAI[0] = 1;
                NPC target = Main.npc[(int)projectile.localAI[1]];
                if (player.ownedProjectileCounts[mod.ProjectileType("MobHook")] + player.ownedProjectileCounts[mod.ProjectileType("EnchantedMobHook")] > 0)
                {
                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        Projectile p = Main.projectile[i];
                        if (p.active && p.owner == projectile.owner && (p.type == mod.ProjectileType("MobHook") || p.type == mod.ProjectileType("EnchantedMobHook")))
                        {
                            p.Kill();
                            break;
                        }
                    }
                }
                if (target.active && target.life > 0)
                {
                    if (player.controlUseTile || projectile.localAI[0] == 2)
                    {
                        if (player.controlUseItem)
                        {
                            projectile.localAI[0] = 2;
                        }
                        else
                        {
                            projectile.ai[0] = -1;
                        }
                        player.itemRotation = (float)Math.Atan2(aim.Y * projectile.direction, aim.X * projectile.direction);
                        if (target.knockBackResist > 0)
                        {
                            target.position = origin + aim * 8 + new Vector2(-target.width / 2, player.gravDir > 0 ? -target.height : 0);
                            projectile.timeLeft = 3;
                            target.GetGlobalNPC<NPCs.JoostGlobalNPC>().immunePlayer = player.whoAmI;
                            target.velocity = player.velocity;
                        }
                        else
                        {
                            Vector2 offset = new Vector2(target.width / 2 + 4, -player.height / 2);
                            if (player.direction > 0)
                            {
                                offset.X = -(player.width + 4 + target.width / 2);
                            }
                            if (projectile.timeLeft <= 40 && !Collision.SolidCollision(new Vector2(player.Center.X, player.position.Y + player.gravDir * (40 - projectile.timeLeft) * 0.5f), 1, player.height))
                            {
                                offset.Y += player.gravDir * (40 - projectile.timeLeft) * 0.5f;
                            }
                            Vector2 pos = target.Center + offset;
                            if (pos.Y < 666 || pos.Y + player.height > (Main.maxTilesY - 10) * 16 || Collision.SolidCollision(pos, player.width, player.height / 2))
                            {
                                projectile.timeLeft = 1;
                            }
                            if (projectile.timeLeft < 2)
                            {
                                player.velocity.X = player.direction * -projectile.knockBack * 2;
                                player.velocity.Y = player.gravDir * -3;
                                if (player.immuneTime < 10)
                                {
                                    player.immune = true;
                                    player.immuneNoBlink = false;
                                    player.immuneTime = 10;
                                }
                                projectile.ai[1] = -1;
                                projectile.ai[0] = -1;
                                projectile.timeLeft = 15;
                            }
                            else
                            {
                                player.position = pos;
                                player.velocity = target.velocity;
                            }
                        }
                    }
                    else
                    {
                        projectile.timeLeft = 3;
                        projectile.localAI[0] = 3;
                        if (target.knockBackResist > 0)
                        {
                            target.GetGlobalNPC<NPCs.JoostGlobalNPC>().immunePlayer = player.whoAmI;
                            float rot = -135 + (player.direction < 0 ? 90 : 0);
                            if (projectile.ai[0] > -15 && projectile.ai[0] <= 0)
                            {
                                player.bodyFrame.Y = player.bodyFrame.Height;
                                projectile.ai[0] -= 1.5f * speed;
                            }
                            if (projectile.ai[0] <= -15)
                            {
                                projectile.ai[0] = 0.1f;
                                Main.PlaySound(42, (int)projectile.Center.X, (int)projectile.Center.Y, 214);
                            }
                            if (projectile.ai[0] > 0)
                            {
                                projectile.ai[0] += 20f * speed;
                                rot = (-135 + projectile.ai[0]) * projectile.direction * 0.0174f + (player.direction < 0 ? 3.14f : 0);
                            }
                            if (projectile.ai[0] > 90)
                            {
                                target.velocity = player.velocity + aim * projectile.knockBack * 3;
                                Projectile.NewProjectile(target.Center, target.velocity, mod.ProjectileType("GrabThrow"), (int)(projectile.damage * 4f), projectile.knockBack, projectile.owner, target.whoAmI);
                                if (player.immuneTime < 10)
                                {
                                    player.immune = true;
                                    player.immuneNoBlink = false;
                                    player.immuneTime = 10;
                                }
                                projectile.ai[1] = -1;
                                projectile.ai[0] = -1;
                                projectile.timeLeft = 15;
                            }
                            projectile.velocity = rot.ToRotationVector2() * 8;
                            target.position = origin + projectile.velocity + new Vector2(-target.width / 2, player.gravDir > 0 ? -target.height : 0);
                            projectile.rotation = projectile.velocity.ToRotation() - 1.57f;
                        }
                        else
                        {
                            player.velocity.X = player.direction * -projectile.knockBack * 2;
                            player.velocity.Y = player.gravDir * -3;
                            if (player.immuneTime < 10)
                            {
                                player.immune = true;
                                player.immuneNoBlink = false;
                                player.immuneTime = 10;
                            }
                            projectile.ai[1] = -1;
                            projectile.ai[0] = -1;
                            projectile.timeLeft = 15;
                        }
                    }
                }
                else
                {
                    projectile.Kill();
                }
            }
            if (projectile.ai[1] == 3) //Pvp
            {
                projectile.timeLeft = 3;
                projectile.localAI[0] = 3;
                Player target = Main.player[(int)projectile.localAI[1]];
                if (target.active && target.statLife > 0)
                {
                    if (player.controlUseTile || projectile.localAI[0] == 2)
                    {
                        target.position = origin + aim * 8 + new Vector2(-target.width / 2, player.gravDir > 0 ? -target.height : 0);
                        if (player.controlUseItem)
                        {
                            projectile.localAI[0] = 2;
                        }
                        else
                        {
                            projectile.ai[0] = -1;
                        }
                        player.itemRotation = (float)Math.Atan2(aim.Y * projectile.direction, aim.X * projectile.direction);
                    }
                    else
                    {
                        projectile.timeLeft = 3;
                        projectile.localAI[0] = 1;
                        float rot = -135 + (player.direction < 0 ? 90 : 0);
                        if (projectile.ai[0] > -15 && projectile.ai[0] <= 0)
                        {
                            player.bodyFrame.Y = player.bodyFrame.Height;
                            projectile.ai[0] -= 1.5f * speed;
                        }
                        if (projectile.ai[0] <= -15)
                        {
                            projectile.ai[0] = 0.1f;
                            Main.PlaySound(42, (int)projectile.Center.X, (int)projectile.Center.Y, 214);
                        }
                        if (projectile.ai[0] > 0)
                        {
                            projectile.ai[0] += 10f * speed;
                            rot = (-135 + projectile.ai[0]) * projectile.direction * 0.0174f + (player.direction < 0 ? 3.14f : 0);
                        }
                        if (projectile.ai[0] > 90)
                        {
                            target.velocity = player.velocity + aim * projectile.knockBack * 3;
                            Projectile.NewProjectile(target.Center, target.velocity, mod.ProjectileType("GrabThrow"), projectile.damage, projectile.knockBack, projectile.owner, 0, target.whoAmI);
                            if (player.immuneTime < 10)
                            {
                                player.immune = true;
                                player.immuneNoBlink = false;
                                player.immuneTime = 10;
                            }
                            projectile.ai[1] = -1;
                            projectile.ai[0] = -1;
                            projectile.timeLeft = 15;
                        }
                        projectile.velocity = rot.ToRotationVector2() * 8;
                        target.position = origin + projectile.velocity + new Vector2(-target.width / 2, player.gravDir > 0 ? -target.height : 0);
                        projectile.rotation = projectile.velocity.ToRotation() - 1.57f;
                    }
                }
                else
                {
                    projectile.Kill();
                }
            }
            if (projectile.localAI[0] == 2 && (projectile.ai[1] == 2 || projectile.ai[1] == 3)) //Pummel
            {
                projectile.localAI[0] = 2;
                if (projectile.ai[0] > -15 && projectile.ai[0] <= 0)
                {
                    projectile.ai[0] -= 3f * speed;
                }
                if (projectile.ai[0] <= -15)
                {
                    projectile.ai[0] = 0.1f;
                    Main.PlaySound(SoundID.Item18, projectile.Center);
                }
                if (projectile.ai[0] > 0)
                {
                    projectile.ai[0] += 3f * speed;
                }
                if (projectile.ai[0] >= 30)
                {
                    projectile.ai[0] = -1f;
                    projectile.localAI[0] = 1;
                }
                Vector2 dir = projectile.velocity;
                dir.Normalize();
                projectile.velocity = dir * projectile.ai[0] * 0.5f;
            }
            projectile.position = (projectile.velocity + origin) - projectile.Size / 2f;
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
            return false;
		}
        public override bool? CanHitNPC(NPC target)
		{
			return !target.friendly && projectile.ai[0] >= 1;
		}
        public override bool CanHitPvp(Player target)
        {
            if (projectile.ai[0] >= 1)
            {
                return base.CanHitPvp(target);
            }
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.ai[1] == 1)
            {
                crit = true;
            }
            if (target.knockBackResist > 0 && target.life < (damage - target.defense / 2) * (crit ? 2 : 1) && (projectile.ai[1] == 1 || projectile.localAI[0] == 3))
            {
                damage = (target.life + target.defense / 2 - 3) / (crit ? 2 : 1);
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            if (projectile.ai[1] == 1)
            {
                crit = true;
            }
            if (target.statLife < (damage - target.statDefense / 2) * (crit ? 2 : 1) && (projectile.ai[1] == 1 || projectile.localAI[0] == 3))
            {
                damage = (target.statLife + target.statDefense / 2 - 3) / (crit ? 2 : 1);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)
		{
            Player player = Main.player[projectile.owner];
            Vector2 dir = projectile.velocity;
            dir.Normalize();
            if (projectile.ai[1] == 0)
            {
                if (target.knockBackResist > 0)
                {
                    target.velocity = (dir * knockBack * target.knockBackResist) + player.velocity;
                    if (!target.noTileCollide)
                    {
                        Vector2 push = new Vector2(projectile.Center.X + 16, target.position.Y);
                        if (projectile.direction < 0)
                        {
                            push.X = (projectile.Center.X - 16) - target.width;
                        }
                        if (dir.Y > 0.3f)
                        {
                            push.Y = target.position.Y + 16;
                        }
                        if (dir.Y < -0.3f)
                        {
                            push.Y = target.position.Y - 16;
                        }
                        Vector2 pos = push + player.velocity;
                        if (Collision.SolidCollision(pos, target.width, target.height))
                        {
                            player.velocity = -dir * knockBack;
                            if (player.immuneTime < 2)
                            {
                                player.immune = true;
                                player.immuneNoBlink = true;
                                player.immuneTime = 2;
                            }
                        }
                    }
                }
                else
                {
                    bool incoming = false;
                    if ((target.velocity.Y > 0 && player.Center.Y > target.Center.Y) ||
                        (target.velocity.Y < 0 && player.Center.Y < target.Center.Y) ||
                        (target.velocity.X > 0 && player.Center.X > target.Center.X) ||
                        (target.velocity.X < 0 && player.Center.X < target.Center.X))
                    {
                        incoming = true;
                    }
                    player.velocity = -dir * (knockBack + (incoming ? target.velocity.Length() : 0));
                    if (player.immuneTime < 2)
                    {
                        player.immune = true;
                        player.immuneNoBlink = true;
                        player.immuneTime = 2;
                    }
                }
            }
            if (projectile.ai[1] == 1 && target.life > 0)
            {
                projectile.timeLeft = 120;
                projectile.localAI[1] = target.whoAmI;
                projectile.ai[0] = -1;
                projectile.ai[1] = 2;
                Main.PlaySound(42, (int)projectile.Center.X, (int)projectile.Center.Y, 215);
                if (player.immuneTime < 20)
                {
                    player.immune = true;
                    player.immuneNoBlink = false;
                    player.immuneTime = 20;
                }
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[projectile.owner];
            Vector2 dir = projectile.velocity;
            dir.Normalize();
            if (projectile.ai[1] == 0)
            {
                if (!target.noKnockback)
                {
                    target.velocity = dir * (projectile.knockBack + player.velocity.Length());
                    float push = projectile.Center.X + 16;
                    if (projectile.direction < 0)
                    {
                        push = (projectile.Center.X - 16) - target.width;
                    }
                    Vector2 pos = target.position;
                    pos.X = push + player.velocity.X;
                    if (Collision.SolidCollision(pos, target.width, target.height))
                    {
                        player.velocity = -dir * projectile.knockBack;
                        if (player.immuneTime < 2)
                        {
                            player.immune = true;
                            player.immuneNoBlink = true;
                            player.immuneTime = 2;
                        }
                    }
                }
                else
                {
                    bool incoming = false;
                    if ((target.velocity.Y > 0 && player.Center.Y > target.Center.Y) ||
                        (target.velocity.Y < 0 && player.Center.Y < target.Center.Y) ||
                        (target.velocity.X > 0 && player.Center.X > target.Center.X) ||
                        (target.velocity.X < 0 && player.Center.X < target.Center.X))
                    {
                        incoming = true;
                    }
                    player.velocity = -dir * (projectile.knockBack + (incoming ? target.velocity.Length() : 0));
                    if (player.immuneTime < 2)
                    {
                        player.immune = true;
                        player.immuneNoBlink = true;
                        player.immuneTime = 2;
                    }
                }
            }
            if (projectile.ai[1] == 1 && target.statLife > 0)
            {
                projectile.localAI[1] = target.whoAmI;
                projectile.ai[0] = -1;
                projectile.ai[1] = 3;
                Main.PlaySound(42, (int)projectile.Center.X, (int)projectile.Center.Y, 215);
                if (player.immuneTime < 20)
                {
                    player.immune = true;
                    player.immuneNoBlink = false;
                    player.immuneTime = 20;
                }
            }
        }
    }
}