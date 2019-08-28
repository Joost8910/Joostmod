using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class CheatManipulation : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tome of Greater Manipulation");
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
            projectile.idStaticNPCHitCooldown = 8;
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
            return Main.myPlayer == projectile.owner && Main.mouseRight;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = (target.lifeMax / 20 + (target.defense / 2));
            crit = true;
        }
        public override void AI()
        {
            int enpc = (int)projectile.ai[0] - 1;
            //int pvp = (int)projectile.ai[1] - 1;
            Player player = Main.player[projectile.owner];
            if (Main.myPlayer == projectile.owner)
            {
                projectile.position = Main.MouseWorld;
                projectile.netUpdate = true;
            }
            else
            {
                projectile.Kill();
            }
            for (int i = 0; i < Main.item.Length; i++)
            {
                if (Main.item[i].active)
                {
                    Item I = Main.item[i];
                    if (projectile.Hitbox.Intersects(I.Hitbox) || projectile.oldPosition == I.Center - I.velocity)
                    {
                        I.velocity = Vector2.Zero;
                        I.position = projectile.position - I.Size / 2;
                    }
                }
            }
            if (enpc >= 0)
            {
                NPC target = Main.npc[enpc];
                if (target.active && target.type != NPCID.TargetDummy)
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
                /*if (pvp >= 0)
                {
                    Player pTarget = Main.player[pvp];
                    if (pTarget.active && !pTarget.dead)
                    {
                        pTarget.position = projectile.position - new Vector2(pTarget.width / 2, pTarget.height / 2);
                    }
                    else
                    {
                        projectile.Kill();
                    }
                }
                else
                {
                    for (int i = 0; i < 255; i++)
                    {
                        Player pTarget = Main.player[i];
                        if (pTarget.active && !pTarget.dead && projectile.Hitbox.Intersects(pTarget.Hitbox))
                        {
                            pvp = i;
                            projectile.ai[1] = i + 1;
                        }
                    }
                    if (pvp < 0)
                    {*/
                        for (int i = 0; i < 200; i++)
                        {
                            NPC target = Main.npc[i];
                            if (target.active && projectile.Hitbox.Intersects(target.Hitbox) && target.type != NPCID.TargetDummy)
                            {
                                enpc = i;
                                projectile.ai[0] = i + 1;
                            }
                        }
                    /*}
                }*/
            }
            bool channeling = player.channel && !player.noItems && !player.CCed;
            if (!channeling)
            {
                if (enpc >= 0)
                {
                    NPC target = Main.npc[enpc];
                    if (target.active && target.type != NPCID.TargetDummy)
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
                /*else if (pvp >= 0)
                {
                    Player pTarget = Main.player[pvp];
                    if (pTarget.active && !pTarget.dead)
                    {
                        Vector2 vel = projectile.position - projectile.oldPosition;
                        if (projectile.Distance(projectile.oldPosition) > 25)
                        {
                            vel.Normalize();
                            pTarget.velocity = vel * 25;
                        }
                        else
                        {
                            pTarget.velocity = vel;
                        }
                    }
                }*/
                projectile.Kill();
            }
        }
    }
}
