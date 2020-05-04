using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
{
    public class EmptyHeart : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Empty Heart");
            Tooltip.SetDefault("Reduces your health to 1\n" +
                "20% Increased Damage");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.value = 0;
            item.rare = 4;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<JoostPlayer>().emptyHeart = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Deathweed, 2);
            recipe.AddIngredient(ItemID.HeartLantern);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}