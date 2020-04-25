using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
    public class EyeOfCthulhu : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tooth of Eye of Cthulhu");
            Tooltip.SetDefault("Quest item for the Hunt Master");
        }

        public override void SetDefaults()
        {
            item.questItem = true;
            item.maxStack = 1;
            item.width = 26;
            item.height = 18;
            item.uniqueStack = true;
            item.rare = -11;
        }
    }
}
