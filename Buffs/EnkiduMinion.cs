using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class EnkiduMinion : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Enkidu");
			Description.SetDefault("Enkidu will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			if (player.ownedProjectileCounts[mod.ProjectileType("EnkiduMinion")] == 1)
			{
				modPlayer.EnkiduMinion = true;
			}
			else
			{
				modPlayer.EnkiduMinion = false;
			}
			if (!modPlayer.EnkiduMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}

		}
	}
}
