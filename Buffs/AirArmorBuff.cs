using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class AirArmorBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Swirling Winds");
			Description.SetDefault("Greatly increased mobility and life regen");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.GetModPlayer<JoostPlayer>().airArmorIsActive = true;
        }
	}
}
