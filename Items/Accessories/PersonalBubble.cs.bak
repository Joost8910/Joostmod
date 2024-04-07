using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Accessories
{
	public class PersonalBubble : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Personal Bubble");
			Tooltip.SetDefault("You always count as being in water\n" +
                "Your magic attacks deal 15% more damage to submerged enemies\n" +
                "Pushes back enemies; hide visual to disable");
		}
		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
            Item.value = 225000;
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<JoostPlayer>().waterBubbleItem = Item;
            player.GetModPlayer<JoostPlayer>().hideBubble = hideVisual;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                //.AddIngredient<BubbleShield>()
                .AddIngredient<Materials.WaterEssence>(50)
                .AddRecipeGroup(nameof(ItemID.CobaltBar), 3)
                .AddRecipeGroup(nameof(ItemID.MythrilBar), 3)
                .AddRecipeGroup(nameof(ItemID.AdamantiteBar), 3)
                .AddTile<Tiles.ElementalForge>()
                .Register();

        }
    }
}