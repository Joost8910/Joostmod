using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Mounts
{
	public class SandShark : ModMount
	{
		public override void SetStaticDefaults()
		{
			MountData.spawnDust = 32;
			MountData.buff = ModContent.BuffType<Buffs.SandSharkMount>();
			MountData.heightBoost = 4;
			MountData.fallDamage = 0f;
			MountData.runSpeed = 1.5f;
			MountData.dashSpeed = 3f;
			MountData.flightTimeMax = 0;
			MountData.fatigueMax = 0;
			MountData.jumpHeight = 10;
			MountData.acceleration = 0.1f;
			MountData.jumpSpeed = 5f;
			MountData.blockExtraJumps = false;
			MountData.totalFrames = 4;
			MountData.constantJump = false;
			int[] array = new int[MountData.totalFrames];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = 6;
            }
            MountData.playerYOffsets = array;
			MountData.xOffset = 14;
			MountData.bodyFrame = 3;
			MountData.yOffset = 14;
			MountData.playerHeadOffset = 22;
			MountData.standingFrameCount = 1;
			MountData.standingFrameDelay = 12;
			MountData.standingFrameStart = 0;
			MountData.runningFrameCount = 4;
			MountData.runningFrameDelay = 24;
			MountData.runningFrameStart = 0;
			MountData.flyingFrameCount = 0;
			MountData.flyingFrameDelay = 0;
			MountData.flyingFrameStart = 0;
			MountData.inAirFrameCount = 4;
			MountData.inAirFrameDelay = 8;
			MountData.inAirFrameStart = 0;
			MountData.idleFrameCount = 4;
			MountData.idleFrameDelay = 12;
			MountData.idleFrameStart = 0;
			MountData.idleFrameLoop = false;
			MountData.swimFrameCount = 4;
			MountData.swimFrameDelay = 20;
			MountData.swimFrameStart = 0;
			if (Main.netMode != NetmodeID.Server)
			{
				MountData.textureWidth = MountData.backTexture.Width;
				MountData.textureHeight = MountData.backTexture.Height();
			}
		}

        public override void UpdateEffects(Player player)
        {
            Rectangle rect = new Rectangle((int)(player.position.X - 6), (int)(player.position.Y - 6), player.width + 12, player.height + MountData.heightBoost + 12);
            if (player.controlLeft)
            {
                rect.X -= 16;
            }
            if (player.controlRight)
            {
                rect.Width += 16;
            }
            if (player.controlJump)
            {
                rect.Y -= 16;
            }
            if (player.controlDown)
            {
                rect.Height += 16;
            }
            bool sand = false;
            for (int x = rect.X / 16; x <= (rect.X + rect.Width) / 16 && !sand; x++)
            {
                for (int y = rect.Y / 16; y <= (rect.Y + rect.Height) / 16 && !sand; y++)
                {
                    int type = Main.tile[x, y].TileType;
                    if (Main.tile[x, y].HasUnactuatedTile && (type == TileID.Sand || type == TileID.Pearlsand || type == TileID.Ebonsand || type == TileID.Crimsand || type == TileID.Sandstone || type == TileID.HardenedSand || type == TileID.HallowHardenedSand || type == TileID.CorruptHardenedSand || type == TileID.CrimsonHardenedSand || type == TileID.HallowSandstone || type == TileID.CorruptSandstone || type == TileID.CrimsonSandstone || type == TileID.DesertFossil))
                    {
                        sand = true;
                    }
                }
            }
            float moveSpeed = MountData.dashSpeed * 2.5f * player.GetModPlayer<JoostPlayer>().accRunSpeedMult;
            float jumpSpeed = MountData.jumpSpeed * 2;
            float accel = MountData.acceleration;
            if (player.controlRight && player.GetModPlayer<JoostPlayer>().sandSharkVel.X < moveSpeed)
            {
                if (player.velocity.X < 0)
                {
                    player.GetModPlayer<JoostPlayer>().sandSharkVel.X = 0;
                }
                else
                {
                    player.GetModPlayer<JoostPlayer>().sandSharkVel.X = (player.GetModPlayer<JoostPlayer>().sandSharkVel.X + accel > moveSpeed) ? moveSpeed : player.GetModPlayer<JoostPlayer>().sandSharkVel.X + accel;
                }
            }
            if (player.controlLeft && player.GetModPlayer<JoostPlayer>().sandSharkVel.X > -moveSpeed)
            {
                if (player.velocity.X > 0)
                {
                    player.GetModPlayer<JoostPlayer>().sandSharkVel.X = 0;
                }
                else
                {
                    player.GetModPlayer<JoostPlayer>().sandSharkVel.X = (player.GetModPlayer<JoostPlayer>().sandSharkVel.X - accel < -moveSpeed) ? -moveSpeed : player.GetModPlayer<JoostPlayer>().sandSharkVel.X - accel;
                }
            }
            if (player.controlJump && player.GetModPlayer<JoostPlayer>().sandSharkVel.Y > -jumpSpeed)
            {
                if (player.velocity.Y >= 0)
                {
                    if (player.velocity.Y == 0)
                    {
                        player.GetModPlayer<JoostPlayer>().sandSharkVel.Y = -jumpSpeed;
                    }
                    else
                    {
                        player.GetModPlayer<JoostPlayer>().sandSharkVel.Y = 0;
                    }
                }
                else
                {
                    player.GetModPlayer<JoostPlayer>().sandSharkVel.Y = (player.GetModPlayer<JoostPlayer>().sandSharkVel.Y - accel < -jumpSpeed) ? -jumpSpeed : player.GetModPlayer<JoostPlayer>().sandSharkVel.Y - accel;
                }
            }
            if (player.controlDown)
            {
                if (player.velocity.Y <= 0)
                {
                    player.GetModPlayer<JoostPlayer>().sandSharkVel.Y = 0;
                }
                else if (player.GetModPlayer<JoostPlayer>().sandSharkVel.Y < jumpSpeed)
                {
                    player.GetModPlayer<JoostPlayer>().sandSharkVel.Y = (player.GetModPlayer<JoostPlayer>().sandSharkVel.Y + accel > jumpSpeed) ? jumpSpeed : player.GetModPlayer<JoostPlayer>().sandSharkVel.Y + accel;
                }
            }
            if (sand)
            {
                player.buffImmune[BuffID.Suffocation] = true;
                player.suffocating = false;
                player.noFallDmg = true;
                player.velocity = Collision.AdvancedTileCollision(TileID.Sets.ForAdvancedCollision.ForSandshark, rect.TopLeft(), player.velocity, rect.Width, rect.Height, player.controlDown, player.controlDown, (int)player.gravDir);
                if (player.gravDir > 0)
                {
                    if (player.controlJump && player.velocity.Y > -jumpSpeed)
                    {
                        if (player.velocity.Y <= 0)
                        {
                            player.velocity.Y = player.GetModPlayer<JoostPlayer>().sandSharkVel.Y;
                        }
                        player.velocity.Y -= 1f;
                        player.position.Y += player.velocity.Y / 4;
                    }
                    if (player.controlDown && player.velocity.Y < jumpSpeed)
                    {
                        if (player.velocity.Y >= 0)
                        {
                            player.velocity.Y = player.GetModPlayer<JoostPlayer>().sandSharkVel.Y;
                        }
                        player.velocity.Y += 1f;
                        player.position.Y += player.velocity.Y / 4;
                    }
                }
                else
                {
                    if (player.controlJump && player.velocity.Y < jumpSpeed)
                    {
                        if (player.velocity.Y >= 0)
                        {
                            player.velocity.Y = player.GetModPlayer<JoostPlayer>().sandSharkVel.Y;
                        }
                        player.velocity.Y += 1f;
                        player.position.Y += player.velocity.Y / 4;
                    }
                    if (player.controlDown && player.velocity.Y > -jumpSpeed)
                    {
                        if (player.velocity.Y <= 0)
                        {
                            player.velocity.Y = player.GetModPlayer<JoostPlayer>().sandSharkVel.Y;
                        }
                        player.velocity.Y -= 1f;
                        player.position.Y += player.velocity.Y / 4;
                    }
                }
                if (player.controlRight)
                {
                    if (player.velocity.X < moveSpeed)
                    {
                        if (player.velocity.X >= 0)
                        {
                            player.velocity.X = player.GetModPlayer<JoostPlayer>().sandSharkVel.X;
                        }
                        player.velocity.X++;
                    }
                    player.position.X += player.velocity.X / 4;
                    player.direction = 1;
                }
                if (player.controlLeft)
                {
                    if (player.velocity.X > -moveSpeed)
                    {
                        if (player.velocity.X <= 0)
                        {
                            player.velocity.X = player.GetModPlayer<JoostPlayer>().sandSharkVel.X;
                        }
                        player.velocity.X--;
                    }
                    player.position.X += player.velocity.X / 4;
                    player.direction = -1;
                }
                if (Collision.SlopeCollision(player.position, player.velocity, player.width, player.height + 2, player.gravity, player.stairFall) != new Vector4(player.position, player.velocity.X, player.velocity.Y))
                {
                    if (player.controlDown)
                    {
                        player.position.Y += player.height;
                    }
                    if (player.controlJump)
                    {
                        player.position.Y -= player.height;
                    }
                }
                player.runSoundDelay += (int)player.velocity.Length();
                if (player.runSoundDelay > 40)
                {
                    player.runSoundDelay = 0;
                    SoundEngine.PlaySound(SoundID.WormDig.WithVolumeScale(0.8f), player.Center);
                }
            }
            else if (player.wet)
            {
                if (!player.controlJump)
                {
                    if (player.GetModPlayer<JoostPlayer>().sandSharkVel.Y < 0)
                        player.GetModPlayer<JoostPlayer>().sandSharkVel.Y = 0;
                    else
                        player.GetModPlayer<JoostPlayer>().sandSharkVel.Y += 0.1f;
                }
                player.velocity = player.GetModPlayer<JoostPlayer>().sandSharkVel;
                player.GetModPlayer<JoostPlayer>().sandSharkVel *= 0.99f;
            }
            if (player.velocity.Y != 0)
            {
                player.fullRotation = (float)(Math.Atan2(player.velocity.Y * 0.25f, (Math.Abs(player.velocity.X) + 2) * player.direction)) + (player.direction == 1 ? 0 : (float)Math.PI);
                player.fullRotationOrigin = player.Center - player.position + new Vector2(0, player.height / 2);
            }
            else
            {
                player.fullRotation = 0;
            }
        }
    }
}