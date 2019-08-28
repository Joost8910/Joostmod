using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
    public class GraySlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gray Slime");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void SetDefaults()
        {
            npc.width = 70;
            npc.height = 50;
            npc.damage = 32;
            npc.defense = 32;
            npc.lifeMax = 640;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 0f;
            npc.knockBackResist = 0.0625f;
            npc.aiStyle = 1;
            aiType = NPCID.BlueSlime;
            animationType = NPCID.BlueSlime;
            banner = npc.type;
            bannerItem = mod.ItemType("GraySlimeBanner");
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 23, Main.rand.Next(10, 20));

        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.playerInTown && spawnInfo.spawnTileY >= Main.rockLayer && spawnInfo.spawnTileY < Main.maxTilesY - 250 && !spawnInfo.player.ZoneJungle && Main.hardMode ? 0.007f : 0f;

        }
        public override bool CheckDead()
        {
            if (Main.netMode != 1)
            {
                NPC.NewNPC((int)npc.Center.X + 13, (int)npc.Center.Y - 2, mod.NPCType("GraySlime2"));
                NPC.NewNPC((int)npc.Center.X - 13, (int)npc.Center.Y - 2, mod.NPCType("GraySlime2"));
            }
            return true;
        }

    }
}

