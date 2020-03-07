using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Mounts
{
	public class SandShark : ModMountData
	{
		public override void SetDefaults()
		{
			mountData.spawnDust = 32;
			mountData.buff = mod.BuffType("SandSharkMount");
			mountData.heightBoost = 4;
			mountData.fallDamage = 0f;
			mountData.runSpeed = 1.5f;
			mountData.dashSpeed = 3f;
			mountData.flightTimeMax = 0;
			mountData.fatigueMax = 0;
			mountData.jumpHeight = 10;
			mountData.acceleration = 0.1f;
			mountData.jumpSpeed = 5f;
			mountData.blockExtraJumps = false;
			mountData.totalFrames = 4;
			mountData.constantJump = false;
			int[] array = new int[mountData.totalFrames];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = 6;
            }
            mountData.playerYOffsets = array;
			mountData.xOffset = 14;
			mountData.bodyFrame = 3;
			mountData.yOffset = 14;
			mountData.playerHeadOffset = 22;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 4;
			mountData.runningFrameDelay = 24;
			mountData.runningFrameStart = 0;
			mountData.flyingFrameCount = 0;
			mountData.flyingFrameDelay = 0;
			mountData.flyingFrameStart = 0;
			mountData.inAirFrameCount = 4;
			mountData.inAirFrameDelay = 8;
			mountData.inAirFrameStart = 0;
			mountData.idleFrameCount = 4;
			mountData.idleFrameDelay = 12;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = false;
			mountData.swimFrameCount = 4;
			mountData.swimFrameDelay = 20;
			mountData.swimFrameStart = 0;
			if (Main.netMode != 2)
			{
				mountData.textureWidth = mountData.backTexture.Width;
				mountData.textureHeight = mountData.backTexture.Height;
			}
		}

        public override void UpdateEffects(Player player)
        {
            Rectangle rect = new Rectangle((int)(player.position.X - 6), (int)(player.position.Y - 6), player.width + 12, player.height + mountData.heightBoost + 12);
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
                    int type = Main.tile[x, y].type;
                    if (Main.tile[x, y].nactive() && (type == TileID.Sand || type == TileID.Pearlsand || type == TileID.Ebonsand || type == TileID.Crimsand || type == TileID.Sandstone || type == TileID.HardenedSand || type == TileID.HallowHardenedSand || type == TileID.CorruptHardenedSand || type == TileID.CrimsonHardenedSand || type == TileID.HallowSandstone || type == TileID.CorruptSandstone || type == TileID.CrimsonSandstone || type == TileID.DesertFossil))
                    {
                        sand = true;
                    }
                }
            }
            float moveSpeed = mountData.dashSpeed * 2.5f * player.GetModPlayer<JoostPlayer>().accRunSpeedMult;
            float jumpSpeed = mountData.jumpSpeed * 2;
            float accel = mountData.acceleration;
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
                    Main.PlaySound(15, (int)player.Center.X, (int)player.Center.Y, 1, 0.8f);
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