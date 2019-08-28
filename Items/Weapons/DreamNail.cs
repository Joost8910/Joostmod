using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class DreamNail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dream Nail");
			Tooltip.SetDefault("'Cut through the veil between dreams and waking'\n" + 
			"Attacks pierce 50% of enemy defense");
		}
		public override void SetDefaults()
		{
			item.damage = 99;
			item.melee = true;
			item.width = 50;
			item.height = 50;
			item.noMelee = true;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = 5;
			item.autoReuse = true;
			item.knockBack = 8;
			item.value = 400000;
			item.rare = 8;
			item.UseSound = SoundID.DD2_SonicBoomBladeSlash;
			item.noUseGraphic = true;
			item.channel = true;
			item.shoot = mod.ProjectileType("DreamNail");
			item.shootSpeed = 17f;
		}
		public override bool CanUseItem(Player player)
        {
           return player.ownedProjectileCounts[item.shoot] + player.ownedProjectileCounts[mod.ProjectileType("DreamNail2")] + player.ownedProjectileCounts[mod.ProjectileType("DreamGreatSlash")] + player.ownedProjectileCounts[mod.ProjectileType("DreamDashSlash")] + player.ownedProjectileCounts[mod.ProjectileType("DreamSpinSlash")] < 1;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.statLife >= player.statLifeMax2)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DreamBeam"), damage / 2, 0, player.whoAmI);
            }
            return true;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "PureNail");
			recipe.AddIngredient(ItemID.BrokenHeroSword);
			recipe.AddIngredient(ItemID.SoulofLight, 25);
			recipe.AddIngredient(ItemID.LightShard, 2);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

