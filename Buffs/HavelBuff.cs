using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class HavelBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Stone Flesh");
			Description.SetDefault("Reduces damage taken by 40%, mobility greatly reduced");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            if (!player.GetModPlayer<JoostPlayer>().havelArmorActive)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
	}
}
