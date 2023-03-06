using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Mounts
{
	public class AirScooter : ModMount
	{
		public override void SetStaticDefaults()
		{
			MountData.spawnDust = 31;
			MountData.buff = Mod.Find<ModBuff>("AirScooterMount").Type;
			MountData.heightBoost = 28;
			MountData.fallDamage = 0f;
			MountData.runSpeed = 8f;
			MountData.dashSpeed = 10f;
			MountData.flightTimeMax = 150;
			MountData.fatigueMax = 0;
			MountData.jumpHeight = 6;
			MountData.acceleration = 0.3f;
			MountData.jumpSpeed = 7f;
			MountData.blockExtraJumps = false;
			MountData.totalFrames = 4;
			MountData.constantJump = true;
			int[] array = new int[MountData.totalFrames];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = 30;
			}
			MountData.playerYOffsets = array;
			MountData.xOffset = 10;
            MountData.bodyFrame = 6;
            MountData.yOffset = 2;
			MountData.playerHeadOffset = 22;
			MountData.standingFrameCount = 4;
			MountData.standingFrameDelay = 8;
			MountData.standingFrameStart = 0;
			MountData.runningFrameCount = 4;
			MountData.runningFrameDelay = 20;
			MountData.runningFrameStart = 0;
			MountData.flyingFrameCount = 4;
			MountData.flyingFrameDelay = 4;
			MountData.flyingFrameStart = 0;
			MountData.inAirFrameCount = 4;
			MountData.inAirFrameDelay = 6;
			MountData.inAirFrameStart = 0;
			MountData.idleFrameCount = 4;
			MountData.idleFrameDelay = 8;
			MountData.idleFrameStart = 0;
			MountData.idleFrameLoop = false;
			MountData.swimFrameCount = MountData.inAirFrameCount;
			MountData.swimFrameDelay = MountData.inAirFrameDelay;
			MountData.swimFrameStart = MountData.inAirFrameStart;
			if (Main.netMode != 2)
			{
				MountData.textureWidth = MountData.backTexture.Width + 20;
				MountData.textureHeight = MountData.backTexture.Height;
			}
		}

        public override void UpdateEffects(Player player)
        {
            if (player.mount._flyTime > 0 || Main.rand.NextBool(5))
            {
                Dust.NewDust(player.position + new Vector2(0, 40), player.width, 40, 31, 0, 0, 0, Color.White, Main.rand.NextFloat() * 0.5f + 0.25f);
            }
        }
    }
}