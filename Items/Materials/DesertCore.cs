using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Materials
{
	public class DesertCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desert Core");
			Tooltip.SetDefault("'Filled with mysterious energy'");
		}
		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.width = 26;
			Item.height = 26;
			Item.useTime = 2;
			Item.useAnimation = 2;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 0;
			Item.value = 100000;
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item1;
		}

	}
}

