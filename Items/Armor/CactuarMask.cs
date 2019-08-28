using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class CactuarMask : ModItem
	{
public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactuar Mask");
		}
		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 26;
			item.vanity = true;
			item.value = 10000;
			item.rare = 2;
		}
        public override bool DrawHead()
        {
            return false;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Cactus, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}