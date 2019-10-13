using Microsoft.Xna.Framework;
using Terraria;
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
            npc.aiStyle = -1;
            npc.lifeMax = 1;
            npc.damage = 0;
            npc.defense = 9000;
            npc.knockBackResist = 0f;
            npc.width = 80;
            npc.height = 80;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.noGravity = true;
            npc.dontCountMe = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
        }
        public override void UpdateLifeRegen(ref int damage)
        {
            npc.lifeRegen = 0;
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
                Main.PlaySound(4, npc.Center, 3);
                Main.PlaySound(SoundID.NPCHit4, npc.Center);
                projectile.Kill();
            }
            else if (projectile.penetrate == 1)
            {
                Main.PlaySound(4, npc.Center, 3);
                Main.PlaySound(SoundID.NPCHit4, npc.Center);
            }
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
        public override void AI()
        {
            NPC host = Main.npc[(int)npc.ai[0]];
            if (host.type != mod.NPCType("Gilgamesh2") || !host.active)
            {
                npc.active = false;
            }
            npc.life = 1;
            Vector2 shieldPos = host.Center + new Vector2(29 * host.direction, -45);
            Vector2 vect = host.ai[1].ToRotationVector2();
            npc.position = new Vector2((int)(shieldPos.X + (vect.X * 40 * host.direction)) - (npc.width / 2), (int)(shieldPos.Y + (vect.Y * 40 * host.direction)) - (npc.height / 2));
            npc.velocity = host.velocity;
        }
        public override bool CheckDead()
        {
            NPC host = Main.npc[(int)npc.ai[0]];
            if (host.type != mod.NPCType("Gilgamesh2") || !host.active)
            {
                npc.active = false;
                return true;
            }
            npc.active = true;
            npc.life = 1;
            return false;
        }
    }
}