using JoostMod.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class EmberMinionBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ember");
			Description.SetDefault("The ember will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<EmberMinion>()] > 0)
			{
				modPlayer.emberMinion = true;
			}
			if (!modPlayer.emberMinion)
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
