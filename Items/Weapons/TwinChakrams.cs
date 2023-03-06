using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
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
			Item.damage = 38;
			Item.DamageType = DamageClass.Throwing;
			Item.maxStack = 1;
			Item.consumable = false;
			Item.width = 68;
			Item.height = 62;
			Item.useTime = 31;
			Item.useAnimation = 31;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.knockBack = 4;
			Item.value = 300000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item84;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("Chakram").Type;
			Item.shootSpeed = 2.5f;
		}
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.Mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.OverrideColor = new Color(255, 128, 0);
				}
			}
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("Chakram2").Type, damage, knockback, player.whoAmI);
			return true;
		}

	}
}

