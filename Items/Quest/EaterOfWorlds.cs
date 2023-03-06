using Terraria;
using Terraria.ID;
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
            Item.questItem = true;
            Item.maxStack = 1;
            Item.width = 46;
            Item.height = 34;
            Item.uniqueStack = true;
            Item.rare = ItemRarityID.Quest;
        }
    }
}
