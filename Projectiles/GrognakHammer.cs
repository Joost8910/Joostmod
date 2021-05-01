using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class GrognakHammer : ModProjectile
    {
        int startup = 15;
        int active = 10;
        int endlag = 15;
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Warhammer of Grognak");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
        public override void SetDefaults()
        {
            projectile.width = 64;
            projectile.height = 64;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 17;
            projectile.ownerHitCheck = true;
        }
        public override bool? CanHitNPC(NPC target)
		{
            Player player = Main.player[projectile.owner];
			return !target.friendly && (((projectile.ai[0] == 1 && projectile.ai[1] >= startup && (projectile.ai[1] < startup+active || projectile.localAI[1] == 1)) || (projectile.ai[0] == 2 && projectile.ai[1] < 226)) || player.velocity.Y * player.gravDir > 9);
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
            Player player = Main.player[projectile.owner];
			if (target.knockBackResist > 0 && player.velocity.Y * player.gravDir > 1)
			{
				target.velocity.Y = (knockback + Math.Abs(player.velocity.Y)) * player.gravDir * target.knockBackResist;
			}
		}
        public override bool CanHitPvp(Player target)
        {
            Player player = Main.player[projectile.owner];
            return (((projectile.ai[0] == 1 && projectile.ai[1] >= startup && (projectile.ai[1] < startup+active || projectile.localAI[1] == 1)) || (projectile.ai[0] == 2 && projectile.ai[1] < 226)) || player.velocity.Y * player.gravDir > 9);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (!target.noKnockback)
            {
                target.velocity.Y = (projectile.knockBack + Math.Abs(player.velocity.Y)) * player.gravDir;
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = (int)(damage * 1.3f);
            if (projectile.ai[0] == 2)
            {
                crit = true;
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage = (int)(damage * 1.3f);
            if (projectile.ai[0] == 2)
            {
                crit = true;
            }
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            projectile.scale = player.inventory[player.selectedItem].scale;
            projectile.width = (int)(64 * projectile.scale);
            projectile.height = (int)(64 * projectile.scale);

            if (projectile.ai[0] == 0)
            {
                projectile.ai[1] = 0;
                if (player.controlUseTile)
                {
                    projectile.ai[0] = 1;
                }
                else
                {
                    projectile.Kill();
                    Projectile.NewProjectile(projectile.position, projectile.velocity, mod.ProjectileType("GrognakHammer2"), projectile.damage, projectile.knockBack, projectile.owner);
                }
            }

            if (!player.noItems && !player.CCed && !player.dead)
            {
                float scaleFactor6 = 1f;
                if (player.inventory[player.selectedItem].shoot == projectile.type)
                {
                    scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                }
                projectile.velocity.Y = 0;
                projectile.direction = projectile.velocity.X > 0 ? 1 : -1;
                projectile.velocity.X = projectile.direction;
                projectile.direction *= (int)player.gravDir;
            }
            else
            {
                player.fullRotation = 0f;
                projectile.Kill();
            }
            if (projectile.ai[0] == 1)
            {
                float speed = 1 / player.meleeSpeed;

                projectile.ai[1] += speed;
                float rad = 0;
                if (player.gravDir < 0)
                {
                    rad += 3.14f;
                }
                if (projectile.ai[1] < startup)
                {
                    if (modPlayer.LegendCool <= 0 && player.controlUp)
                    {
                        modPlayer.LegendCool = 300;
                        projectile.ai[0] = 2;
                        projectile.ai[1] = 0;
                    }
                    projectile.rotation = (player.fullRotation) + ((startup - projectile.ai[1]) * 0.0174f * 5 * projectile.direction) + rad;
                }
                if (projectile.ai[1] >= startup && (projectile.ai[1] < (startup + active) || projectile.localAI[1] == 1))
                {
                    if (projectile.localAI[0] == 0)
                    {
                        Main.PlaySound(2, (int)projectile.Center.X, (int)projectile.Center.Y, 1, 1.2f, -0.3f);
                        projectile.localAI[0] = 1;
                        if (Main.myPlayer == projectile.owner)
                        {
                            Vector2 dir = Main.MouseWorld - player.Center;
                            dir.Normalize();
                            if (dir.HasNaNs())
                            {
                                dir = Vector2.UnitX * (float)player.direction;
                            }
                            if (dir.X != projectile.velocity.X || dir.Y != projectile.velocity.Y)
                            {
                                projectile.netUpdate = true;
                            }
                            projectile.direction = dir.X > 0 ? 1 : -1;
                            projectile.velocity.X = projectile.direction;
                        }
                        if (player.velocity.Y != 0 && !player.mount.Active)
                        {
                            projectile.localNPCHitCooldown = active;
                            projectile.localAI[1] = 1;
                            Projectile.NewProjectile(player.Center, projectile.velocity, mod.ProjectileType("GrognakBeam"), (int)(projectile.damage * 1.2f), projectile.knockBack, projectile.owner, speed * 1.8f);
                        }
                        else
                        {
                            Projectile.NewProjectile(player.Center, projectile.velocity, mod.ProjectileType("GrognakBeam"), (int)(projectile.damage * 1.2f), projectile.knockBack, projectile.owner, speed);
                        }
                    }
                    if (projectile.localAI[1] == 1)
                    {
                        player.fullRotation = (projectile.ai[1] - startup) * 0.0174f * player.direction * (360 / (active + endlag));
                        player.mount.Dismount(player);
                    }
                    if (projectile.ai[1] < startup + active)
                    {
                        projectile.rotation = (player.fullRotation) + ((projectile.ai[1] - startup) * 0.0174f * projectile.direction * (180 / active)) + rad;
                    }
                    else
                    {
                        projectile.rotation = (player.fullRotation) + (180 * 0.0174f * projectile.direction) + rad;
                    }
                }
                if (projectile.ai[1] >= startup + active + endlag)
                {
                    projectile.Kill();
                }
                projectile.spriteDirection = projectile.direction;
                projectile.timeLeft = 2;
                if (projectile.ai[1] < startup)
                {
                    player.itemTime = (int)(40 * speed);
                    player.itemAnimation = (int)(40 * speed);
                }
                else if (projectile.ai[1] <= startup + active)
                {
                    player.itemTime = (int)((40 * speed) - ((projectile.ai[1] - startup) * speed * 4));
                    player.itemAnimation = (int)((40 * speed) - ((projectile.ai[1] - startup) * speed * 4));
                }
                else
                {
                    player.itemTime = 2;
                    player.itemAnimation = 2;
                }
                player.itemRotation = projectile.rotation - player.fullRotation;
                if (player.itemTime < 2)
                {
                    player.itemTime = 2;
                }
                if (player.itemAnimation < 2)
                {
                    player.itemAnimation = 2;
                }
                player.itemRotation = 0;
            }
            if (projectile.ai[0] == 2)
            {
                if (projectile.ai[1] < 1)
                {
                    player.velocity.Y = -18f * player.gravDir;
                    player.fallStart = (int)(player.position.Y / 16f);
                }
                if (projectile.ai[1] % 51 == 0)
                {
                    Main.PlaySound(2, (int)projectile.Center.X, (int)projectile.Center.Y, 7, 1.2f, -0.1f);
                }
                if (projectile.ai[1] < 226)
                {
                    projectile.ai[1] += 3;
                }
                projectile.spriteDirection = projectile.direction;
                projectile.timeLeft = 2;
                player.mount.Dismount(player);
                if (player.gravDir == -1)
                {
                    projectile.rotation = -(projectile.ai[1] - 2) * 0.0174f * 7f * projectile.direction - (float)(1.566 * player.direction);
                    player.fullRotation = (float)(projectile.rotation - (3.14 * player.direction));
                }
                else
                {
                    projectile.rotation = (projectile.ai[1] - 2) * 0.0174f * 7f * projectile.direction;
                    player.fullRotation = (float)(projectile.rotation - (1.566 * player.direction));
                }
                player.itemTime = (int)(player.itemAnimationMax * 0.6);
                player.itemAnimation = (int)(player.itemAnimationMax * 0.6);
                player.itemRotation = 0;
                player.portalPhysicsFlag = true;
                if (projectile.ai[1] >= 226 && player.velocity.Y == 0)
                {
                    player.fullRotation = 0f;
                    projectile.Kill();
                }
            }
            player.fullRotationOrigin = player.Center - player.position;
            int length = 50;
            projectile.position = (player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f) + new Vector2((float)Math.Cos(projectile.rotation + 1.566 + (2.349 * projectile.direction)) * length * projectile.scale, (float)Math.Sin(projectile.rotation + 1.566 + (2.349 * projectile.direction)) * length * projectile.scale);
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            return false;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.direction == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = lightColor;
            sb.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale, effects, 0f);

            Texture2D gemTex = mod.GetTexture("Projectiles/GrognakHammerGem");
            Color gemColor = new Color(90, 255, (int)(51 + (Main.DiscoG * 0.75f)));
            sb.Draw(gemTex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), gemColor, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale, effects, 0f);
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            if (projectile.localAI[1] == 1)
            {
                player.fullRotation = 0;
            }
            if (projectile.ai[1] >= 226 && projectile.ai[0] == 2)
            {
                Vector2 posi = new Vector2(player.Center.X + 64 * projectile.direction * projectile.scale, player.position.Y + player.height + 4);
                Point pos = posi.ToTileCoordinates();
                Tile tileSafely = Framing.GetTileSafely(pos.X, pos.Y);
                for (int d = 0; d < 80; d++)
                {
                    Dust dust = Main.dust[WorldGen.KillTile_MakeTileDust(pos.X, pos.Y, tileSafely)];
                    dust.velocity.X = dust.velocity.X + Main.rand.Next(-10, 10) * 0.3f;
                    dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-12, -6) * 0.4f;
                }
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 70);
                if (player.gravDir == -1)
                {
                    Projectile.NewProjectile(player.Center.X + 48 * projectile.direction * projectile.scale, projectile.Center.Y, projectile.direction * 8f, 0f, mod.ProjectileType("GrogWaveFlipped"), (int)(projectile.damage * 3.5f), projectile.knockBack * 2.5f, projectile.owner);
                }
                else
                {
                    Projectile.NewProjectile(player.Center.X + 48 * projectile.direction * projectile.scale, projectile.Center.Y, projectile.direction * 8f, 0f, mod.ProjectileType("GrogWave"), (int)(projectile.damage * 3.5f), projectile.knockBack * 2.5f, projectile.owner);
                }
                player.fullRotation = 0f;
            }
		}
    }
}