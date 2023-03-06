using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Tools
{
	public class DukeFishRod : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Duke Fishrod");
		}
		public override void SetDefaults()
		{
			Item.width = 46;
			Item.height = 36;
			Item.useTime = 8;
			Item.useAnimation = 8;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = 500000;
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.shoot = Mod.Find<ModProjectile>("DukeFishHook2").Type;
			Item.shootSpeed = 18f;
			Item.fishingPole = 75;
		}
 
	}
}

