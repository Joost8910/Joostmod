using Microsoft.Xna.Framework;
using Terraria;
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
			item.damage = 177;
			item.melee = true;
			item.width = 56;
			item.height = 56;
			item.noMelee = true;
			item.useTime = 9;
			item.useAnimation = 9;
			item.useStyle = 5;
			item.autoReuse = true;
			item.knockBack = 9;
			item.value = 800000;
			item.rare = 10;
			item.UseSound = SoundID.DD2_SonicBoomBladeSlash;
			item.noUseGraphic = true;
			item.channel = true;
			item.shoot = mod.ProjectileType("AwokenDreamNail");
			item.shootSpeed = 19f;
		}
		public override bool CanUseItem(Player player)
        {
           return player.ownedProjectileCounts[item.shoot] + player.ownedProjectileCounts[mod.ProjectileType("AwokenDreamNail2")] + player.ownedProjectileCounts[mod.ProjectileType("AwokenDreamGreatSlash")] + player.ownedProjectileCounts[mod.ProjectileType("AwokenDreamDashSlash")] + player.ownedProjectileCounts[mod.ProjectileType("AwokenDreamSpinSlash")] < 1;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.statLife >= player.statLifeMax2)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("AwokenDreamBeam"), damage / 2, 0, player.whoAmI);
            }
            return true;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "DreamNail");
			recipe.AddIngredient(ItemID.FragmentNebula, 3);
			recipe.AddIngredient(ItemID.FragmentSolar, 3);
			recipe.AddIngredient(ItemID.FragmentVortex, 3);
			recipe.AddIngredient(ItemID.FragmentStardust, 3);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

