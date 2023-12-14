using JoostMod.Items.Placeable;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.GameContent.ItemDropRules;
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
            NPC.CloneDefaults(NPCID.DiggerHead);
            NPC.aiStyle = -1;
            NPC.lifeMax = 10000;
            NPC.damage = 40;
            NPC.defense = 0;
            NPC.knockBackResist = 0f;
            NPC.width = 26;
            NPC.height = 26;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.behindTiles = true;
            NPC.value = Item.buyPrice(0, 1, 50, 0);
            NPC.netAlways = true;
            NPC.boss = true;
            Music = MusicID.Boss5;
        }
        public override void Init()
        {
            base.Init();
            head = true;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (Vector2.Distance(target.Center, NPC.Center) > 24)
            {
                return false;
            }
            return base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * bossLifeScale) + 1;
            NPC.damage = (int)(NPC.damage * 0.7f);
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.Heart;
        }
        public override void OnKill()
        {
            if (Main.netMode == NetmodeID.SinglePlayer) //bandaid fix only due to multiplayer compatiability troubles
            {
                Item.NewItem(NPC.GetSource_Death(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Items.Quest.AlphaCactusWorm>());
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            var parameters = new DropOneByOne.Parameters()
            {
                ChanceNumerator = 1,
                ChanceDenominator = 1,
                MinimumStackPerChunkBase = 1,
                MaximumStackPerChunkBase = 1,
                MinimumItemDropsCount = 10,
                MaximumItemDropsCount = 16, 
            };
            npcLoot.Add(new DropOneByOne(ItemID.Heart, parameters));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FifthAnniversary>()));
        }
        public override bool CheckDead()
        {
            Player player = Main.player[NPC.target];
            if (Main.netMode == NetmodeID.Server)
            {
                NPC.NewNPC(NPC.GetSource_Death(), (int)player.Center.X - 300, (int)player.Center.Y - 300, ModContent.NPCType<GrandCactusWormHead>());
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
                NPC.velocity = NPC.DirectionTo(Main.player[NPC.target].Center) * 9;
                chargeTimer = 200;
                NPC.netUpdate = true;
            }
            chargeTimer--;
            if (NPC.AnyNPCs(ModContent.NPCType<GrandCactusWormHead>()))
            {
                NPC.life = 0;
                NPC.HitEffect(0, 10.0);
                NPC.active = false;
            }
        }
    }
}