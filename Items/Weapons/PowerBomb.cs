using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class PowerBomb : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Power Bomb");
			Tooltip.SetDefault("Explodes into a powerful heat wave");
		}
		public override void SetDefaults()
		{
			item.damage = 500;
			item.thrown = true;
			item.width = 16;
			item.height = 16;
			item.useTime = 36;
			item.useAnimation = 36;
			item.useStyle = 1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.knockBack = 0;
			item.value = 10000000;
			item.rare = 11;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/LayBomb");
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("PowerBomb");
			item.shootSpeed = 12f;
        }
        public override bool CanUseItem(Player player)
        {
            if ((player.ownedProjectileCounts[item.shoot] + player.ownedProjectileCounts[mod.ProjectileType("PowerBombExplosion")] + player.ownedProjectileCounts[mod.ProjectileType("PowerBombExplosion2")]) >= 1)
            {
                return false;
            }
            else return true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float distance = player.Distance(Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY));
            Vector2 velocity = new Vector2(speedX, speedY);
            velocity.Normalize();
            speedX = velocity.X * (distance / 60);
            speedY = velocity.Y * (distance / 60);
            return true;
        }
        public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "IceCoreX", 1);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}
