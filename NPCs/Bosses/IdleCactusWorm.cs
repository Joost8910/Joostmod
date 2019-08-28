using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Bosses
{
    [AutoloadBossHead]
	public class IdleCactusWorm : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Alpha Cactus Worm");
		}
		public override void SetDefaults()
		{
			npc.width = 156;
			npc.height = 38;
			npc.damage = 0;
			npc.defense = 0;
            npc.lifeMax = 500;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = 0;
			npc.knockBackResist = 0f;
			npc.aiStyle = -1;
			npc.frameCounter = 0;
            npc.netAlways = true;
		}
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.player.ZoneBeach && spawnInfo.player.ZoneDesert && spawnInfo.spawnTileY > Main.worldSurface && !JoostWorld.downedCactusWorm && JoostWorld.activeQuest == npc.type && !NPC.AnyNPCs(npc.type) && !NPC.AnyNPCs(mod.NPCType("AlphaCactusWormHead")) && !NPC.AnyNPCs(mod.NPCType("GrandCactusWormHead")) ? 0.15f : 0f;
        }
        public override void AI()
        {
            if (NPC.AnyNPCs(mod.NPCType("AlphaCactusWormHead")) || NPC.AnyNPCs(mod.NPCType("GrandCactusWormHead")))
            {
                npc.active = false;
            }
        }
        public override bool CheckDead()
        {
            if (Main.netMode != 1)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AlphaCactusWormHead"));
            }
            return true;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}

