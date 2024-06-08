using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Magic
{
    public class Star : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_9";
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.aiStyle = 5;
            AIType = ProjectileID.Starfury;
            Projectile.friendly = true;
            Projectile.penetrate = 2;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);
            Color newColor7 = (Projectile.ai[2] == 1 ? Color.CornflowerBlue : Color.Pink);
            if (Main.tenthAnniversaryWorld)
            {
                newColor7 = (Projectile.ai[2] == 1 ? Color.DeepPink : Color.HotPink);
                newColor7.A /= 2;
            }
            for (int num645 = 0; num645 < 7; num645++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Pink, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 150, default(Color), 0.8f);
            }
            for (float num646 = 0f; num646 < 1f; num646 += 0.25f)
            {
                Dust.NewDustPerfect(Projectile.Center, 278, new Vector2?(Vector2.UnitY.RotatedBy((double)(num646 * 6.28318548f + Main.rand.NextFloat() * 0.5f), default(Vector2)) * (2f + Main.rand.NextFloat() * 2f)), 150, newColor7, 1f).noGravity = true;
            }
            for (float num647 = 0f; num647 < 1f; num647 += 0.5f)
            {
                Dust.NewDustPerfect(Projectile.Center, 278, new Vector2?(Vector2.UnitY.RotatedBy((double)(num647 * 6.28318548f + Main.rand.NextFloat() * 0.5f), default(Vector2)) * (1f + Main.rand.NextFloat() * 1.5f)), 150, Color.Gold, 1f).noGravity = true;
            }
            Vector2 vector54 = new Vector2((float)Main.screenWidth, (float)Main.screenHeight);
            if (Projectile.Hitbox.Intersects(Utils.CenteredRectangle(Main.screenPosition + vector54 / 2f, vector54 + new Vector2(400f))))
            {
                for (int num648 = 0; num648 < 3; num648++)
                {
                    Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, Main.rand.NextVector2CircularEdge(0.5f, 0.5f), Utils.SelectRandom<int>(Main.rand, new int[]
                    {
                            16,
                            16,
                            17
                    }), 1f);
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Color color = new Color(255, 255, 255, 255 - Projectile.alpha);
            Vector2 vector = Projectile.velocity;
            Color color2 = (Projectile.ai[2] == 1 ? Color.CornflowerBlue * 0.2f : Color.Pink * 0.2f);
            if (Main.tenthAnniversaryWorld)
            {
                color2 = (Projectile.ai[2] == 1 ? Color.DeepPink * 0.3f : Color.HotPink * 0.2f);
                color2.A /= 2;
            }
            Vector2 spinningpoint = new Vector2(0f, -4f);
            float num = -0.6f;
            float t = vector.Length();
            float num2 = Utils.GetLerpValue(3f, 5f, t, true);
            bool flag = true;
            
            Vector2 posPlusVel = Projectile.Center + vector;
            Texture2D value = TextureAssets.Projectile[Projectile.type].Value;
            //new Rectangle(0, 0, value.Width, value.Height).Size / 2f;
            Texture2D value2 = TextureAssets.Extra[91].Value;
            Rectangle rectangle = value2.Frame(1, 1, 0, 0, 0, 0);
            Vector2 origin2 = new Vector2((float)rectangle.Width / 2f, 10f);
            //Color.Cyan * 0.5f * num2;
            Vector2 vector2 = new Vector2(0f, Projectile.gfxOffY);
            float num8 = (float)Main.timeForVisualEffects / 60f;
            Vector2 vector3 = posPlusVel - vector * 0.5f;
            Color color3 = Color.White * 0.5f * num2;
            color3.A = 0;
            Color color4 = color2 * num2;
            color4.A = 0;
            Color color5 = color2 * num2;
            color5.A = 0;
            Color color6 = color2 * num2;
            color6.A = 0;
            float num9 = vector.ToRotation();
            Main.EntitySpriteDraw(value2, vector3 - Main.screenPosition + vector2 + spinningpoint.RotatedBy((double)(6.28318548f * num8), default(Vector2)), new Rectangle?(rectangle), color4, num9 + 1.57079637f, origin2, 1.5f + num, 0, 0f);
            Main.EntitySpriteDraw(value2, vector3 - Main.screenPosition + vector2 + spinningpoint.RotatedBy((double)(6.28318548f * num8 + 2.09439516f), default(Vector2)), new Rectangle?(rectangle), color5, num9 + 1.57079637f, origin2, 1.1f + num, 0, 0f);
            Main.EntitySpriteDraw(value2, vector3 - Main.screenPosition + vector2 + spinningpoint.RotatedBy((double)(6.28318548f * num8 + 4.18879032f), default(Vector2)), new Rectangle?(rectangle), color6, num9 + 1.57079637f, origin2, 1.3f + num, 0, 0f);
            Vector2 vector4 = Projectile.Center;
            for (float num10 = 0f; num10 < 1f; num10 += 0.5f)
            {
                float num11 = num8 % 0.5f / 0.5f;
                num11 = (num11 + num10) % 1f;
                float num12 = num11 * 2f;
                if (num12 > 1f)
                {
                    num12 = 2f - num12;
                }
                Main.EntitySpriteDraw(value2, vector4 - Main.screenPosition + vector2, new Rectangle?(rectangle), color3 * num12, num9 + 1.57079637f, origin2, 0.3f + num11 * 0.5f, 0, 0f);
            }
            if (flag)
            {
                float rotation = Projectile.rotation + Projectile.localAI[1];
                float arg_571_0 = (float)Main.timeForVisualEffects / 240f;
                float arg_577_0 = Main.GlobalTimeWrappedHourly;
                float num13 = Main.GlobalTimeWrappedHourly;
                num13 %= 5f;
                num13 /= 2.5f;
                if (num13 >= 1f)
                {
                    num13 = 2f - num13;
                }
                num13 = num13 * 0.5f + 0.5f;
                Vector2 position = Projectile.Center - Main.screenPosition;
                Main.instance.LoadItem(75);
                Texture2D expr_5E1 = TextureAssets.Item[75].Value;
                Rectangle rectangle2 = expr_5E1.Frame(1, 8, 0, 0, 0, 0);
                Vector2 origin3 = rectangle2.Size() / 2f;
                Main.EntitySpriteDraw(expr_5E1, position, new Rectangle?(rectangle2), color, rotation, origin3, Projectile.scale, 0, 0f);
            }
            return false;
        }
    }
}

