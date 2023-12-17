using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class FierySoles : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fiery Soles");
			Description.SetDefault("Hawt");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(ModContent.MountType<Mounts.FierySoles>(), player);
			player.buffTime[buffIndex] = 10;
		}
	}
}
