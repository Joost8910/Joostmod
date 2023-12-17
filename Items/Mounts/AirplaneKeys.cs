//TODO: Make the item into Airplane Keys
using JoostMod.Mounts;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Mounts
{
	public class AirplaneKeys : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Airplane");
			Tooltip.SetDefault("'It can fly!'");
		}
		public override void SetDefaults()
		{
			Item.width = 64;
			Item.height = 38;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = 300000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item79;
			Item.noMelee = true;
			Item.mountType = ModContent.MountType<Plane>();
		}

	}
}