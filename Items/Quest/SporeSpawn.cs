using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
    public class SporeSpawn : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spore Core");
        }

        public override void SetDefaults()
        {
            item.questItem = true;
            item.maxStack = 1;
            item.width = 22;
            item.height = 30;
            item.uniqueStack = true;
            item.rare = -11;
        }
    }
}
