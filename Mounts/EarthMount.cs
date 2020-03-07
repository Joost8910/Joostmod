using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Mounts
{
	public class EarthMount : ModMountData
	{
		public override void SetDefaults()
		{
			mountData.spawnDust = 1;
			mountData.buff = mod.BuffType("EarthMount");
			mountData.heightBoost = 14;
			mountData.fallDamage = 0f;
			mountData.runSpeed = 14f;
			mountData.dashSpeed = 14f;
			mountData.flightTimeMax = 0;
			mountData.fatigueMax = 0;
			mountData.jumpHeight = 20;
			mountData.acceleration = 0.05f;
			mountData.jumpSpeed = 8f;
			mountData.blockExtraJumps = false;
			mountData.totalFrames = 8;
			mountData.constantJump = true;
			int[] array = new int[mountData.totalFrames];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = 20;
            }
            array[0] = 14;
            array[1] = 16;
            array[2] = 18;
            mountData.playerYOffsets = array;
			mountData.xOffset = 16;
			mountData.bodyFrame = 0;
			mountData.yOffset = 7;
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
                Dust.NewDustDirect(player.Center + player.velocity + new Vector2(player.velocity.X - 20, 10), 30, 20, 1, -player.velocity.X, player.velocity.Y * -0.5f, 0, default, 1f).noGravity = true;
                Dust.NewDustDirect(player.Center + player.velocity + new Vector2(player.velocity.X - 15, 10), 30, 20, 1, -player.velocity.X, player.velocity.Y * -0.5f, 0, default, 1.5f).noGravity = true;
                Dust.NewDustDirect(player.Center + player.velocity + new Vector2(player.velocity.X - 10, 10), 30, 20, 1, -player.velocity.X, player.velocity.Y * -0.5f, 0, default, 2f).noGravity = true;
                Dust.NewDustDirect(player.Center + player.velocity + new Vector2(player.velocity.X - 15, 10), 30, 20, 1, -player.velocity.X * 0.5f, player.velocity.Y * -0.5f - 2, 0, default, Main.rand.NextFloat() + 1f);
            }
        }
        public override bool UpdateFrame(Player player, int state, Vector2 velocity)
        {
            //player.bodyFrame.Y = 0;
            mountData.bodyFrame = 0;
            player.legFrame.Y = 0;
            player.mount._frameCounter++;
            if (player.velocity.Y == 0 && (player.controlRight || player.controlLeft))
            {
                if (player.mount._frameCounter > ((player.mount._frame == 4 || player.mount._frame == 6) ? 18 : 5))
                {
                    player.mount._frame++;
                    player.mount._frameCounter = 0;
                }
                if (player.mount._frame >= 8)
                {
                    player.mount._frame -= 4;
                }
            }
            else if (player.velocity.Y == 0 && player.mount._frame > 0)
            {
                if (player.mount._frame > 4)
                {
                    player.mount._frame -= 4;
                }
                if (player.mount._frameCounter > 5)
                {
                    player.mount._frame--;
                    player.mount._frameCounter = 0;
                }
            }
            if (player.mount._frame == 0 || player.mount._frame == 4)
            {
                //player.bodyFrame.Y = 7 * player.bodyFrame.Height;
                mountData.bodyFrame = 10;
                player.legFrame.Y = 0 * player.legFrame.Height;
            }
            if (player.mount._frame == 1 || player.mount._frame == 3 || player.mount._frame == 5 || player.mount._frame == 7)
            {
                //player.bodyFrame.Y = 6 * player.bodyFrame.Height;
                mountData.bodyFrame = 13;
                player.legFrame.Y = 6 * player.legFrame.Height;
            }
            if (player.mount._frame == 2 || player.mount._frame == 6)
            {
                //player.bodyFrame.Y = 15 * player.bodyFrame.Height;
                mountData.bodyFrame = 17;
                player.legFrame.Y = 19 * player.legFrame.Height;
            }
            return false;
        }
    }
}