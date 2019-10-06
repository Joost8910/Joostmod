using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class StickyCactus : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sticky Cactus");
        }
        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 6;
            projectile.timeLeft = 600;
            aiType = ProjectileID.WoodenArrowFriendly;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 30;
        }
        int hitMob = -1;
        bool pvp = false;
        Vector2 offSet = Vector2.Zero;
        float rot = 0;
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            pvp = false;
            hitMob = target.whoAmI;
            offSet = target.Center - projectile.Center;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            pvp = true;
            hitMob = target.whoAmI;
            offSet = target.Center - projectile.Center;
        }
        public override void AI()
        {
            if (pvp && hitMob >= 0 && hitMob < 255)
            {
                Player P = Main.player[hitMob];
                projectile.velocity = P.velocity;
                projectile.position = (P.Center - offSet) - (projectile.Size / 2);
                if (!P.active || P.dead)
                {
                    hitMob = -1;
                    pvp = false;
                }
                projectile.netUpdate = true;
            }
            else if (hitMob >= 0)
            {
                NPC npc = Main.npc[hitMob];
                projectile.velocity = npc.velocity;
                projectile.position = (npc.Center - offSet) - (projectile.Size / 2);
                if (!npc.active || npc.friendly || npc.life <= 0)
                {
                    hitMob = -1;
                }
                projectile.netUpdate = true;
            }
            else if (projectile.timeLeft < 560)
            {
                rot += projectile.direction * projectile.velocity.Length() * 0.0174f;
                projectile.velocity.Y = projectile.velocity.Y + 0.15f;
                projectile.velocity.X = projectile.velocity.X * 0.99f;
            }
            projectile.rotation = rot;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(hitMob);
            writer.Write(pvp);
            writer.WriteVector2(offSet);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            hitMob = reader.ReadInt16();
            pvp = reader.ReadBoolean();
            offSet = reader.ReadVector2();
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity = Vector2.Zero;
            return false;
        }
    }
}

