using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class WindMinion : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Magic Tornado");
			Description.SetDefault("The Tornado will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			JoostPlayer modPlayer = (JoostPlayer)player.GetModPlayer(Mod, "JoostPlayer");
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("WindMinion").Type] > 0)
			{
				modPlayer.WindMinion = true;
			}
			if (!modPlayer.WindMinion)
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
