using JoostMod.Items.Accessories;
using JoostMod.Items.Armor;
using JoostMod.Items.Materials;
using JoostMod.Items.Placeable;
using JoostMod.NPCs.Bosses;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Consumables
{
    public class JumboCactuarBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
            ItemID.Sets.BossBag[Item.type] = true;
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

        //public override int BossBagNPC => Mod.Find<ModNPC>("JumboCactuar").Type;

        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            player.TryGettingDevArmor(player.GetSource_OpenItem(Item.type));
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<JumboCactuar>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CactuarShield>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Cactustoken>(), 1, 1, 3));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DecisiveBattleMusicBox>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<JumboCactuarMask>(), 4));
        }
        /*
        public override void OpenBossBag(Player player)
        {
            player.TryGettingDevArmor();
            player.TryGettingDevArmor();

            player.QuickSpawnItem(Mod.Find<ModItem>("CactuarShield").Type);
            player.QuickSpawnItem(Mod.Find<ModItem>("Cactustoken").Type, 1 + Main.rand.Next(2));
            player.QuickSpawnItem(Mod.Find<ModItem>("DecisiveBattleMusicBox").Type);
            if (Main.rand.Next(4) == 0)
            {
                player.QuickSpawnItem(Mod.Find<ModItem>("JumboCactuarMask").Type);
            }
        }
        */
    }
}
