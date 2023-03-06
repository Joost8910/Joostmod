using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Consumables
{
    public class XBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("Right click to open");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 8));
            ItemID.Sets.ItemNoGravity[Item.type] = true;
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

        public override int BossBagNPC => Mod.Find<ModNPC>("SAXCoreX").Type;

        public override bool CanRightClick()
        {
            return true;
        }

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
    }
}

