using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Mounts
{
	public class Plane : ModMount
	{
		public override void SetStaticDefaults()
		{
			MountData.spawnDust = 56;
			MountData.buff = ModContent.BuffType<Buffs.PlaneMount>();
			MountData.heightBoost = 20;
			MountData.fallDamage = 0f;
			MountData.runSpeed = 10f;
			MountData.dashSpeed = 10f;
			MountData.flightTimeMax = 600;
			MountData.fatigueMax = 0;
			MountData.jumpHeight = 1;
			MountData.acceleration = 0.1f;
			MountData.jumpSpeed = 1.5f;
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
			MountData.flyingFrameCount = 4;
			MountData.flyingFrameDelay = 3;
			MountData.flyingFrameStart = 0;
			MountData.inAirFrameCount = 4;
			MountData.inAirFrameDelay = 10;
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
		public override void UpdateEffects(Player player)
        {
			//player.fullRotation = (float)(player.velocity.Y * 2.5f * 0.0174f * player.direction * player.gravDir);
			//player.fullRotation = (float)(Math.Atan2(player.velocity.Y, player.velocity.X) / (2 * Math.PI));
           	//player.fullRotationOrigin = player.Center - player.position;
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			modPlayer.planeMount = true;
        }
	}
}