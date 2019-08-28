using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class FrostEmberMinion : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Frostfire Ember");
			Description.SetDefault("The frostfire ember will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			JoostPlayer modPlayer = (JoostPlayer)player.GetModPlayer(mod, "JoostPlayer");
			if (player.ownedProjectileCounts[mod.ProjectileType("FrostEmberMinion")] > 0)
			{
				modPlayer.frostEmberMinion = true;
			}
			if (!modPlayer.frostEmberMinion)
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
