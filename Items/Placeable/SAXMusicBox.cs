using Terraria.ModLoader;

namespace JoostMod.Items.Placeable
{
	public class SAXMusicBox : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Music Box (Vs. SA-X)");
			Tooltip.SetDefault("From Metroid Fusion");
		}
		public override void SetDefaults()
		{
			item.useStyle = 1;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = mod.TileType("SAXMusicBox");
			item.width = 24;
			item.height = 24;
			item.rare = 8;
            item.value = 500000;
            item.accessory = true;
		}
	}
}
