using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class TerraFury : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Fury");
        }
        public override void SetDefaults()
        {
            item.damage = 110;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.width = 60;
            item.height = 64;
            item.useTime = 44;
            item.useAnimation = 44;
            item.useStyle = 5;
            item.knockBack = 10;
            item.value = 200000;
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.channel = true;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("TerraFury");
            item.shootSpeed = 28f;
        }
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, -speedX, -speedY, type, damage, knockBack, player.whoAmI);
            return true;
        }
				public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "TrueHallowedFlail");
			recipe.AddIngredient(null, "TrueNightsFury");
			recipe.AddTile(TileID.MythrilAnvil); 
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "TrueHallowedFlail");
			recipe.AddIngredient(null, "TrueBloodMoon");
			recipe.AddTile(TileID.MythrilAnvil); 
			recipe.SetResult(this);
			recipe.AddRecipe();

		}

	}
}

