using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
{
    public class GilgBag : ModItem
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
        public override int BossBagNPC => mod.NPCType("Gilgamesh2");
        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            player.TryGettingDevArmor();
            player.TryGettingDevArmor();
            player.TryGettingDevArmor();
            player.TryGettingDevArmor();
            
            player.QuickSpawnItem(mod.ItemType("COTBBMusicBox"));
            player.QuickSpawnItem(mod.ItemType("Gilgameshset"));
            player.QuickSpawnItem(mod.ItemType("GenjiToken"), 2 + Main.rand.Next(2));
            if (Main.rand.Next(4) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("GilgameshMask"));
            }


            if (Main.rand.NextBool(100))
            {
                player.QuickSpawnItem(mod.ItemType("EvilStone"));
            }
            else if (Main.rand.NextBool(99))
            {
                player.QuickSpawnItem(mod.ItemType("SkullStone"));
            }
            else if (Main.rand.NextBool(98))
            {
                player.QuickSpawnItem(mod.ItemType("JungleStone"));
            }
            else if (Main.rand.NextBool(97))
            {
                player.QuickSpawnItem(mod.ItemType("InfernoStone"));
            }
            else if (Main.rand.NextBool(96))
            {
                player.QuickSpawnItem(mod.ItemType("SeaStoneDeep"));
            }
            else if (Main.rand.NextBool(95))
            {
                player.QuickSpawnItem(mod.ItemType("SeaStoneEast"));
            }
            else if (Main.rand.NextBool(94))
            {
                player.QuickSpawnItem(mod.ItemType("SeaStoneHigh"));
            }
            else if (Main.rand.NextBool(93))
            {
                player.QuickSpawnItem(mod.ItemType("SeaStoneWest"));
            }
        }
    }
}
