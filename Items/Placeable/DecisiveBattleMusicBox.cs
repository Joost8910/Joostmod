using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable
{
	public class DecisiveBattleMusicBox : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Music Box (The Decisive Battle)");
			Tooltip.SetDefault("From Final Fantasy VI");

			MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Sounds/Music/TheDecisiveBattle"), ModContent.ItemType<DecisiveBattleMusicBox>(), ModContent.TileType<Tiles.DecisiveBattleMusicBox>());
		}
        public override void SetDefaults()
        {
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<Tiles.DecisiveBattleMusicBox>();
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.Yellow;
            Item.value = 500000;
            Item.accessory = true;
		}
	}
}
