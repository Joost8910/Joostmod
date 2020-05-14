using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class ImpLordFlame : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lord's Flame");
            Main.projFrames[projectile.type] = 18;
        }
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 26;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.magic = true;
            //projectile.light = 0.50f;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            bool channeling = player.channel && !player.noItems && !player.CCed && !player.dead;
            if (channeling)
            {
                if (projectile.ai[0] % 10 < 1 && projectile.ai[0] < 60 && !player.CheckMana(player.inventory[player.selectedItem].mana, true))
                {
                    projectile.Kill();
                }
                projectile.ai[0]++;
                if (Main.myPlayer == projectile.owner)
                {
                    float scaleFactor6 = 1f;
                    Vector2 vector13 = Main.MouseWorld - vector;
                    vector13.Normalize();
                    if (vector13.HasNaNs())
                    {
                        vector13 = Vector2.UnitX * (float)player.direction;
                    }
                    vector13 *= scaleFactor6;
                    if (vector13.X != projectile.velocity.X || vector13.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.velocity = vector13;
                }
            }
            else
            {
                if (projectile.ai[0] >= 60)
                    projectile.ai[0] = 100;
                projectile.Kill();
            }
            if ((projectile.ai[0] % 6) == 0 && projectile.ai[0] < 60)
            {
                projectile.frame = (projectile.frame + 1) % 12;
            }
            if (projectile.ai[0] >= 40)
            {
                player.velocity *= 0.98f;
            }
            if (projectile.ai[0] >= 60)
            {
                player.velocity *= 0.98f;
                if ((projectile.ai[0] % 3) == 0)
                {
                    projectile.frame = (projectile.frame >= 17 ? 10 : projectile.frame + 1);
                    projectile.ai[0] = 60;
                }
            }
            float light = (projectile.ai[0] > 60 ? 60 : projectile.ai[0]) / 60f;
            Lighting.AddLight(projectile.Center, light, light * 0.5f, light * 0.1f);
        

            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f + new Vector2(projectile.direction * -10, -player.gravDir * ((projectile.ai[0] > 60 ? 12 : projectile.ai[0] / 5) + 18));
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 15;
            player.itemAnimation = 15;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Vector2 pos = player.RotatedRelativePoint(player.MountedCenter, true);
            Vector2 dir = Vector2.Normalize(projectile.velocity);
            if (float.IsNaN(dir.X) || float.IsNaN(dir.Y))
            {
                dir = -Vector2.UnitY;
            }
            if (Main.myPlayer == projectile.owner)
            {
                if (projectile.ai[0] < 40)
                {
                    Projectile.NewProjectile(pos.X, pos.Y, dir.X * 5, dir.Y * 5, mod.ProjectileType("BurningSphere"), projectile.damage, projectile.knockBack, projectile.owner);
                    Main.PlaySound(SoundID.Item1, projectile.Center);
                }
                else if (projectile.ai[0] < 60)
                {
                    Projectile.NewProjectile(pos.X, pos.Y, dir.X * 12, dir.Y * 12, mod.ProjectileType("FireBolt"), (int)(projectile.damage * 2.5f), projectile.knockBack * 3, projectile.owner);
                    Main.PlaySound(SoundID.Item45, projectile.Center);
                }
                else if (projectile.ai[0] == 100)
                {
                    /*
                    if (Main.netMode != 1)
                    {
                        int n = NPC.NewNPC((int)pos.X, (int)pos.Y + 14, mod.NPCType("FireBall2"), 1, dir.X * 8, dir.Y * 8, projectile.damage * 3, 0, projectile.owner);
                        Main.npc[n].netUpdate = true;
                        projectile.netUpdate = true;
                    }
                    */
                    Projectile.NewProjectile(pos.X, pos.Y, dir.X * 8, dir.Y * 8, mod.ProjectileType("FireBlast"), projectile.damage * 3, projectile.knockBack * 5, projectile.owner);
                    Main.PlaySound(SoundID.Item73, projectile.Center);
                }
            }
        }
    }
}