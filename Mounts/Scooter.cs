using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Mounts
{
	public class Scooter : ModMount
	{
		public override void SetStaticDefaults()
		{
			MountData.spawnDust = 56;
			MountData.buff = ModContent.BuffType<Buffs.ScooterMount>();
			MountData.heightBoost = 20;
			MountData.fallDamage = 1f;
			MountData.runSpeed = 7f;
			MountData.dashSpeed = 7f;
			MountData.flightTimeMax = 0;
			MountData.fatigueMax = 0;
			MountData.jumpHeight = 7;
			MountData.acceleration = 0.08f;
			MountData.jumpSpeed = 6f;
			MountData.blockExtraJumps = false;
			MountData.totalFrames = 4;
			MountData.constantJump = true;
			int[] array = new int[MountData.totalFrames];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = 20;
			}
			MountData.playerYOffsets = array;
			MountData.xOffset = 14;
			MountData.bodyFrame = 3;
			MountData.yOffset = -4;
			MountData.playerHeadOffset = 22;
			MountData.standingFrameCount = 1;
			MountData.standingFrameDelay = 12;
			MountData.standingFrameStart = 0;
			MountData.runningFrameCount = 4;
			MountData.runningFrameDelay = 12;
			MountData.runningFrameStart = 0;
			MountData.flyingFrameCount = 0;
			MountData.flyingFrameDelay = 0;
			MountData.flyingFrameStart = 0;
			MountData.inAirFrameCount = 1;
			MountData.inAirFrameDelay = 12;
			MountData.inAirFrameStart = 0;
			MountData.idleFrameCount = 4;
			MountData.idleFrameDelay = 12;
			MountData.idleFrameStart = 0;
			MountData.idleFrameLoop = false;
			MountData.swimFrameCount = MountData.inAirFrameCount;
			MountData.swimFrameDelay = MountData.inAirFrameDelay;
			MountData.swimFrameStart = MountData.inAirFrameStart;
			if (Main.netMode != NetmodeID.Server)
			{
				MountData.textureWidth = MountData.backTexture.Width() + 20;
				MountData.textureHeight = MountData.backTexture.Height();
			}
		}

	}
}