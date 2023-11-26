using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class AdamantiteChainedchainsaw : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Adamantite Chained-Chainsaw");
        }
        public override void SetDefaults()
        {
            Projectile.width = 56;
            Projectile.height = 56;
            Projectile.aiStyle = 2;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = -1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 6;
            Projectile.timeLeft = 660;
            AIType = ProjectileID.Shuriken;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            height = 34;
            width = 34;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[1] = 2;
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.ai[1] != 2)
            {
                Projectile.ai[0] = 0;
                Projectile.ai[1] = 1;
                Projectile.rotation = Projectile.DirectionTo(target.Center).ToRotation();
            }
            Projectile.velocity = target.velocity + Projectile.DirectionTo(target.Center) * 0.5f;
            if (!target.noGravity && target.velocity.Y != 0)
            {
                Projectile.velocity.Y += 0.3f;
            }
            if (Projectile.timeLeft > 600)
            {
                Projectile.timeLeft = 600;
            }
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.timeLeft == 660)
            {
                SoundEngine.PlaySound(SoundID.Item23, Projectile.Center);
            }
            Projectile.localAI[0]--;
            if (Projectile.localAI[0] <= 0)
            {
                Projectile.localAI[0] = 30;
                SoundEngine.PlaySound(SoundID.Item22, Projectile.Center);
            }
            if (Projectile.ai[1] == 0)
            {
                if (Projectile.timeLeft <= 600)
                {
                    Projectile.ai[1] = 1;
                }
            }
            if (Projectile.ai[1] == 1)
            {
                if (Projectile.ai[0] > 8 || Projectile.timeLeft < 500 || Projectile.Distance(player.Center) > 600)
                {
                    Projectile.ai[1] = 2;
                }
                Projectile.ai[0]++;
                if (Projectile.timeLeft % 4 < 2)
                {
                    Projectile.position += Projectile.rotation.ToRotationVector2();
                }
                else
                {
                    Projectile.position -= Projectile.rotation.ToRotationVector2();
                }
                Projectile.aiStyle = 0;
                AIType = 0;
            }
            if (Projectile.ai[1] == 2)
            {
                Projectile.aiStyle = 3;
                AIType = 0;
                if (Projectile.timeLeft < 360)
                {
                    Projectile.velocity = Projectile.DirectionTo(player.Center) * ((540 - Projectile.timeLeft) / 20);
                }
            }
            if (Projectile.Distance(player.Center) > 720)
            {
                Projectile.position += Projectile.DirectionTo(player.Center) * (Projectile.Distance(player.Center) - 720);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/Projectiles/Adamantite_Chain");

            Vector2 position = Projectile.Center;
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
            Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            float num1 = (float)texture.Height;
            Vector2 vector2_4 = mountedCenter - position;
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
                    vector2_4 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = Projectile.GetAlpha(color2);
                    Main.EntitySpriteDraw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0);
                }
            }

            return true;
        }

    }
}
