using JoostMod.Items.Dyes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using ReLogic.Content;

namespace JoostMod.Projectiles.Melee
{
    public class TrueGungnirBeam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Gungnir");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 7;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.scale = 1.2f;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 24;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.alpha = 255;
            //Projectile.light = 0.5f;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 22;
            height = 22;
            return true;
        }
        public override void AI()
        {
            if (Projectile.ai[0] > 0)
            {
                Player player = Main.player[Projectile.owner];
                Projectile spear = Main.projectile[(int)Projectile.ai[0]];
                if (spear.type == ModContent.ProjectileType<TrueGungnir>() && spear.owner == Projectile.owner)
                {
                    float max = player.itemAnimationMax / 2;
                    if (player.itemAnimation >= max)
                    {
                        Projectile.scale = spear.scale;
                        Projectile.Center = spear.Center + player.velocity;
                        Projectile.width = (int)(60 * Projectile.scale);
                        Projectile.height = (int)(60 * Projectile.scale);
                        Projectile.rotation = (float)Math.Atan2((double)spear.velocity.Y, (double)spear.velocity.X) + 0.785f;
                        Projectile.velocity = spear.velocity * Projectile.ai[1];
                        Projectile.netUpdate = true;
                        Projectile.timeLeft = 24;
                        Projectile.alpha = (int)(255f * ((player.itemAnimation - max) / max));
                    }
                    else
                    {
                        Projectile.ai[0] = -1;
                        Projectile.tileCollide = true;
                        Projectile.penetrate = 3;
                        Projectile.localNPCHitCooldown = 12;
                    }
                }
            }
            else
            {
                Projectile.alpha += 10;
            }
            /*
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.scale -= 0.02f;
                Projectile.alpha += 20;
                if (Projectile.alpha >= 250)
                {
                    Projectile.alpha = 255;
                    Projectile.localAI[0] = 1f;
                }
            }
            else if (Projectile.localAI[0] == 1f)
            {
                Projectile.scale += 0.02f;
                Projectile.alpha -= 20;
                if (Projectile.alpha <= 0)
                {
                    Projectile.alpha = 0;
                    Projectile.localAI[0] = 0f;
                }
            }
            */
            int num1 = Dust.NewDust(
                     Projectile.position,
                     Projectile.width,
                     Projectile.height,
                     DustID.PortalBoltTrail, //Dust ID
                     Projectile.velocity.X,
                     Projectile.velocity.Y,
                     100, //alpha goes from 0 to 255
                     new Color(231, 135, 223),
                     1f
                     );

            Main.dust[num1].noGravity = true;
            Main.dust[num1].velocity *= 0.1f;
            Lighting.AddLight(Projectile.Center, 0.625f, 0.3f, 0.6f);
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10 * Projectile.scale; i++)
            {
                Dust.NewDustDirect(
                         Projectile.position,
                         Projectile.width,
                         Projectile.height,
                         DustID.RainbowMk2, //Dust ID
                         0,
                         0,
                         100, //alpha goes from 0 to 255
                         new Color(231, 135, 223),
                         1.5f
                         ).noGravity = true;
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
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);

            Color color = new Color(79, 14, 74) * ((255f - Projectile.alpha) / 255f);
            float scale = Projectile.scale * 1.1f;
            Vector2 offset = Vector2.Normalize(Projectile.velocity);


            int intended = Main.CurrentDrawnEntityShader; //Temporary until 1.4.4
            int shader = GameShaders.Armor.GetShaderIdFromItemId(ModContent.ItemType<GungnirBeamShader>());

            Main.instance.PrepareDrawnEntityDrawing(Projectile, shader);

            //DrawData data = new DrawData(tex, Projectile.Center - offset * 72 - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, scale, effects, 0);

            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Color c = color * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length) * Projectile.Opacity;
                Vector2 drawPos = Projectile.oldPos[k] + new Vector2(Projectile.width / 2, Projectile.height / 2);
                Texture2D trailTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_Trail");
                Main.EntitySpriteDraw(trailTex, drawPos - offset * 64 - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), c, Projectile.rotation, drawOrigin, scale, effects, 0);
            }

            Main.EntitySpriteDraw(tex, Projectile.Center - offset * 72 - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, scale, effects, 0);

            scale = Projectile.scale * 1.1f;
            color = Color.White * ((255f - Projectile.alpha) / 255f);
            //DrawData data2 = new DrawData(tex, Projectile.Center - offset * 64 - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, scale, effects, 0);

            Main.EntitySpriteDraw(tex, Projectile.Center - offset * 64 - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, scale, effects, 0);

            scale = Projectile.scale;
            color = new Color(231, 135, 223) * ((255f - Projectile.alpha) / 255f);
            //DrawData data3 = new DrawData(tex, Projectile.Center - offset * 64 - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, scale, effects, 0);

            Main.EntitySpriteDraw(tex, Projectile.Center - offset * 64 - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, scale, effects, 0);

            Main.instance.PrepareDrawnEntityDrawing(Projectile, intended);



            /* Wont work until 1.4.4
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);

            MiscShaderData shaderData = GameShaders.Misc["TrueGungnirBeam"];
            shaderData.UseColor(new Color(57, 115, 210));
            shaderData.UseImage0(tex)
            shaderData.Apply(new DrawData?());
            data.Draw(Main.spriteBatch);
            shaderData.Apply(new DrawData?());
            data2.Draw(Main.spriteBatch);
            shaderData.Apply(new DrawData?());
            data3.Draw(Main.spriteBatch);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin();
            */
            return false;
        }

    }
}
