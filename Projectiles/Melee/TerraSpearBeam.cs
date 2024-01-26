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
using Terraria.Audio;

namespace JoostMod.Projectiles.Melee
{
    public class TerraSpearBeam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Spear");
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
                if (spear.type == ModContent.ProjectileType<TerraSpear>() && spear.owner == Projectile.owner)
                {
                    float max = player.itemAnimationMax * 2f / 3f;
                    if (player.itemAnimation >= max)
                    {
                        Projectile.scale = spear.scale;
                        Projectile.Center = spear.Center + player.velocity;
                        Projectile.width = (int)(60 * Projectile.scale);
                        Projectile.height = (int)(60 * Projectile.scale);
                        Projectile.rotation = (float)Math.Atan2((double)spear.velocity.Y, (double)spear.velocity.X) + 0.785f;
                        Projectile.velocity = spear.velocity * Projectile.ai[1] * 0.5f;
                        Projectile.netUpdate = true;
                        Projectile.timeLeft = 24;
                        Projectile.alpha = (int)(255f * ((player.itemAnimation - max) / max));
                    }
                    else
                    {
                        SoundEngine.PlaySound(SoundID.Item8, Projectile.Center);
                        Vector2 velA = Projectile.velocity.RotatedBy(24 * Math.PI / 180);
                        Vector2 velB = Projectile.velocity.RotatedBy(-24 * Math.PI / 180);
                        Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, velA * 0.9f, ModContent.ProjectileType<TerraSpearBeam2>(), Projectile.damage, Projectile.knockBack / 2, Projectile.owner, Projectile.ai[0], 12);
                        Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, velB * 0.9f, ModContent.ProjectileType<TerraSpearBeam2>(), Projectile.damage, Projectile.knockBack / 2, Projectile.owner, Projectile.ai[0], -12);

                        SoundEngine.PlaySound(SoundID.Item8, Projectile.Center);
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
            int num1 = Dust.NewDust(
                     Projectile.position,
                     Projectile.width,
                     Projectile.height,
                     DustID.PortalBoltTrail, //Dust ID
                     Projectile.velocity.X,
                     Projectile.velocity.Y,
                     100, //alpha goes from 0 to 255
                     new Color(83, 255, 40),
                     1f
                     );

            Main.dust[num1].noGravity = true;
            Main.dust[num1].velocity *= 0.1f;
            Lighting.AddLight(Projectile.Center, 0.325f, 1f, 0.157f);
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
                         new Color(83, 255, 40),
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

            Color color = new Color(44, 417, 00) * ((255f - Projectile.alpha) / 255f);
            float scale = Projectile.scale * 1.1f;
            Vector2 offset = Vector2.Normalize(Projectile.velocity);


            int intended = Main.CurrentDrawnEntityShader; //Temporary until 1.4.4
            int shader = GameShaders.Armor.GetShaderIdFromItemId(ModContent.ItemType<TerraBeamShader>());

            Main.instance.PrepareDrawnEntityDrawing(Projectile, shader);

            //DrawData data = new DrawData(tex, Projectile.Center - offset * 72 - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, scale, effects, 0);

            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Color c = color * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length) * Projectile.Opacity;
                Vector2 drawPos = Projectile.oldPos[k] + new Vector2(Projectile.width / 2, Projectile.height / 2);
                Texture2D trailTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_Trail");
                Main.EntitySpriteDraw(trailTex, drawPos - offset * 64 - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), c, Projectile.rotation, drawOrigin, scale, effects, 0);
            }

            Main.EntitySpriteDraw(tex, Projectile.Center - offset * 68 - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, scale, effects, 0);

            scale = Projectile.scale * 1.1f;
            color = new Color(205, 255, 198) * ((255f - Projectile.alpha) / 255f);
            //DrawData data2 = new DrawData(tex, Projectile.Center - offset * 64 - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, scale, effects, 0);

            Main.EntitySpriteDraw(tex, Projectile.Center - offset * 64 - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, scale, effects, 0);

            scale = Projectile.scale;
            color = new Color(151, 255, 125) * ((255f - Projectile.alpha) / 255f);
            //DrawData data3 = new DrawData(tex, Projectile.Center - offset * 64 - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, scale, effects, 0);

            Main.EntitySpriteDraw(tex, Projectile.Center - offset * 64 - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, scale, effects, 0);

            Main.instance.PrepareDrawnEntityDrawing(Projectile, intended);

            return false;
        }

    }
    public class TerraSpearBeam2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Spear");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.scale = 1.1f;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 24;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.alpha = 128;
            //Projectile.light = 0.5f;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 12;
            height = 12;
            return true;
        }
        public override void ModifyDamageScaling(ref float damageScale)
        {
            damageScale *= 0.75f;
        }
        public override void AI()
        {
            if (Projectile.ai[0] > 0)
            {
                Player player = Main.player[Projectile.owner];
                Projectile spear = Main.projectile[(int)Projectile.ai[0]];
                if (spear.type == ModContent.ProjectileType<TerraSpear>() && spear.owner == Projectile.owner)
                {
                    if (Projectile.localAI[0] == 0)
                    {
                        Vector2 size = Projectile.Size;
                        Projectile.scale = spear.scale;
                        Projectile.width = (int)(38 * Projectile.scale);
                        Projectile.height = (int)(38 * Projectile.scale);
                        Projectile.position -= (Projectile.Size / 2) - (size / 2);

                        Vector2 offset = Vector2.Normalize(spear.velocity) * spear.scale * Projectile.ai[1];
                        offset = offset.RotatedBy(Math.PI / 2);
                        Projectile.Center += offset + player.velocity;

                        Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 0.785f;
                        Projectile.netUpdate = true;
                        Projectile.localAI[0] = 1;
                    }
                    else
                    {
                        Projectile.ai[0] = -1;
                        Projectile.tileCollide = true;
                        Projectile.penetrate = 3;
                    }
                }
            }
            else
            {
                Projectile.alpha += 6;
            }
            if (Projectile.timeLeft % 2 == 0)
            {
                int num1 = Dust.NewDust(
                         Projectile.position,
                         Projectile.width,
                         Projectile.height,
                         DustID.PortalBoltTrail, //Dust ID
                         Projectile.velocity.X,
                         Projectile.velocity.Y,
                         100, //alpha goes from 0 to 255
                         new Color(83, 255, 40),
                         1f
                         );

                Main.dust[num1].noGravity = true;
                Main.dust[num1].velocity *= 0.1f;
            }
            Lighting.AddLight(Projectile.Center, 0.162f, 0.5f, 0.078f);
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 6; i++)
            {
                Dust.NewDustDirect(
                     Projectile.position,
                     Projectile.width,
                     Projectile.height,
                     DustID.PortalBoltTrail, //Dust ID
                     Projectile.velocity.X * 0.5f,
                     Projectile.velocity.Y * 0.5f,
                     100, //alpha goes from 0 to 255
                     new Color(83, 255, 40),
                     1f
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

            Color color = new Color(44, 417, 00) * ((255f - Projectile.alpha) / 255f);
            float scale = Projectile.scale * 1.1f;
            Vector2 offset = Vector2.Normalize(Projectile.velocity);


            int intended = Main.CurrentDrawnEntityShader; //Temporary until 1.4.4
            int shader = GameShaders.Armor.GetShaderIdFromItemId(ModContent.ItemType<TerraBeamShader>());

            Main.instance.PrepareDrawnEntityDrawing(Projectile, shader);

            //DrawData data = new DrawData(tex, Projectile.Center - offset * 72 - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, scale, effects, 0);


            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Color c = color * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length) * Projectile.Opacity;
                Vector2 drawPos = Projectile.oldPos[k] + new Vector2(Projectile.width / 2, Projectile.height / 2);
                Main.EntitySpriteDraw(tex, drawPos - offset * 64 - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), c, Projectile.rotation, drawOrigin, scale, effects, 0);
            }


            Main.EntitySpriteDraw(tex, Projectile.Center - offset * 56 - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, scale, effects, 0);

            scale = Projectile.scale * 1.1f;
            color = new Color(205, 255, 198) * ((255f - Projectile.alpha) / 255f);
            //DrawData data2 = new DrawData(tex, Projectile.Center - offset * 64 - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, scale, effects, 0);

            Main.EntitySpriteDraw(tex, Projectile.Center - offset * 48 - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, scale, effects, 0);

            scale = Projectile.scale;
            color = new Color(151, 255, 125) * ((255f - Projectile.alpha) / 255f);
            //DrawData data3 = new DrawData(tex, Projectile.Center - offset * 64 - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, scale, effects, 0);

            Main.EntitySpriteDraw(tex, Projectile.Center - offset * 48 - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, scale, effects, 0);

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
