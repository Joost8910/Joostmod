using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class GenjiHelmThrown : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Veridian Genji Helm");
            Tooltip.SetDefault("60% Increased throwing velocity and damage\n" + "You no longer consume thrown items");
        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.value = 10000000;
            item.rare = 11;
            item.defense = 35;
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
            return body.type == mod.ItemType("GenjiArmorThrown") && legs.type == mod.ItemType("GenjiLeggings");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Press the Armor ability key to activate Counter Dodge, 20 sec cooldown\n" + 
                "Throwing ability increases after a successful dodge";
            player.GetModPlayer<JoostPlayer>(mod).gThrown = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<JoostModPlayer>(mod).throwNone = true;
            player.thrownVelocity *= 1.60f;
            player.thrownDamage += 0.60f;
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowSubtle = true;
            player.armorEffectDrawShadowLokis = true;
            player.armorEffectDrawOutlines = true;
            if (player.HasBuff(mod.BuffType("gThrownCooldown")))
            {
                player.armorEffectDrawOutlines = false;
            }
            if (player.HasBuff(mod.BuffType("gThrownDodge")) || player.HasBuff(mod.BuffType("gThrownBuff")))
            {
                player.armorEffectDrawOutlines = true;
                player.armorEffectDrawShadow = true;
                player.armorEffectDrawShadowLokis = true;
                player.armorEffectDrawShadowSubtle = true;
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