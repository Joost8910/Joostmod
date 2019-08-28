using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class PureNail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pure Nail");
			Tooltip.SetDefault("'Crafted to perfection, this ancient nail reveals its true form'\n" + 
                "Fires beams that deal half damage while at full health");
		}
		public override void SetDefaults()
		{
			item.damage = 55;
			item.melee = true;
			item.width = 46;
			item.height = 44;
			item.noMelee = true;
			item.useTime = 11;
			item.useAnimation = 11;
			item.useStyle = 5;
			item.autoReuse = true;
			item.knockBack = 7;
			item.value = 200000;
			item.rare = 6;
			item.UseSound = SoundID.Item18;
			item.noUseGraphic = true;
			item.channel = true;
			item.shoot = mod.ProjectileType("PureNail");
			item.shootSpeed = 15f;
		}
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] + player.ownedProjectileCounts[mod.ProjectileType("PureNail2")] + player.ownedProjectileCounts[mod.ProjectileType("GreatSlash")] + player.ownedProjectileCounts[mod.ProjectileType("DashSlash")] + player.ownedProjectileCounts[mod.ProjectileType("SpinSlash")] < 1;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.statLife >= player.statLifeMax2)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("PureBeam"), damage / 2, 0, player.whoAmI);
            }
            return true;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "CoiledNail");
			recipe.AddIngredient(ItemID.SoulofMight, 10);
			recipe.AddIngredient(ItemID.SoulofSight, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

