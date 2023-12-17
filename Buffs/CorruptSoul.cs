using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace JoostMod.Buffs
{
	public class CorruptSoul : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Corrupted Soul");
            Description.SetDefault("Losing Life; will spawn a corrupted soul upon death");
            Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<JoostPlayer>().corruptSoul = true;
            if (Main.rand.NextBool(5))
            {
                Dust.NewDust(player.position, player.width, player.height, DustID.Demonite, Main.rand.Next(-15, 16) * 0.01f, -3f, 0, default(Color), (6 + Main.rand.Next(5)) * 0.1f);
            }
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCs.JoostGlobalNPC>().corruptSoul = true;
			if (Main.rand.NextBool(5))
			{
            	Dust.NewDust(npc.position, npc.width, npc.height, DustID.Demonite, Main.rand.Next(-15, 16) * 0.01f, -3f, 0, default(Color), (6+Main.rand.Next(5)) * 0.1f);
			}
        }
    }
}
