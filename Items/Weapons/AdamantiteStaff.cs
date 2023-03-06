using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class AdamantiteStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Laser Staff");
            Tooltip.SetDefault("Fires a bouncing laser");
        }
		public override void SetDefaults()
		{
			Item.damage = 32;
			Item.DamageType = DamageClass.Magic;
			Item.width = 52;
			Item.height = 52;
			Item.mana = 6;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.staff[Item.type] = true; 
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; 
			Item.knockBack = 0;
			Item.value = 75000;
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item12;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("GreenLaser").Type;
			Item.shootSpeed = 15f;
		}


		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.AdamantiteBar, 12)
				.AddIngredient(ItemID.Emerald, 1)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}

	}
}


