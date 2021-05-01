using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Bosses
{
    [AutoloadBossHead]
    public class AlphaCactusWormHead : AlphaCactusWorm
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Alpha Cactus Worm");
        }
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DiggerHead);
            npc.aiStyle = -1;
            npc.lifeMax = 10000;
            npc.damage = 40;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.width = 26;
            npc.height = 26;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.behindTiles = true;
            npc.value = Item.buyPrice(0, 1, 50, 0);
            npc.netAlways = true;
            npc.boss = true;
            music = MusicID.Boss5;
        }
        public override void Init()
        {
            base.Init();
            head = true;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (Vector2.Distance(target.Center, npc.Center) > 24)
            {
                return false;
            }
            return base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.7f * bossLifeScale) + 1;
            npc.damage = (int)(npc.damage * 0.7f);
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.Heart;
        }
        public override void NPCLoot()
        {
            if (Main.netMode == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AlphaCactusWorm"), 1);
            }
            for (int i = 0; i < 10 + Main.rand.Next(6); i++)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Heart);
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FifthAnniversary"), 1);
            }
        }
        public override bool CheckDead()
        {
            Player player = Main.player[npc.target];
            if (Main.netMode == 2)
            {
                NPC.NewNPC((int)player.Center.X - 300, (int)player.Center.Y - 300, mod.NPCType("GrandCactusWormHead"));
            }
            return base.CheckDead();
        }
        int chargeTimer = 0;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(chargeTimer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            chargeTimer = reader.ReadInt32();
        }
        public override void CustomBehavior()
        {
            if (chargeTimer <= 0)
            {
                npc.velocity = npc.DirectionTo(Main.player[npc.target].Center) * 9;
                chargeTimer = 200;
                npc.netUpdate = true;
            }
            chargeTimer--;
            if (NPC.AnyNPCs(mod.NPCType("GrandCactusWormHead")))
            {
                npc.life = 0;
                npc.HitEffect(0, 10.0);
                npc.active = false;
            }
        }
    }
}