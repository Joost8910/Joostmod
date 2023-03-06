using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable
{
	public class GrandCactusWormTrophy : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactus Worm Trophy");
		}
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.createTile = Mod.Find<ModTile>("BossTrophy").Type;
			Item.placeStyle = 3;
		}
	}
}