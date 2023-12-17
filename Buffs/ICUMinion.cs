using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class ICUMinion : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("ICU");
			Description.SetDefault("The ICU will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<ICUMinion>()] > 0)
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
