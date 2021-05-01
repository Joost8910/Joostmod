using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Bosses
{
    [AutoloadBossHead]
    public class AlphaCactusWormBody : AlphaCactusWorm
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Alpha Cactus Worm");
        }
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DiggerBody);
            npc.aiStyle = -1;
            npc.damage = 30;
            npc.defense = 5;
            npc.knockBackResist = 0f;
            npc.width = 26;
            npc.height = 26;
            npc.behindTiles = true;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.noGravity = true;
            npc.dontCountMe = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
        }
        public override void Init()
        {
            base.Init();
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/AlphaCactusWormBody"), npc.scale);
            }
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
            npc.damage = (int)(npc.damage * 0.7f);
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
    }
}