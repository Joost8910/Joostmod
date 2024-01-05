using JoostMod.Items.Accessories;
using JoostMod.Items.Armor;
using JoostMod.Items.GrappleHooks;
using JoostMod.Items.Materials;
using JoostMod.Items.Placeable;
using JoostMod.NPCs.Bosses;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Consumables
{
	public class GrandCactusWormBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag (Alpha Cactus Worm)");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
            ItemID.Sets.BossBag[Item.type] = true;
            ItemID.Sets.PreHardmodeLikeBossBag[Item.type] = true;
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

        //public override int BossBagNPC => Mod.Find<ModNPC>("GrandCactusWormHead").Type;

        public override bool CanRightClick()
		{
			return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<GrandCactusWormHead>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CactusWormHook>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<LusciousCactus>(), 1, 10, 25));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DeoremMuaMusicBox>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<GrandCactusWormMask>(), 4));
        }
        /*
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
        */
	}
}
