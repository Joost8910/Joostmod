using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
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
            projectile.width = 56;
            projectile.height = 56;
            projectile.aiStyle = 2;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = -1;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 6;
            projectile.timeLeft = 660;
            aiType = ProjectileID.Shuriken;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            height = 34;
            width = 34;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.ai[1] = 2;
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[1] != 2)
            {
                projectile.ai[0] = 0;
                projectile.ai[1] = 1;
                projectile.rotation = projectile.DirectionTo(target.Center).ToRotation();
            }
            projectile.velocity = target.velocity + projectile.DirectionTo(target.Center) * 0.5f;
            if (!target.noGravity && target.velocity.Y != 0)
            {
                projectile.velocity.Y += 0.3f;
            }
            if (projectile.timeLeft > 600)
            {
                projectile.timeLeft = 600;
            }
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (projectile.timeLeft == 660)
            {
                Main.PlaySound(SoundID.Item23, projectile.Center);
            }
            projectile.localAI[0]--;
            if (projectile.localAI[0] <= 0)
            {
                projectile.localAI[0] = 30;
                Main.PlaySound(SoundID.Item22, projectile.Center);
            }
            if (projectile.ai[1] == 0)
            {
                if (projectile.timeLeft <= 600)
                {
                    projectile.ai[1] = 1;
                }
            }
            if (projectile.ai[1] == 1)
            {
                if (projectile.ai[0] > 8 || projectile.timeLeft < 500 || projectile.Distance(player.Center) > 600)
                {
                    projectile.ai[1] = 2;
                }
                projectile.ai[0]++;
                if (projectile.timeLeft % 4 < 2)
                {
                    projectile.position += projectile.rotation.ToRotationVector2();
                }
                else
                {
                    projectile.position -= projectile.rotation.ToRotationVector2();
                }
                projectile.aiStyle = 0;
                aiType = 0;
            }
            if (projectile.ai[1] == 2)
            {
                projectile.aiStyle = 3;
                aiType = 0;
                if (projectile.timeLeft < 360)
                {
                    projectile.velocity = projectile.DirectionTo(player.Center) * ((540 - projectile.timeLeft) / 20);
                }
            }
            if (projectile.Distance(player.Center) > 720)
            {
                projectile.position += projectile.DirectionTo(player.Center) * (projectile.Distance(player.Center) - 720);
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = ModContent.GetTexture("JoostMod/Projectiles/Adamantite_Chain");

            Vector2 position = projectile.Center;
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
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
                    Microsoft.Xna.Framework.Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }

            return true;
        }

    }
}
