using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class HavelBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stone Flesh");
			Description.SetDefault("Reduces damage taken by 40%, mobility greatly reduced");
			Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            //canBeCleared/* tModPorter Note: Removed. Use BuffID.Sets.NurseCannotRemoveDebuff instead, and invert the logic */ = false;
		}
	}
}
