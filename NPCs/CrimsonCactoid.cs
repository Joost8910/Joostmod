using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
    public class CrimsonCactoid : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactoid");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 24;
            npc.height = 70;
            npc.damage = 25;
            npc.defense = 8;
            npc.lifeMax = 100;
            if (NPC.downedMoonlord)
            {
                npc.damage = 75;
                npc.defense = 30;
                npc.lifeMax = 800;
            }
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 0, 1, 0);
            npc.knockBackResist = 0.5f;
            npc.aiStyle = 26;
            aiType = NPCID.Unicorn;
            npc.frameCounter = 0;
            banner = mod.NPCType("Cactoid");
            bannerItem = mod.ItemType("CactoidBanner");
        }
        public override void AI()
        {
            npc.velocity.X = npc.velocity.X * 0.99f;
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Cactus, 10);
            if (Main.rand.Next(100) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Anniversary"), 1);
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/CrimsonCactite1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/CrimsonCactite2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/CrimsonCactite2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/CrimsonCactoid1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/CrimsonCactoid2"), 1f);
            }

        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frameCounter++;
            if (npc.frameCounter >= 15 / (1 + Math.Abs(npc.velocity.X)))
            {
                npc.frameCounter = 0;
                npc.frame.Y = (npc.frame.Y + 74);
            }
            if (npc.frame.Y >= 296)
            {
                npc.frame.Y = 0;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY];
            return !spawnInfo.player.ZoneBeach && !spawnInfo.playerInTown && !spawnInfo.invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse && spawnInfo.spawnTileY < Main.rockLayer && spawnInfo.player.ZoneDesert && spawnInfo.player.ZoneCrimson ? 0.1f : 0f;
        }


    }
}

