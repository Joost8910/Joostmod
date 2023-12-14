using JoostMod.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Bosses
{
    public class IceXParasite : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("X Parasite");
            Main.npcFrameCount[NPC.type] = 6;
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[]
                {
                    BuffID.Frostburn,
                    BuffID.Frostburn2,
                    ModContent.BuffType<InfectedRed>(),
                    ModContent.BuffType<InfectedGreen>(),
                    ModContent.BuffType<InfectedBlue>(),
                    ModContent.BuffType<InfectedYellow>()
                }
            };
            NPCID.Sets.DebuffImmunitySets[Type] = debuffData;
        }
        public override void SetDefaults()
        {
            NPC.width = 42;
            NPC.height = 42;
            NPC.damage = 70;
            NPC.defense = 15;
            NPC.lifeMax = 2000;
            NPC.HitSound = SoundID.NPCHit25;
            NPC.DeathSound = SoundID.NPCDeath28;
            NPC.value = 0;
            NPC.knockBackResist = 0.1f;
            NPC.aiStyle = -1;
            NPC.coldDamage = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.frameCounter = 0;
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 6)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y = (NPC.frame.Y + 50);
            }
            if (NPC.frame.Y >= 300)
            {
                NPC.frame.Y = 0;
            }
        }
        private ModPacket GetPacket()
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)JoostModMessageType.KillNPC);
            packet.Write(NPC.whoAmI);
            return packet;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            NPC.DeathSound = SoundID.NPCDeath19;
            target.AddBuff(ModContent.BuffType<InfectedBlue>(), 900);
            NPC.life = 0;
            NPC.checkDead();
            if (Main.netMode != 0)
            {
                ModPacket netMessage = GetPacket();
                netMessage.Send();
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<InfectedBlue>(), 900);
            NPC.life = 0;
            NPC.checkDead();
        }
        public override bool PreKill()
        {
            return false;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                var sauce = NPC.GetSource_Death();
                for (int i = 0; i < 4; i++)
                {
                    Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("IceXParasite").Type);
                }
            }
            //Item.NewItem(NPC.Center, ItemID.Star);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Star));
        }
        public override void AI()
        {
            if (NPC.ai[0] == 0)
            {
                NPC.scale = 0.7f + (Main.rand.Next(7) * 0.05f);
                NPC.width = (int)(42 * NPC.scale);
                NPC.height = (int)(42 * NPC.scale);
                NPC.damage = (int)(70 * NPC.scale * (Main.expertMode ? 2 : 1));
                NPC.life = (int)(1500 * NPC.scale * (Main.expertMode ? 2 : 1));
                NPC.lifeMax = NPC.life;
                NPC.velocity = new Vector2(Main.rand.Next(11) - 5, Main.rand.Next(11) - 5);
                NPC.ai[0]++;
                NPC.netUpdate = true;
            }
            Player P = Main.player[NPC.target];
            if (NPC.target < 0 || NPC.target == 255 || P.dead || !P.active)
            {
                NPC.TargetClosest(true);
            }
            NPC.ai[1]++;
            if (NPC.ai[1] > 40)
            {
                Vector2 move = P.MountedCenter - NPC.Center;
                NPC.localAI[1] = 8f * (1 + (1 - NPC.scale));
                if (NPC.Distance(P.MountedCenter) > 400)
                {
                    NPC.localAI[1] = 8 * (1 + (1 - NPC.scale)) + (NPC.Distance(P.MountedCenter) - 400) / 40 * (1 + (1 - NPC.scale));
                }
                if (move.Length() > NPC.localAI[1] && NPC.localAI[1] > 0)
                {
                    move *= NPC.localAI[1] / move.Length();
                }
                float home = 10f;
                NPC.velocity = ((home - 1f) * NPC.velocity + move) / home;
                if (NPC.velocity.Length() < NPC.localAI[1] && NPC.localAI[1] > 0)
                {
                    NPC.velocity *= (NPC.localAI[1] / NPC.velocity.Length());
                }
            }
            else if (NPC.velocity.Length() > 12)
            {
                NPC.velocity.Normalize();
                NPC.velocity *= 12;
            }
            if (NPC.ai[1] > 90)
            {
                NPC.ai[1] = 0;
            }
            /*
            if (npc.ai[2] > 10)
            {
                npc.DeathSound = SoundID.NPCDeath19;
                npc.damage = 0;
                npc.dontTakeDamage = true;
                npc.position = P.MountedCenter - new Vector2(npc.width / 2, 42 - npc.scale * 21);
                npc.velocity = P.velocity;
                npc.ai[0] -= 0.05f;
                npc.scale = npc.ai[0];
                if (npc.ai[0] <= 0.1f)
                {
                    if (Main.expertMode)
                    {
                        if (!P.HasBuff(BuffID.Frozen) && Main.rand.Next(4) < 3)
                        {
                            P.AddBuff(BuffID.Frozen, 30, true);
                        }
                    }
                    P.AddBuff(mod.BuffType("InfectedBlue"), 900);
                    npc.life = 0;
                    npc.active = false;
                    npc.checkDead();
                    if (Main.netMode != 0)
                    {
                        NetMessage.SendData(23, -1, -1, null, npc.whoAmI);
                    }
                }
            }
            */
        }
    }
}

