using System; 
using Microsoft.Xna.Framework; 
using Terraria; 
using Terraria.ID; 
using Terraria.ModLoader; 

namespace JoostMod.Items.Consumables
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
			Item.maxStack = 30;
			Item.consumable = true;
			Item.width = 20;
			Item.height = 26;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 2;
			Item.knockBack = 5;
			Item.value = 250000;
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item3;
			Item.buffTime = 54000;
			Item.buffType = Mod.Find<ModBuff>("JoostJuice").Type;
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
			CreateRecipe()
				.AddIngredient(ItemID.ChlorophyteOre)
				.AddIngredient(ItemID.RegenerationPotion)
				.AddIngredient(ItemID.SwiftnessPotion)
				.AddIngredient(ItemID.IronskinPotion)
				.AddIngredient(ItemID.HeartreachPotion)
				.AddIngredient(ItemID.LifeforcePotion)
				.AddIngredient(ItemID.EndurancePotion)
				.AddIngredient(ItemID.RagePotion)
				.AddIngredient(ItemID.WrathPotion)
				.AddIngredient(ItemID.WarmthPotion)
				.AddIngredient(ItemID.SummoningPotion)
				.AddTile(TileID.Bottles)
				.Register();
		}

	}
}

