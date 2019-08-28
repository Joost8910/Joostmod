using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class HarpyMinion : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Miniature Harpy");
			Description.SetDefault("The Harpy will fight with you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			JoostPlayer modPlayer = (JoostPlayer)player.GetModPlayer(mod, "JoostPlayer");
			if (player.ownedProjectileCounts[mod.ProjectileType("HarpyMinion")] > 0)
			{
				modPlayer.HarpyMinion = true;
			}
			if (!modPlayer.HarpyMinion)
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
