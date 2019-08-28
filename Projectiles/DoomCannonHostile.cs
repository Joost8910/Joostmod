using System;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class DoomCannonHostile : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Doom Cannon");
			Main.projFrames[projectile.type] = 12;
		}
        public override void SetDefaults()
        {
            projectile.width = 50;
            projectile.height = 50;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 666;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override bool PreAI()
        {
            NPC owner = Main.npc[(int)projectile.ai[0]];
            Vector2 vector = owner.Center;
            bool channeling = owner.active && owner.life > 0 && owner.ai[2] == 4 && owner.type == mod.NPCType("SkeletonDemoman") && projectile.ai[1] < 660;
            if (channeling)
            {
                Vector2 vector13 = Main.player[owner.target].Center - vector;
                vector13.Normalize();
                if (vector13.HasNaNs())
                {
                    vector13 = Vector2.UnitX * owner.direction;
                }
                if (vector13.X != projectile.velocity.X || vector13.Y != projectile.velocity.Y)
                {
                    projectile.netUpdate = true;
                }
                projectile.velocity = vector13;
            }
            else
            {
                projectile.Kill();
            }

            if (projectile.ai[1] < 60)
            {
                projectile.frame = 0;
            }
            else if (projectile.ai[1] < 120)
            {
                projectile.frame = 1;
            }
            else if (projectile.ai[1] < 180)
            {
                projectile.frame = 2;
            }
            else if (projectile.ai[1] < 240)
            {
                projectile.frame = 3;
            }
            else if (projectile.ai[1] < 300)
            {
                projectile.frame = 4;
            }
            else if (projectile.ai[1] < 360)
            {
                projectile.frame = 5;
            }
            else if (projectile.ai[1] < 420)
            {
                projectile.frame = 6;
            }
            else if (projectile.ai[1] < 480)
            {
                projectile.frame = 7;
            }
            else if (projectile.ai[1] < 540)
            {
                projectile.frame = 8;
            }
            else if (projectile.ai[1] < 600)
            {
                projectile.frame = 9;
            }
            else if (projectile.ai[1] < 660)
            {
                projectile.frame = 10;
            }
            projectile.ai[1]++;
            if (projectile.ai[1] % 60 == 0)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 75);
                if (projectile.ai[1] >= 360)
                {
                    Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 114);
                }
            }
            projectile.rotation = projectile.velocity.ToRotation() + (projectile.direction == -1 ? 3.14f : 0);
            projectile.position = vector - projectile.Size / 2 + new Vector2(-14, 10) + new Vector2((float)Math.Cos(projectile.rotation - (Math.PI / 2)), (float)Math.Sin(projectile.rotation - (Math.PI / 2))) * 14;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            owner.direction = projectile.direction;
            return false;
        }
        public override void Kill(int timeLeft)
        {
            NPC owner = Main.npc[(int)projectile.ai[0]];
            if (owner.active && owner.life > 0 && owner.ai[2] == 4 && owner.type == mod.NPCType("SkeletonDemoman") && projectile.ai[1] >= 660)
            {
                Main.PlaySound(42, (int)projectile.position.X, (int)projectile.position.Y, 217);
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 62);
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 74);
                if (Main.netMode != 1)
                {
                    Vector2 pos = owner.Center + projectile.velocity * 140;
                    float speed = 7;
                    int type = mod.ProjectileType("DoomSkullHostile");
                    if (float.IsNaN(projectile.velocity.X) || float.IsNaN(projectile.velocity.Y))
                    {
                        projectile.velocity = -Vector2.UnitY;
                    }
                    Projectile.NewProjectile(pos, projectile.velocity * speed, type, projectile.damage, projectile.knockBack, projectile.owner);
                }
            }
        }
    }
}