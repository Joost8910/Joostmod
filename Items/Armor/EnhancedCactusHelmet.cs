using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class EnhancedCactusHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enhanced Cactus Helmet");
            Tooltip.SetDefault("3% increased critical strike chance");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 16;
            Item.value = Item.buyPrice(0, 7, 50, 0);
            Item.rare = ItemRarityID.Green;
            Item.defense = 7;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("EnhancedCactusBreastplate").Type && legs.type == Mod.Find<ModItem>("EnhancedCactusLeggings").Type;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 3;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Return half of enemy contact damage back to the attacker";
            player.thorns += 0.5f;
        }
    }
}