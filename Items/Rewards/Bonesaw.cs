using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
	public class Bonesaw : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bonesaw");
			Tooltip.SetDefault("Slain enemies explode into bones\n" + "Guaranteed critical hits against skeletal creatures");
		}
		public override void SetDefaults()
		{
			item.damage = 18;
			item.melee = true;
			item.width = 54;
			item.height = 28;
            item.axe = 16;
			item.useTime = 8;
			item.useAnimation = 24;
			item.useStyle = 5;
			item.knockBack = 2.5f;
			item.value = 65000;
			item.rare = 3;
			item.UseSound = SoundID.Item23;
			item.autoReuse = true;
			item.noUseGraphic = true;
			item.channel = true;
			item.noMelee = true;
			item.shoot = mod.ProjectileType("Bonesaw");
			item.shootSpeed = 40f;
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

