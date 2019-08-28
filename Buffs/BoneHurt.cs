using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Buffs
{
	public class BoneHurt : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Your bones hurt");
            Description.SetDefault("Losing life, amount lost increases over time");
            Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCs.JoostGlobalNPC>(mod).bonesHurt = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<JoostPlayer>(mod).bonesHurt = true;
        }
    }
}
