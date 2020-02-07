using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class FireCrown : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crown of Fire");
            Tooltip.SetDefault("18% increased ranged crit Chance");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.value = 225000;
            item.rare = 5;
            item.defense = 9;
        }
        public override void UpdateEquip(Player player)
        {
            player.rangedCrit += 18;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("FireChest") && legs.type == mod.ItemType("FireLeggings");
        }
        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawAltHair = true;
        }

        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Press the Armor Ability key to become overflowing with fire\n" +
                "You deal increased ranged damage and gain increased movement speed, at the cost of losing life";
            player.GetModPlayer<JoostPlayer>().fireArmor = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FireEssence", 50);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 4);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 4);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 4);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}