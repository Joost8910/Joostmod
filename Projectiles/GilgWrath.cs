using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class GilgWrath : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gilgamesh's Wrath");
            Main.projFrames[projectile.type] = 3;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 192;
            projectile.height = 192;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 600;
            projectile.alpha = 25;
            projectile.light = 0.7f;
            projectile.tileCollide = true;
        }
        public bool Collides(Vector2 ellipsePos, Vector2 ellipseDim, Vector2 boxPos, Vector2 boxDim)
        {
            Vector2 ellipseCenter = ellipsePos + 0.5f * ellipseDim;
            float x = 0f; //ellipse center
            float y = 0f; //ellipse center
            if (boxPos.X > ellipseCenter.X)
            {
                x = boxPos.X - ellipseCenter.X; //left corner
            }
            else if (boxPos.X + boxDim.X < ellipseCenter.X)
            {
                x = boxPos.X + boxDim.X - ellipseCenter.X; //right corner
            }
            if (boxPos.Y > ellipseCenter.Y)
            {
                y = boxPos.Y - ellipseCenter.Y; //top corner
            }
            else if (boxPos.Y + boxDim.Y < ellipseCenter.Y)
            {
                y = boxPos.Y + boxDim.Y - ellipseCenter.Y; //bottom corner
            }
            float a = ellipseDim.X / 2f;
            float b = ellipseDim.Y / 2f;
            return (x * x) / (a * a) + (y * y) / (b * b) < 1; //point collision detection
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return Collides(projectile.position, projHitbox.Size(), targetHitbox.TopLeft(), targetHitbox.Size());
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 68;
            height = 68;
            return true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.ai[1] >= 1)
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            if (projectile.ai[1] >= 1)
            {
                return base.CanHitPlayer(target);
            }
            return false;
        }
        public override void AI()
        {
            NPC host = Main.npc[(int)projectile.ai[0]];
            projectile.frameCounter++;
            if (projectile.frameCounter > 3)
            {
                projectile.frame = (projectile.frame + 1) % Main.projFrames[projectile.type];
                projectile.frameCounter = 0;
            }
            projectile.scale = projectile.ai[1];
            projectile.position.X = projectile.Center.X - (float)((192 * projectile.scale) / 2f);
            projectile.position.Y = projectile.Center.Y - (float)((192 * projectile.scale) / 2f);
            projectile.width = (int)(Math.Round(192 * projectile.scale));
            projectile.height = (int)(Math.Round(192 * projectile.scale));
            if (projectile.ai[1] < 1)
            {
                projectile.ai[1] += 0.01f;
                projectile.position = host.Center - projectile.Size / 2;
                projectile.position.Y -= 180;
            }
            else 
            {
                if (projectile.localAI[0] < 1)
                {
                    float speed = 20;
                    projectile.velocity = projectile.DirectionTo(PredictiveAim(speed, projectile.Center, true)) * speed;
                    projectile.localAI[0] = 1;
                    Main.PlaySound(42, projectile.Center, 217);
                }
                if (projectile.localAI[1] > 0)
                {
                    projectile.localAI[1]++;
                    if (projectile.localAI[1] % 8 == 0)
                    {
                        float Speed = 5 + Main.rand.Next(70) * 0.1f;
                        float randRot = Main.rand.Next(360);
                        Vector2 vel = new Vector2((float)((Math.Cos(randRot) * Speed) * -1), (float)((Math.Sin(randRot) * Speed) * -1));
                        int type = ProjectileID.CultistBossLightningOrbArc;
                        int p = Projectile.NewProjectile(projectile.Center, vel, type, projectile.damage / 2, 0, Main.myPlayer, randRot * 0.0174f);
                        Main.projectile[p].tileCollide = false;
                        Main.projectile[p].netUpdate = true;
                    }
                    if (projectile.localAI[1] % 6 == 0)
                    {
                        Main.PlaySound(42, projectile.Center, 21);
                    }
                    if (projectile.localAI[1] % 60 == 0)
                    {
                        Main.PlaySound(SoundID.DD2_SkyDragonsFuryCircle, projectile.Center);
                    }
                    projectile.ai[1] *= 1.02f;
                    if (projectile.ai[1] > 6)
                    {
                        projectile.Kill();
                    }
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.timeLeft = 180;
            projectile.velocity = Vector2.Zero;
            projectile.localAI[1]++;
            return false;
        }
        private Vector2 PredictiveAim(float speed, Vector2 origin, bool ignoreY)
        {
            Player P = Main.player[Main.npc[(int)projectile.ai[0]].target];
            Vector2 vel = (ignoreY ? new Vector2(P.velocity.X, 0) : P.velocity);
            Vector2 predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(P.MountedCenter, origin) / speed));
            predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(predictedPos, origin) / speed));
            predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(predictedPos, origin) / speed));
            return predictedPos;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, (tex.Height / Main.projFrames[projectile.type]) * 0.5f);
            Rectangle? rect = new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[projectile.type]) * projectile.frame, tex.Width, tex.Height / Main.projFrames[projectile.type]));
            SpriteEffects effects = SpriteEffects.None;
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (projectile.oldPos[k] + projectile.Size / 2) - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(tex, drawPos, rect, color, projectile.rotation, drawOrigin, projectile.scale, effects, 0f);
            }
            Vector2 drawPosition = projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
            spriteBatch.Draw(tex, drawPosition, rect, Color.White, projectile.rotation, drawOrigin, projectile.scale, effects, 0f);
            return false;
        }
    }
}

