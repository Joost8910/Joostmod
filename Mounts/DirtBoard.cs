using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Mounts
{
	public class DirtBoard : ModMount
	{
		public override void SetStaticDefaults()
		{
			MountData.spawnDust = 0;
			MountData.buff = ModContent.BuffType<Buffs.DirtBoard>();
			MountData.heightBoost = 6;
			MountData.fallDamage = 1f;
			MountData.runSpeed = 0.5f;
            MountData.dashSpeed = 5f;
            MountData.flightTimeMax = 0;
			MountData.fatigueMax = 0;
			MountData.jumpHeight = 6;
			MountData.acceleration = 0f;
			MountData.jumpSpeed = 6f;
			MountData.blockExtraJumps = false;
			MountData.totalFrames = 1;
			MountData.constantJump = true;
			int[] array = new int[MountData.totalFrames];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = 6;
			}
			MountData.playerYOffsets = array;
            MountData.xOffset = 8;
            MountData.bodyFrame = 6;
            MountData.yOffset = 6;
            MountData.playerHeadOffset = 22;
			MountData.idleFrameLoop = false;
            if (Main.netMode != NetmodeID.Server)
            {
                MountData.textureWidth = MountData.backTexture.Width() + 20;
                MountData.textureHeight = MountData.backTexture.Height();
            }
        }
        public override void JumpSpeed(Player mountedPlayer, ref float jumpSpeed, float xVelocity)
        {
            jumpSpeed = Math.Abs(xVelocity * 0.5f) + 3f;
        }
        public override void UpdateEffects(Player player)
        {
            if (player.velocity.Y != 0)
            {
                player.fullRotation = (float)(Math.Atan2(player.velocity.Y * 0.25f, (Math.Abs(player.velocity.X) + 2) * player.direction)) + (player.direction == 1 ? 0 : (float)Math.PI);
                player.fullRotationOrigin = player.Center - player.position + new Vector2(0, player.height / 2);
                if (player.controlLeft)
                {
                    if (player.velocity.X > -2)
                    {
                        player.velocity.X -= 0.08f;
                    }
                }
                if (player.controlRight)
                {
                    if (player.velocity.X < 2)
                    {
                        player.velocity.X += 0.08f;
                    }
                }
                player.runSlowdown = 0;
            }
            else
            {
                if (player.velocity.X != 0)
                {
                    Dust.NewDustDirect(player.MountedCenter + new Vector2(player.velocity.X - 5, 26), 10, 4, 0, -player.velocity.X * 0.5f, Math.Abs(player.velocity.X) * -0.125f, 0, default, Main.rand.NextFloat() * 0.5f + 0.5f);
                }
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
            }
        }
    }
}