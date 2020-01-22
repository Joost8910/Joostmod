using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class HungeringArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hungering Arrow");
			Tooltip.SetDefault("A homing arrow with a 35% chance on hit to pierce again");
		}
		public override void SetDefaults()
		{
			item.maxStack = 1;
			item.ranged = true;
			item.damage = 8;
			item.width = 18;
			item.height = 44;
			item.consumable = false;
			item.knockBack = 0f;
			item.value = 300000;
			item.rare = 3;
			item.shoot = mod.ProjectileType("HungeringArrow");
			item.shootSpeed = 7f;
			item.ammo = AmmoID.Arrow;
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

