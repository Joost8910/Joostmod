using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
    public class GraySlime4 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gray Slime");
            Main.npcFrameCount[NPC.type] = 5;
        }
        public override void SetDefaults()
        {
            NPC.width = 24;
            NPC.height = 18;
            NPC.damage = 4;
            NPC.defense = 4;
            NPC.lifeMax = 40;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 0f;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = 1;
            AIType = NPCID.BlueSlime;
            AnimationType = NPCID.BlueSlime;
        }
        public override void OnKill()
        {
            Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, 23, Main.rand.Next(1, 3));

        }

        public override bool CheckDead()
        {
            if (Main.netMode != 1)
            {
                NPC.NewNPC((int)NPC.Center.X + 13, (int)NPC.Center.Y - 2, Mod.Find<ModNPC>("GraySlime5").Type);
                NPC.NewNPC((int)NPC.Center.X - 13, (int)NPC.Center.Y - 2, Mod.Find<ModNPC>("GraySlime5").Type);
            }
            return true;
        }

    }
}

