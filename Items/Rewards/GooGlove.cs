using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Rewards
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class GooGlove : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gooey Glove");
			Tooltip.SetDefault("Flings multiple globules of bouncy pink gel");
		}
		public override void SetDefaults()
		{
			item.damage = 10;
			item.thrown = true;
			item.width = 32;
			item.height = 34;
			item.useTime = 43;
			item.useAnimation = 43;
			item.useStyle = 1;
			item.noMelee = true;
            item.noUseGraphic = true;
			item.knockBack = 2;
			item.value = 10000;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("PinkGoo"); 
			item.shootSpeed = 9.5f;
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
public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 2 + Main.rand.Next(3); 
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); 
				float scale = 1f - (Main.rand.NextFloat() * .3f);
				perturbedSpeed = perturbedSpeed * scale; 
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false; 
		}
	}
}
