using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
    [AutoloadEquip(EquipType.Back)]
    public class SwordSapling : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sapling - Sword");
            Tooltip.SetDefault("Stabs enemies behind you\n" + "5% increased melee speed");
        }
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 30;
            Item.value = 20000;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.damage = 8;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.knockBack = 4;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetAttackSpeed(DamageClass.Melee) += 0.05f;
            player.GetModPlayer<JoostPlayer>().swordSaplingItem = Item;
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
                    if (line2.Name == "Damage" || line2.Name == "CritChance" || line2.Name == "knockback")
                    {
                        line2.OverrideColor = Color.DarkGray;
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup("JoostMod:Saplings")
                .AddIngredient(ItemID.CopperShortsword)
                .Register();
            CreateRecipe()
                .AddRecipeGroup("JoostMod:Saplings")
                .AddIngredient(ItemID.TinShortsword)
                .Register();
        }
    }
}