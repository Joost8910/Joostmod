using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
    public class GraySlime5 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gray Slime");
            Main.npcFrameCount[NPC.type] = 5;
        }
        public override void SetDefaults()
        {
            NPC.width = 16;
            NPC.height = 16;
            NPC.damage = 2;
            NPC.defense = 2;
            NPC.lifeMax = 20;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 0f;
            NPC.knockBackResist = 0.75f;
            NPC.aiStyle = 1;
            AIType = NPCID.BlueSlime;
            AnimationType = NPCID.BlueSlime;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 1, 2));
        }

        public override bool CheckDead()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + 13, (int)NPC.Center.Y - 2, Mod.Find<ModNPC>("GraySlime6").Type);
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X - 13, (int)NPC.Center.Y - 2, Mod.Find<ModNPC>("GraySlime6").Type);
            }
            return true;
        }

    }
}

