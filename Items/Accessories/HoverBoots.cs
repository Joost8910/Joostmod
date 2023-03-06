using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Shoes)]
    public class HoverBoots : ModItem
    { 
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hover Boots");
            Tooltip.SetDefault("Allows you to walk on air");
        }
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 28;
            Item.value = 52000;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<JoostPlayer>().hoverBoots = true;
        }
    }
}