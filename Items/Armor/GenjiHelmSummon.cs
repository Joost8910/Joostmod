using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class GenjiHelmSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Silver Genji Helm");
            Tooltip.SetDefault("75% Increased Minion damage and Knockback\n" + "Max sentries increased by 4");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 24;
            item.value = 10000000;
            item.rare = 11;
            item.defense = 20;
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
            return body.type == mod.ItemType("GenjiArmorSummon") && legs.type == mod.ItemType("GenjiLeggings");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Enkidu will fight for you";
            player.AddBuff(mod.BuffType("EnkiduMinion"), 2);
            player.GetModPlayer<JoostPlayer>(mod).EnkiduMinion = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.75f;
            player.minionKB *= 1.75f;
            player.maxTurrets += 4;
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowSubtle = true;
            player.armorEffectDrawShadowLokis = true;
            player.armorEffectDrawOutlines = true;
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