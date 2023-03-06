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
			Item.damage = 21;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 52;
			Item.height = 24;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; 
			Item.knockBack = 1;
            Item.value = 25000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item34;
			Item.autoReuse = true;
			Item.shoot = 85;
			Item.shootSpeed = 6f;
			Item.useAmmo = ItemID.Gel;
		}
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, -4);
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(230, 204, 128);
                }
            }
        }
    }
}


