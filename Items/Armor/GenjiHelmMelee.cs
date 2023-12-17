using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class GenjiHelmMelee : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Golden Genji Helm");
            Tooltip.SetDefault("50% Increased Melee damage\n" + "25% Increased Melee speed");
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = 10000000;
            Item.rare = ItemRarityID.Purple;
            Item.defense = 40;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(0, 255, 0);
                }
            }
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<GenjiArmorMelee>() && legs.type == ModContent.ItemType<GenjiLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Swing the Masamune when you hit an enemy with a melee weapon";
            player.GetModPlayer<JoostPlayer>().gMelee = true;
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowSubtle = true;
            player.armorEffectDrawShadowLokis = true;
            player.armorEffectDrawOutlines = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.50f;
            player.GetAttackSpeed(DamageClass.Melee) *= 1.25f;

        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.GenjiToken>()
                .Register();
        }
    }
}