using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class GilgWrath : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gilgamesh's Wrath");
            Main.projFrames[Projectile.type] = 3;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 192;
            Projectile.height = 192;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.alpha = 25;
            Projectile.light = 0.7f;
            Projectile.tileCollide = true;
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
            return x * x / (a * a) + y * y / (b * b) < 1; //point collision detection
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return Collides(Projectile.position, projHitbox.Size(), targetHitbox.TopLeft(), targetHitbox.Size());
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 68;
            height = 68;
            return true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[1] >= 1)
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            if (Projectile.ai[1] >= 1)
            {
                return base.CanHitPlayer(target);
            }
            return false;
        }
        public override void AI()
        {
            NPC host = Main.npc[(int)Projectile.ai[0]];
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 3)
            {
                Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Projectile.type];
                Projectile.frameCounter = 0;
            }
            Projectile.scale = Projectile.ai[1];
            Projectile.position.X = Projectile.Center.X - (float)(192 * Projectile.scale / 2f);
            Projectile.position.Y = Projectile.Center.Y - (float)(192 * Projectile.scale / 2f);
            Projectile.width = (int)Math.Round(192 * Projectile.scale);
            Projectile.height = (int)Math.Round(192 * Projectile.scale);
            if (Projectile.ai[1] < 1)
            {
                Projectile.ai[1] += 0.01f;
                Projectile.position = host.Center - Projectile.Size / 2;
                Projectile.position.Y -= 180;
            }
            else
            {
                if (Projectile.localAI[0] < 1)
                {
                    float speed = 20;
                    Projectile.velocity = Projectile.DirectionTo(PredictiveAim(speed, Projectile.Center, true)) * speed;
                    Projectile.localAI[0] = 1;
                    SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_phantom_phoenix_shot_0"), Projectile.Center); //217

                }
                if (Projectile.localAI[1] > 0)
                {
                    Projectile.localAI[1]++;
                    if (Projectile.localAI[1] % 8 == 0)
                    {
                        float Speed = 5 + Main.rand.Next(70) * 0.1f;
                        float randRot = Main.rand.Next(360);
                        Vector2 vel = new Vector2((float)(Math.Cos(randRot) * Speed * -1), (float)(Math.Sin(randRot) * Speed * -1));
                        int type = ProjectileID.CultistBossLightningOrbArc;
                        int p = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, vel, type, Projectile.damage / 2, 0, Main.myPlayer, randRot * 0.0174f);
                        Main.projectile[p].tileCollide = false;
                        Main.projectile[p].netUpdate = true;
                    }
                    if (Projectile.localAI[1] % 6 == 0)
                    {
                        SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_lightning_aura_zap_1"), Projectile.Center); //21
                    }
                    if (Projectile.localAI[1] % 60 == 0)
                    {
                        SoundEngine.PlaySound(SoundID.DD2_SkyDragonsFuryCircle, Projectile.Center);
                    }
                    Projectile.ai[1] *= 1.02f;
                    if (Projectile.ai[1] > 6)
                    {
                        Projectile.Kill();
                    }
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.timeLeft = 180;
            Projectile.velocity = Vector2.Zero;
            Projectile.localAI[1]++;
            return false;
        }
        private Vector2 PredictiveAim(float speed, Vector2 origin, bool ignoreY)
        {
            Player P = Main.player[Main.npc[(int)Projectile.ai[0]].target];
            Vector2 vel = ignoreY ? new Vector2(P.velocity.X, 0) : P.velocity;
            Vector2 predictedPos = P.MountedCenter + P.velocity + vel * (Vector2.Distance(P.MountedCenter, origin) / speed);
            predictedPos = P.MountedCenter + P.velocity + vel * (Vector2.Distance(predictedPos, origin) / speed);
            predictedPos = P.MountedCenter + P.velocity + vel * (Vector2.Distance(predictedPos, origin) / speed);
            return predictedPos;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height / Main.projFrames[Projectile.type] * 0.5f);
            Rectangle? rect = new Rectangle?(new Rectangle(0, tex.Height / Main.projFrames[Projectile.type] * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type]));
            SpriteEffects effects = SpriteEffects.None;
            float opacity = Projectile.ai[1] > 4 ? (6 - Projectile.ai[1]) * 0.5f : 1f;
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] + Projectile.Size / 2 - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(tex, drawPos, rect, color * opacity, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            }
            Vector2 drawPosition = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Main.EntitySpriteDraw(tex, drawPosition, rect, Color.White * opacity, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            return false;
        }
    }
}

