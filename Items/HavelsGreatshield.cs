using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	[AutoloadEquip(EquipType.Shield)]
	public class HavelsGreatshield : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Havel's Greatshield");
			Tooltip.SetDefault("Reduces movement speed by 5%\n" +
                "Right click to block attacks in front of you\n" + 
                "Blocking an attack reduces its damage by 80%\n" + 
                "Left click while blocking to shield bash");
		}
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 46;
            item.value = 225000;
            item.rare = 5;
			item.accessory = true;
			item.defense = 5;
            item.damage = 50;
            item.knockBack = 8;
            item.melee = true;
        }

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
            player.moveSpeed *= 0.95f;
            player.maxRunSpeed *= 0.95f;
            player.GetModPlayer<JoostPlayer>().accRunSpeedMult *= 0.95f;
            player.GetModPlayer<JoostPlayer>().havelShield = true;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria")
                {
                    if (line2.Name == "Damage" || line2.Name == "CritChance" || line2.Name == "Knockback")
                    {
                        line2.overrideColor = Color.DarkGray;
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EarthEssence", 50);
            recipe.AddIngredient(ItemID.StoneBlock, 200);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 4);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 4);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 4);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}