using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class Whirlwind : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Whirlwind");
			Description.SetDefault("Defense increased by 25");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statDefense += 25;
		}

	}
}
