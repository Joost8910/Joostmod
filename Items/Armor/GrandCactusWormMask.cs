using Terraria.ModLoader;
using Terraria.ID;

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
			Item.width = 18;
			Item.height = 26;
			Item.vanity = true;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
		}
        
	}
}