using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class WindChimes : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chimes of the Wind");
			Tooltip.SetDefault("Creates currents of wind that damages enemies");
		}
		public override void SetDefaults()
		{
			item.damage = 28;
			item.summon = true;
			item.mana = 10;
			item.width = 42;
			item.height = 42;
			item.useTime = 7;
			item.useAnimation = 21;
			item.useStyle = 4;
			item.knockBack = 7f;
            item.value = 225000;
            item.rare = 5;
			item.noMelee = true;
			item.UseSound = SoundID.Item35;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Wind");
			item.shootSpeed = 12.8f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 30);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            speedX = ((8f + Main.rand.NextFloat() * 8f) * player.direction) + (player.direction * player.velocity.X > 0 ? player.velocity.X : 0);
            speedY = 0;
            position.X -= 1000 * player.direction;
            position.Y += Main.rand.Next(-8, 8) * 10;
            knockBack = Math.Abs(speedX);
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TinyTwister", 50);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 4);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 4);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 4);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

