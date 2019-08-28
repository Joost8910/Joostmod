using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class GrandCactusWormMask : ModItem
	{
public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactus Worm Mask");
		}
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 26;
			item.vanity = true;
			item.value = 10000;
			item.rare = 2;
		}
        
	}
}