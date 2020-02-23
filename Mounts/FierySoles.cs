using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Mounts
{
	public class FierySoles : ModMountData
	{
		public override void SetDefaults()
		{
			mountData.spawnDust = 6;
			mountData.buff = mod.BuffType("FierySoles");
			mountData.heightBoost = 20;
			mountData.fallDamage = 0f;
			mountData.runSpeed = 8.5f;
			mountData.dashSpeed = 11f;
			mountData.flightTimeMax = 60;
			mountData.fatigueMax = 0;
			mountData.jumpHeight = 6;
			mountData.acceleration = 0.2f;
			mountData.jumpSpeed = 8f;
			mountData.blockExtraJumps = false;
			mountData.totalFrames = 12;
			mountData.constantJump = true;
			int[] array = new int[mountData.totalFrames];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = 24;
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = 10;
            mountData.yOffset = 4;
            mountData.bodyFrame = 0;
			mountData.playerHeadOffset = 22;
			mountData.standingFrameCount = 4;
			mountData.standingFrameDelay = 6;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 4;
			mountData.runningFrameDelay = 24;
			mountData.runningFrameStart = 0;
			mountData.flyingFrameCount = 4;
			mountData.flyingFrameDelay = 3;
			mountData.flyingFrameStart = 0;
			mountData.inAirFrameCount = 4;
			mountData.inAirFrameDelay = 5;
			mountData.inAirFrameStart = 0;
			mountData.idleFrameCount = 4;
			mountData.idleFrameDelay = 6;
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
            if(player.wet && !player.lavaWet)
            {
                player.mount.Dismount(player);
            }
            if (Main.rand.NextBool(5))
                Dust.NewDust(player.position + new Vector2(0, 40), player.width, 2, 6);
            if (player.velocity != Vector2.Zero || Main.rand.NextBool(10))
            {
                Dust.NewDustPerfect(player.MountedCenter + new Vector2(-1, 20), 6, player.velocity * -0.5f + new Vector2(0, 2), 0, default, 2).noGravity = true;
                Dust.NewDustPerfect(player.MountedCenter + new Vector2(3, 20), 6, player.velocity * -0.5f + new Vector2(0, 2), 0, default, 2).noGravity = true;
            }
        }
        public override bool UpdateFrame(Player player, int state, Vector2 velocity)
        {
            player.legFrame.Y = 0;
            player.mount._frameCounter++;
            if ((player.direction > 0 && player.controlRight) || (player.direction < 0 && player.controlLeft))
            {
                if (player.mount._frame < 4)
                {
                    player.mount._frame += 4;
                }
                if (player.mount._frameCounter > 2)
                {
                    player.mount._frame++;
                    player.mount._frameCounter = 0;
                }
                if (player.mount._frame >= 8)
                {
                    player.mount._frame -= 4;
                }
            }
            else if ((player.direction > 0 && player.controlLeft) || (player.direction < 0 && player.controlRight))
            {
                if (player.mount._frame < 8)
                {
                    player.mount._frame = 8;
                }
                if (player.mount._frameCounter > 2)
                {
                    player.mount._frame++;
                    player.mount._frameCounter = 0;
                }
                if (player.mount._frame >= 12)
                {
                    player.mount._frame -= 4;
                }
            }
            else
            {
                if (player.mount._frameCounter >= (player.controlJump ? 3 : 5))
                {
                    player.mount._frame++;
                    player.mount._frameCounter = 0;
                }
                if (player.mount._frame >= 4)
                {
                    player.mount._frame = 0;
                }
            }
            return false;
        }
    }
}