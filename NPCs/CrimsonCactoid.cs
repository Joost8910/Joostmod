using JoostMod.Items.Placeable;
using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
    public class CrimsonCactoid : ModNPC
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
            NPC.damage = 25;
            NPC.defense = 8;
            NPC.lifeMax = 100;
            if (NPC.downedMoonlord)
            {
                NPC.damage = 75;
                NPC.defense = 30;
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
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Cactus, 1, 8, 12));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Anniversary>(), 100));
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                var sauce = NPC.GetSource_Death();
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("CrimsonCactite1").Type);
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("CrimsonCactite2").Type);
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("CrimsonCactite2").Type);
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("CrimsonCactoid1").Type);
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("CrimsonCactoid2").Type);
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
            return !spawnInfo.Player.ZoneBeach && !spawnInfo.PlayerInTown && !spawnInfo.Invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse && spawnInfo.SpawnTileY < Main.rockLayer && spawnInfo.Player.ZoneDesert && spawnInfo.Player.ZoneCrimson ? 0.1f : 0f;
        }


    }
}

