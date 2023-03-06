using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Bosses
{
    [AutoloadBossHead]
    public class AlphaCactusWormTail : AlphaCactusWorm
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Alpha Cactus Worm");
        }
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerTail);
            NPC.aiStyle = -1;
            NPC.damage = 20;
            NPC.defense = 10;
            NPC.knockBackResist = 0f;
            NPC.width = 26;
            NPC.height = 26;
            NPC.behindTiles = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.noGravity = true;
            NPC.dontCountMe = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
        }
        public override void Init()
        {
            base.Init();
            tail = true;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (Vector2.Distance(target.Center, NPC.Center) > 24)
            {
                return false;
            }
            return base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/AlphaCactusWormBody"), NPC.scale);
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.damage = (int)(NPC.damage * 0.7f);
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }
    }
}