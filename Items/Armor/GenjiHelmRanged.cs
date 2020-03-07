using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class GenjiHelmRanged : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Genji Helm");
            Tooltip.SetDefault("50% Increased Ranged damage\n" + "You no longer consume ammo");
        }
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 26;
            item.value = 10000000;
            item.rare = 11;
            item.defense = 30;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(0, 255, 0);
                }
            }
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("GenjiArmorRanged") && legs.type == mod.ItemType("GenjiLeggings");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Press the Armor Ability key to sacrifice all your defense for increased ranged ability";
            player.GetModPlayer<JoostPlayer>().gRanged = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.50f;
            player.GetModPlayer<JoostModPlayer>().ammoConsume = 0;

        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowSubtle = true;
            player.armorEffectDrawShadowLokis = true;
            if (player.HasBuff(mod.BuffType("gRangedBuff")))
            {
                player.armorEffectDrawOutlines = true;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GenjiToken", 1);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}