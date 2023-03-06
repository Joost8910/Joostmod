using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class GiantKnife : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Giant's Knife");
			Tooltip.SetDefault("'It's so long!'");
		}
		public override void SetDefaults()
		{
			Item.damage = 40;
			Item.DamageType = DamageClass.Throwing;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 28;
			Item.height = 76;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.knockBack = 7;
			Item.value = 200;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("GiantKnife").Type;
			Item.shootSpeed = 8f;
		}
	}
}

