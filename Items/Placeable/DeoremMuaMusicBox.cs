using Terraria.ModLoader;

namespace JoostMod.Items.Placeable
{
	public class DeoremMuaMusicBox : ModItem
	{
		  public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Music Box (Deorem/Mua Boss Fight)");
			Tooltip.SetDefault("From Metroid: Zero Mission");
		}
		public override void SetDefaults()
		{
			item.useStyle = 1;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = mod.TileType("DeoremMuaMusicBox");
			item.width = 24;
			item.height = 24;
			item.rare = 8;
			item.value = 10000;
			item.accessory = true;
		}
	}
}
