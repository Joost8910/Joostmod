using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class Gnome : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Gnome Warrior");
			Description.SetDefault("The Gnome Warrior will fight with you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			JoostPlayer modPlayer = (JoostPlayer)player.GetModPlayer(mod, "JoostPlayer");
			if (player.ownedProjectileCounts[mod.ProjectileType("Gnome")] > 0)
			{
				modPlayer.Gnome = true;
			}
			if (!modPlayer.Gnome)
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
