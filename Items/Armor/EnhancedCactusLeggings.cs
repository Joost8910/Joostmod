using Terraria;
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
            item.width = 22;
            item.height = 18;
            item.value = Item.buyPrice(0, 7, 50, 0);
            item.rare = 2;
            item.defense = 6;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.05f;
            player.maxRunSpeed += 0.05f;
        }
    }
}