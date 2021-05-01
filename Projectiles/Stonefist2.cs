using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class Stonefist2 : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stone Hand");
            Main.projFrames[projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.width = 82;
            projectile.height = 82;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.ownerHitCheck = true;
            projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 27;
        }
        private void JumpOff(Player player)
        {
            player.velocity.X = player.direction * -6;
            player.velocity.Y = player.gravDir * -10;
            player.wingTime = (float)player.wingTimeMax;
            if (player.doubleJumpCloud)
            {
                player.jumpAgainCloud = true;
            }
            if (player.doubleJumpSandstorm)
            {
                player.jumpAgainSandstorm = true;
            }
            if (player.doubleJumpBlizzard)
            {
                player.jumpAgainBlizzard = true;
            }
            if (player.doubleJumpFart)
            {
                player.jumpAgainFart = true;
            }
            if (player.doubleJumpSail)
            {
                player.jumpAgainSail = true;
            }
            if (player.doubleJumpUnicorn)
            {
                player.jumpAgainUnicorn = true;
            }
            if (player.immuneTime < 10)
            {
                player.immune = true;
                player.immuneNoBlink = false;
                player.immuneTime = 10;
            }
            projectile.ai[1] = 0;
            projectile.ai[0] = -1;
            projectile.timeLeft = 15;
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 origin = player.RotatedRelativePoint(player.MountedCenter, true);
            Vector2 aim = Vector2.Zero;
            float speed = (55f / player.inventory[player.selectedItem].useTime) / player.meleeSpeed;
            projectile.localNPCHitCooldown = (int)(27 / speed);
            if (!player.noItems && !player.CCed)
            {
                if (Main.myPlayer == projectile.owner)
                {
                    float scaleFactor6 = 1f;
                    if (player.inventory[player.selectedItem].shoot == mod.ProjectileType("Stonefist"))
                    {
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * projectile.scale * ((projectile.ai[0] * 0.5f) + 1.75f);
                    }
                    Vector2 vector13 = Main.MouseWorld - origin;
                    vector13.Normalize();
                    if (vector13.HasNaNs())
                    {
                        vector13 = Vector2.UnitX * (float)player.direction;
                    }
                    aim = vector13;
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

            if (projectile.soundDelay <= 0 && projectile.soundDelay > -10)
            {
                Main.PlaySound(42, (int)projectile.position.X, (int)projectile.position.Y, 216, 1, -0.3f);
                projectile.soundDelay = -10;
            }


            if (projectile.ai[1] == 1) //Grab
            {
                projectile.timeLeft = 3;
                projectile.ai[0] += 0.2f * speed;
                Vector2 dir = projectile.velocity;
                dir.Normalize();
                dir = dir * 10f * (projectile.ai[1] + 0.75f) * speed;
                if (projectile.ai[0] > 2)
                {
                    projectile.ai[1] = -1;
                    projectile.ai[0] = -1;
                    projectile.timeLeft = 15;
                }
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
                    if (player.controlUseItem || projectile.localAI[0] == 2)
                    {
                        if (player.controlUseTile)
                        {
                            projectile.localAI[0] = 2;
                        }
                        else
                        {
                            projectile.ai[0] = 0;
                        }
                        player.itemRotation = (float)Math.Atan2(aim.Y * projectile.direction, aim.X * projectile.direction);
                        if (target.knockBackResist > 0)
                        {
                            target.position = projectile.Center + new Vector2(-target.width / 2, player.gravDir > 0 ? -target.height : 0);
                            projectile.timeLeft = 3;
                            target.GetGlobalNPC<NPCs.JoostGlobalNPC>().immunePlayer = player.whoAmI;
                            target.velocity = player.velocity;
                        }
                        else
                        {
                            JumpOff(player);
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
                            if (projectile.ai[0] > -1 && projectile.ai[0] <= 0)
                            {
                                player.bodyFrame.Y = player.bodyFrame.Height;
                                projectile.ai[0] -= 0.1f * speed;
                            }
                            if (projectile.ai[0] <= -1)
                            {
                                projectile.ai[0] = 0.1f;
                                Main.PlaySound(42, (int)projectile.Center.X, (int)projectile.Center.Y, 214);
                            }
                            if (projectile.ai[0] > 0)
                            {
                                projectile.ai[0] += 0.4f * speed;
                                rot = (-135 + projectile.ai[0] * 45) * projectile.direction * 0.0174f + (player.direction < 0 ? 3.14f : 0);
                            }
                            if (projectile.ai[0] > 2)
                            {
                                target.velocity = player.velocity + aim * projectile.knockBack;
                                Projectile.NewProjectile(target.Center, target.velocity, mod.ProjectileType("GrabThrow"), projectile.damage, projectile.knockBack, projectile.owner, target.whoAmI);
                                if (player.immuneTime < 10)
                                {
                                    player.immune = true;
                                    player.immuneNoBlink = false;
                                    player.immuneTime = 10;
                                }
                                projectile.ai[1] = 0;
                                projectile.ai[0] = -1;
                                projectile.timeLeft = 15;
                                projectile.velocity = new Vector2(player.direction, 0);
                                projectile.Center = origin;
                            }
                            projectile.velocity = rot.ToRotationVector2() * 8;
                            target.position = projectile.Center + projectile.velocity + new Vector2(-target.width / 2, player.gravDir > 0 ? -target.height : 0);
                        }
                        else
                        {
                            JumpOff(player);
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
                    if (player.controlUseItem || projectile.localAI[0] == 2)
                    {
                        target.position = projectile.Center + new Vector2(-target.width / 2, player.gravDir > 0 ? -target.height : 0);
                        if (player.controlUseTile)
                        {
                            projectile.localAI[0] = 2;
                        }
                        else
                        {
                            projectile.ai[0] = 0;
                        }
                        player.itemRotation = (float)Math.Atan2(aim.Y * projectile.direction, aim.X * projectile.direction);
                    }
                    else
                    {
                        projectile.timeLeft = 3;
                        projectile.localAI[0] = 1;
                        float rot = -135 + (player.direction < 0 ? 90 : 0);
                        if (projectile.ai[0] > -1 && projectile.ai[0] <= 0)
                        {
                            player.bodyFrame.Y = player.bodyFrame.Height;
                            projectile.ai[0] -= 0.1f * speed;
                        }
                        if (projectile.ai[0] <= -1)
                        {
                            projectile.ai[0] = 0.1f;
                            Main.PlaySound(42, (int)projectile.Center.X, (int)projectile.Center.Y, 214);
                        }
                        if (projectile.ai[0] > 0)
                        {
                            projectile.ai[0] += 0.4f * speed;
                            rot = (-135 + projectile.ai[0] * 45) * projectile.direction * 0.0174f + (player.direction < 0 ? 3.14f : 0);
                        }
                        if (projectile.ai[0] > 2)
                        {
                            target.velocity = player.velocity + aim * projectile.knockBack;
                            Projectile.NewProjectile(target.Center, target.velocity, mod.ProjectileType("GrabThrow"), projectile.damage * 2, projectile.knockBack, projectile.owner, 0, target.whoAmI);
                            if (player.immuneTime < 10)
                            {
                                player.immune = true;
                                player.immuneNoBlink = false;
                                player.immuneTime = 10;
                            }
                            projectile.ai[1] = 0;
                            projectile.ai[0] = -1;
                            projectile.timeLeft = 15;
                            projectile.velocity = new Vector2(player.direction, 0);
                            projectile.Center = origin;
                        }
                        projectile.velocity = rot.ToRotationVector2() * 8;
                        target.position = projectile.Center + projectile.velocity + new Vector2(-target.width / 2, player.gravDir > 0 ? -target.height : 0);
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
                if (projectile.ai[0] > -1 && projectile.ai[0] <= 0)
                {
                    projectile.ai[0] -= 0.05f * speed;
                }
                if (projectile.ai[0] <= -1)
                {
                    projectile.ai[0] = 0.05f;
                    Main.PlaySound(SoundID.Item18, projectile.Center);
                }
                if (projectile.ai[0] > 0)
                {
                    projectile.ai[0] += 0.1f * speed;
                }
                if (projectile.ai[0] > 1)
                {
                    projectile.ai[0] = 0;
                    projectile.localAI[0] = 1;
                }
            }
            if (projectile.ai[1] == 1 || projectile.ai[1] == 0)
            {
                projectile.frame = 0;
            }
            else
            {
                projectile.frame = 1;
            }
            
            projectile.position = (projectile.velocity + origin) - projectile.Size / 2f;
            projectile.rotation = projectile.velocity.ToRotation() + 1.57f + (projectile.direction * 0.785f);
            projectile.spriteDirection = projectile.direction;
            if (projectile.localAI[0] != 3)
            {
                player.ChangeDir(projectile.direction);
            }
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
            Vector2 drawOrigin = new Vector2(tex.Width / 2, (tex.Height / Main.projFrames[projectile.type]) / 2);
            if (projectile.ai[1] >= 1)
            {
                for (int k = 0; k < projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + new Vector2(projectile.width / 2, projectile.height / 2);
                    Color color2 = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[projectile.type]) * projectile.frame, tex.Width, tex.Height / Main.projFrames[projectile.type]));
                    spriteBatch.Draw(tex, drawPos, rect, color2, projectile.oldRot[k], drawOrigin, projectile.scale, effects, 0f);
                }
            }
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[projectile.type]) * projectile.frame, tex.Width, tex.Height / Main.projFrames[projectile.type])), lightColor, projectile.rotation, drawOrigin, projectile.scale, effects, 0f);

            return false;
		}
        
        public override bool? CanHitNPC(NPC target)
		{
			return !target.friendly && projectile.ai[0] > 0;
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
            if (target.knockBackResist > 0 && target.life < (damage - target.defense / 2) * (crit ? 2 : 1) && (projectile.ai[1] == 1 || projectile.localAI[0] == 3))
            {
                damage = (target.life + target.defense / 2 - 3) / (crit ? 2 : 1);
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            if (target.statLife < (damage - target.statDefense / 2) * (crit ? 2 : 1) && (projectile.ai[1] == 1 || projectile.localAI[0] == 3))
            {
                damage = (target.statLife + target.statDefense / 2 - 3) / (crit ? 2 : 1);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)
		{
            Player player = Main.player[projectile.owner];
            if (projectile.ai[1] == 1 && target.life > 0 && target.knockBackResist > 0)
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