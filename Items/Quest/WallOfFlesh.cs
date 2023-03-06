using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Quest
{
    public class WallOfFlesh : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ball of Flesh");
            Tooltip.SetDefault("Quest item for the Hunt Master");
        }

        public override void SetDefaults()
        {
            Item.questItem = true;
            Item.maxStack = 1;
            Item.width = 46;
            Item.height = 46;
            Item.uniqueStack = true;
            Item.rare = ItemRarityID.Quest;
        }
    }
}
