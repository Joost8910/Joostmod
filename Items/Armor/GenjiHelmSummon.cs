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
            Tooltip.SetDefault("75% Increased Minion damage and knockback\n" + "Max sentries increased by 4");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 24;
            Item.value = 10000000;
            Item.rare = ItemRarityID.Purple;
            Item.defense = 20;
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
            return body.type == ModContent.ItemType<GenjiArmorSummon>() && legs.type == ModContent.ItemType<GenjiLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Enkidu will fight for you";
            player.AddBuff(ModContent.BuffType<Buffs.EnkiduMinionBuff>(), 2);
            player.GetModPlayer<JoostPlayer>().EnkiduMinion = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.75f;
            player.GetKnockback(DamageClass.Summon).Base *= 1.75f;
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
            CreateRecipe()
                .AddIngredient<Materials.GenjiToken>()
                .Register();
        }
    }
}