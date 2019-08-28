using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class gThrownBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Counter Attack");
			Description.SetDefault("Thrown damage and velocity increased by 50%");
			Main.buffNoSave[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            player.thrownDamageMult *= 1.5f;
            player.thrownVelocity *= 1.5f;
            int dust = Dust.NewDust(player.position + new Vector2(0, player.width / 2), player.width, player.width, 163);
            Main.dust[dust].noGravity = true;
        }
	}
}
