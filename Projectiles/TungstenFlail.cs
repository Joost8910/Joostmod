using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class TungstenFlail : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tungsten Flail");
		}
		public override void SetDefaults()
		{
			Projectile.width = 34;
			Projectile.height = 34;
			Projectile.aiStyle = 15;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.timeLeft = 3600;
			Projectile.penetrate = -1;
            Projectile.usesIDStaticNPCImmunity = true;
			Projectile.idStaticNPCHitCooldown = 10;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = (int)(26 * Projectile.scale);
            height = (int)(26 * Projectile.scale);
            return true;
        }
        public override bool PreDrawExtras()
        {
            return false;
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            if (player.inventory[player.selectedItem].shoot == Projectile.type)
            {
                Projectile.scale = player.inventory[player.selectedItem].scale;
            }
            Projectile.width = (int)(34 * Projectile.scale);
            Projectile.height = (int)(34 * Projectile.scale);
            if (player.dead)
            {
                Projectile.Kill();
                return false;
            }
            player.itemAnimation = 10;
            player.itemTime = 10;
            if (Projectile.Center.X > player.Center.X)
            {
                Projectile.direction = 1;
            }
            else
            {
                Projectile.direction = -1;
            }
            Vector2 playerCenter = player.MountedCenter;
            Vector2 dir = player.DirectionTo(Projectile.Center);
            if (Main.myPlayer == Projectile.owner)
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
                    playerCenter += dir * 42;
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
                    if (dir.X != Projectile.velocity.X || dir.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                }
            }
            player.itemRotation = (float)Math.Atan2((double)(dir.Y * (float)player.direction), (double)(dir.X * (float)player.direction));
            float num207 = playerCenter.X - Projectile.Center.X;
            float num208 = playerCenter.Y - Projectile.Center.Y;
            float num209 = (float)Math.Sqrt((double)(num207 * num207 + num208 * num208));
            if (Projectile.ai[0] == 0f)
            {
                float num210 = 100f; //Distance before coming back
                Projectile.tileCollide = true;
                if (num209 > num210)
                {
                    Projectile.ai[0] = 1f;
                    Projectile.netUpdate = true;
                }
                else if (!player.channel)
                {
                    if (Projectile.velocity.Y < 0f)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y * 0.9f;
                    }
                    Projectile.velocity.Y = Projectile.velocity.Y + 1f;
                    Projectile.velocity.X = Projectile.velocity.X * 0.9f;
                }
            }
            else if (Projectile.ai[0] == 1f)
            {
                float num211 = 14f / player.GetAttackSpeed(DamageClass.Melee);
                float num212 = 0.9f / player.GetAttackSpeed(DamageClass.Melee);
                float num213 = 400f;
                float num214 = Math.Abs(num207);
                float num215 = Math.Abs(num208);
                if (Projectile.ai[1] == 1f)
                {
                    Projectile.tileCollide = false;
                }
                if (!player.channel || num209 > num213 || !Projectile.tileCollide || Projectile.timeLeft < 600)
                {
                    Projectile.ai[1] = 1f;
                    if (Projectile.tileCollide)
                    {
                        Projectile.netUpdate = true;
                    }
                    Projectile.tileCollide = false;
                    if (num209 < 20f)
                    {
                        Projectile.Kill();
                    }
                }
                if (!Projectile.tileCollide)
                {
                    num212 *= 2f;
                }
                int num216 = 40;
                if (num209 > (float)num216 || !Projectile.tileCollide)
                {
                    num209 = num211 / num209;
                    num207 *= num209;
                    num208 *= num209;
                    Vector2 vector21 = new Vector2(Projectile.velocity.X, Projectile.velocity.Y);
                    float num217 = num207 - Projectile.velocity.X;
                    float num218 = num208 - Projectile.velocity.Y;
                    float num219 = (float)Math.Sqrt((double)(num217 * num217 + num218 * num218));
                    num219 = num212 / num219;
                    num217 *= num219;
                    num218 *= num219;
                    Projectile.velocity.X = Projectile.velocity.X * 0.98f;
                    Projectile.velocity.Y = Projectile.velocity.Y * 0.98f;
                    Projectile.velocity.X = Projectile.velocity.X + num217;
                    Projectile.velocity.Y = Projectile.velocity.Y + num218;
                }
                else
                {
                    if (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) < 6f)
                    {
                        Projectile.velocity.X = Projectile.velocity.X * 0.96f;
                        Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
                    }
                    if (Main.player[Projectile.owner].velocity.X == 0f)
                    {
                        Projectile.velocity.X = Projectile.velocity.X * 0.96f;
                    }
                }
            }
            if (Projectile.velocity.X < 0f)
            {
                Projectile.rotation -= (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.01f;
                if (player.direction < 0)
                {
                    player.heldProj = Projectile.whoAmI;
                }
            }
            else
            {
                Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.01f;
                if (player.direction > 0)
                {
                    player.heldProj = Projectile.whoAmI;
                }
            }
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/Projectiles/TungstenFlail_Chain");
            Player player = Main.player[Projectile.owner];
            Vector2 position = Projectile.Center;
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
            float num1 = (float)texture.Height * Projectile.scale;
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
                    color2 = Projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, Projectile.scale, SpriteEffects.None, 0.0f);
                }
            }
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = Lighting.GetColor((int)(Projectile.Center.X / 16), (int)(Projectile.Center.Y / 16.0));
            spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, effects, 0f);
            return false;

        }
    }
}
