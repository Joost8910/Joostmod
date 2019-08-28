using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
	public class FireFlinger : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fire Flinger");
			Tooltip.SetDefault("Uses Gel for ammo");
		}
		public override void SetDefaults()
		{
			item.damage = 21;
			item.ranged = true;
			item.width = 52;
			item.height = 24;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = 5;
			item.noMelee = true; 
			item.knockBack = 1;
            item.value = 25000;
            item.rare = 3;
            item.UseSound = SoundID.Item34;
			item.autoReuse = true;
			item.shoot = 85;
			item.shootSpeed = 6f;
			item.useAmmo = ItemID.Gel;
		}
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, -4);
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(230, 204, 128);
                }
            }
        }
    }
}


