using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace JoostMod.Buffs
{
	public class LifeDrink : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Life Rend");
            Description.SetDefault("Will heal a nearby enemy player on death");
            Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<JoostPlayer>().lifeRend = true;
            if (Main.rand.Next(5) == 0)
            {
                Dust.NewDust(player.position, player.width, player.height, 5, Main.rand.Next(-20, 20) * 0.01f, 3f, 0, default(Color), (6 + Main.rand.Next(5)) * 0.1f);
            }
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCs.JoostGlobalNPC>().lifeRend = true;
			if (Main.rand.Next(5) == 0)
			{
    	        Dust.NewDust(npc.position, npc.width, npc.height, 5, Main.rand.Next(-20, 20) * 0.01f, 3f, 0, default(Color), (6+Main.rand.Next(5)) * 0.1f);
			}
        }
    }
}
