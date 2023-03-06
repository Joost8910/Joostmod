using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class GilgFlail : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gilgamesh's Flail");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.tileCollide = false;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                target.wingTime = 0;
                target.rocketTime = 0;
                target.mount.Dismount(target);
                target.velocity.Y += Projectile.knockBack;
            }
            if (Main.rand.NextBool(2))
            {
                target.AddBuff(BuffID.BrokenArmor, 600);
            }
        }
        public override void AI()
        {
            NPC host = Main.npc[(int)Projectile.ai[0]];
            Vector2 arm = host.Center + new Vector2(40 * host.direction, -31);
            if (Projectile.localAI[0] < 250)
            {
                Projectile.localAI[0] += 2.5f;
            }
            double deg = Projectile.localAI[1];
            double rad = deg * (Math.PI / 180);
            double dist = Projectile.localAI[0];
            if (Projectile.timeLeft < 100)
            {
                dist = Projectile.timeLeft * 2.5f;
            }
            Projectile.position.X = arm.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = arm.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
            Projectile.rotation = (float)rad + 1.57f;
            Projectile.localAI[1] += 6f * Projectile.ai[1];
            if (!host.active || host.life <= 0)
            {
                Projectile.Kill();
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color2 = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Rectangle? rect = new Rectangle?(new Rectangle(0, (TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]) * Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]));
                spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, rect, color2, Projectile.oldRot[k], drawOrigin, Projectile.scale, effects, 0f);
            }
            
            Texture2D texture = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/Projectiles/Flail_Chain");
            NPC host = Main.npc[(int)Projectile.ai[0]];
            Vector2 position = Projectile.Center;
            Vector2 arm = host.Center + new Vector2(40 * host.direction, -31);
            Vector2 dir = position - arm;
            dir.Normalize();
            arm += dir * 56;
            Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            float num1 = (float)texture.Height;
            Vector2 vector2_4 = arm - position;
            float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                flag = false;
            while (flag)
            {
                if ((double)vector2_4.Length() < (double)num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    vector2_4 = arm - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = Projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }

            return true;
        }
    }
}
