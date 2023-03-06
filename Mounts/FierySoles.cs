using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Mounts
{
	public class FierySoles : ModMount
	{
		public override void SetStaticDefaults()
		{
			MountData.spawnDust = 6;
			MountData.buff = Mod.Find<ModBuff>("FierySoles").Type;
			MountData.heightBoost = 20;
			MountData.fallDamage = 0f;
			MountData.runSpeed = 8.5f;
			MountData.dashSpeed = 11f;
			MountData.flightTimeMax = 75;
			MountData.fatigueMax = 0;
			MountData.jumpHeight = 6;
			MountData.acceleration = 0.2f;
			MountData.jumpSpeed = 8f;
			MountData.blockExtraJumps = false;
			MountData.totalFrames = 12;
			MountData.constantJump = true;
			int[] array = new int[MountData.totalFrames];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = 24;
			}
			MountData.playerYOffsets = array;
			MountData.xOffset = 10;
            MountData.yOffset = 4;
            MountData.bodyFrame = 0;
			MountData.playerHeadOffset = 22;
			MountData.standingFrameCount = 4;
			MountData.standingFrameDelay = 6;
			MountData.standingFrameStart = 0;
			MountData.runningFrameCount = 4;
			MountData.runningFrameDelay = 24;
			MountData.runningFrameStart = 0;
			MountData.flyingFrameCount = 4;
			MountData.flyingFrameDelay = 3;
			MountData.flyingFrameStart = 0;
			MountData.inAirFrameCount = 4;
			MountData.inAirFrameDelay = 5;
			MountData.inAirFrameStart = 0;
			MountData.idleFrameCount = 4;
			MountData.idleFrameDelay = 6;
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
            if(player.wet && !player.lavaWet)
            {
                player.mount.Dismount(player);
            }
            if (Main.rand.NextBool(5))
                Dust.NewDust(player.position + new Vector2(0, 40), player.width, 2, 6);
            if ((player.mount._flyTime > 0 && player.velocity != Vector2.Zero) || Main.rand.NextBool(10))
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