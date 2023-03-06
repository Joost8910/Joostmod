using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
    public class GraySlime3 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gray Slime");
            Main.npcFrameCount[NPC.type] = 5;
        }
        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 24;
            NPC.damage = 8;
            NPC.defense = 8;
            NPC.lifeMax = 80;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 0f;
            NPC.knockBackResist = 0.25f;
            NPC.aiStyle = 1;
            AIType = NPCID.BlueSlime;
            AnimationType = NPCID.BlueSlime;
        }
        public override void OnKill()
        {
            Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, 23, Main.rand.Next(1, 5));

        }

        public override bool CheckDead()
        {
            if (Main.netMode != 1)
            {
                NPC.NewNPC((int)NPC.Center.X + 13, (int)NPC.Center.Y - 2, Mod.Find<ModNPC>("GraySlime4").Type);
                NPC.NewNPC((int)NPC.Center.X - 13, (int)NPC.Center.Y - 2, Mod.Find<ModNPC>("GraySlime4").Type);
            }
            return true;
        }

    }
}

