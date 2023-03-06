using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
    public class QueenBee : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Royal Antenna");
            Tooltip.SetDefault("Quest item for the Hunt Master");
        }

        public override void SetDefaults()
        {
            Item.questItem = true;
            Item.maxStack = 1;
            Item.width = 38;
            Item.height = 26;
            Item.uniqueStack = true;
            Item.rare = ItemRarityID.Quest;
        }
    }
}
