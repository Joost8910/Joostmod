using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Consumables
{
    public class InfectedArmCannon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infected Arm Cannon");
            Tooltip.SetDefault("Summons the SA-X");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 16;
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
            return !NPC.AnyNPCs(Mod.Find<ModNPC>("SAX").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("SAXMutant").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("SAXCoreX").Type);
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("SAX").Type);
            SoundEngine.PlaySound(SoundID.Roar, player.position);

            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe(20)
                .AddIngredient<Materials.Cactustoken>()
                .AddIngredient(ItemID.FrostCore, 1)
                .AddIngredient(ItemID.MeteoriteBar, 20)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}

