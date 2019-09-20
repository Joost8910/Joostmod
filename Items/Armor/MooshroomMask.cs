using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class MooshroomMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mooshroom Mask");
            Tooltip.SetDefault("MOOOO");
        }
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 5000;
            item.rare = 4;
            item.vanity = true;
        }
        public override bool DrawHead()
        {
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Mushroom, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}