using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class Whirlwind : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Whirlwind");
			Tooltip.SetDefault("Increases defense by 20 while in use");
		}
		public override void SetDefaults()
		{
			item.damage = 44;
			item.melee = true;
			item.width = 84;
			item.height = 58;
			item.noMelee = true;
			item.useTime = 20;
			item.useAnimation = 20;
			item.reuseDelay = 5;
			item.useStyle = 3;
			item.knockBack = 4;
			item.value = 300000;
			item.rare = 3;
			item.noUseGraphic = true;
			item.channel = true;
			item.UseSound = SoundID.Item1;
			item.shoot = mod.ProjectileType("Whirlwind");
			item.shootSpeed = 8f;
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
            Terraria.Projectile.NewProjectile(position.X, position.Y, speedX * 0.01f, -8f, type, damage, knockBack, player.whoAmI);
			return false;
		} 

	}
}

