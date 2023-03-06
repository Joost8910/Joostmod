using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
    [AutoloadEquip(EquipType.Back)]
    public class HatchetSapling : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sapling - Hatchet");
            Tooltip.SetDefault("Throws Hatchets at enemies behind you\n" + "5% increased throwing velocity");
        }
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 30;
            Item.value = 20000;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.damage = 7;
            Item.DamageType = DamageClass.Throwing;
            Item.knockBack = 3;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.ThrownVelocity += 0.05f;
            player.GetModPlayer<JoostPlayer>().hatchetSapling = true;
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
                .AddIngredient<Weapons.CopperHatchet>(3)
                .Register();
            CreateRecipe()
                .AddRecipeGroup("JoostMod:Saplings")
                .AddIngredient<Weapons.TinHatchet>(3)
                .Register();
        }
    }
}