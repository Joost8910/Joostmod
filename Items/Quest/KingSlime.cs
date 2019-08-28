using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
    public class KingSlime : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slime King's Jewel");
        }

        public override void SetDefaults()
        {
            item.questItem = true;
            item.maxStack = 1;
            item.width = 18;
            item.height = 22;
            item.uniqueStack = true;
            item.rare = -11;
        }
    }
}
