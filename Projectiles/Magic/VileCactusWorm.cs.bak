using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Magic
{
    public class VileCactusWorm : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vile Cactus Worm");
            ProjectileID.Sets.DontAttachHideToAlpha[Projectile.type] = true;
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.hide = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 50;
        }
        public override void AI()
        {
            var source = Projectile.GetSource_FromAI();
            Projectile.frame = (int)Projectile.ai[0];
            Player player = Main.player[Projectile.owner];
            Vector2 playerPos = player.RotatedRelativePoint(player.MountedCenter, true);
            if (Projectile.localAI[0] <= 0 && Projectile.ai[0] <= 0)
            {
                int latestProj = Projectile.whoAmI;
                int cactusWormLength = 4;
                for (int i = 0; i < cactusWormLength; ++i)
                {
                    latestProj = Projectile.NewProjectile(source, Projectile.Center, Vector2.Zero, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner, 1, latestProj);
                }
                latestProj = Projectile.NewProjectile(source, Projectile.Center, Vector2.Zero, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner, 2, latestProj);
                Projectile.localAI[0] = 1;
                Projectile.netUpdate = true;
            }
            bool solid = false;
            for (int i = (int)(Projectile.position.X / 16); i < (int)((Projectile.position.X + Projectile.width) / 16); i++)
            {
                for (int j = (int)(Projectile.Center.Y / 16); j < (int)((Projectile.position.Y + Projectile.height) / 16); j++)
                {
                    Tile tile = Framing.GetTileSafely(i, j);
                    if (tile.HasTile && Main.tileSolid[tile.TileType])
                    {
                        solid = true;
                        break;
                    }
                }
            }
            if (Projectile.ai[0] <= 0)
            {
                bool channeling = player.channel && !player.noItems && !player.CCed && Projectile.localAI[0] == 1;
                if (Main.myPlayer == Projectile.owner && channeling)
                {
                    float scaleFactor = 1f;
                    if (player.inventory[player.selectedItem].shoot == Projectile.type)
                    {
                        scaleFactor = player.inventory[player.selectedItem].shootSpeed * Projectile.scale;
                        player.itemAnimation = player.inventory[player.selectedItem].useTime;
                        player.itemTime = player.inventory[player.selectedItem].useTime;
                    }
                    Vector2 dir = Main.MouseWorld - Projectile.Center;
                    Vector2 rot = player.DirectionTo(Main.MouseWorld);
                    if (rot.X < 0)
                    {
                        player.ChangeDir(-1);
                    }
                    if (rot.X > 0)
                    {
                        player.ChangeDir(1);
                    }
                    player.itemRotation = (float)Math.Atan2((double)(rot.Y * player.direction), (double)(rot.X * player.direction));
                    float length = scaleFactor / dir.Length();
                    dir *= length;
                    if (dir.X != Projectile.velocity.X || dir.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    Projectile.velocity.X = (Projectile.velocity.X * 20 + dir.X) / 21;
                    if (solid)
                    {
                        Projectile.velocity.Y = (Projectile.velocity.Y * 20 + dir.Y) / 21;
                        Point pos = Projectile.Center.ToTileCoordinates();
                        Tile tileSafely = Framing.GetTileSafely(pos.X, pos.Y);
                        Dust dust = Main.dust[WorldGen.KillTile_MakeTileDust(pos.X, pos.Y, tileSafely)];
                        Projectile.localAI[1]++;
                        if (Projectile.localAI[1] > 15)
                        {
                            SoundEngine.PlaySound(SoundID.WormDig, Projectile.position);
                            Projectile.localAI[1] = 0;
                        }
                    }
                    else
                    {
                        if (Projectile.velocity.Y < 10)
                        {
                            Projectile.velocity.Y += 0.2f;
                        }
                    }
                    Projectile.timeLeft = 300;
                }
                else
                {
                    Projectile.localAI[0] = 2;
                    if (solid)
                    {
                        if (Projectile.velocity.Y > -10)
                        {
                            Projectile.velocity.Y -= 0.2f;
                        }
                        Projectile.localAI[1]++;
                        if (Projectile.localAI[1] > 15)
                        {
                            SoundEngine.PlaySound(SoundID.WormDig, Projectile.position);
                            Projectile.localAI[1] = 0;
                        }
                    }
                    else if (Projectile.velocity.Y < 10)
                    {
                        Projectile.velocity.Y += 0.2f;
                    }
                }
                Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            }
            else
            {
                if (!Main.projectile[(int)Projectile.ai[1]].active || Main.projectile[(int)Projectile.ai[1]].owner != Projectile.owner || Main.projectile[(int)Projectile.ai[1]].type != Projectile.type)
                {
                    Projectile.Kill();
                }
                if (Projectile.ai[1] < (double)Main.projectile.Length)
                {
                    Projectile.timeLeft = Main.projectile[(int)Projectile.ai[1]].timeLeft;
                    float dirX = Main.projectile[(int)Projectile.ai[1]].Center.X - Projectile.Center.X;
                    float dirY = Main.projectile[(int)Projectile.ai[1]].Center.Y - Projectile.Center.Y;
                    Projectile.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                    float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                    float dist = (length - Projectile.width) / length;
                    if (Main.projectile[(int)Projectile.ai[1]].ai[0] == 0)
                    {
                        dist = (length - Projectile.width / 2) / length;
                    }
                    float posX = dirX * dist;
                    float posY = dirY * dist;
                    Projectile.velocity = Vector2.Zero;
                    Projectile.position.X = Projectile.position.X + posX;
                    Projectile.position.Y = Projectile.position.Y + posY;
                }
            }
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindNPCsAndTiles.Add(index);
        }
    }
}
