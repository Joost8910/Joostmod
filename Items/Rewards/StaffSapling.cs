using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
    [AutoloadEquip(EquipType.Back)]
    public class StaffSapling : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sapling - Staff");
            Tooltip.SetDefault("Shoots enemies behind you\n" + "5% reduced mana usage");
        }
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 30;
            Item.value = 20000;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.damage = 15;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 3.5f;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaCost -= 0.05f;
            player.GetModPlayer<JoostPlayer>().staffSaplingItem = Item;
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
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup("JoostMod:Saplings")
                .AddIngredient(ItemID.AmethystStaff)
                .Register();
            CreateRecipe()
                .AddRecipeGroup("JoostMod:Saplings")
                .AddIngredient(ItemID.TopazStaff)
                .Register();
        }
    }
}