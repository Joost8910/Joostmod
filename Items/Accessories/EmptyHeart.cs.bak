using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Accessories
{
    public class EmptyHeart : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Empty Heart");
            Tooltip.SetDefault("Reduces your health to 1\n" +
                "20% Increased Damage");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.value = 0;
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<JoostPlayer>().emptyHeart = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Deathweed, 2)
                .AddIngredient(ItemID.HeartLantern)
                .AddTile(TileID.DemonAltar)
                .Register();
        }

    }
}