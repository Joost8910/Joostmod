using Terraria;
using Terraria.ID;
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
            Item.questItem = true;
            Item.maxStack = 1;
            Item.width = 32;
            Item.height = 26;
            Item.uniqueStack = true;
            Item.rare = ItemRarityID.Quest;
        }
    }
}
