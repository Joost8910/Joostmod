using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
    public class WoodGuardian : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Strange Seed");
            Tooltip.SetDefault("Quest item for the Hunt Master");
        }

        public override void SetDefaults()
        {
            Item.questItem = true;
            Item.maxStack = 1;
            Item.width = 20;
            Item.height = 24;
            Item.uniqueStack = true;
            Item.rare = ItemRarityID.Quest;
        }
    }
}
