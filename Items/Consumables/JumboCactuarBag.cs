using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Consumables
{
    public class JumboCactuarBag : ModItem
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

        public override int BossBagNPC => Mod.Find<ModNPC>("JumboCactuar").Type;

        public override bool CanRightClick()
        {
            return true;
        }

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
    }
}
