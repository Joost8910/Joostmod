using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Consumables
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
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 130;
            Item.height = 102;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
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
            return !NPC.AnyNPCs(Mod.Find<ModNPC>("Gilgamesh").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("Gilgamesh2").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("Enkidu").Type);
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Gilgamesh").Type);
            NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("Enkidu").Type);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            if (Main.netMode != NetmodeID.Server)
            {
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
            }
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe(20)
                .AddIngredient<Materials.IceCoreX>()
                .AddIngredient(ItemID.HallowedBar, 20)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}

