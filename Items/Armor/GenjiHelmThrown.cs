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
            Item.width = 26;
            Item.height = 26;
            Item.value = 10000000;
            Item.rare = ItemRarityID.Purple;
            Item.defense = 35;
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
            return body.type == ModContent.ItemType<GenjiArmorThrown>() && legs.type == ModContent.ItemType<GenjiLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Press the Armor ability key to activate Counter Dodge, 20 sec cooldown\n" + 
                "Throwing ability increases after a successful dodge";
            player.GetModPlayer<JoostPlayer>().gThrown = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<JoostModPlayer>().throwConsume = 0;
            player.ThrownVelocity *= 1.60f;
            player.GetDamage(DamageClass.Throwing) += 0.60f;
        }
        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowSubtle = true;
            player.armorEffectDrawShadowLokis = true;
            player.armorEffectDrawOutlines = true;
            if (player.HasBuff(ModContent.BuffType<Buffs.gThrownCooldown>()))
            {
                player.armorEffectDrawOutlines = false;
            }
            if (player.HasBuff(ModContent.BuffType<Buffs.gThrownDodge>()) || player.HasBuff(ModContent.BuffType<Buffs.gThrownBuff>()))
            {
                player.armorEffectDrawOutlines = true;
                player.armorEffectDrawShadow = true;
                player.armorEffectDrawShadowLokis = true;
                player.armorEffectDrawShadowSubtle = true;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.GenjiToken>()
                .Register();
        }
    }
}