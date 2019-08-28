using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
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
            item.maxStack = 999;
            item.consumable = true;
            item.width = 24;
            item.height = 16;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 4;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.value = 10000;
            item.rare = 11;
            item.UseSound = SoundID.Item1;

        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(106, 63, 202);
                }
            }
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType("SAX")) && !NPC.AnyNPCs(mod.NPCType("SAXMutant")) && !NPC.AnyNPCs(mod.NPCType("SAXCoreX"));
        }
        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("SAX"));
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);

            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Cactustoken", 1);
            recipe.AddIngredient(ItemID.FrostCore, 1);
            recipe.AddIngredient(ItemID.MeteoriteBar, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();
        }
    }
}

