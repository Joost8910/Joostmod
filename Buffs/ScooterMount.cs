using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class ScooterMount : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Scooter");
			Description.SetDefault("Scoot... Scoot...");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(mod.MountType("Scooter"), player);
			player.buffTime[buffIndex] = 10;
		}
	}
}
