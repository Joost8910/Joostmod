using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
    [AutoloadEquip(EquipType.Back)]
    public class TownNPCSpawner : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Free Real Estate Banner");
			Tooltip.SetDefault("Greatly increases the rate that town NPCs arrive");
		}
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 58; 
			item.value = 10000;
			item.rare = 2;
            item.accessory = true;
		}
        public override void UpdateEquip(Player player)
        {
            if (Main.netMode != 1)
            {
                Main.checkForSpawns += 23;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Silk, 10);
            recipe.AddIngredient(ItemID.GoldBar, 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Silk, 10);
            recipe.AddIngredient(ItemID.PlatinumBar, 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}


