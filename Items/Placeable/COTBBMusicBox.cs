using Terraria.ModLoader;

namespace JoostMod.Items.Placeable
{
	public class COTBBMusicBox : ModItem
	{
		  public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Music Box (Clash on the Big Bridge)");
			Tooltip.SetDefault("From Final Fantasy XII: The Zodiac Age");
		}
		public override void SetDefaults()
		{
			item.useStyle = 1;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = mod.TileType("COTBBMusicBox");
			item.width = 24;
			item.height = 24;
			item.rare = 8;
            item.value = 500000;
            item.accessory = true;
		}
	}
}
