using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Mounts
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
			Item.width = 30;
			Item.height = 30;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = 225000;
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item79;
			Item.noMelee = true;
			Item.mountType = Mod.Find<ModMount>("SandShark").Type;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
				.AddIngredient<Materials.DesertCore>()
				.AddIngredient(ItemID.SharkFin, 4)
				.AddRecipeGroup("Joostmod:AnyAdamantite", 15)
				.AddTile(TileID.MythrilAnvil)
				.Register();
        }
    }
}