using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class PinkSlimeBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bouncy Slime");
			Description.SetDefault("Very bouncy. 50% reduced damage. Cannot use items.");
			Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true;
            canBeCleared/* tModPorter Note: Removed. Use BuffID.Sets.NurseCannotRemoveDebuff instead, and invert the logic */ = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
            player.buffTime[buffIndex] = 10;
        }
    }
}
