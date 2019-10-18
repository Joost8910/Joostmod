using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace JoostMod.Buffs
{
	public class InfectedYellow : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Infected!");
            Description.SetDefault("Losing Life; will spread X Parasites upon death");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
		}
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<JoostPlayer>().infectedYellow = true;
            if (Main.rand.Next(30) == 0)
            {
                Dust.NewDust(player.position, player.width, player.height, 4, 0, 0, 0, Color.Yellow, (1 + Main.rand.Next(5)) * 0.1f);
            }
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCs.JoostGlobalNPC>().infectedYellow = true;
			if (Main.rand.Next(30) == 0)
			{
            	Dust.NewDust(npc.position, npc.width, npc.height, 4, 0, 0, 0, Color.Yellow, (1+Main.rand.Next(5)) * 0.1f);
			}
        }
    }
}
