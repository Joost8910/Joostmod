using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class DirtMinion : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soil Spirit");
			Description.SetDefault("The soil spirit will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			JoostPlayer modPlayer = (JoostPlayer)player.GetModPlayer(Mod, "JoostPlayer");
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("DirtMinion").Type] > 0)
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
