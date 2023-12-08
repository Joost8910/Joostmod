using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Minions
{
    public class PowerSpiritBlast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit of Power");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            ProjectileID.Sets.MinionShot[Projectile.type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.width = 500;
            Projectile.height = 500;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.timeLeft = 14;
            Projectile.tileCollide = false;
        }
        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                Projectile.ai[0] = 1;
                Projectile.ai[1] = 14;
            }
            if (Projectile.ai[1] == 0)
            {
                Projectile.ai[1] = 14;
            }
            if (Projectile.localAI[0] == 0)
            {
                Projectile.timeLeft = (int)Projectile.ai[1];
                Projectile.localAI[0] = 1;
            }
            int size = (int)Projectile.ai[1] - Projectile.timeLeft;
            Projectile.scale = size * (1f / Projectile.ai[1]) * Projectile.ai[0];
            Projectile.position.X = Projectile.Center.X - (float)(500 * Projectile.scale / 2f);
            Projectile.position.Y = Projectile.Center.Y - (float)(500 * Projectile.scale / 2f);
            Projectile.width = (int)Math.Round(500 * Projectile.scale);
            Projectile.height = (int)Math.Round(500 * Projectile.scale);
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Collides(Projectile.position, Projectile.Size, target.position, target.Size))
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            if (Collides(Projectile.position, Projectile.Size, target.position, target.Size))
            {
                return base.CanHitPlayer(target);
            }
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            if (Collides(Projectile.position, Projectile.Size, target.position, target.Size))
            {
                return base.CanHitPvp(target);
            }
            return false;
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
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = new Color(90, 255, (int)(51 + Main.DiscoG * 0.75f));
            color *= Projectile.timeLeft / Projectile.ai[1];
            float scale = (Projectile.ai[1] - Projectile.timeLeft) * (1f / Projectile.ai[1]) * Projectile.ai[0];
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), scale, effects, 0);
            return false;
        }
    }
}

