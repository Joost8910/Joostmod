using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class AirLegs : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tornado Legs");
            Tooltip.SetDefault("20% increased movement speed\n" +
                "Increases your max number of minions");
            ArmorIDs.Legs.Sets.HidesBottomSkin[Item.legSlot] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = 250000;
            Item.rare = ItemRarityID.Pink;
            Item.defense = 8;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.2f;
            player.maxRunSpeed *= 1.2f;
            player.GetModPlayer<JoostPlayer>().accRunSpeedMult *= 1.2f;
            player.maxMinions++;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.TinyTwister>(50)
                .AddRecipeGroup(nameof(ItemID.CobaltBar), 6)
                .AddRecipeGroup(nameof(ItemID.MythrilBar), 6)
                .AddRecipeGroup(nameof(ItemID.AdamantiteBar), 6)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}