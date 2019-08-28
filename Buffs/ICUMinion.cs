using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class ICUMinion : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("ICU");
			Description.SetDefault("The ICU will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			JoostPlayer modPlayer = (JoostPlayer)player.GetModPlayer(mod, "JoostPlayer");
			if (player.ownedProjectileCounts[mod.ProjectileType("ICUMinion")] > 0)
			{
				modPlayer.icuMinion = true;
			}
			if (!modPlayer.icuMinion)
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
