using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
    public class GrandCactusWorm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Head of the Grand Cactus Worm");
            Tooltip.SetDefault("Quest item for the Hunt Master");
        }

        public override void SetDefaults()
        {
            Item.questItem = true;
            Item.maxStack = 1;
            Item.width = 86;
            Item.height = 106;
            Item.uniqueStack = true;
            Item.rare = ItemRarityID.Quest;
        }
    }
}
