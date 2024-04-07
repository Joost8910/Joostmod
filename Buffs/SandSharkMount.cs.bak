using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class SandSharkMount : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sand Shark");
			Description.SetDefault("'The desert DOES have fish'");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(ModContent.MountType<Mounts.SandShark>(), player);
			player.buffTime[buffIndex] = 10;
            player.suffocating = false;
        }
	}
}
