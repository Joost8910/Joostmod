using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class PowerSpirit : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spirit of Power");
			Description.SetDefault("The Spirit of Power will protect you\n" + "Max minions increased by 1");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.maxMinions++;
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("PowerSpirit").Type] > 0)
			{
				modPlayer.powerSpirit = true;
			}
			if (!modPlayer.powerSpirit)
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