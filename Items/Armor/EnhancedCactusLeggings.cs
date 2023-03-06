using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class EnhancedCactusLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enhanced Cactus Leggings");
            Tooltip.SetDefault("5% increased movement speed");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 7, 50, 0);
            Item.rare = ItemRarityID.Green;
            Item.defense = 6;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.05f;
            player.maxRunSpeed += 0.05f;
        }
    }
}