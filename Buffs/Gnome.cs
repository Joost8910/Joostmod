using System;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class Gnome : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gnome Warrior");
			Description.SetDefault("The Gnome Warrior will fight with you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			JoostPlayer modPlayer = (JoostPlayer)player.GetModPlayer(Mod, "JoostPlayer");
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Gnome").Type] > 0)
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
