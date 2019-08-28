using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
    public class QueenBee : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Royal Antenna");
        }

        public override void SetDefaults()
        {
            item.questItem = true;
            item.maxStack = 1;
            item.width = 38;
            item.height = 26;
            item.uniqueStack = true;
            item.rare = -11;
        }
    }
}
