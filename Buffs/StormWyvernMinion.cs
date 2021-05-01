using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class StormWyvernMinion : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Storm Wyvern");
			Description.SetDefault("The storm wyvern will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			JoostPlayer modPlayer = (JoostPlayer)player.GetModPlayer(mod, "JoostPlayer");
			if (player.ownedProjectileCounts[mod.ProjectileType("StormWyvernMinion")] > 0)
			{
				modPlayer.stormWyvernMinion = true;
			}
			if (!modPlayer.stormWyvernMinion)
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
