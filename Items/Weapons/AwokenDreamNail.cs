using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class AwokenDreamNail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Awoken Dream Nail");
			Tooltip.SetDefault("'Can break into even the most protected mind'\n" + 
			"Attacks ignore enemy defense");
		}
		public override void SetDefaults()
		{
			Item.damage = 177;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 56;
			Item.height = 56;
			Item.noMelee = true;
			Item.useTime = 9;
			Item.useAnimation = 9;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.autoReuse = true;
			Item.knockBack = 9;
			Item.value = 800000;
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.DD2_SonicBoomBladeSlash;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.shoot = Mod.Find<ModProjectile>("AwokenDreamNail").Type;
			Item.shootSpeed = 19f;
		}
		public override bool CanUseItem(Player player)
        {
           return player.ownedProjectileCounts[Item.shoot] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("AwokenDreamNail2").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("AwokenDreamGreatSlash").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("AwokenDreamDashSlash").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("AwokenDreamSpinSlash").Type] < 1;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.statLife >= player.statLifeMax2)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("AwokenDreamBeam").Type, damage / 2, 0, player.whoAmI);
            }
            return true;
        }
        public override void AddRecipes()
		{
			//TENTATIVE: Empress of light drop instead of Lunar Fragments?
			CreateRecipe()
				.AddIngredient<DreamNail>()
				.AddIngredient(ItemID.FragmentNebula, 3)
				.AddIngredient(ItemID.FragmentSolar, 3)
				.AddIngredient(ItemID.FragmentVortex, 3)
				.AddIngredient(ItemID.FragmentStardust, 3)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
	}
}

