using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class DirtHelmet : ModItem
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soil Helm");
		}
		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 16;
            item.value = Item.sellPrice(0, 0, 0, 10);
			item.rare = 2;
		}
        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("DirtBreastplate") && legs.type == mod.ItemType("DirtLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Grants 1 more defense for every 666 blocks of dirt in your inventory\n" + 
                "Getting hit consumes dirt equal to the attack's damage, up to a maximum of half of the defense provided";
            player.GetModPlayer<JoostPlayer>().dirtArmor = true;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(151, 107, 75);
                }
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock, 666);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}