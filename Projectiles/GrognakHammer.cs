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
    public class GrognakHammer : ModProjectile
    {
        int startup = 15;
        int active = 10;
        int endlag = 15;
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Warhammer of Grognak");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 17;
            Projectile.ownerHitCheck = true;
        }
        public override bool? CanHitNPC(NPC target)
		{
            Player player = Main.player[Projectile.owner];
			return !target.friendly && (((Projectile.ai[0] == 1 && Projectile.ai[1] >= startup && (Projectile.ai[1] < startup+active || Projectile.localAI[1] == 1)) || (Projectile.ai[0] == 2 && Projectile.ai[1] < 226)) || player.velocity.Y * player.gravDir > 9);
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
            Player player = Main.player[Projectile.owner];
			if (target.knockBackResist > 0 && player.velocity.Y * player.gravDir > 1)
			{
				target.velocity.Y = (knockback + Math.Abs(player.velocity.Y)) * player.gravDir * target.knockBackResist;
			}
		}
        public override bool CanHitPvp(Player target)
        {
            Player player = Main.player[Projectile.owner];
            return (((Projectile.ai[0] == 1 && Projectile.ai[1] >= startup && (Projectile.ai[1] < startup+active || Projectile.localAI[1] == 1)) || (Projectile.ai[0] == 2 && Projectile.ai[1] < 226)) || player.velocity.Y * player.gravDir > 9);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (!target.noKnockback)
            {
                target.velocity.Y = (Projectile.knockBack + Math.Abs(player.velocity.Y)) * player.gravDir;
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = (int)(damage * 1.3f);
            if (Projectile.ai[0] == 2)
            {
                crit = true;
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage = (int)(damage * 1.3f);
            if (Projectile.ai[0] == 2)
            {
                crit = true;
            }
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            Projectile.scale = player.inventory[player.selectedItem].scale;
            Projectile.width = (int)(64 * Projectile.scale);
            Projectile.height = (int)(64 * Projectile.scale);

            if (Projectile.ai[0] == 0)
            {
                Projectile.ai[1] = 0;
                if (player.controlUseTile)
                {
                    Projectile.ai[0] = 1;
                }
                else
                {
                    Projectile.Kill();
                    Projectile.NewProjectile(Projectile.position, Projectile.velocity, Mod.Find<ModProjectile>("GrognakHammer2").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
                }
            }

            if (!player.noItems && !player.CCed && !player.dead)
            {
                float scaleFactor6 = 1f;
                if (player.inventory[player.selectedItem].shoot == Projectile.type)
                {
                    scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * Projectile.scale;
                }
                Projectile.velocity.Y = 0;
                Projectile.direction = Projectile.velocity.X > 0 ? 1 : -1;
                Projectile.velocity.X = Projectile.direction;
                Projectile.direction *= (int)player.gravDir;
            }
            else
            {
                player.fullRotation = 0f;
                Projectile.Kill();
            }
            if (Projectile.ai[0] == 1)
            {
                float speed = 1 / player.GetAttackSpeed(DamageClass.Melee);

                Projectile.ai[1] += speed;
                float rad = 0;
                if (player.gravDir < 0)
                {
                    rad += 3.14f;
                }
                if (Projectile.ai[1] < startup)
                {
                    if (modPlayer.LegendCool <= 0 && player.controlUp)
                    {
                        modPlayer.LegendCool = 300;
                        Projectile.ai[0] = 2;
                        Projectile.ai[1] = 0;
                    }
                    Projectile.rotation = (player.fullRotation) + ((startup - Projectile.ai[1]) * 0.0174f * 5 * Projectile.direction) + rad;
                }
                if (Projectile.ai[1] >= startup && (Projectile.ai[1] < (startup + active) || Projectile.localAI[1] == 1))
                {
                    if (Projectile.localAI[0] == 0)
                    {
                        SoundEngine.PlaySound(SoundID.Item1.WithVolumeScale(1.2f).WithPitchOffset(-0.3f), Projectile.Center);
                        Projectile.localAI[0] = 1;
                        if (Main.myPlayer == Projectile.owner)
                        {
                            Vector2 dir = Main.MouseWorld - player.Center;
                            dir.Normalize();
                            if (dir.HasNaNs())
                            {
                                dir = Vector2.UnitX * (float)player.direction;
                            }
                            if (dir.X != Projectile.velocity.X || dir.Y != Projectile.velocity.Y)
                            {
                                Projectile.netUpdate = true;
                            }
                            Projectile.direction = dir.X > 0 ? 1 : -1;
                            Projectile.velocity.X = Projectile.direction;
                        }
                        if (player.velocity.Y != 0 && !player.mount.Active)
                        {
                            Projectile.localNPCHitCooldown = active;
                            Projectile.localAI[1] = 1;
                            Projectile.NewProjectile(source, player.Center, Projectile.velocity, Mod.Find<ModProjectile>("GrognakBeam").Type, (int)(Projectile.damage * 1.2f), Projectile.knockBack, Projectile.owner, speed * 1.8f);
                        }
                        else
                        {
                            Projectile.NewProjectile(source, player.Center, Projectile.velocity, Mod.Find<ModProjectile>("GrognakBeam").Type, (int)(Projectile.damage * 1.2f), Projectile.knockBack, Projectile.owner, speed);
                        }
                    }
                    if (Projectile.localAI[1] == 1)
                    {
                        player.fullRotation = (Projectile.ai[1] - startup) * 0.0174f * player.direction * (360 / (active + endlag));
                        player.mount.Dismount(player);
                    }
                    if (Projectile.ai[1] < startup + active)
                    {
                        Projectile.rotation = (player.fullRotation) + ((Projectile.ai[1] - startup) * 0.0174f * Projectile.direction * (180 / active)) + rad;
                    }
                    else
                    {
                        Projectile.rotation = (player.fullRotation) + (180 * 0.0174f * Projectile.direction) + rad;
                    }
                }
                if (Projectile.ai[1] >= startup + active + endlag)
                {
                    Projectile.Kill();
                }
                Projectile.spriteDirection = Projectile.direction;
                Projectile.timeLeft = 2;
                if (Projectile.ai[1] < startup)
                {
                    player.itemTime = (int)(40 * speed);
                    player.itemAnimation = (int)(40 * speed);
                }
                else if (Projectile.ai[1] <= startup + active)
                {
                    player.itemTime = (int)((40 * speed) - ((Projectile.ai[1] - startup) * speed * 4));
                    player.itemAnimation = (int)((40 * speed) - ((Projectile.ai[1] - startup) * speed * 4));
                }
                else
                {
                    player.itemTime = 2;
                    player.itemAnimation = 2;
                }
                player.itemRotation = Projectile.rotation - player.fullRotation;
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
            if (Projectile.ai[0] == 2)
            {
                if (Projectile.ai[1] < 1)
                {
                    player.velocity.Y = -18f * player.gravDir;
                    player.fallStart = (int)(player.position.Y / 16f);
                }
                if (Projectile.ai[1] % 51 == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item7.WithVolumeScale(1.2f).WithPitchOffset(-0.1f), Projectile.Center);
                }
                if (Projectile.ai[1] < 226)
                {
                    Projectile.ai[1] += 3;
                }
                Projectile.spriteDirection = Projectile.direction;
                Projectile.timeLeft = 2;
                player.mount.Dismount(player);
                if (player.gravDir == -1)
                {
                    Projectile.rotation = -(Projectile.ai[1] - 2) * 0.0174f * 7f * Projectile.direction - (float)(1.566 * player.direction);
                    player.fullRotation = (float)(Projectile.rotation - (3.14 * player.direction));
                }
                else
                {
                    Projectile.rotation = (Projectile.ai[1] - 2) * 0.0174f * 7f * Projectile.direction;
                    player.fullRotation = (float)(Projectile.rotation - (1.566 * player.direction));
                }
                player.itemTime = (int)(player.itemAnimationMax * 0.6);
                player.itemAnimation = (int)(player.itemAnimationMax * 0.6);
                player.itemRotation = 0;
                player.portalPhysicsFlag = true;
                if (Projectile.ai[1] >= 226 && player.velocity.Y == 0)
                {
                    player.fullRotation = 0f;
                    Projectile.Kill();
                }
            }
            player.fullRotationOrigin = player.Center - player.position;
            int length = 50;
            Projectile.position = (player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f) + new Vector2((float)Math.Cos(Projectile.rotation + 1.566 + (2.349 * Projectile.direction)) * length * Projectile.scale, (float)Math.Sin(Projectile.rotation + 1.566 + (2.349 * Projectile.direction)) * length * Projectile.scale);
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.direction == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = lightColor;
            sb.Draw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, effects, 0f);

            Texture2D gemTex = Mod.Assets.Request<Texture2D>("Projectiles/GrognakHammerGem").Value;
            Color gemColor = new Color(90, 255, (int)(51 + (Main.DiscoG * 0.75f)));
            sb.Draw(gemTex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), gemColor, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, effects, 0f);
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.localAI[1] == 1)
            {
                player.fullRotation = 0;
            }
            if (Projectile.ai[1] >= 226 && Projectile.ai[0] == 2)
            {
                Vector2 posi = new Vector2(player.Center.X + 64 * Projectile.direction * Projectile.scale, player.position.Y + player.height + 4);
                Point pos = posi.ToTileCoordinates();
                Tile tileSafely = Framing.GetTileSafely(pos.X, pos.Y);
                for (int d = 0; d < 80; d++)
                {
                    Dust dust = Main.dust[WorldGen.KillTile_MakeTileDust(pos.X, pos.Y, tileSafely)];
                    dust.velocity.X = dust.velocity.X + Main.rand.Next(-10, 10) * 0.3f;
                    dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-12, -6) * 0.4f;
                }
                SoundEngine.PlaySound(SoundID.Item70, Projectile.position);
                if (player.gravDir == -1)
                {
                    Projectile.NewProjectile(source, player.Center.X + 48 * Projectile.direction * Projectile.scale, Projectile.Center.Y, Projectile.direction * 8f, 0f, Mod.Find<ModProjectile>("GrogWaveFlipped").Type, (int)(Projectile.damage * 3.5f), Projectile.knockBack * 2.5f, Projectile.owner);
                }
                else
                {
                    Projectile.NewProjectile(source, player.Center.X + 48 * Projectile.direction * Projectile.scale, Projectile.Center.Y, Projectile.direction * 8f, 0f, Mod.Find<ModProjectile>("GrogWave").Type, (int)(Projectile.damage * 3.5f), Projectile.knockBack * 2.5f, Projectile.owner);
                }
                player.fullRotation = 0f;
            }
		}
    }
}