using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
    public class DancingZombie : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zombie");
            Main.npcFrameCount[NPC.type] = 16;
        }
        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 40;
            NPC.damage = 14;
            NPC.defense = 6;
            NPC.lifeMax = 50;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = Item.buyPrice(0, 0, 8, 0);
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = 3;
            AIType = NPCID.Zombie;
            NPC.frameCounter = 0;
            Banner = NPCID.Zombie;
            BannerItem = ItemID.ZombieBanner;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.PlayerInTown && !spawnInfo.Invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse && spawnInfo.SpawnTileY <= Main.worldSurface && !spawnInfo.Sky && !Main.dayTime ? 0.005f : 0f;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DancingZombie").Type);
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 15)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y = (NPC.frame.Y + 50);
            }
            if (NPC.frame.Y >= 800)
            {
                NPC.frame.Y = 0;
            }
        }

    }
}

