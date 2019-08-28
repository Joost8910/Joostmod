using System; 
using Microsoft.Xna.Framework; 
using Terraria; 
using Terraria.ID; 
using Terraria.ModLoader; 

namespace JoostMod.Items
{
	public class BONELESSPizza : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("BONELESS Pizza");
			Tooltip.SetDefault("Minor improvements to all stats\n" + "Increases max health by 50");
		}
		public override void SetDefaults()
		{
			item.maxStack = 30;
			item.consumable = true;
			item.width = 20;
			item.height = 26;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 2;
			item.knockBack = 5;
			item.value = 100000;
			item.rare = 7;
			item.UseSound = SoundID.Item2;
			item.buffTime = 108000;
			item.buffType = BuffID.WellFed;
		}
		public override bool ConsumeItem(Player player)
		{
			player.AddBuff(mod.BuffType("BONELESSPizza"), 108000);
			return true;
		}
				public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Hay, 32); 
			recipe.AddIngredient(ItemID.Mushroom, 16); 
			recipe.AddIngredient(ItemID.Bacon, 4); 
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this, 8);
			recipe.AddRecipe();
		}

	}
}

