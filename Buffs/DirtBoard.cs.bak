using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class DirtBoard : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dirt Board");
			Description.SetDefault("All-terrain snowboard");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(ModContent.MountType<Mounts.DirtBoard>(), player);
			player.buffTime[buffIndex] = 10;
        }
	}
}
