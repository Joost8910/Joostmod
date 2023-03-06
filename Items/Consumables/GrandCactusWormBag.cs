using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Consumables
{
	public class GrandCactusWormBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("Right click to open");
		}
		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 24;
			Item.height = 24;
			Item.expert = true;
			Item.rare = ItemRarityID.Expert;
		}

        public override int BossBagNPC => Mod.Find<ModNPC>("GrandCactusWormHead").Type;

        public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
        {
            player.QuickSpawnItem(Mod.Find<ModItem>("CactusWormHook").Type);
            player.QuickSpawnItem(Mod.Find<ModItem>("LusciousCactus").Type, 10+Main.rand.Next(16));
            player.QuickSpawnItem(Mod.Find<ModItem>("DeoremMuaMusicBox").Type);
            if (Main.rand.Next(4) == 0)
            {
                player.QuickSpawnItem(Mod.Find<ModItem>("GrandCactusWormMask").Type);
            }
        }
	}
}
