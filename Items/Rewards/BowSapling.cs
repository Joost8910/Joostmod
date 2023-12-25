using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
    [AutoloadEquip(EquipType.Back)]
	public class BowSapling : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sapling - Bow");
			Tooltip.SetDefault("Shoots enemies behind you\n" + "5% increased ranged crit chance");
		}
		public override void SetDefaults()
		{
			Item.width = 38;
			Item.height = 30;
			Item.value = 20000;
			Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.damage = 7;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 2;
            Item.shootSpeed = 6.6f;
        }
        public override bool? CanChooseAmmo(Item ammo, Player player)
        {
            return ammo.ammo == AmmoID.Arrow;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Ranged) += 5;
            player.GetModPlayer<JoostPlayer>().bowSaplingItem = Item;
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
            .AddIngredient(ItemID.CopperBow)
            .Register();
            CreateRecipe()
            .AddRecipeGroup("JoostMod:Saplings")
            .AddIngredient(ItemID.TinBow)
            .Register();
        }
    }
}