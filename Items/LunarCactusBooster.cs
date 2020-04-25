using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	[AutoloadEquip(EquipType.Wings)]
	public class LunarCactusBooster : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Lunar Cactus Booster");
            Tooltip.SetDefault("Allows flight and slow fall\n" + "Hold UP while flying to quickly ascend\n" + "Hold DOWN while flying to quickly descend");
        }

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 26;
			item.value = 400000;
			item.rare = 10;
			item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 150;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 1f;
			ascentWhenRising = 0.5f;
			maxCanAscendMultiplier = 1.75f;
			maxAscentMultiplier = 3.5f;
			constantAscend = 0.15f;
            if (player.controlUp)
            {
                player.wingTime--;
                ascentWhenFalling = 4f;
                ascentWhenRising = 2f;
                maxCanAscendMultiplier = 3.5f;
                maxAscentMultiplier = 10f;
                constantAscend = 0.3f;
            }
            else if (player.controlDown)
            {
                player.wingTime += 0.5f;
                ascentWhenFalling = -0.5f;
                ascentWhenRising = -1f;
                maxCanAscendMultiplier = 3.5f;
                maxAscentMultiplier = 3.5f;
                constantAscend = -0.15f;
                player.portalPhysicsFlag = true;
                player._portalPhysicsTime++;
                player.maxFallSpeed = 50;
            }
        }

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 6f;
        }
        public override bool WingUpdate(Player player, bool inUse)
        {
            if (inUse || (player.controlJump && !player.controlDown && player.wingTime <= 0 && player.velocity.Y != 0))
            {
                player.rocketDelay2--;
                if (player.rocketDelay2 <= 0)
                {
                    Main.PlaySound(SoundID.Item24, player.position);
                    player.rocketDelay2 = inUse ? 25 : 35;
                }
                int num70 = 2;
                if (inUse)
                {
                    num70 = 5;
                    if (player.controlUp)
                    {
                        num70 = 7;
                    }
                    else if (player.controlDown && player.controlJump && player.velocity.Y > 12)
                    {
                        player.portalPhysicsFlag = true;
                        player._portalPhysicsTime++;
                    }
                }
                for (int num71 = 0; num71 < num70; num71++)
                {
                    int type = mod.DustType("LunarRing");
                    if (player.head == 41)
                    {
                        int num72 = player.body;
                    }
                    float scale = 1f;
                    int alpha = 100;
                    float x3 = player.Center.X + 9f;
                    if (player.direction > 0)
                    {
                        x3 = player.Center.X - 16f;
                    }
                    float num73 = player.Center.Y + 8f;
                    if (player.controlDown && inUse && !player.controlUp)
                    {
                        num73 = player.Center.Y - 10f;
                    }
                    if (num71 % 2 == 1)
                    {
                        x3 = player.Center.X - 7f;
                        if (player.direction > 0)
                        {
                            x3 = player.Center.X;
                        }
                        num73 -= 2f;
                        scale = 0.9f;
                    }
                    else if ((player.controlLeft || player.controlRight) && !(player.controlDown && !player.controlUp))
                    {
                        float x4 = x3;
                        int dir = 1;
                        if (player.controlRight)
                        {
                            dir = -1;
                        }
                        if (player.direction * -dir < 0)
                        {
                            x4 = player.Center.X - 7f;
                            if (player.direction > 0)
                            {
                                x4 = player.Center.X;
                            }
                        }
                        int numech = Dust.NewDust(new Vector2(x4, num73 - 20), 1, 1, type, dir*3, 0f, alpha, default(Color), scale);
                        Dust dust8 = Main.dust[numech];
                        dust8.velocity.Y = dust8.velocity.Y * 0.05f;
                        Main.dust[numech].velocity.X = Main.dust[numech].velocity.X - player.velocity.X * 0.3f;
                        Main.dust[numech].noGravity = true;
                        Main.dust[numech].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
                    }
                    if (num71 > 1 && num71 <= 4)
                    {
                        num73 += player.velocity.Y;
                    }
                    int num74 = Dust.NewDust(new Vector2(x3, num73), 1, 1, type, 0f, 0f, alpha, default(Color), scale);
                    Dust dust9 = Main.dust[num74];
                    dust9.velocity.X = dust9.velocity.X * 0.05f;
                    Main.dust[num74].velocity.Y = Main.dust[num74].velocity.Y * 1f + 2f * player.gravDir - player.velocity.Y * 0.3f;
                    Main.dust[num74].noGravity = true;
                    Main.dust[num74].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
                    if (num70 == 4)
                    {
                        Dust dust10 = Main.dust[num74];
                        dust10.velocity.Y = dust10.velocity.Y + 6f;
                    }
                }
                player.wingFrameCounter++;
                if (player.wingFrameCounter > 3)
                {
                    player.wingFrame++;
                    player.wingFrameCounter = 0;
                    if (player.wingFrame > 3)
                    {
                        player.wingFrame = 0;
                    }
                }
            }
            if (!player.controlJump || player.velocity.Y == 0f || player.jump > 0)
            {
                player.wingFrame = 0;
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cactus, 30);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}