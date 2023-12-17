using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Mounts
{
	public class EarthMount : ModMount
	{
		public override void SetStaticDefaults()
		{
			MountData.spawnDust = 1;
			MountData.buff = ModContent.BuffType<Buffs.EarthMount>();
			MountData.heightBoost = 14;
			MountData.fallDamage = 0f;
			MountData.runSpeed = 14.75f;
			MountData.dashSpeed = 14.75f;
			MountData.flightTimeMax = 0;
			MountData.fatigueMax = 0;
			MountData.jumpHeight = 20;
			MountData.acceleration = 0.05f;
			MountData.jumpSpeed = 8f;
			MountData.blockExtraJumps = false;
			MountData.totalFrames = 8;
			MountData.constantJump = true;
			int[] array = new int[MountData.totalFrames];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = 20;
            }
            array[0] = 14;
            array[1] = 16;
            array[2] = 18;
            MountData.playerYOffsets = array;
			MountData.xOffset = 16;
			MountData.bodyFrame = 0;
			MountData.yOffset = 7;
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
            MountData.bodyFrame = 0;
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
                MountData.bodyFrame = 10;
                player.legFrame.Y = 0 * player.legFrame.Height;
            }
            if (player.mount._frame == 1 || player.mount._frame == 3 || player.mount._frame == 5 || player.mount._frame == 7)
            {
                //player.bodyFrame.Y = 6 * player.bodyFrame.Height;
                MountData.bodyFrame = 13;
                player.legFrame.Y = 6 * player.legFrame.Height;
            }
            if (player.mount._frame == 2 || player.mount._frame == 6)
            {
                //player.bodyFrame.Y = 15 * player.bodyFrame.Height;
                MountData.bodyFrame = 17;
                player.legFrame.Y = 19 * player.legFrame.Height;
            }
            return false;
        }
    }
}