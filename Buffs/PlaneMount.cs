using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class PlaneMount : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Airplane");
			Description.SetDefault("It can fly!");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(Mod.Find<ModMount>("Plane").Type, player);
			player.buffTime[buffIndex] = 10;
		}
	}
}
