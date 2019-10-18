using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class GenjiHelmMagic : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Azure Genji Helm");
            Tooltip.SetDefault("65% Increased Magic damage\n" + "35% Reduced Mana Usage");
        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 24;
            item.value = 10000000;
            item.rare = 11;
            item.defense = 25;
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
            return body.type == mod.ItemType("GenjiArmorMagic") && legs.type == mod.ItemType("GenjiLeggings");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Press the Armor Ability Hotkey to cast Bitter End\n" +
                "This consumes all of your mana and cant be used while you have mana sickness";
            player.GetModPlayer<JoostPlayer>().GMagic = true;
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowSubtle = true;
            player.armorEffectDrawShadowLokis = true;
            if (player.statMana >= player.statManaMax2 && !player.HasBuff(BuffID.ManaSickness) && player.ownedProjectileCounts[mod.ProjectileType("BitterEndFriendly")] + player.ownedProjectileCounts[mod.ProjectileType("BitterEndFriendly2")] <= 0)
            {
                player.armorEffectDrawOutlines = true;
            }
        }
        public override void UpdateEquip(Player player)
        {
            player.magicDamage += 0.65f;
            player.manaCost *= 0.65f;

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