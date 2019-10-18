using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items
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
            item.width = 34;
            item.height = 28;
            item.value = 52000;
            item.rare = 3;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<JoostPlayer>().hoverBoots = true;
        }
    }
}