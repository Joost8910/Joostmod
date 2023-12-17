using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace JoostMod.Buffs
{
	public class InfectedRed : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Infected!");
            Description.SetDefault("Losing Life; will spread X Parasites upon death");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<JoostPlayer>().infectedRed = true;
            if (Main.rand.NextBool(30))
            {
                Dust.NewDust(player.position, player.width, player.height, DustID.TintableDust, 0, 0, 0, Color.Red, (1 + Main.rand.Next(5)) * 0.1f);
            }
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCs.JoostGlobalNPC>().infectedRed = true;
            if (Main.rand.NextBool(30))
            {
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.TintableDust, 0, 0, 0, Color.Red, (1 + Main.rand.Next(5)) * 0.1f);
            }
        }
    }
}
