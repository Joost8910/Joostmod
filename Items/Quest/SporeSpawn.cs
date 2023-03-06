using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Quest
{
    public class SporeSpawn : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spore Core");
            Tooltip.SetDefault("Quest item for the Hunt Master");
        }

        public override void SetDefaults()
        {
            Item.questItem = true;
            Item.maxStack = 1;
            Item.width = 22;
            Item.height = 30;
            Item.uniqueStack = true;
            Item.rare = ItemRarityID.Quest;
        }
    }
}
