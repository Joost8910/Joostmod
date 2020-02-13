using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Mounts
{
	public class AirScooter : ModMountData
	{
		public override void SetDefaults()
		{
			mountData.spawnDust = 31;
			mountData.buff = mod.BuffType("AirScooterMount");
			mountData.heightBoost = 28;
			mountData.fallDamage = 0f;
			mountData.runSpeed = 11f;
			mountData.dashSpeed = 11f;
			mountData.flightTimeMax = 120;
			mountData.fatigueMax = 0;
			mountData.jumpHeight = 12;
			mountData.acceleration = 0.25f;
			mountData.jumpSpeed = 8f;
			mountData.blockExtraJumps = false;
			mountData.totalFrames = 4;
			mountData.constantJump = true;
			int[] array = new int[mountData.totalFrames];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = 30;
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = 10;
			mountData.yOffset = 2;
			mountData.playerHeadOffset = 22;
			mountData.standingFrameCount = 4;
			mountData.standingFrameDelay = 8;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 4;
			mountData.runningFrameDelay = 20;
			mountData.runningFrameStart = 0;
			mountData.flyingFrameCount = 4;
			mountData.flyingFrameDelay = 4;
			mountData.flyingFrameStart = 0;
			mountData.inAirFrameCount = 4;
			mountData.inAirFrameDelay = 6;
			mountData.inAirFrameStart = 0;
			mountData.idleFrameCount = 4;
			mountData.idleFrameDelay = 8;
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

	}
}