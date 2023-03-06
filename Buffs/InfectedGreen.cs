using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace JoostMod.Buffs
{
	public class InfectedGreen : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Infected!");
            Description.SetDefault("Losing Life; will spread X Parasites upon death");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<JoostPlayer>().infectedGreen = true;
            if (Main.rand.Next(30) == 0)
            {
                Dust.NewDust(player.position, player.width, player.height, 4, 0, 0, 0, Color.Green, (1 + Main.rand.Next(5)) * 0.1f);
            }
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCs.JoostGlobalNPC>().infectedGreen = true;
            if (Main.rand.Next(30) == 0)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 4, 0, 0, 0, Color.Green, (1 + Main.rand.Next(5)) * 0.1f);
            }
        }
    }
}
