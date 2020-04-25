using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
    public class BrainOfCthulhu : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eye of Heart of Brain of Cthulhu");
            Tooltip.SetDefault("Quest item for the Hunt Master");
        }

        public override void SetDefaults()
        {
            item.questItem = true;
            item.maxStack = 1;
            item.width = 32;
            item.height = 26;
            item.uniqueStack = true;
            item.rare = -11;
        }
    }
}
