using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class EmberMinion : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Ember");
			Description.SetDefault("The ember will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			JoostPlayer modPlayer = (JoostPlayer)player.GetModPlayer(mod, "JoostPlayer");
			if (player.ownedProjectileCounts[mod.ProjectileType("EmberMinion")] > 0)
			{
				modPlayer.emberMinion = true;
			}
			if (!modPlayer.emberMinion)
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
