using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class TailWhip : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tail Whip");
		}
		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
            Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.timeLeft = 600;
			Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Venom, 600);
            Projectile.ai[1] += 5;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Venom, 600);
            Projectile.ai[1] += 5;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
            Projectile.ai[1] += 10;
            return false;
        }
        /*public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            projectile.rotation = projectile.DirectionFrom(player.Center).ToRotation() + 1.57f;
            player.itemAnimation = 5;
            player.itemTime = 5;
            if (player.dead)
            {
                projectile.Kill();
                return false;
            }
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
                    playerCenter += dir * 72;
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
                num210 *= 1f;
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
                num211 *= 1f; // Floatiness?
                num212 *= 20f; // Comeback Speed
                float num214 = Math.Abs(num207);
                float num215 = Math.Abs(num208);
                if (projectile.ai[1] == 1f)
                {
                    projectile.tileCollide = false;
                }
                if (!player.channel || num209 > num213 || !projectile.tileCollide || projectile.timeLeft < 300)
                {
                    projectile.ai[1] = 1f;
                    if (projectile.tileCollide)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.tileCollide = false;
                    if (num209 < 16f)
                    {
                        projectile.Kill();
                    }
                }
                if (!projectile.tileCollide)
                {
                    num212 *= 2f;
                }
                int num216 = 70;
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
                    Vector2 vel = projectile.velocity;
                    vel.X = vel.X + num217;
                    vel.Y = vel.Y + num218;
                    if (projectile.tileCollide)
                    {
                        vel.Normalize();
                        projectile.velocity = vel * 20f / player.meleeSpeed;
                    }
                    else
                    {
                        projectile.velocity = vel;
                    }
                }
            }
            return false;
        }*/
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 playerCenter = player.MountedCenter;
            if (Projectile.Center.X > player.Center.X)
            {
                player.direction = 1;
            }
            if (Projectile.Center.X < player.Center.X)
            {
                player.direction = -1;
            }
            if (player.itemAnimation < 2)
            {
                player.itemAnimation = 2;
                player.itemTime = 2;
            }
            Projectile.ai[0]++;
            Projectile.ai[1]++;
            bool channeling = player.channel && !player.noItems && !player.CCed;
            if (channeling)
            {
                if (Main.myPlayer == Projectile.owner)
                {
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
                }
            }
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
        
            Projectile.rotation = Projectile.DirectionFrom(playerCenter).ToRotation() + 1.57f;
            if (Projectile.ai[1] > 4)
            {
                if (Projectile.ai[1] > 30 || !channeling)
                {
                    Projectile.velocity = Projectile.DirectionTo(player.MountedCenter) * 20 + player.velocity;
                    Projectile.tileCollide = false;
                }
                else if (Projectile.owner == Main.myPlayer && (Projectile.ai[1] % 5 == 0) && channeling)
                {
                    Projectile.velocity = Projectile.DirectionTo(Main.MouseWorld) * 20 + player.velocity;
                    Projectile.netUpdate = true;
                }
                if (Projectile.Distance(player.Center) < 16)
                {
                    Projectile.Kill();
                }
            }
        }
        public override bool PreDrawExtras()
        {
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/Projectiles/TailWhip_Chain");

            Vector2 position = Projectile.Center;
            Player player = Main.player[Projectile.owner];
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
                    color2 = Projectile.GetAlpha(color2);
                    Main.EntitySpriteDraw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0);
                }
            }

            return true;
        }
}
}
