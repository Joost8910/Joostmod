using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Rewards
{
	public class TomatoHead : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tomato Head");
			Tooltip.SetDefault("It's still wriggling");
		}
		public override void SetDefaults()
		{
			Item.damage = 26;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 32;
			Item.height = 36;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.reuseDelay = 5;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 3;
			Item.value = 15000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item13;
			Item.autoReuse = true;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.noMelee = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.Melee.TomatoHead>();
			Item.shootSpeed = 10f;
        }

        public override bool MeleePrefix()
        {
            return true;
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

