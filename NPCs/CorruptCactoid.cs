using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
    public class CorruptCactoid : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactoid");
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.width = 24;
            NPC.height = 70;
            NPC.defense = 5;
            NPC.lifeMax = 100;
            NPC.damage = 30;
            if (NPC.downedMoonlord)
            {
                NPC.damage = 90;
                NPC.defense = 20;
                NPC.lifeMax = 800;
            }
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 0, 1, 0);
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = 26;
            AIType = NPCID.Unicorn;
            NPC.frameCounter = 0;
            Banner = Mod.Find<ModNPC>("Cactoid").Type;
            BannerItem = Mod.Find<ModItem>("CactoidBanner").Type;
        }
        public override void AI()
        {
            NPC.velocity.X = NPC.velocity.X * 0.99f;
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
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/CorruptCactite1"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/CorruptCactite2"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/CorruptCactite2"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/CorruptCactoid1"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/CorruptCactoid2"), 1f);
            }

        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            NPC.frameCounter++;
            if (NPC.frameCounter >= 15 / (1 + Math.Abs(NPC.velocity.X)))
            {
                NPC.frameCounter = 0;
                NPC.frame.Y = (NPC.frame.Y + 74);
            }
            if (NPC.frame.Y >= 296)
            {
                NPC.frame.Y = 0;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            return !spawnInfo.Player.ZoneBeach && !spawnInfo.PlayerInTown && !spawnInfo.Invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse && spawnInfo.SpawnTileY < Main.rockLayer && spawnInfo.Player.ZoneDesert && spawnInfo.Player.ZoneCorrupt ? 0.1f : 0f;
        }
    }
}

