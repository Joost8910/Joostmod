using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class EnergyBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Enhanced Energy");
			Description.SetDefault("Damage and Movement Speed increased by 10%");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.allDamageMult += 0.1f;
            player.moveSpeed += 0.1f;
            player.accRunSpeed *= 1.1f;
            player.maxRunSpeed *= 1.1f;
            int dust = Dust.NewDust(player.position, player.width, player.height, 163);
            Main.dust[dust].noGravity = true;
        }
	}
}
