using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
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
			item.width = 40;
			item.height = 30;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.value = 50000;
			item.rare = 2;
			item.UseSound = SoundID.Item79;
			item.noMelee = true;
			item.mountType = mod.MountType("Scooter");
		}

	}
}