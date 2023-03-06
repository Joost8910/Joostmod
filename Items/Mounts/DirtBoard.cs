using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Mounts
{
	public class DirtBoard : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dirt Board");
			Tooltip.SetDefault("Slide down slopes to gain velocity");
		}
		public override void SetDefaults()
		{
			Item.width = 38;
			Item.height = 20;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = 50000;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item79;
			Item.noMelee = true;
			Item.mountType = Mod.Find<ModMount>("DirtBoard").Type;
        }
    }
}