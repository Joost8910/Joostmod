using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Mounts
{
	public class Scooter : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scooter");
			Tooltip.SetDefault("'Scoot around on this!'");
		}
		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 30;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = 50000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item79;
			Item.noMelee = true;
			Item.mountType = Mod.Find<ModMount>("Scooter").Type;
		}

	}
}