using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
    public class WoodGuardian : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Strange Seed");
        }

        public override void SetDefaults()
        {
            item.questItem = true;
            item.maxStack = 1;
            item.width = 20;
            item.height = 24;
            item.uniqueStack = true;
            item.rare = -11;
        }
    }
}
