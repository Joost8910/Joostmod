using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class Cactuar : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Cactuar");
			Description.SetDefault("The Cactuar will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("Cactuar")] > 0)
			{
				modPlayer.cactuarMinions = true;
			}
			if (!modPlayer.cactuarMinions)
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