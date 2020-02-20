using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class ZoraHelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zora Helm");
            Tooltip.SetDefault("18% increased magic crit chance");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.value = 225000;
            item.rare = 5;
            item.defense = 8;
        }
        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 18;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("ZoraArmor") && legs.type == mod.ItemType("ZoraGreaves");
        }
        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawAltHair = true;
        }

        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Allows you to swim and breath water\n" +
                "Press the Armor Ability key to do a watery spin attack\n" +
                "The spin attack is far less effective out of water";
            player.accFlipper = true;
            player.gills = true;
            if (!player.mount.Active)
                player.ignoreWater = true;
            player.GetModPlayer<JoostPlayer>().zoraArmor = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WaterEssence", 50);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 4);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 4);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 4);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}