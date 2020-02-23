using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class HavelArmor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Havel's Armor");
            Tooltip.SetDefault("Grants immunity to knockback\n" + 
                "7% increased melee damage\n" +
                "10% reduced movement speed");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 18;
            item.value = 300000;
            item.rare = 5;
            item.defense = 30;
        }
        public override bool DrawBody()
        {
            return false;
        }
        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += 0.07f;
            player.moveSpeed *= 0.9f;
            player.maxRunSpeed *= 0.9f;
            player.GetModPlayer<JoostPlayer>().accRunSpeedMult *= 0.9f;
            player.noKnockback = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EarthEssence", 50);
            recipe.AddIngredient(ItemID.StoneBlock, 200);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 8);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 8);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 8);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}