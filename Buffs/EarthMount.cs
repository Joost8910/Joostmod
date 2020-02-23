using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class EarthMount : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Stone Platforms");
			Description.SetDefault("Rocky");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(mod.MountType("EarthMount"), player);
			player.buffTime[buffIndex] = 10;
		}
	}
}
