using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class SnowFlake : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Snow Flake");
			Tooltip.SetDefault("'Don't get frostbite!");
		}
		public override void SetDefaults()
		{
			Item.damage = 60;
			Item.DamageType = DamageClass.Throwing;
			Item.maxStack = 1;
			Item.consumable = false;
			Item.width = 46;
			Item.height = 46;
			Item.useTime = 33;
			Item.useAnimation = 33;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.knockBack = 5;
			Item.value = 200000;
			Item.rare = ItemRarityID.Lime;
			Item.UseSound = SoundID.Item28;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("SnowFlake").Type;
			Item.shootSpeed = 9f;
		}
	}
}

