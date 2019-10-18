using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class gThrownCooldown : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Counter Dodge Cooldown");
			Description.SetDefault("Cannot use Counter Dodge");
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            Main.persistentBuff[Type] = true;
            Main.debuff[Type] = true;
            canBeCleared = false;
		}
		/*public override void Update(Player player, ref int buffIndex)
		{
			JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
			if (!modPlayer.gThrown)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}*/
	}
}
