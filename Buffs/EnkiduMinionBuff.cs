using JoostMod.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class EnkiduMinionBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enkidu");
			Description.SetDefault("Enkidu will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<EnkiduMinion>()] == 1)
			{
				modPlayer.EnkiduMinion = true;
			}
			else
			{
				modPlayer.EnkiduMinion = false;
			}
			if (!modPlayer.EnkiduMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}

		}
	}
}
