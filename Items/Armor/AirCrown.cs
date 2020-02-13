using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class AirCrown : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crown of Wind");
            Tooltip.SetDefault("20% increased summon damage\n" +
                "Increases your max number of minions");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.value = 225000;
            item.rare = 5;
            item.defense = 6;
        }
        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.2f;
            player.maxMinions++;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("AirArmor") && legs.type == mod.ItemType("AirLegs");
        }
        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawAltHair = true;
        }

        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Press the Armor Ability key to sacrifice your minions\n" +
                "You gain greatly increased mobility and life regen\n" +
                "The duration is based on how many minions you sacrifice\n" +
                "You also gain brief invulnerability on activation";
            player.GetModPlayer<JoostPlayer>().airArmor = true;
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