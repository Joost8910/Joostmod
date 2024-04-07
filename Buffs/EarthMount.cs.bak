using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class EarthMount : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Slabs of Stone");
			Description.SetDefault("Rocky");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(ModContent.MountType<Mounts.EarthMount>(), player);
			player.buffTime[buffIndex] = 10;
		}
	}
}
