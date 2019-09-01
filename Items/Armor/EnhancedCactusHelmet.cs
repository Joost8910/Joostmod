using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class EnhancedCactusHelmet : ModItem
	{
public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enhanced Cactus Helmet");
            Tooltip.SetDefault("3% increased critical strike chance");
        }
		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 16;
            item.value = Item.buyPrice(0, 7, 50, 0); 
			item.rare = 2;
            item.defense = 7;
		}
        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("EnhancedCactusBreastplate") && legs.type == mod.ItemType("EnhancedCactusLeggings");
        }
        public override void UpdateEquip(Player player)
        {
            player.meleeCrit += 3;
            player.thrownCrit += 3;
            player.rangedCrit += 3;
            player.magicCrit += 3;
        }

        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Return half of enemy contact damage back to the attacker";
            player.thorns += 0.5f;
        }
	}
}