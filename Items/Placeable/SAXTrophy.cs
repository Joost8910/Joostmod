using Terraria.ModLoader;

namespace JoostMod.Items.Placeable
{
	public class SAXTrophy : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SA-X Trophy");
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
            item.value = 500000;
            item.rare = 8;
			item.createTile = mod.TileType("BossTrophy");
			item.placeStyle = 1;
		}
	}
}
