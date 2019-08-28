using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Bosses
{
    public class GreenXParasite : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("X Parasite");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void SetDefaults()
        {
            npc.width = 28;
            npc.height = 28;
            npc.damage = 50;
            npc.defense = 5;
            npc.lifeMax = 1000;
            npc.HitSound = SoundID.NPCHit25;
            npc.DeathSound = SoundID.NPCDeath28;
            npc.buffImmune[mod.BuffType("InfectedRed")] = true;
            npc.buffImmune[mod.BuffType("InfectedGreen")] = true;
            npc.buffImmune[mod.BuffType("InfectedBlue")] = true;
            npc.buffImmune[mod.BuffType("InfectedYellow")] = true;
            npc.value = 0;
            npc.knockBackResist = 0.5f;
            npc.aiStyle = -1;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.frameCounter = 0;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter >= 6)
            {
                npc.frameCounter = 0;
                npc.frame.Y = (npc.frame.Y + 34);
            }
            if (npc.frame.Y >= 204)
            {
                npc.frame.Y = 0;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            npc.localAI[3] = 15;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("InfectedGreen"), 900);
            npc.life = 0;
            npc.checkDead();
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GreenXParasite"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GreenXParasite"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GreenXParasite"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GreenXParasite"), npc.scale);
                Item.NewItem(npc.position, mod.ItemType("EnergyFragment"));
            }
        }
        public override void AI()
        {
            if (npc.ai[0] < 1)
            {
                npc.scale = 0.7f + (Main.rand.Next(9) * 0.05f);
                npc.width = (int)(28 * npc.scale);
                npc.height = (int)(28 * npc.scale);
                npc.damage = (int)(50 * npc.scale * (Main.expertMode ? 2 : 1));
                npc.life = (int)(1000 * npc.scale * (Main.expertMode ? 2 : 1));
                npc.lifeMax = npc.life;
                npc.velocity = new Vector2(Main.rand.Next(11) - 5, Main.rand.Next(11) - 5);
            }
            npc.ai[0]++;
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || P.dead || !P.active)
            {
                npc.TargetClosest(true);
            }
            npc.ai[1]++;
            if (npc.ai[1] > 30)
            {
                Vector2 move = P.MountedCenter - npc.Center;
                npc.localAI[1] = 10f * (1 + (1 - npc.scale));
                if (npc.Distance(P.MountedCenter) > 400)
                {
                    npc.localAI[1] = 10 * (1 + (1 - npc.scale)) + (npc.Distance(P.MountedCenter) - 400) / 40 * (1 + (1 - npc.scale));
                }
                if (move.Length() > npc.localAI[1] && npc.localAI[1] > 0)
                {
                    move *= npc.localAI[1] / move.Length();
                }
                float home = 10f;
                npc.velocity = ((home - 1f) * npc.velocity + move) / home;
                if (npc.velocity.Length() < npc.localAI[1] && npc.localAI[1] > 0)
                {
                    npc.velocity *= (npc.localAI[1] / npc.velocity.Length());
                }
            }
            else if (npc.velocity.Length() > 12)
            {
                npc.velocity.Normalize();
                npc.velocity *= 12;
            }
            if (npc.ai[1] > 90)
            {
                npc.ai[1] = 0;
            }
            if (npc.localAI[3] > 10)
            {
                npc.DeathSound = SoundID.NPCDeath19;
                npc.damage = 0;
                npc.dontTakeDamage = true;
                npc.position = P.MountedCenter - new Vector2(npc.width / 2, 28 - npc.scale * 14);
                npc.velocity = P.velocity;
                npc.scale -= 0.05f;
                if (npc.scale <= 0.1f)
                {
                    P.AddBuff(mod.BuffType("InfectedGreen"), 900);
                    npc.life = 0;
                    npc.checkDead();
                }
            }
        }
    }
}

