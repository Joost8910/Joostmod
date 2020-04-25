using Terraria.ModLoader;

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
            item.questItem = true;
            item.maxStack = 1;
            item.width = 46;
            item.height = 46;
            item.uniqueStack = true;
            item.rare = -11;
        }
    }
}
