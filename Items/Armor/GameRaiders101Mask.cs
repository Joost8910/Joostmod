using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class GameRaiders101Mask : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gameraiders101 Mask");
            Tooltip.SetDefault("'Great for impersonating youtubers!'");
        }
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 10000;
            item.rare = 10;
            item.vanity = true;
        }
        public override bool DrawHead()
        {
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("MooshroomMask"));
            recipe.AddIngredient(ItemID.Sunglasses);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}