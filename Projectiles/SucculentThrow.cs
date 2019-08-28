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
            projectile.CloneDefaults(ProjectileID.Cascade);
            projectile.width = 22;
            projectile.height = 22;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 12;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Succulent Throw");
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 12f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 12f;
            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 200f;
        }
        int enpc = -1;
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!target.friendly && target.knockBackResist > 0 && target.type != NPCID.TargetDummy && projectile.ai[0] > 0 && !projectile.velocity.HasNaNs())
            {
                enpc = target.whoAmI;
                Vector2 value2 = projectile.Center - target.Center;
                value2.Normalize();
                projectile.velocity -= value2 * 16f;
                projectile.velocity *= -2f;
                projectile.netUpdate = true;
            }
        }
        public override bool PreAI()
        {
            if (projectile.velocity.HasNaNs())
            {
                projectile.velocity = Vector2.Zero;
            }
            return base.PreAI();
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (enpc >= 0)
            {
                NPC target = Main.npc[enpc];
                if (!target.friendly && target.active && target.knockBackResist > 0 && target.type != NPCID.TargetDummy && !player.dead && player.active && !projectile.velocity.HasNaNs())
                {
                    if (projectile.Hitbox.Intersects(target.Hitbox) && projectile.ai[0] > 0)
                    {
                        target.position = projectile.Center - new Vector2(target.width / 2, target.height / 2);
                        target.velocity = projectile.velocity;
                        projectile.rotation -= 0.45f;
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
