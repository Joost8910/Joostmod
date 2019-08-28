using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class CactoidFriend : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Friend of Cactoids");
			Description.SetDefault("Cactoids become friendly and will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}
	}
}