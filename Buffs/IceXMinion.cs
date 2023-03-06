using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class IceXMinion : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ice Core-X Minion");
			Description.SetDefault("The Ice Core-X will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("IceXMinion").Type] > 0)
			{
				modPlayer.IceXMinion = true;
			}
			if (!modPlayer.IceXMinion)
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
