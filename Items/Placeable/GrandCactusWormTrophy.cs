using Terraria.ModLoader;

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
			item.width = 30;
			item.height = 30;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.value = 10000;
			item.rare = 2;
			item.createTile = mod.TileType("BossTrophy");
			item.placeStyle = 3;
		}
	}
}