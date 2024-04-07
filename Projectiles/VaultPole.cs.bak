using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class VaultPole : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vaulting Pole");
            Main.projFrames[Projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            /*
            Projectile.penetrate = -1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 20;
            Projectile.ownerHitCheck = true;
            */
        }
        public override bool? CanDamage()
        {
            return false;
        }
        /*
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Player p = Main.player[Projectile.owner];
            Vector2 unit = Projectile.velocity;
            float point = 0f;
            Vector2 pCenter = p.MountedCenter;
            Vector2 start = pCenter;
            Vector2 end = start + unit * 220;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, 6, ref point))
            {
                return true;
            }
            return false;
        }*/
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 pCenter = player.MountedCenter;
            if (player.channel && !player.noItems && !player.CCed)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    Vector2 mousePos = Main.MouseWorld;
                    Vector2 diff = mousePos - pCenter;
                    diff.Normalize();
                    float home = 11f;
                    Vector2 dir = ((home - 1f) * Projectile.velocity + diff) / home;
                    dir.Normalize();
                    if (dir.X != Projectile.velocity.X || dir.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    if (Projectile.localAI[0] < 136)
                    {
                        Vector2 point = new Vector2(Projectile.ai[0], Projectile.ai[1]);
                        Vector2 nCenter = point - dir * Projectile.localAI[0];

                        Vector2 pVel = nCenter - Projectile.Center;
                        pVel = Collision.TileCollision(Projectile.Center - player.Size / 2, pVel, player.width, player.height);
                        Vector4 vect2 = Collision.SlopeCollision(Projectile.Center - player.Size / 2, pVel, player.width, player.height);
                        pVel = new Vector2(vect2.Z, vect2.W);
                        if (pVel == nCenter - Projectile.Center)
                        {
                            Projectile.velocity = dir;
                        }
                    }
                    else
                    {
                        Projectile.velocity = dir;
                    }
                }
            }
            else
            {
                Projectile.Kill();
            }
        
            Projectile.spriteDirection = Projectile.direction * (int)player.gravDir;
            Projectile.timeLeft = 2;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 15;
            player.itemAnimation = 15;
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * (float)Projectile.direction), (double)(Projectile.velocity.X * (float)Projectile.direction));

            if (Projectile.localAI[0] >= 136 || Projectile.localAI[0] == 0)
            {
                Vector2 unit = Projectile.velocity;
                Vector2 start = pCenter;
                Vector2 lineVect = unit * 8;
                Vector2 point = start - new Vector2(3, 3);
                bool hit = false;
                for (int i = 0; i < 17; i++)
                {
                    lineVect = Collision.TileCollision(point, lineVect, 6, 6);
                    Vector4 vect = Collision.SlopeCollision(point, lineVect, 6, 6);
                    lineVect = new Vector2(vect.Z, vect.W);
                    point = new Vector2(vect.X, vect.Y) + lineVect;
                    //Dust.NewDustPerfect(point + new Vector2(3, 3), 206, Vector2.Zero, 0, Color.Red, 0.5f).noGravity = true;
                    if (lineVect != unit * 8)
                    {
                        hit = true;
                        break;
                    }
                }
                point += new Vector2(3, 3);

                if (hit)
                {
                    //Dust.NewDustPerfect(point, 206, Vector2.Zero).noGravity = true;

                    float dist = start.Distance(point);
                    Projectile.localAI[0] = dist;
                    Projectile.ai[0] = point.X;
                    Projectile.ai[1] = point.Y;
                    Projectile.Center = point - Projectile.velocity * Projectile.localAI[0];
                    Vector2 vs = start + player.velocity;
                    float dist2 = vs.Distance(point);
                    Projectile.localAI[1] = dist2 - dist;

                    player.velocity = Projectile.Center - pCenter;
                    //player.MountedCenter = Projectile.Center;
                }
                else
                {
                    Projectile.localAI[0] = 136;
                    Projectile.localAI[1] = 0;
                    Projectile.Center = pCenter;
                }
            }
            else
            {
                Vector2 point = new Vector2(Projectile.ai[0], Projectile.ai[1]);

                Point tileCoord = point.ToTileCoordinates();
                Tile tile = Main.tile[tileCoord.X, tileCoord.Y];
                int conveyorDir = TileID.Sets.ConveyorDirection[tile.TileType];
                if (conveyorDir == 0)
                {
                    tileCoord.Y += (int)player.gravDir;
                    tile = Main.tile[tileCoord.X, tileCoord.Y];
                    conveyorDir = TileID.Sets.ConveyorDirection[tile.TileType];
                }
                if (conveyorDir != 0)
                {
                    Vector2 lineStart = default(Vector2);
                    Vector2 lineStart2 = default(Vector2);
                    lineStart.X = (lineStart2.X = (float)(tileCoord.X * 16));
                    Vector2 lineEnd = default(Vector2);
                    Vector2 lineEnd2 = default(Vector2);
                    lineEnd.X = (lineEnd2.X = (float)(tileCoord.X * 16 + 16));
                    int num = 0;
                    int num2 = 0;
                    bool flag = false;
                    switch (tile.Slope)
                    {
                        case (SlopeType)1:
                            lineStart2.Y = (float)(tileCoord.Y * 16);
                            lineEnd2.Y = (lineEnd.Y = (lineStart.Y = (float)(tileCoord.Y * 16 + 16)));
                            break;
                        case (SlopeType)2:
                            lineEnd2.Y = (float)(tileCoord.Y * 16);
                            lineStart2.Y = (lineEnd.Y = (lineStart.Y = (float)(tileCoord.Y * 16 + 16)));
                            break;
                        case (SlopeType)3:
                            lineEnd.Y = (lineStart2.Y = (lineEnd2.Y = (float)(tileCoord.Y * 16)));
                            lineStart.Y = (float)(tileCoord.Y * 16 + 16);
                            break;
                        case (SlopeType)4:
                            lineStart.Y = (lineStart2.Y = (lineEnd2.Y = (float)(tileCoord.Y * 16)));
                            lineEnd.Y = (float)(tileCoord.Y * 16 + 16);
                            break;
                        default:
                            if (tile.IsHalfBlock)
                            {
                                lineStart2.Y = (lineEnd2.Y = (float)(tileCoord.Y * 16 + 8));
                            }
                            else
                            {
                                lineStart2.Y = (lineEnd2.Y = (float)(tileCoord.Y * 16));
                            }
                            lineStart.Y = (lineEnd.Y = (float)(tileCoord.Y * 16 + 16));
                            break;
                    }
                    Vector2 vector = new Vector2(0.0001f);
                    Vector2 size = new Vector2(6, 6);
                    Vector2 pos = point - size / 2;
                    int num5 = 0;
                    if (!TileID.Sets.Platforms[tile.TileType] && Collision.CheckAABBvLineCollision2(pos - vector, size + vector * 2f, lineStart, lineEnd))
                    {
                        num5--;
                    }
                    if (Collision.CheckAABBvLineCollision2(pos - vector, size + vector * 2f, lineStart2, lineEnd2))
                    {
                        num5++;
                    }
                    if (num5 != 0)
                    {
                        flag = true;
                        num += conveyorDir * num5 * (int)player.gravDir;
                        if (tile.LeftSlope)
                        {
                            num2 += (int)player.gravDir * conveyorDir;
                        }
                        if (tile.RightSlope)
                        {
                            num2 -= (int)player.gravDir * conveyorDir;
                        }
                    }

                    if (flag && num != 0)
                    {
                        num = Math.Sign(num);
                        num2 = Math.Sign(num2);
                        Vector2 velocity = Vector2.Normalize(new Vector2((float)num * player.gravDir, (float)num2)) * 2.5f;
                        Vector2 vector2 = Collision.TileCollision(pos, velocity, (int)size.X, (int)size.Y, false, false, (int)player.gravDir);
                        pos += vector2;
                        Vector2 velocity2 = new Vector2(0f, 2.5f * player.gravDir);
                        vector2 = Collision.TileCollision(pos, velocity2, (int)size.X, (int)size.Y, false, false, (int)player.gravDir);
                        pos += vector2;
                        pos += size / 2;
                        point = pos;
                        Projectile.ai[0] = point.X;
                        Projectile.ai[1] = point.Y;
                    }
                }

                Vector2 nCenter = point - Projectile.velocity * (Projectile.localAI[0] + Projectile.localAI[1]);
                Vector2 pVel = nCenter - Projectile.Center;
                pVel = Collision.TileCollision(Projectile.Center - player.Size / 2, pVel, player.width, player.height);
                Vector4 vect2 = Collision.SlopeCollision(Projectile.Center - player.Size/2, pVel, player.width, player.height);
                pVel = new Vector2(vect2.Z, vect2.W);
                if (Projectile.localAI[1] > 0)
                {
                    player.fallStart = (int)player.position.Y / 16;
                }
                if (pVel == nCenter - Projectile.Center)
                {
                    Projectile.localAI[0] += Projectile.localAI[1];
                    Projectile.localAI[1] += 0.5f;
                }
                else if (Projectile.localAI[1] < 0)
                {
                    Projectile.localAI[1] = 0.5f;
                }

                if (Projectile.localAI[0] < 0)
                {
                    Projectile.localAI[0] = 0;
                }

                Projectile.Center = point - Projectile.velocity * Projectile.localAI[0];
                player.velocity = Projectile.Center - pCenter;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() - 1.57f;
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
		{
			Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
			if (Projectile.spriteDirection == -1)
			{
				effects = SpriteEffects.FlipHorizontally;
            }
            int yOff = 0;
            if (136 - Projectile.localAI[0] >= 4)
            {
                yOff = 4;
                Projectile.frame = 1;
            }
            else
            {
                Projectile.frame = 0;
            }
            if (136 - Projectile.localAI[0] >= 18)
            {
                yOff = 18;
                Projectile.frame = 2;
            }
            if (136 - Projectile.localAI[0] >= 38)
            {
                yOff = 38;
                Projectile.frame = 3;
            }
            if (136 - Projectile.localAI[0] >= 74)
            {
                yOff = 74;
                Projectile.frame = 4;
            }
            if (136 - Projectile.localAI[0] >= 100)
            {
                yOff = 100;
                Projectile.frame = 5;
            }

            Vector2 drawOrigin = new Vector2(tex.Width / 2, yOff);
            Rectangle? rect = new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[Projectile.type]) * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type]));
            Color color = Lighting.GetColor((int)(Projectile.Center.X / 16), (int)(Projectile.Center.Y / 16.0));
			Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), rect, color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
			return false;
		}
    }
}