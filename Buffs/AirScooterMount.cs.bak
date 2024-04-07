using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class AirScooterMount : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Whirlwind Sphere");
			Description.SetDefault("Floaty");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(ModContent.MountType<Mounts.AirScooter>(), player);
            player.slowFall = true;
			player.buffTime[buffIndex] = 10;
		}
	}
}
