using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Mounts
{
	public class Plane : ModMountData
	{
		public override void SetDefaults()
		{
			mountData.spawnDust = 56;
			mountData.buff = mod.BuffType("PlaneMount");
			mountData.heightBoost = 20;
			mountData.fallDamage = 0f;
			mountData.runSpeed = 10f;
			mountData.dashSpeed = 10f;
			mountData.flightTimeMax = 600;
			mountData.fatigueMax = 0;
			mountData.jumpHeight = 1;
			mountData.acceleration = 0.1f;
			mountData.jumpSpeed = 1.5f;
			mountData.blockExtraJumps = false;
			mountData.totalFrames = 4;
			mountData.constantJump = true;
			int[] array = new int[mountData.totalFrames];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = 20;
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = 14;
			mountData.bodyFrame = 3;
			mountData.yOffset = -4;
			mountData.playerHeadOffset = 22;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 4;
			mountData.runningFrameDelay = 12;
			mountData.runningFrameStart = 0;
			mountData.flyingFrameCount = 4;
			mountData.flyingFrameDelay = 3;
			mountData.flyingFrameStart = 0;
			mountData.inAirFrameCount = 4;
			mountData.inAirFrameDelay = 10;
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
			//player.fullRotation = (float)(player.velocity.Y * 2.5f * 0.0174f * player.direction * player.gravDir);
			//player.fullRotation = (float)(Math.Atan2(player.velocity.Y, player.velocity.X) / (2 * Math.PI));
           	//player.fullRotationOrigin = player.Center - player.position;
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>(mod);
			modPlayer.planeMount = true;
        }
	}
}