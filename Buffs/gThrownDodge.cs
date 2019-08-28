using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class gThrownDodge : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Counter Dodge");
			Description.SetDefault("You will dodge the next attack and gain Counter Attack");
			Main.buffNoSave[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            int dust = Dust.NewDust(player.position, player.width, player.height, DustID.Smoke, 0, 0, 0, Color.Black);
            Main.dust[dust].noGravity = true;
        }
	}
}
