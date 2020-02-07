using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Body, EquipType.HandsOff)]
    public class FireChest : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Smoldering Breastplate");
            Tooltip.SetDefault("20% increased ranged damage");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 18;
            item.value = 300000;
            item.rare = 5;
            item.defense = 16;
        }
        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.20f;
        }
        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            drawArms = true;
            drawHands = true;
        }
        public override void UpdateVanity(Player player, EquipType type)
        {
            if (player.handoff == -1)
                player.handoff = (sbyte)mod.GetEquipSlot("FireChest", EquipType.HandsOff);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 50);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 8);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 8);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 8);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}