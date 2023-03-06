using Terraria;
using Terraria.ID;
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
            Item.questItem = true;
            Item.maxStack = 1;
            Item.width = 26;
            Item.height = 18;
            Item.uniqueStack = true;
            Item.rare = ItemRarityID.Quest;
        }
    }
}
