using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class FrozenOrb : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frozen Orb");
			Tooltip.SetDefault("Creates an orb of ice that fires icicles");
		}
		public override void SetDefaults()
		{
			item.damage = 60;
			item.magic = true;
			item.mana = 30;
			item.maxStack = 1;
			item.consumable = false;
			item.width = 50;
			item.height = 50;
			item.useTime = 60;
			item.useAnimation = 60;
			item.useStyle = 5;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.knockBack = 4;
			item.value = 100000;
			item.rare = 3;
			item.UseSound = SoundID.Item120;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("FrozenOrb");
			item.shootSpeed = 6f;
		}
		    public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.overrideColor = new Color(255, 128, 0);
            }
        }
    }
	}
}

