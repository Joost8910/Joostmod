using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace JoostMod.Buffs
{
	public class Sap : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sap");
            Description.SetDefault("Losing Life");
            Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<JoostPlayer>().sap = true;
            Dust d = Dust.NewDustDirect(player.position, player.width, player.height, 183, 0, 2f);
            d.velocity.X = 0;
            d.velocity.Y = 2;
            d.position.Y -= 16;
            d.noGravity = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCs.JoostGlobalNPC>().sap = true;
            Dust d = Dust.NewDustDirect(npc.position, npc.width, npc.height, 183, 0, 2f);
            d.velocity.X = 0;
            d.velocity.Y = 2;
            d.position.Y -= 16;
            d.noGravity = true;
        }
    }
}
