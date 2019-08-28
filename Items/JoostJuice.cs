using System; 
using Microsoft.Xna.Framework; 
using Terraria; 
using Terraria.ID; 
using Terraria.ModLoader; 

namespace JoostMod.Items
{
	public class JoostJuice : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Joost Juice");
			Tooltip.SetDefault("Gives the effects of Well fed, Regeneration, Swiftness, Ironskin\n" + 
			"Heartreach, Lifeforce, Endurance, Rage, Wrath, Warmth, and Summoning Buffs\n" +
			"'Joosy'");
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
			item.rare = 9;
			item.UseSound = SoundID.Item3;
			item.buffTime = 54000;
			item.buffType = mod.BuffType("JoostJuice");
		}
		/*public override bool ConsumeItem(Player player)
		{
			player.AddBuff(BuffID.WellFed, 54000);
			player.AddBuff(BuffID.Regeneration, 54000);
			player.AddBuff(BuffID.Swiftness, 54000);
			player.AddBuff(BuffID.Ironskin, 54000);
			player.AddBuff(BuffID.Heartreach, 54000);
			player.AddBuff(BuffID.Lifeforce, 54000);
			player.AddBuff(BuffID.Endurance, 54000);
			player.AddBuff(BuffID.Rage, 54000);
			player.AddBuff(BuffID.Wrath, 54000);
			player.AddBuff(BuffID.Warmth, 54000);
			player.AddBuff(BuffID.Summoning, 54000);
			player.AddBuff(mod.BuffType("JoostJuice"), 54000);	
			return true;
		}*/
				public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ChlorophyteOre); 
			recipe.AddIngredient(ItemID.RegenerationPotion);
			recipe.AddIngredient(ItemID.SwiftnessPotion);
			recipe.AddIngredient(ItemID.IronskinPotion);
			recipe.AddIngredient(ItemID.HeartreachPotion);
			recipe.AddIngredient(ItemID.LifeforcePotion);
			recipe.AddIngredient(ItemID.EndurancePotion);
			recipe.AddIngredient(ItemID.RagePotion);
			recipe.AddIngredient(ItemID.WrathPotion);
			recipe.AddIngredient(ItemID.WarmthPotion);
			recipe.AddIngredient(ItemID.SummoningPotion);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}

