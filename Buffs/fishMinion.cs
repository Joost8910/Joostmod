using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class fishMinion : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pufferfish Minion");
			Description.SetDefault("The pufferfish will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("fishMinion").Type] > 0)
			{
				modPlayer.fishMinion = true;
			}
			if (!modPlayer.fishMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else
			{
				player.buffTime[buffIndex] = 18000;
			}
		}
	}
}
