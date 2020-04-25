using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
    public class EaterOfWorlds : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jaws of the Eater of Worlds");
            Tooltip.SetDefault("Quest item for the Hunt Master");
        }

        public override void SetDefaults()
        {
            item.questItem = true;
            item.maxStack = 1;
            item.width = 46;
            item.height = 34;
            item.uniqueStack = true;
            item.rare = -11;
        }
    }
}
