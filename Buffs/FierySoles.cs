using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class FierySoles : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Fiery Soles");
			Description.SetDefault("Hawt");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(mod.MountType("FierySoles"), player);
			player.buffTime[buffIndex] = 10;
		}
	}
}
