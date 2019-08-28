using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class LunarRod : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunar Rod");
			Tooltip.SetDefault("Fires multiple fishing hooks\n" + "Can fish up Lunar Fragments");
		}
		public override void SetDefaults()
		{
			item.width = 56;
			item.height = 32;
			item.useTime = 8;
			item.useAnimation = 8;
			item.useStyle = 1;
			item.value = 100000;
			item.rare = 9;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.shoot = mod.ProjectileType("LunarFishHook2");
			item.shootSpeed = 17f;
			item.fishingPole = 100;
		}
		public override void HoldItem (Player player)
		{
			player.GetModPlayer<JoostPlayer>(mod).lunarRod = true;
		}
		public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FragmentNebula, 3);
			recipe.AddIngredient(ItemID.FragmentSolar, 3);
			recipe.AddIngredient(ItemID.FragmentVortex, 3);
			recipe.AddIngredient(ItemID.FragmentStardust, 3);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}
		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
{
    float spread = 45f * 0.0174f;
    float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
    double startAngle = Math.Atan2(speedX, speedY)- spread/32;
    double deltaAngle = spread/32f;
    double offsetAngle;
    int i;
    for (i = 0; i < 4;i++ )
    {
        offsetAngle = startAngle + deltaAngle * i;
		   Projectile.NewProjectile(position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), type, damage, knockBack, player.whoAmI);

    }

    return false;
}
	}
}

