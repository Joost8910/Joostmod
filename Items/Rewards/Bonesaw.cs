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
			Item.damage = 18;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 54;
			Item.height = 28;
            Item.axe = 16;
			Item.useTime = 8;
			Item.useAnimation = 24;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 2.5f;
			Item.value = 65000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item23;
			Item.autoReuse = true;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.noMelee = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.Melee.Bonesaw>();
			Item.shootSpeed = 40f;
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

