using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class TerraFury : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Fury");
        }
        public override void SetDefaults()
        {
            projectile.width = 42;
            projectile.height = 42;
            projectile.aiStyle = 15;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.timeLeft = 9600;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 30;
            height = 30;
            return true;
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            if (player.dead)
            {
                projectile.Kill();
                return false;
            }
            player.itemAnimation = 10;
            player.itemTime = 10;
            if (projectile.Center.X > player.Center.X)
            {
                projectile.direction = 1;
            }
            else
            {
                projectile.direction = -1;
            }
            Vector2 playerCenter = player.MountedCenter;
            Vector2 dir = player.DirectionTo(projectile.Center);
            if (Main.myPlayer == projectile.owner)
            {
                bool channeling = player.channel && !player.noItems && !player.CCed;
                if (channeling)
                {
                    dir = Main.MouseWorld - playerCenter;
                    dir.Normalize();
                    if (dir.HasNaNs())
                    {
                        dir = Vector2.UnitX * (float)player.direction;
                    }
                    playerCenter += dir * 122;
                    if (Main.MouseWorld.X > player.Center.X)
                    {
                        if (player.direction < 0 && ((Main.MouseWorld.Y < player.position.Y + player.height && player.gravDir > 0) || (Main.MouseWorld.Y > player.position.Y && player.gravDir < 0)))
                        {
                            player.bodyFrame.Y = player.bodyFrame.Height;
                        }
                    }
                    else
                    {
                        if (player.direction > 0 && ((Main.MouseWorld.Y < player.position.Y + player.height && player.gravDir > 0) || (Main.MouseWorld.Y > player.position.Y && player.gravDir < 0)))
                        {
                            player.bodyFrame.Y = player.bodyFrame.Height;
                        }
                    }
                    if (dir.X != projectile.velocity.X || dir.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                }
            }
            player.itemRotation = (float)Math.Atan2((double)(dir.Y * (float)player.direction), (double)(dir.X * (float)player.direction));
            float num207 = playerCenter.X - projectile.Center.X;
            float num208 = playerCenter.Y - projectile.Center.Y;
            float num209 = (float)Math.Sqrt((double)(num207 * num207 + num208 * num208));
            if (projectile.ai[0] == 0f)
            {
                float num210 = 160f; //Distance before coming back
                num210 *= 1.5f;
                projectile.tileCollide = true;
                if (num209 > num210)
                {
                    projectile.ai[0] = 1f;
                    projectile.netUpdate = true;
                }
                else if (!player.channel)
                {
                    if (projectile.velocity.Y < 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y * 0.9f;
                    }
                    projectile.velocity.Y = projectile.velocity.Y + 1f;
                    projectile.velocity.X = projectile.velocity.X * 0.9f;
                }
            }
            else if (projectile.ai[0] == 1f)
            {
                float num211 = 14f / player.meleeSpeed;
                float num212 = 0.9f / player.meleeSpeed;
                float num213 = 300f;
                num213 *= 5f; // Distance when it forces back
                num211 *= 1.5f; // Floatiness?
                num212 *= 2.5f; // Comeback Speed
                float num214 = Math.Abs(num207);
                float num215 = Math.Abs(num208);
                if (projectile.ai[1] == 1f)
                {
                    projectile.tileCollide = false;
                }
                if (!player.channel || num209 > num213 || !projectile.tileCollide || projectile.timeLeft < 600)
                {
                    projectile.ai[1] = 1f;
                    if (projectile.tileCollide)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.tileCollide = false;
                    if (num209 < 20f)
                    {
                        projectile.Kill();
                    }
                }
                if (!projectile.tileCollide)
                {
                    num212 *= 2f;
                }
                int num216 = 120; //Distance before coming back
                if (num209 > (float)num216 || !projectile.tileCollide)
                {
                    num209 = num211 / num209;
                    num207 *= num209;
                    num208 *= num209;
                    Vector2 vector21 = new Vector2(projectile.velocity.X, projectile.velocity.Y);
                    float num217 = num207 - projectile.velocity.X;
                    float num218 = num208 - projectile.velocity.Y;
                    float num219 = (float)Math.Sqrt((double)(num217 * num217 + num218 * num218));
                    num219 = num212 / num219;
                    num217 *= num219;
                    num218 *= num219;
                    projectile.velocity.X = projectile.velocity.X * 0.98f;
                    projectile.velocity.Y = projectile.velocity.Y * 0.98f;
                    projectile.velocity.X = projectile.velocity.X + num217;
                    projectile.velocity.Y = projectile.velocity.Y + num218;
                    if (projectile.localAI[1] <= 0)
                    {
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("TerraFuryBeam"), (int)(projectile.damage * 1.0f), 0, projectile.owner);
                        projectile.localAI[1] = 20;
                    }
                    else
                    {
                        projectile.localAI[1]--;
                    }
                }
                else
                {
                    if (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y) < 6f)
                    {
                        projectile.velocity.X = projectile.velocity.X * 0.96f;
                        projectile.velocity.Y = projectile.velocity.Y + 0.2f;
                    }
                    if (Main.player[projectile.owner].velocity.X == 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X * 0.96f;
                    }
                }
            }
            if (projectile.velocity.X < 0f)
            {
                projectile.rotation -= (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 0.01f;
            }
            else
            {
                projectile.rotation += (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 0.01f;
            }
            return false;
        }
        public override bool PreDrawExtras(SpriteBatch spriteBatch)
        {
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = ModContent.GetTexture("JoostMod/Projectiles/TerraFury_Chain");
            Player player = Main.player[projectile.owner];
            Vector2 position = projectile.Center;
            Vector2 playerCenter = player.MountedCenter;
            if (player.bodyFrame.Y == player.bodyFrame.Height * 3)
            {
                playerCenter.X += 8 * player.direction;
                playerCenter.Y += 2 * player.gravDir;
            }
            else if (player.bodyFrame.Y == player.bodyFrame.Height * 2)
            {
                playerCenter.X += 6 * player.direction;
                playerCenter.Y += -12 * player.gravDir;
            }
            else if (player.bodyFrame.Y == player.bodyFrame.Height * 4)
            {
                playerCenter.X += 6 * player.direction;
                playerCenter.Y += 8 * player.gravDir;
            }
            else if (player.bodyFrame.Y == player.bodyFrame.Height)
            {
                playerCenter.X += -10 * player.direction;
                playerCenter.Y += -14 * player.gravDir;
            }
            Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            float num1 = (float)texture.Height;
            Vector2 vector2_4 = playerCenter - position;
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
                    vector2_4 = playerCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }
            return true;
        }
    }
}

