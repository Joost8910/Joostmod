using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class Gravebuster : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Weather Star - Slime");
            Main.projFrames[Type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
        }
        public override void AI()
        {
            Projectile.velocity = Vector2.Zero;
            if (Projectile.timeLeft < 255)
            {
                Projectile.frameCounter++;
                if (Projectile.frameCounter > 4)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame = (Projectile.frame + 1) % 4;
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Stone);
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            Point tPos = Projectile.Center.ToTileCoordinates();
            if (Main.tile[tPos.X, tPos.Y].TileType == TileID.Tombstones)
            {
                Main.player[Projectile.owner].PickTile(tPos.X, tPos.Y, 999);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, (tex.Height / Main.projFrames[Projectile.type]) * 0.5f);
            SpriteEffects effects = SpriteEffects.None;
            Vector2 drawPos = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Rectangle? rect = new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[Projectile.type]) * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type]));

            Texture2D leaves = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_Leaves");
            Texture2D eyebrows = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_Eyebrows");
            Vector2 eyebrowOffset = Vector2.Zero;
            Vector2 scale = new Vector2(1f, 1f);
            Vector2 leafScale = new Vector2(1f, 1f);
            Vector2 browScale = new Vector2(1f, 1f);
                
            int age = 300 - Projectile.timeLeft;
            if (age <= 22) //Spawning Animation
            {
                eyebrowOffset.Y -= age / 12f;
                scale.Y -= age / 120f;
                scale.X = 1f / scale.Y;
            }
            else if (age <= 45) 
            {
                eyebrowOffset.Y -= (45 - age) / 12f;
                scale.Y -= (45 - age) / 120f;
                scale.X = 1f / scale.Y;
            }
            else
            {
                drawPos.Y += (age - 45) / 14f; //How far down the Gravebuster travels

                int a = ((age - 45) % 48) - 24; //Squish periodically
                if (a > 12)
                {
                    scale.X += (24 - a) / 120f;
                    scale.Y = 1f / scale.X;
                }
                else if (a > 0)
                {
                    scale.X += a / 120f;
                    scale.Y = 1f / scale.X;
                }
            }

            int l = age % 60; //Squish the leaves for a mock animation
            if (l > 45)
            {
                leafScale.X -= (60 - l) / 240f;
                leafScale.Y = 1f / leafScale.X;
            }
            else if (l > 15)
            {
                leafScale.X += (30 - l) / 240f;
                leafScale.Y = 1f / leafScale.X;
            }
            else
            {
                leafScale.X += l / 240f;
                leafScale.Y = 1f / leafScale.X;
            }
            Vector2 eyeBrowPos = drawPos + eyebrowOffset;
            Vector2 leafPos = drawPos + new Vector2(0, -4 * scale.Y);


            Main.EntitySpriteDraw(tex, drawPos, rect, lightColor, Projectile.rotation, drawOrigin, scale, effects);
            Main.EntitySpriteDraw(leaves, leafPos, leaves.Bounds, lightColor, Projectile.rotation, leaves.Size() / 2, leafScale, effects);
            Main.EntitySpriteDraw(eyebrows, eyeBrowPos, eyebrows.Bounds, lightColor, Projectile.rotation, eyebrows.Size() / 2, browScale, effects);
            return false;
        }
    }
}

