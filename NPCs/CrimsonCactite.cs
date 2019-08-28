using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
    public class CrimsonCactite : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactite");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 24;
            npc.height = 46;
            npc.defense = 0;
            npc.lifeMax = 75;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 0, 0, 70);
            npc.knockBackResist = 0.5f;
            npc.aiStyle = 3;
            aiType = NPCID.ArmoredSkeleton;
            npc.damage = 15;
            npc.frameCounter = 0;
            banner = mod.NPCType("Cactoid");
            bannerItem = mod.ItemType("CactoidBanner");
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
            }

        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frameCounter++;
            if (npc.frameCounter >= 15 / (1 + Math.Abs(npc.velocity.X)))
            {
                npc.frameCounter = 0;
                npc.frame.Y = (npc.frame.Y + 54);
            }
            if (npc.frame.Y >= 216)
            {
                npc.frame.Y = 0;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY];
            return !spawnInfo.player.ZoneBeach && !spawnInfo.playerInTown && !spawnInfo.invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse && spawnInfo.spawnTileY < Main.rockLayer && spawnInfo.player.ZoneDesert && spawnInfo.player.ZoneCrimson && !Main.hardMode ? 0.15f : 0f;
        }
    }
}

