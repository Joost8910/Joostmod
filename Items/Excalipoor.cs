using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
    public class Excalipoor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Excalipoor");
            Tooltip.SetDefault("Summons Gilgamesh and his sidekick");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 130;
            item.height = 102;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 1;
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
            return !NPC.AnyNPCs(mod.NPCType("Gilgamesh")) && !NPC.AnyNPCs(mod.NPCType("Gilgamesh2")) && !NPC.AnyNPCs(mod.NPCType("Enkidu"));
        }
        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Gilgamesh"));
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Enkidu"));
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            if (JoostWorld.downedGilgamesh)
            {
                Main.NewText("<Gilgamesh> Oh, so you want to go again?", 225, 25, 25);
                Main.NewText("Then let us fight! This won't go as easily as last time!", 225, 25, 25);
                Main.NewText("<Enkidu> It probably will though.", 25, 225, 25);
            }
            else
            {
                Main.NewText("<Gilgamesh> AhHA! The legendary Excalibur!", 225, 25, 25);
                Main.NewText("Come Enkidu, my faithful sidekick, let's return the trouble", 225, 25, 25);
                Main.NewText("and make it double! Come on!", 225, 25, 25);
            }

            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "IceCoreX", 1);
            recipe.AddIngredient(ItemID.HallowedBar, 20);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 20);
            recipe.AddRecipe();
            /*
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "BrokenExcalipoor", 1);
            recipe.AddIngredient(ItemID.HallowedBar, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
            */
        }
    }
}

