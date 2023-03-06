//Remove? 1.4 already adds pizza
using Terraria; 
using Terraria.ID; 
using Terraria.ModLoader;

namespace JoostMod.Items.Consumables
{
	public class BONELESSPizza : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("BONELESS Pizza");
			Tooltip.SetDefault("Minor improvements to all stats\n" + "Increases max health by 50");
		}
		public override void SetDefaults()
		{
			Item.maxStack = 30;
			Item.consumable = true;
			Item.width = 20;
			Item.height = 26;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 2;
			Item.knockBack = 5;
			Item.value = 100000;
			Item.rare = ItemRarityID.Lime;
			Item.UseSound = SoundID.Item2;
			Item.buffTime = 108000;
			Item.buffType = BuffID.WellFed;
		}
		public override bool ConsumeItem(Player player)
		{
			player.AddBuff(Mod.Find<ModBuff>("BONELESSPizza").Type, 108000);
			return true;
		}
		public override void AddRecipes()
		{
			CreateRecipe(8)
				.AddIngredient(ItemID.Hay, 32)
				.AddIngredient(ItemID.Mushroom, 16)
				.AddIngredient(ItemID.Bacon, 4)
				.AddTile(TileID.Furnaces)
				.Register();
		}

	}
}

