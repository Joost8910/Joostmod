using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class ClippedWings : ModBuff
	{
		public override void SetStaticDefaults()
		{
            Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
        public override void Update(Player player, ref int buffIndex)
        {
            //player.rocketTime = 0;
            player.slowFall = false;
            player.wingTime = 0;
            player.wingsLogic = 0;
            player.mount.Dismount(player);
        }
    }
}
