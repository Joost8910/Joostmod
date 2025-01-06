using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class JumboCactuarFireball : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sand Ball");
        }
        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.aiStyle = 1;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 90;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = false;
            AIType = ProjectileID.Bullet;
            CooldownSlot = 1;
        }
        public override void AI()
        {
            if (Projectile.timeLeft % 5 == 0)
            {
                int num1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X, Projectile.velocity.Y, 0, default, 1.5f);

                Main.dust[num1].noGravity = true;
                Main.dust[num1].velocity *= 0.1f;
            }
            Lighting.AddLight(Projectile.Center, 1f, 0.25f, 0.1f);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);

            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Rectangle rect = new Rectangle(0, 0, tex.Width, tex.Height);
            Vector2 origin = rect.Size() / 2;
            Vector2 drawPos = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            SpriteEffects effects = SpriteEffects.None;
            float rot = Projectile.rotation + MathHelper.ToRadians(120);

            MiscShaderData shaderData = GameShaders.Misc["JoostMeteor"];

            DrawData data = new DrawData(tex, drawPos, new Rectangle?(rect), Color.White, rot, origin, Projectile.scale, effects, 0);

            shaderData.UseImage0(TextureAssets.Projectile[Projectile.type]);
            shaderData.Apply(data);
            data.Draw(Main.spriteBatch);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin();
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item20, Projectile.Center);
            for(int i = 0; i < 5; i++)
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X, Projectile.velocity.Y, 0, default, 2f);
                d.velocity = Projectile.DirectionTo(d.position) * 10;
            }
            var source = Projectile.GetSource_Death();
            float numberProjectiles = 3;
            float rotation = MathHelper.ToRadians(45);
            float damageMult = 0.8f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                Projectile.NewProjectile(source, Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<JumboCactuarFireball2>(), (int)(Projectile.damage * damageMult), Projectile.knockBack * damageMult, Projectile.owner, 1);
            }
        }

    }
    public class JumboCactuarFireball2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sand Ball");
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.aiStyle = 1;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = false;
            AIType = ProjectileID.Bullet;
            CooldownSlot = 1;
        }
        public override void AI()
        {
            if (Projectile.timeLeft % 5 == 0)
            {
                int num1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X, Projectile.velocity.Y, 0, default, 1f);

                Main.dust[num1].noGravity = true;
                Main.dust[num1].velocity *= 0.1f;
            }
            if (Projectile.timeLeft % 4 == 0)
            {
                Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Projectile.type];
            }
            Lighting.AddLight(Projectile.Center, 0.5f, 0.25f, 0.01f);

            if (Projectile.velocity.Y > 0)
                Projectile.tileCollide = true;
            if (Projectile.velocity.Y < 15)
                Projectile.velocity.Y += 0.3f;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 4; i++)
            {
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X, Projectile.velocity.Y, 0, default, 1f);
                d.velocity = Projectile.DirectionTo(d.position) * 10;
            }
        }
    }
}
