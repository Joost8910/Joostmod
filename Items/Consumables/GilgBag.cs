using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using JoostMod.Items.Placeable;
using JoostMod.Items.Materials;
using JoostMod.Items.Armor;
using JoostMod.NPCs.Bosses;
using JoostMod.Items.Weapons.Hybrid;
using JoostMod.Items.Legendaries;

namespace JoostMod.Items.Consumables
{
    public class GilgBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
            ItemID.Sets.BossBag[Type] = true;
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
        //public override int BossBagNPC => Mod.Find<ModNPC>("Gilgamesh2").Type;
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            for (int i = 0; i < 3; i++)
                player.TryGettingDevArmor(player.GetSource_OpenItem(Item.type));
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<Gilgamesh2>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Gilgameshset>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<GenjiToken>(), 1, 2, 4));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<COTBBMusicBox>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<GilgameshMask>(), 4));
            itemLoot.Add(ItemDropRule.OneFromOptions(100,
                ModContent.ItemType<EvilStone>(),
                ModContent.ItemType<SkullStone>(),
                ModContent.ItemType<JungleStone>(),
                ModContent.ItemType<InfernoStone>(),
                ModContent.ItemType<SeaStoneHigh>(),
                ModContent.ItemType<SeaStoneEast>(),
                ModContent.ItemType<SeaStoneWest>(),
                ModContent.ItemType<SeaStoneDeep>()));
        }
        /*
        public override void OpenBossBag(Player player)
        {
            player.TryGettingDevArmor();
            player.TryGettingDevArmor();
            player.TryGettingDevArmor();
            player.TryGettingDevArmor();
            
            player.QuickSpawnItem(Mod.Find<ModItem>("COTBBMusicBox").Type);
            player.QuickSpawnItem(Mod.Find<ModItem>("Gilgameshset").Type);
            player.QuickSpawnItem(Mod.Find<ModItem>("GenjiToken").Type, 2 + Main.rand.Next(2));
            if (Main.rand.Next(4) == 0)
            {
                player.QuickSpawnItem(Mod.Find<ModItem>("GilgameshMask").Type);
            }


            if (Main.rand.NextBool(100))
            {
                player.QuickSpawnItem(Mod.Find<ModItem>("EvilStone").Type);
            }
            else if (Main.rand.NextBool(99))
            {
                player.QuickSpawnItem(Mod.Find<ModItem>("SkullStone").Type);
            }
            else if (Main.rand.NextBool(98))
            {
                player.QuickSpawnItem(Mod.Find<ModItem>("JungleStone").Type);
            }
            else if (Main.rand.NextBool(97))
            {
                player.QuickSpawnItem(Mod.Find<ModItem>("InfernoStone").Type);
            }
            else if (Main.rand.NextBool(96))
            {
                player.QuickSpawnItem(Mod.Find<ModItem>("SeaStoneDeep").Type);
            }
            else if (Main.rand.NextBool(95))
            {
                player.QuickSpawnItem(Mod.Find<ModItem>("SeaStoneEast").Type);
            }
            else if (Main.rand.NextBool(94))
            {
                player.QuickSpawnItem(Mod.Find<ModItem>("SeaStoneHigh").Type);
            }
            else if (Main.rand.NextBool(93))
            {
                player.QuickSpawnItem(Mod.Find<ModItem>("SeaStoneWest").Type);
            }
        }
        */
    }
}
