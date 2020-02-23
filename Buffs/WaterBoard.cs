using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class WaterBoard : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Water Board");
			Description.SetDefault("Hang ten!");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(mod.MountType("WaterBoard"), player);
			player.buffTime[buffIndex] = 10;
            player.waterWalk2 = true;
            player.ignoreWater = true;

        }
	}
}
