using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Placeable
{
	public class DecisiveBattleMusicBox : ModItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Music Box (The Decisive Battle)");
            // Tooltip.SetDefault("From Final Fantasy VI");

            ItemID.Sets.CanGetPrefixes[Type] = false;
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<DontBeAfraidMusicBox>();
            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Sounds/Music/TheDecisiveBattle"), ModContent.ItemType<DecisiveBattleMusicBox>(), ModContent.TileType<Tiles.DecisiveBattleMusicBox>());
		}
        public override void SetDefaults()
        {
            Item.DefaultToMusicBox(ModContent.TileType<Tiles.DecisiveBattleMusicBox>());
            Item.rare = ItemRarityID.Yellow;
            Item.value = 500000;
        }
    }
}
