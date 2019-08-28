using Terraria.ModLoader;

namespace JoostMod.Items.Placeable
{
	public class DecisiveBattleMusicBox : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Music Box (The Decisive Battle)");
			Tooltip.SetDefault("From Final Fantasy V");
		}
        public override void SetDefaults()
        {
			item.useStyle = 1;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = mod.TileType("DecisiveBattleMusicBox");
			item.width = 24;
			item.height = 24;
			item.rare = 8;
            item.value = 500000;
            item.accessory = true;
		}
	}
}
