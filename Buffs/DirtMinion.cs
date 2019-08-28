using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class DirtMinion : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Soil Spirit");
			Description.SetDefault("The soil spirit will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			JoostPlayer modPlayer = (JoostPlayer)player.GetModPlayer(mod, "JoostPlayer");
			if (player.ownedProjectileCounts[mod.ProjectileType("DirtMinion")] > 0)
			{
				modPlayer.dirtMinion = true;
			}
			if (!modPlayer.dirtMinion)
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
