using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
	public class DukeFishRod : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Duke Fishrod");
		}
		public override void SetDefaults()
		{
			item.width = 46;
			item.height = 36;
			item.useTime = 8;
			item.useAnimation = 8;
			item.useStyle = 1;
			item.value = 500000;
			item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.shoot = mod.ProjectileType("DukeFishHook2");
			item.shootSpeed = 18f;
			item.fishingPole = 75;
		}
 
	}
}

