using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class CactusBanner : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactus Banner");
			Tooltip.SetDefault("Rapidly summons miniature cactus worms from the ground that damage enemies");
		}
		public override void SetDefaults()
		{
			item.damage = 17;
			item.summon = true;
            item.mana = 10;
			item.width = 22;
			item.height = 44;
			item.useTime = 16;
			item.useAnimation = 16;
			item.useStyle = 4;
			item.noMelee = true; 
			item.knockBack = 2.5f;
			item.value = 60000;
			item.rare = 2;
			item.UseSound = SoundID.Item8;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("CactusSwarm");
			item.shootSpeed = 10f;
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(player.Center.X - 50, player.position.Y + (player.gravDir == -1 ? player.height : 0), 0.08f, 10, type, damage, knockBack, player.whoAmI, player.gravDir);
            Projectile.NewProjectile(player.Center.X + 50, player.position.Y + (player.gravDir == -1 ? player.height : 0), -0.08f, 10, type, damage, knockBack, player.whoAmI, player.gravDir);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "LusciousCactus", 10);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}


