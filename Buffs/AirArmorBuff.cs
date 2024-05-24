using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class AirArmorBuff : ModBuff
    {
        private int defAdded;
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Swirling Winds");
			// Description.SetDefault("Greatly increased mobility and life regen");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.GetModPlayer<JoostPlayer>().airArmorIsActive = true;
			int def = 1 + player.buffTime[buffIndex] / 120;
            player.statDefense += def;
            defAdded = def;
        }
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            tip += defAdded;
        }
    }
}
