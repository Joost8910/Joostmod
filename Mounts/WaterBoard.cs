using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Mounts
{
	public class WaterBoard : ModMount
	{
		public override void SetStaticDefaults()
		{
			MountData.spawnDust = 172;
			MountData.buff = ModContent.BuffType<Buffs.WaterBoard>();
			MountData.heightBoost = 12;
			MountData.fallDamage = 0f;
			MountData.runSpeed = 7.5f;
			MountData.dashSpeed = 11.8f;
			MountData.flightTimeMax = 45;
			MountData.fatigueMax = 0;
			MountData.jumpHeight = 6;
			MountData.acceleration = 0.15f;
			MountData.jumpSpeed = 5f;
			MountData.blockExtraJumps = false;
			MountData.totalFrames = 4;
			MountData.constantJump = true;
			int[] array = new int[MountData.totalFrames];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = 16;
            }
            array[0] = 10;
            array[1] = 12;
            array[2] = 14;
            MountData.playerYOffsets = array;
			MountData.xOffset = 16;
			MountData.bodyFrame = 6;
			MountData.yOffset = 10;
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
        public override void JumpSpeed(Player mountedPlayer, ref float jumpSpeed, float xVelocity)
        {
            jumpSpeed += Math.Abs(xVelocity * 0.25f);
        }
        public override void UpdateEffects(Player player)
        {
            if (player.velocity != Vector2.Zero)
            {
                if (player.mount._flyTime > 0)
                {
                    Dust.NewDustDirect(player.position + new Vector2(player.direction * 10 + player.velocity.X - 20, 40), 60, 20, 172, -player.velocity.X, player.velocity.Y * -0.5f, 0, default, 2f).noGravity = true;
                    Dust.NewDustDirect(player.position + new Vector2(player.direction * 10 + player.velocity.X - 20, 40), 60, 20, 172, -player.velocity.X, player.velocity.Y * -0.5f, 0, default, 1f).noGravity = true;
                }
                Dust.NewDustDirect(player.position + new Vector2(player.direction * 10 + player.velocity.X - 20, 40), 60, 20, 33, -player.velocity.X, player.velocity.Y * -0.5f, 0, default, Main.rand.NextFloat() + 1f).noGravity = true;
                Dust.NewDustDirect(player.position + new Vector2(player.direction * 10 + player.velocity.X - 20, 40), 60, 20, 33, -player.velocity.X * 0.5f, player.velocity.Y * -0.5f - 2, 0, default, Main.rand.NextFloat() + 1f);
            }
            if (player.velocity.Y != 0)
            {
                player.fullRotation = (float)(Math.Atan2(player.velocity.Y * 0.25f, (Math.Abs(player.velocity.X) + 2) * player.direction)) + (player.direction == 1 ? 0 : (float)Math.PI);
                player.fullRotationOrigin = player.Center - player.position + new Vector2(0, player.height / 2);
                if (player.controlLeft)
                {
                    if (player.velocity.X > -player.maxRunSpeed)
                    {
                        player.velocity.X -= player.runAcceleration;
                    }
                }
                if (player.controlRight)
                {
                    if (player.velocity.X < player.maxRunSpeed)
                    {
                        player.velocity.X += player.runAcceleration;
                    }
                }
                player.runSlowdown = 0;
            }
            else
            {
                bool slope = false;
                float rotation = 0;
                float rampVel = 0;
                float rampMult = 0.75f;
                float loopVel = 0;
                float loopMult = 0.75f;
                int x = (int)(player.position.X / 16f);
                int x2 = (int)((player.position.X + player.width) / 16f);
                int y = (int)((player.position.Y + player.height) / 16f);
                if ((!Main.tile[x, y].IsHalfBlock && Main.tile[x, y].HasTile && Main.tileSolid[Main.tile[x, y].TileType] && (Main.tile[x2, y].IsHalfBlock || Main.tile[x2, y].Slope == SlopeType.SlopeDownLeft || !Main.tile[x2, y].HasTile || !Main.tileSolid[Main.tile[x2, y].TileType])) || (Main.tile[x, y].IsHalfBlock && (!Main.tile[x2, y].HasTile || !Main.tileSolid[Main.tile[x2, y].TileType])))
                {
                    rotation = (float)Math.PI / 8;
                    if (player.velocity.X >= 0)
                    {
                        rampVel = Math.Abs(player.velocity.Y) * 0.75f;
                    }
                    else
                    {
                        if (Math.Abs(player.velocity.X) > 0)
                        {
                            player.velocity.Y = -Math.Abs(player.velocity.X) * rampMult * 0.75f;
                        }
                        loopVel = Math.Abs(player.velocity.X) * loopMult * 0.75f;
                    }
                    player.velocity.X += player.gravity * rampMult * 0.75f;
                    player.slippy = true;
                    slope = true;
                }

                if (((Main.tile[x, y].IsHalfBlock || Main.tile[x, y].Slope == SlopeType.SlopeDownRight || !Main.tile[x, y].HasTile || !Main.tileSolid[Main.tile[x, y].TileType]) && !Main.tile[x2, y].IsHalfBlock && Main.tile[x2, y].HasTile && Main.tileSolid[Main.tile[x2, y].TileType]) || (Main.tile[x2, y].IsHalfBlock && (!Main.tile[x, y].HasTile || !Main.tileSolid[Main.tile[x, y].TileType])))
                {
                    rotation = -(float)Math.PI / 8;
                    if (player.velocity.X <= 0)
                    {
                        rampVel = Math.Abs(player.velocity.Y) * 0.75f;
                    }
                    else
                    {
                        if (Math.Abs(player.velocity.X) > 0)
                        {
                            player.velocity.Y = -Math.Abs(player.velocity.X) * rampMult * 0.75f;
                        }
                        loopVel = Math.Abs(player.velocity.X) * loopMult * 0.75f;
                    }
                    player.velocity.X -= player.gravity * rampMult * 0.75f;
                    player.slippy = true;
                    slope = true;
                }

                //Full slope
                if (Main.tile[x, y].Slope == SlopeType.SlopeDownLeft || Main.tile[x2, y + 1].Slope == SlopeType.SlopeDownLeft)
                {
                    rotation = (float)Math.PI / 4;
                    if (player.velocity.X >= 0)
                    {
                        rampVel = Math.Abs(player.velocity.Y);
                    }
                    else
                    {
                        if (Math.Abs(player.velocity.X) > 0)
                        {
                            player.velocity.Y = -Math.Abs(player.velocity.X) * rampMult;
                        }
                        loopVel = Math.Abs(player.velocity.X) * loopMult;
                    }
                    player.velocity.X += player.gravity * rampMult;
                    player.slippy2 = true;
                    slope = true;
                }
                if (Main.tile[x, y + 1].Slope == SlopeType.SlopeDownRight || Main.tile[x2, y].Slope == SlopeType.SlopeDownRight)
                {
                    rotation = -(float)Math.PI / 4;
                    if (player.velocity.X <= 0)
                    {
                        rampVel = Math.Abs(player.velocity.Y);
                    }
                    else
                    {
                        if (Math.Abs(player.velocity.X) > 0)
                        {
                            player.velocity.Y = -Math.Abs(player.velocity.X) * rampMult;
                        }
                        loopVel = Math.Abs(player.velocity.X) * loopMult;
                    }
                    player.velocity.X -= player.gravity * rampMult;
                    player.slippy2 = true;
                    slope = true;
                }
                player.fullRotationOrigin = player.Center - player.position;
                player.fullRotation = rotation;

                if (player.velocity.X == 0 && loopVel != 0)
                {
                    player.velocity.Y += -loopVel * player.gravDir;
                }
                if (player.velocity.Y == 0 && rampVel != 0)
                {
                    player.velocity.X += rampVel * Math.Sign(player.velocity.X);
                }
                if (slope)
                {
                    player.mount.ResetFlightTime(player.velocity.X);
                }
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