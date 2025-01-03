using Terraria.ModLoader;
using Terraria.ID;
using JoostMod.Items.Materials;

namespace JoostMod.Items.Placeable
{
	public class DontBeAfraidMusicBox : ModItem
	{
		public override void SetStaticDefaults()
		{
            ItemID.Sets.CanGetPrefixes[Type] = false;
            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Sounds/Music/DontBeAfraid"), ModContent.ItemType<DontBeAfraidMusicBox>(), ModContent.TileType<Tiles.DontBeAfraidMusicBox>());
		}
        public override void SetDefaults()
        {
            Item.DefaultToMusicBox(ModContent.TileType<Tiles.DontBeAfraidMusicBox>());
            Item.rare = ItemRarityID.Yellow;
            Item.value = 500000;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddCustomShimmerResult(ModContent.ItemType<DecisiveBattleMusicBox>())
                .Register();
        }
    }
}
