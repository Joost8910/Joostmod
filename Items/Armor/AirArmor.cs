using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class AirArmor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tornado Plate");
            Tooltip.SetDefault("Increases your max number of minions");
            ArmorIDs.Body.Sets.HidesArms[Item.bodySlot] = true;
            ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 18;
            Item.value = 300000;
            Item.rare = ItemRarityID.Pink;
            Item.defense = 10;
        }
        public override void UpdateEquip(Player player)
        {
            player.maxMinions++;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.TinyTwister>(50)
                .AddRecipeGroup("JoostMod:AnyCobalt", 8)
                .AddRecipeGroup("JoostMod:AnyMythril", 8)
                .AddRecipeGroup("JoostMod:AnyAdamantite", 8)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}