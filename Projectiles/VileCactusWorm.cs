using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class VileCactusWorm : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vile Cactus Worm");
            ProjectileID.Sets.DontAttachHideToAlpha[projectile.type] = true;
            Main.projFrames[projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.hide = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
            projectile.tileCollide = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 50;
        }
        public override void AI()
        {
            projectile.frame = (int)projectile.ai[0];
            Player player = Main.player[projectile.owner];
            Vector2 playerPos = player.RotatedRelativePoint(player.MountedCenter, true);
            if (projectile.localAI[0] <= 0 && projectile.ai[0] <= 0)
            {
                int latestProj = projectile.whoAmI;
                int cactusWormLength = 4;
                for (int i = 0; i < cactusWormLength; ++i)
                {
                    latestProj = Projectile.NewProjectile(projectile.Center, Vector2.Zero, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 1, latestProj);
                }
                latestProj = Projectile.NewProjectile(projectile.Center, Vector2.Zero, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 2, latestProj);
                projectile.localAI[0] = 1;
                projectile.netUpdate = true;
            }
            bool solid = false;
            for (int i = (int)(projectile.position.X / 16); i < (int)((projectile.position.X + projectile.width) / 16); i++)
            {
                for (int j = (int)(projectile.Center.Y / 16); j < (int)((projectile.position.Y + projectile.height) / 16); j++)
                {
                    Tile tile = Framing.GetTileSafely(i, j);
                    if (tile.active() && Main.tileSolid[(int)tile.type])
                    {
                        solid = true;
                        break;
                    }
                }
            }
            if (projectile.ai[0] <= 0)
            {
                bool channeling = player.channel && !player.noItems && !player.CCed && projectile.localAI[0] == 1;
                if (Main.myPlayer == projectile.owner && channeling)
                {
                    float scaleFactor = 1f;
                    if (player.inventory[player.selectedItem].shoot == projectile.type)
                    {
                        scaleFactor = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                        player.itemAnimation = player.inventory[player.selectedItem].useTime;
                        player.itemTime = player.inventory[player.selectedItem].useTime;
                    }
                    Vector2 dir = Main.MouseWorld - projectile.Center;
                    Vector2 rot = player.DirectionTo(Main.MouseWorld);
                    if (rot.X < 0)
                    {
                        player.ChangeDir(-1);
                    }
                    if (rot.X > 0)
                    {
                        player.ChangeDir(1);
                    }
                    player.itemRotation = (float)Math.Atan2((double)(rot.Y * (float)player.direction), (double)(rot.X * (float)player.direction));
                    float length = scaleFactor / dir.Length();
                    dir *= length;
                    if (dir.X != projectile.velocity.X || dir.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.velocity.X = (projectile.velocity.X * 20 + dir.X) / 21;
                    if (solid)
                    {
                        projectile.velocity.Y = (projectile.velocity.Y * 20 + dir.Y) / 21;
                        Point pos = projectile.Center.ToTileCoordinates();
                        Tile tileSafely = Framing.GetTileSafely(pos.X, pos.Y);
                        Dust dust = Main.dust[WorldGen.KillTile_MakeTileDust(pos.X, pos.Y, tileSafely)];
                        projectile.localAI[1]++;
                        if (projectile.localAI[1] > 15)
                        {
                            Main.PlaySound(15, (int)projectile.position.X, (int)projectile.position.Y, 1);
                            projectile.localAI[1] = 0;
                        }
                    }
                    else
                    {
                        if (projectile.velocity.Y < 10)
                        {
                            projectile.velocity.Y += 0.2f;
                        }
                    }
                    projectile.timeLeft = 300;
                }
                else
                {
                    projectile.localAI[0] = 2;
                    if (solid)
                    {
                        if (projectile.velocity.Y > -10)
                        {
                            projectile.velocity.Y -= 0.2f;
                        }
                        projectile.localAI[1]++;
                        if (projectile.localAI[1] > 15)
                        {
                            Main.PlaySound(15, (int)projectile.position.X, (int)projectile.position.Y, 1);
                            projectile.localAI[1] = 0;
                        }
                    }
                    else if (projectile.velocity.Y < 10)
                    {
                        projectile.velocity.Y += 0.2f;
                    }
                }
                projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            }
            else
            {
                if (!Main.projectile[(int)projectile.ai[1]].active || Main.projectile[(int)projectile.ai[1]].owner != projectile.owner || Main.projectile[(int)projectile.ai[1]].type != projectile.type)
                {
                    projectile.Kill();
                }
                if (projectile.ai[1] < (double)Main.projectile.Length)
                {
                    projectile.timeLeft = Main.projectile[(int)projectile.ai[1]].timeLeft;
                    float dirX = Main.projectile[(int)projectile.ai[1]].Center.X - projectile.Center.X;
                    float dirY = Main.projectile[(int)projectile.ai[1]].Center.Y - projectile.Center.Y;
                    projectile.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                    float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                    float dist = (length - (float)projectile.width) / length;
                    if (Main.projectile[(int)projectile.ai[1]].ai[0] == 0)
                    {
                        dist = (length - projectile.width / 2) / length;
                    }
                    float posX = dirX * dist;
                    float posY = dirY * dist;
                    projectile.velocity = Vector2.Zero;
                    projectile.position.X = projectile.position.X + posX;
                    projectile.position.Y = projectile.position.Y + posY;
                }
            }
        }
        public override void DrawBehind(int index, List<int> drawCacheProjsBehindprojectilesAndTiles, List<int> drawCacheProjsBehindprojectiles, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
        {
            drawCacheProjsBehindprojectilesAndTiles.Add(index);
        }
    }
}
