using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class SandySaddle : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sandy Saddle");
			Tooltip.SetDefault("Summons a rideable sand shark\n" +
                "Swims through sand");
		}
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.value = 225000;
			item.rare = 4;
			item.UseSound = SoundID.Item79;
			item.noMelee = true;
			item.mountType = mod.MountType("SandShark");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesertCore", 1);
            recipe.AddIngredient(ItemID.SharkFin, 4);
            recipe.AddIngredient(ItemID.AdamantiteBar, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesertCore", 1);
            recipe.AddIngredient(ItemID.SharkFin, 4);
            recipe.AddIngredient(ItemID.TitaniumBar, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}