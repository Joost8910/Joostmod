using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Accessories
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
			Item.width = 32;
			Item.height = 58; 
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
            Item.accessory = true;
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
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.GoldBar, 2)
                .AddTile(TileID.WorkBenches)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.PlatinumBar, 2)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}


