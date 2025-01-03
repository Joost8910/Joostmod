using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Magic
{
    public class DavidBolt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bolt of David");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.alpha = 25;
            Projectile.extraUpdates = 1;
            Projectile.light = 0.3f;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45);
            if (Projectile.timeLeft > 570)
            {
                if (Projectile.ai[0] == 0)
                {
                    Projectile.velocity = (Projectile.velocity.ToRotation() - MathHelper.ToRadians(3)).ToRotationVector2() * Projectile.velocity.Length();
                }
                if (Projectile.ai[0] == 2)
                {
                    Projectile.velocity = (Projectile.velocity.ToRotation() + MathHelper.ToRadians(3)).ToRotationVector2() * Projectile.velocity.Length();
                }
            }
            if (Projectile.timeLeft % 5 == 0)
            {
                //say you wanted to add particles that stay mostly still to leave a trail behind a projectile
                int num1 = Dust.NewDust(
                         Projectile.position,
                         Projectile.width,
                         Projectile.height,
                         178, //Dust ID
                         Projectile.velocity.X,
                         Projectile.velocity.Y,
                         100, //alpha goes from 0 to 255
                         new Color(0, 255, 146),
                         2f
                         );

                Main.dust[num1].noGravity = true;
                Main.dust[num1].velocity *= 0.1f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            Projectile.timeLeft -= 100;
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
            Color color = new Color(90, 255, (int)(51 + Main.DiscoG * 0.75f));
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);

            MiscShaderData shaderData = GameShaders.Misc["JoostBolt"];
            shaderData.UseColor(Color.White);
            shaderData.UseSecondaryColor(color);
            shaderData.UseImage0(TextureAssets.Projectile[Projectile.type]);
            
            for (int k = Projectile.oldPos.Length - 1; k >= 0; k--)
            {
                Color c = color * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length) * Projectile.Opacity;
                Vector2 drawPos = Projectile.oldPos[k] + new Vector2(Projectile.width / 2, Projectile.height / 2);
                DrawData dataTrail = new DrawData(tex, drawPos - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), c, Projectile.oldRot[k], new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, effects, 0);
                shaderData.Apply(dataTrail);
                dataTrail.Draw(Main.spriteBatch);
            }
            DrawData data = new DrawData(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, effects, 0);
            shaderData.Apply(data);
            data.Draw(Main.spriteBatch);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin();
            return false;
        }
    }
}

