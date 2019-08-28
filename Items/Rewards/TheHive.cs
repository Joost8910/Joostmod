using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
	public class TheHive : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Hive");
            Tooltip.SetDefault("Rapidly spews bees that deal one-third damage");
		}
		public override void SetDefaults()
		{
			item.damage = 25;
			item.melee = true;
			item.noMelee = true;
			item.scale = 1f;
			item.noUseGraphic = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 19;
			item.useAnimation = 19;
			item.useStyle = 5;
			item.knockBack = 6.5f;
			item.value = 55000;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.channel = true;
			item.shoot = mod.ProjectileType("TheHive");
			item.shootSpeed = 10f;
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

