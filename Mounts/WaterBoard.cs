using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Mounts
{
	public class WaterBoard : ModMountData
	{
		public override void SetDefaults()
		{
			mountData.spawnDust = 172;
			mountData.buff = mod.BuffType("WaterBoard");
			mountData.heightBoost = 12;
			mountData.fallDamage = 0f;
			mountData.runSpeed = 7.5f;
			mountData.dashSpeed = 13f;
			mountData.flightTimeMax = 0;
			mountData.fatigueMax = 0;
			mountData.jumpHeight = 60;
			mountData.acceleration = 0.15f;
			mountData.jumpSpeed = 6f;
			mountData.blockExtraJumps = false;
			mountData.totalFrames = 4;
			mountData.constantJump = true;
			int[] array = new int[mountData.totalFrames];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = 16;
            }
            array[0] = 10;
            array[1] = 12;
            array[2] = 14;
            mountData.playerYOffsets = array;
			mountData.xOffset = 16;
			mountData.bodyFrame = 6;
			mountData.yOffset = 10;
			mountData.playerHeadOffset = 22;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 4;
			mountData.runningFrameDelay = 12;
			mountData.runningFrameStart = 0;
			mountData.flyingFrameCount = 0;
			mountData.flyingFrameDelay = 0;
			mountData.flyingFrameStart = 0;
			mountData.inAirFrameCount = 1;
			mountData.inAirFrameDelay = 12;
			mountData.inAirFrameStart = 0;
			mountData.idleFrameCount = 4;
			mountData.idleFrameDelay = 12;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = false;
			mountData.swimFrameCount = mountData.inAirFrameCount;
			mountData.swimFrameDelay = mountData.inAirFrameDelay;
			mountData.swimFrameStart = mountData.inAirFrameStart;
			if (Main.netMode != 2)
			{
				mountData.textureWidth = mountData.backTexture.Width + 20;
				mountData.textureHeight = mountData.backTexture.Height;
			}
		}

        public override void UpdateEffects(Player player)
        {
            if (player.velocity != Vector2.Zero)
            {
                Dust.NewDustDirect(player.position + new Vector2(player.direction * 10 + player.velocity.X - 20, 40), 60, 20, 33, -player.velocity.X, player.velocity.Y * -0.5f, 0, default, Main.rand.NextFloat() + 1f).noGravity = true;
                Dust.NewDustDirect(player.position + new Vector2(player.direction * 10 + player.velocity.X - 20, 40), 60, 20, 172, -player.velocity.X, player.velocity.Y * -0.5f, 0, default, 2f).noGravity = true;
                Dust.NewDustDirect(player.position + new Vector2(player.direction * 10 + player.velocity.X - 20, 40), 60, 20, 172, -player.velocity.X, player.velocity.Y * -0.5f, 0, default, 1f).noGravity = true;
                Dust.NewDustDirect(player.position + new Vector2(player.direction * 10 + player.velocity.X - 20, 40), 60, 20, 33, -player.velocity.X * 0.5f, player.velocity.Y * -0.5f - 2, 0, default, Main.rand.NextFloat() + 1f);
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
            if (player.wet)
            {
                if (player.gravDir > 0)
                {
                    if (player.controlJump && player.velocity.Y > -player.maxRunSpeed)
                    {
                        player.velocity.Y -= 0.25f;
                    }
                }
                else
                {
                    if (player.controlJump && player.velocity.Y < player.maxRunSpeed)
                    {
                        player.velocity.Y += 0.25f;
                    }
                }
            }
        }
        public override bool UpdateFrame(Player player, int state, Vector2 velocity)
        {
            player.legFrame.Y = 0;
            player.mount._frameCounter++;
            if (player.velocity.Y == 0 && (player.controlRight || player.controlLeft))
            {
                if (player.mount._frameCounter > 5 && player.mount._frame < 3)
                {
                    player.mount._frame++;
                    player.mount._frameCounter = 0;
                }
                if (player.mount._frame >= 4)
                {
                    player.mount._frame = 3;
                }
            }
            else if (player.velocity.Y == 0 && player.mount._frame > 0)
            {
                if (player.mount._frameCounter > 5)
                {
                    player.mount._frame--;
                    player.mount._frameCounter = 0;
                }
            }
            else if (player.velocity.Y != 0)
            {
                player.mount._frame = 3;
            }
            return false;
        }
    }
}