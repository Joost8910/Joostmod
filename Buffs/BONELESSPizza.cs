using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class BONELESSPizza : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("BONELESS Pizza");
			Description.SetDefault("Max health increased by 50");
			Main.buffNoSave[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statLifeMax2 += 50;
		}

	}
}
