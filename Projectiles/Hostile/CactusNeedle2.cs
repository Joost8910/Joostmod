using JoostMod.NPCs.Bosses;
using JoostMod.NPCs.Town;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class CactusNeedle2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cactus Needle");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = 1;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 45;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            Projectile.damage = 1;
        }
        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            modifiers.SetMaxDamage(1);
            modifiers.Knockback *= 0;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (!target.dead)
            {
                target.immuneTime = 1;
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.type == ModContent.NPCType<CactusPerson>() && !NPC.AnyNPCs(ModContent.NPCType<JumboCactuar>()))
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
    }
}

