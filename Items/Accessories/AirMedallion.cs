using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class AirMedallion : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gust Amulet");
			Tooltip.SetDefault("Increases movement speed by 10%\n" +
                "Your summon attacks have a 10% chance to create a gust of wind");
		}
		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 24;
            Item.value = 225000;
            Item.rare = ItemRarityID.Pink;
			Item.accessory = true;
        }

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
            player.moveSpeed *= 1.1f;
            player.maxRunSpeed *= 1.1f;
            player.GetModPlayer<JoostPlayer>().accRunSpeedMult *= 1.1f;
            player.GetModPlayer<JoostPlayer>().airMedallion = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.TinyTwister>(50)
                .AddRecipeGroup(nameof(ItemID.CobaltBar), 3)
                .AddRecipeGroup(nameof(ItemID.MythrilBar), 3)
                .AddRecipeGroup(nameof(ItemID.AdamantiteBar), 3)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}