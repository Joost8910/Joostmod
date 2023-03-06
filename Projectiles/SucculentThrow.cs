using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class SucculentThrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Cascade);
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 12;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Succulent Throw");
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 12f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 12f;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 200f;
        }
        int enpc = -1;
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!target.friendly && target.knockBackResist > 0 && target.type != NPCID.TargetDummy && Projectile.ai[0] > 0 && !Projectile.velocity.HasNaNs())
            {
                enpc = target.whoAmI;
                Vector2 value2 = Projectile.Center - target.Center;
                value2.Normalize();
                Projectile.velocity -= value2 * 16f;
                Projectile.velocity *= -2f;
                Projectile.netUpdate = true;
            }
        }
        public override bool PreAI()
        {
            if (Projectile.velocity.HasNaNs())
            {
                Projectile.velocity = Vector2.Zero;
            }
            return base.PreAI();
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (enpc >= 0)
            {
                NPC target = Main.npc[enpc];
                if (!target.friendly && target.active && target.knockBackResist > 0 && target.type != NPCID.TargetDummy && !player.dead && player.active && !Projectile.velocity.HasNaNs())
                {
                    if (Projectile.Hitbox.Intersects(target.Hitbox) && Projectile.ai[0] > 0)
                    {
                        target.position = Projectile.Center - new Vector2(target.width / 2, target.height / 2);
                        target.velocity = Projectile.velocity;
                        Projectile.rotation -= 0.45f;
                    }
                    else
                    {
                        enpc = -1;
                    }
                }
            }
        }
    }
}
