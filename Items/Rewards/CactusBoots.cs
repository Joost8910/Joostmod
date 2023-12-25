using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
    [AutoloadEquip(EquipType.Shoes)]
    public class CactusBoots : ModItem
    { 
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactus Boots");
            Tooltip.SetDefault("Sprouts damaging cacti as you walk");
        }
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 28;
            Item.value = 75000;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.damage = 18;
            Item.DamageType = DamageClass.Summon;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<JoostPlayer>().cactusBootsItem = Item;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria")
                {
                    if (line2.Name == "ItemName")
                    {
                        line2.OverrideColor = new Color(230, 204, 128);
                    }
                    if (line2.Name == "Damage" || line2.Name == "CritChance" || line2.Name == "Knockback")
                    {
                        line2.OverrideColor = Color.DarkGray;
                    }
                }
            }
        }
    }
}