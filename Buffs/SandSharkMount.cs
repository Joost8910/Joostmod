using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class SandSharkMount : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Sand Shark");
			Description.SetDefault("'The desert DOES have fish'");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(mod.MountType("SandShark"), player);
			player.buffTime[buffIndex] = 10;
            player.suffocating = false;
        }
	}
}
