using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
	[AutoloadEquip(EquipType.Shield)]
	public class FleshShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shield of Flesh");
			Tooltip.SetDefault("Double tap left or right to dash into enemies\n" + 
                "Occasionally summons leeches that steal life\n" + 
                "Leeches summon faster the less life you have");
		}
		public override void SetDefaults()
		{
			Item.width = 36;
			Item.height = 52;
			Item.value = 85000;
			Item.rare = ItemRarityID.Orange;
			Item.accessory = true;
			Item.defense = 2;
            Item.damage = 40;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            //item.knockback = 9;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<JoostPlayer>().fleshShieldItem = Item;
            player.GetModPlayer<JoostPlayer>().dashType = 1;
            player.GetModPlayer<JoostPlayer>().dashDamage = player.GetWeaponDamage(Item);
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
    }
}