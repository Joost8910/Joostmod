using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class HavelHelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Havel's Helmet");
            Tooltip.SetDefault("8% increased melee crit Chance\n" +
                "10% reduced movement speed");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.value = 225000;
            item.rare = 5;
            item.defense = 18;
        }
        public override void UpdateEquip(Player player)
        {
            player.meleeCrit += 8;
            player.moveSpeed *= 0.9f;
            player.maxRunSpeed *= 0.9f;
            player.GetModPlayer<JoostPlayer>().accRunSpeedMult *= 0.9f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("HavelArmor") && legs.type == mod.ItemType("HavelLeggings");
        }
        public override bool DrawHead()
        {
            return false;
        }

        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Press the Armor Ability key to reduced damage taken at the cost of mobility";
            player.GetModPlayer<JoostPlayer>().havelArmor = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EarthEssence", 50);
            recipe.AddIngredient(ItemID.StoneBlock, 100);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 4);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 4);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 4);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}