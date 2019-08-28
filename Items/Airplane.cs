using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class Airplane : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Airplane");
			Tooltip.SetDefault("'It can fly!'");
		}
		public override void SetDefaults()
		{
			item.width = 64;
			item.height = 38;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.value = 300000;
			item.rare = 3;
			item.UseSound = SoundID.Item79;
			item.noMelee = true;
			item.mountType = mod.MountType("Plane");
		}

	}
}