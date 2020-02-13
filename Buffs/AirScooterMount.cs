using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class AirScooterMount : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Whirlwind Sphere");
			Description.SetDefault("Floaty");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(mod.MountType("AirScooter"), player);
            player.slowFall = true;
			player.buffTime[buffIndex] = 10;
		}
	}
}
