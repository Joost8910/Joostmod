using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class EnhancedCactusBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enhanced Cactus Breastplate");
            Tooltip.SetDefault("4% increased damage");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 18;
            item.value = Item.buyPrice(0, 10, 0, 0);
            item.rare = 2;
            item.defense = 8;
        }
        public override void UpdateEquip(Player player)
        {
            player.allDamage += 0.04f;
        }
    }
}