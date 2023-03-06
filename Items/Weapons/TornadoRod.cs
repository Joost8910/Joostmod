using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class TornadoRod : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tornado Rod");
			Tooltip.SetDefault("Summons a miniature tornado to fight for you");
		}
		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.DamageType = DamageClass.Summon;
			Item.mana = 10;
			Item.width = 36;
			Item.height = 36;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.knockBack = 3;
			Item.value = 225000;
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item44;
			Item.shoot = Mod.Find<ModProjectile>("WindMinion").Type;
			Item.shootSpeed = 7f;
			Item.buffType = Mod.Find<ModBuff>("WindMinion").Type;
			Item.buffTime = 3600;
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			position = Main.MouseWorld;
			return player.altFunctionUse != 2;
		}

		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			if (player.altFunctionUse == 2)
			{
				player.MinionNPCTargetAim(false);
			}
			return base.UseItem(player);
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<Materials.TinyTwister>(50)
				.AddRecipeGroup("JoostMod:AnyCobalt", 4)
				.AddRecipeGroup("JoostMod:AnyMythril", 4)
				.AddRecipeGroup("JoostMod:AnyAdamantite", 4)
				.AddTile<Tiles.ElementalForge>()
				.Register();
		}

	}
}


