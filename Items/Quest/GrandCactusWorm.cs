using Terraria;
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
            item.questItem = true;
            item.maxStack = 1;
            item.width = 86;
            item.height = 106;
            item.uniqueStack = true;
            item.rare = -11;
        }
    }
}
