using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
{
    public class XBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("Right click to open");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 8));
            ItemID.Sets.ItemNoGravity[item.type] = true;
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

        public override int BossBagNPC => mod.NPCType("SAXCoreX");

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            player.TryGettingDevArmor();
            player.TryGettingDevArmor();
            player.TryGettingDevArmor();

            player.QuickSpawnItem(mod.ItemType("XShield"));
            player.QuickSpawnItem(mod.ItemType("IceCoreX"), 1 + Main.rand.Next(2));
            player.QuickSpawnItem(mod.ItemType("SAXMusicBox"));
            if (Main.rand.Next(4) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("SAXMask"));
            }
        }
    }
}

