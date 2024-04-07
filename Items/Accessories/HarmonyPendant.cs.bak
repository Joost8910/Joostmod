using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class HarmonyPendant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Harmony Pendant");
            Tooltip.SetDefault("Reduces spawnrates by 80%");
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 36;
            Item.value = 10000;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<JoostPlayer>().HarmonyPendant = true;
        }
        public override void AddRecipes()
        {
            //TODO: Add a material drop from Empress of Light to this recipe.
            CreateRecipe()
                .AddIngredient(ItemID.SoulofLight, 8)
                .AddIngredient(ItemID.HallowedBar, 8)
                .AddTile(TileID.MythrilAnvil)
                .Register();

        }

    }
}