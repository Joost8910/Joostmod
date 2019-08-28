using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
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
			item.maxStack = 999;
			item.consumable = true;
			item.width = 24;
			item.height = 24;
			item.expert = true;
			item.rare = -12;
		}

        public override int BossBagNPC => mod.NPCType("GrandCactusWormHead");

        public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
        {
            player.QuickSpawnItem(mod.ItemType("CactusWormHook"));
            player.QuickSpawnItem(mod.ItemType("LusciousCactus"), 10+Main.rand.Next(16));
            if (Main.rand.Next(2) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("DeoremMuaMusicBox"));
            }
            if (Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("GrandCactusWormMask"));
            }
            if (Main.rand.Next(5) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("GrandCactusWormTrophy"));
            }
        }
	}
}
