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
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.width = 24;
            NPC.height = 46;
            NPC.defense = 0;
            NPC.lifeMax = 75;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 0, 0, 70);
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = 3;
            AIType = NPCID.ArmoredSkeleton;
            NPC.damage = 15;
            NPC.frameCounter = 0;
            Banner = Mod.Find<ModNPC>("Cactoid").Type;
            BannerItem = Mod.Find<ModItem>("CactoidBanner").Type;
        }
        public override void OnKill()
        {
            Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.Cactus, 10);
            if (Main.rand.Next(100) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("Anniversary").Type, 1);
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/CrimsonCactite1"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/CrimsonCactite2"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/CrimsonCactite2"), 1f);
            }

        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            NPC.frameCounter++;
            if (NPC.frameCounter >= 15 / (1 + Math.Abs(NPC.velocity.X)))
            {
                NPC.frameCounter = 0;
                NPC.frame.Y = (NPC.frame.Y + 54);
            }
            if (NPC.frame.Y >= 216)
            {
                NPC.frame.Y = 0;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            return !spawnInfo.Player.ZoneBeach && !spawnInfo.PlayerInTown && !spawnInfo.Invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse && spawnInfo.SpawnTileY < Main.rockLayer && spawnInfo.Player.ZoneDesert && spawnInfo.Player.ZoneCrimson && !Main.hardMode ? 0.15f : 0f;
        }
    }
}

