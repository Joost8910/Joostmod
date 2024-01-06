using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using JoostMod.Items.Armor;
using JoostMod.Items.Materials;
using JoostMod.Items.Weapons;
using Terraria.GameContent.ItemDropRules;
using JoostMod.Items.Placeable;
using JoostMod.Items.Accessories;
using JoostMod.NPCs.Bosses;

namespace JoostMod.Items.Consumables
{
    public class XBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag (SA-X)");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 8));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
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

        //public override int BossBagNPC => Mod.Find<ModNPC>("SAXCoreX").Type;

        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            player.TryGettingDevArmor(player.GetSource_OpenItem(Item.type));
            player.TryGettingDevArmor(player.GetSource_OpenItem(Item.type));
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<SAXCoreX>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<XShield>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<IceCoreX>(), 1, 1, 3));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SAXMusicBox>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SAXMask>(), 4));
        }
        /*
        public override void OpenBossBag(Player player)
        {
            player.TryGettingDevArmor();
            player.TryGettingDevArmor();
            player.TryGettingDevArmor();

            player.QuickSpawnItem(Mod.Find<ModItem>("XShield").Type);
            player.QuickSpawnItem(Mod.Find<ModItem>("IceCoreX").Type, 1 + Main.rand.Next(2));
            player.QuickSpawnItem(Mod.Find<ModItem>("SAXMusicBox").Type);
            if (Main.rand.Next(4) == 0)
            {
                player.QuickSpawnItem(Mod.Find<ModItem>("SAXMask").Type);
            }
        }
        */
    }
}

