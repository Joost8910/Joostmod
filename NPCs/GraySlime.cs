using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
    public class GraySlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gray Slime");
            Main.npcFrameCount[NPC.type] = 5;
        }
        public override void SetDefaults()
        {
            NPC.width = 70;
            NPC.height = 50;
            NPC.damage = 32;
            NPC.defense = 32;
            NPC.lifeMax = 640;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 0f;
            NPC.knockBackResist = 0.0625f;
            NPC.aiStyle = 1;
            AIType = NPCID.BlueSlime;
            AnimationType = NPCID.BlueSlime;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<GraySlimeBanner>();
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 10, 20));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.PlayerInTown && spawnInfo.SpawnTileY >= Main.rockLayer && spawnInfo.SpawnTileY < Main.maxTilesY - 250 && !spawnInfo.Player.ZoneJungle && Main.hardMode ? 0.007f : 0f;

        }
        public override bool CheckDead()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + 13, (int)NPC.Center.Y - 2, ModContent.NPCType<GraySlime2>());
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X - 13, (int)NPC.Center.Y - 2, ModContent.NPCType<GraySlime2>());
            }
            return true;
        }

    }
}

