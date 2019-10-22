using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class Manipulation : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tome of Manipulation");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 2;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.aiStyle = 0;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 4;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return target.friendly && Main.myPlayer == projectile.owner && Main.mouseRight && target.type != mod.NPCType("FireBall");
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = (target.lifeMax / 20 + (target.defense / 2));
            crit = true;
        }
        public override void AI()
        {
            int enpc = (int)projectile.ai[0] - 1;
            Player player = Main.player[projectile.owner];
            if (Main.myPlayer == projectile.owner)
            {
                projectile.position = Main.MouseWorld;
                projectile.netUpdate = true;
            }
            if (enpc >= 0)
            {
                NPC target = Main.npc[enpc];
                if (target.friendly && target.active && target.type != mod.NPCType("FireBall"))
                {
                    target.position = projectile.position - new Vector2(target.width / 2, target.height / 2);
                    target.netUpdate = true;
                }
                else
                {
                    projectile.Kill();
                }
            }
            else
            {
                for (int i = 0; i < 200; i++)
                {
                    NPC target = Main.npc[i];
                    if (target.friendly && target.active && projectile.Hitbox.Intersects(target.Hitbox) && target.type != mod.NPCType("FireBall"))
                    {
                        enpc = i;
                        projectile.ai[0] = i + 1;
                    }
                }
            }
            bool channeling = player.channel && !player.noItems && !player.CCed;
            if (!channeling)
            {
                if (enpc >= 0)
                {
                    NPC target = Main.npc[enpc];
                    if (target.friendly && target.active && target.type != mod.NPCType("FireBall"))
                    {
                        Vector2 vel = projectile.position - projectile.oldPosition;
                        if (projectile.Distance(projectile.oldPosition) > 25)
                        {
                            vel.Normalize();
                            target.velocity = vel * 25;
                            target.netUpdate = true;
                        }
                        else
                        {
                            target.velocity = vel;
                            target.netUpdate = true;
                        }
                    }
                }
                projectile.Kill();
            }
        }
    }
}
