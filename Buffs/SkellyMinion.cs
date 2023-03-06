using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class SkellyMinion : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mini Skeleton");
			Description.SetDefault("The Skelly will fight with you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			JoostPlayer modPlayer = (JoostPlayer)player.GetModPlayer(Mod, "JoostPlayer");
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("SkellyMinion").Type] > 0)
			{
				modPlayer.Skelly = true;
			}
			if (!modPlayer.Skelly)
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
