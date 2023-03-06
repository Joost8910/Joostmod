using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class HavocPendant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Havoc Pendant");
            Tooltip.SetDefault("Multiplies spawnrates by 5");
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
            player.GetModPlayer<JoostPlayer>().HavocPendant = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SoulofNight, 8)
                .AddIngredient(ItemID.DemoniteBar, 8)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.SoulofNight, 8)
                .AddIngredient(ItemID.CrimtaneBar, 8)
                .AddTile(TileID.MythrilAnvil)
                .Register();

        }

    }
}