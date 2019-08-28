using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class CactusSpears : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactus Spears");
            Tooltip.SetDefault("'What's better than a spear? Five spears!'");
        }
		public override void SetDefaults()
		{
            item.damage = 19;
			item.melee = true;
			item.width = 60;
			item.height = 60;
			item.useTime = 35;
			item.useAnimation = 35;
            item.useStyle = 5;
            item.noMelee = true;
            item.noUseGraphic = true;
			item.knockBack = 4.5f;
			item.value = 60000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("CactusSpear");
			item.shootSpeed = 7f;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[mod.ProjectileType("CactusSpear")] < 1;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float numberProjectiles = 5;
            float rotation = MathHelper.ToRadians(60);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
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


