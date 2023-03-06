using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class Yinyangcharm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yin Yang Charm");
			Tooltip.SetDefault("'Find your inner pieces'");
		}
		public override void SetDefaults()
		{
			Item.damage = 33;
			Item.DamageType = DamageClass.Summon;
			Item.mana = 15;
			Item.width = 42;
			Item.height = 42;
			Item.useTime = 20;
			Item.useAnimation = 40;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.knockBack = 4;
			Item.value = 144000;
			Item.rare = ItemRarityID.Pink;
			Item.noMelee = true;
			Item.UseSound = SoundID.Item43;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("DarkBolt").Type;
			Item.shootSpeed = 12f;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("LightBolt").Type, damage, knockback, player.whoAmI);
			return false;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.LightShard)
				.AddIngredient(ItemID.DarkShard)
				.AddIngredient(ItemID.SoulofLight, 7)
				.AddIngredient(ItemID.SoulofNight, 7)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}

