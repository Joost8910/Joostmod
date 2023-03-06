using Terraria;
using Terraria.ID;
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
            Item.width = 30;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.defense = 8;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.04f;
        }
    }
}