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
			NPC.width = 156;
			NPC.height = 38;
			NPC.damage = 0;
			NPC.defense = 0;
            NPC.lifeMax = 500;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 0;
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1;
			NPC.frameCounter = 0;
            NPC.netAlways = true;
		}
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.Player.ZoneBeach && spawnInfo.Player.ZoneDesert && spawnInfo.SpawnTileY > Main.worldSurface && !JoostWorld.downedCactusWorm && JoostWorld.activeQuest.Contains(NPC.type) && !NPC.AnyNPCs(NPC.type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("AlphaCactusWormHead").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("GrandCactusWormHead").Type) ? 0.15f : 0f;
        }
        public override void AI()
        {
            if (NPC.AnyNPCs(Mod.Find<ModNPC>("AlphaCactusWormHead").Type) || NPC.AnyNPCs(Mod.Find<ModNPC>("GrandCactusWormHead").Type))
            {
                NPC.active = false;
            }
        }
        public override bool CheckDead()
        {
            if (Main.netMode != 1)
            {
                NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("AlphaCactusWormHead").Type);
            }
            return true;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}

