using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Ranged
{
    public class StickyCactus : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sticky Cactus");
        }
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 6;
            Projectile.timeLeft = 600;
            AIType = ProjectileID.WoodenArrowFriendly;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
        }
        int hitMob = -1;
        bool pvp = false;
        Vector2 offSet = Vector2.Zero;
        float rot = 0;
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            pvp = false;
            hitMob = target.whoAmI;
            offSet = target.Center - Projectile.Center;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            pvp = true;
            hitMob = target.whoAmI;
            offSet = target.Center - Projectile.Center;
        }
        public override void AI()
        {
            if (pvp && hitMob >= 0 && hitMob < 255)
            {
                Player P = Main.player[hitMob];
                Projectile.velocity = P.velocity;
                Projectile.position = P.Center - offSet - Projectile.Size / 2;
                if (!P.active || P.dead)
                {
                    hitMob = -1;
                    pvp = false;
                }
                Projectile.netUpdate = true;
            }
            else if (hitMob >= 0)
            {
                NPC npc = Main.npc[hitMob];
                Projectile.velocity = npc.velocity;
                Projectile.position = npc.Center - offSet - Projectile.Size / 2;
                if (!npc.active || npc.friendly || npc.life <= 0)
                {
                    hitMob = -1;
                }
                Projectile.netUpdate = true;
            }
            else if (Projectile.timeLeft < 560)
            {
                rot += Projectile.direction * Projectile.velocity.Length() * 0.0174f;
                Projectile.velocity.Y = Projectile.velocity.Y + 0.15f;
                Projectile.velocity.X = Projectile.velocity.X * 0.99f;
            }
            Projectile.rotation = rot;
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
            Projectile.velocity = Vector2.Zero;
            return false;
        }
    }
}

