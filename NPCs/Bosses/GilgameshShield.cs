using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Bosses
{
    public class GilgameshShield : AlphaCactusWorm
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gilgamesh");
        }
        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 1;
            NPC.damage = 0;
            NPC.defense = 9000;
            NPC.knockBackResist = 0f;
            NPC.width = 106;
            NPC.height = 106;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.noGravity = true;
            NPC.dontCountMe = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath3;
        }
        public override void UpdateLifeRegen(ref int damage)
        {
            NPC.lifeRegen = 0;
        }
        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            damage = 0;
            return false;
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.CanReflect())
            {
                SoundEngine.PlaySound(SoundID.NPCDeath3, NPC.Center);
                SoundEngine.PlaySound(SoundID.NPCHit4, NPC.Center);
                projectile.Kill();
            }
            else if (projectile.penetrate == 1)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath3, NPC.Center);
                SoundEngine.PlaySound(SoundID.NPCHit4, NPC.Center);
            }
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
        public override void AI()
        {
            NPC host = Main.npc[(int)NPC.ai[0]];
            if (host.type != ModContent.NPCType<Gilgamesh2>() || !host.active)
            {
                NPC.active = false;
            }
            NPC.life = 1;
            Vector2 shieldPos = host.Center + new Vector2(29 * host.direction, -35);
            Vector2 vect = host.ai[1].ToRotationVector2();
            NPC.position = new Vector2((int)(shieldPos.X + (vect.X * 40 * host.direction)) - (NPC.width / 2), (int)(shieldPos.Y + (vect.Y * 40 * host.direction)) - (NPC.height / 2));
            NPC.velocity = host.velocity;
        }
        public override bool CheckDead()
        {
            NPC host = Main.npc[(int)NPC.ai[0]];
            if (host.type != ModContent.NPCType<Gilgamesh2>() || !host.active)
            {
                NPC.active = false;
                return true;
            }
            NPC.active = true;
            NPC.life = 1;
            return false;
        }
    }
}