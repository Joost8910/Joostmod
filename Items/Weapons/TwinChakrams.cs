using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class TwinChakrams : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Twin chakrams");
			Tooltip.SetDefault("Throws two chakrams that pierce through enemies");
		}
		public override void SetDefaults()
		{
			item.damage = 38;
			item.thrown = true;
			item.maxStack = 1;
			item.consumable = false;
			item.width = 68;
			item.height = 62;
			item.useTime = 31;
			item.useAnimation = 31;
			item.useStyle = 1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.knockBack = 4;
			item.value = 300000;
			item.rare = 3;
			item.UseSound = SoundID.Item84;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Chakram");
			item.shootSpeed = 2.5f;
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
		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
					Terraria.Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("Chakram2"), damage, knockBack, player.whoAmI);
			return true;
		} 

	}
}

