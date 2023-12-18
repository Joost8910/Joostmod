using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Consumables
{
    public class Cactusofdoom : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mustachioed Cactus");
            Tooltip.SetDefault("Summons the Jumbo Cactuar in the desert");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 36;
            Item.height = 46;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.value = 10000;
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item1;

        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(106, 63, 202);
                }
            }
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ZoneDesert && !NPC.AnyNPCs(Mod.Find<ModNPC>("JumboCactuar").Type))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("JumboCactuar").Type);
            SoundEngine.PlaySound(SoundID.Roar, player.position);

            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe(5)
                .AddIngredient(ItemID.Cactus, 50)
                .AddIngredient<Materials.SucculentCactus>(10)
                .AddIngredient<Materials.LusciousCactus>(5)
                .AddIngredient(ItemID.LunarBar, 2)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}

