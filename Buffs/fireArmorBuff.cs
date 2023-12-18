using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class FireArmorBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Overwhelming Fire");
			Description.SetDefault("Ranged damage and movement speed increased by 40%, rapidly losing life");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            /* //These are handled in JoostPlayer 
			player.rangedDamageMult *= 1.4f;
            player.moveSpeed *= 1.4f;
            player.maxRunSpeed *= 1.4f;
            player.accRunSpeed *= 1.4f;
            player.onFire = true;
            Dust.NewDust(player.position, player.width, player.width, 6);
            */
            if (!player.GetModPlayer<JoostPlayer>().fireArmorIsActive)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
	}
}
