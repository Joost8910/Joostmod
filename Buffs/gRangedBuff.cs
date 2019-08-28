using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class gRangedBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Ranged Mastery");
			Description.SetDefault("Defense reduced to 0, ranged damage increased by 0.5% per defense point lost");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			//player.rangedDamage += 1f;
            Dust.NewDust(player.position, player.width, player.width, 258);
            if (!player.GetModPlayer<JoostPlayer>(mod).gRangedIsActive)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
	}
}
