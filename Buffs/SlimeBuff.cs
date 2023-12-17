using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class SlimeBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sticky Slime");
			Description.SetDefault("20% reduced damage. Sticks to enemies. Cannot use items.");
			Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
            player.buffTime[buffIndex] = 10;
        }
    }
}
