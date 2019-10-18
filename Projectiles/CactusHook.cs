using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class CactusHook : ModProjectile
    {
        private bool isHooked;
        private bool canGrab = true;
        private bool jump;
        private bool retreat;
        private float pullTime = 0;
        private float pullSpeed = 9.5f;
        private float retreatSpeed = 13;
        private Vector2 vel = Vector2.Zero;
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.GemHookAmethyst);
            projectile.width = 16;
            projectile.height = 16;
            projectile.light = 0.2f;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(isHooked);
            writer.Write(canGrab);
            writer.Write(retreat);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            isHooked = reader.ReadBoolean();
            canGrab = reader.ReadBoolean();
            retreat = reader.ReadBoolean();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactus Worm Hook");
        }

        public override bool? SingleGrappleHook(Player player)
        {
            return true;
        }
        
        public override float GrappleRange()
        {
            return 420f;
        }

        public override void NumGrappleHooks(Player player, ref int numHooks)
        {
            numHooks = 1;
        }
        
        public override bool PreAI()
        {
            if (vel == Vector2.Zero)
            {
                vel = projectile.velocity * 0.5f;
            }
            Player player = Main.player[projectile.owner];
            if (player.dead || (Vector2.Distance(player.Center, projectile.Center) > GrappleRange() && !isHooked))
            {
                retreat = true;
                isHooked = false;
                canGrab = false;
            }
            if (Vector2.Distance(player.Center, projectile.Center) > GrappleRange() * 3)
            {
                projectile.Kill();
            }
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Vector2 vector6 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
            float num69 = mountedCenter.X - vector6.X;
            float num70 = mountedCenter.Y - vector6.Y;
            float num71 = (float)Math.Sqrt((double)(num69 * num69 + num70 * num70));
            projectile.rotation = (float)Math.Atan2((double)num70, (double)num69) - 1.57f;
            projectile.direction = (projectile.rotation < 0 && projectile.rotation > -3.14f) ? -1 : 1;
            projectile.spriteDirection = projectile.direction;
            projectile.soundDelay--;
            if (retreat)
            {
                projectile.velocity = projectile.DirectionTo(player.Center) * retreatSpeed;
                canGrab = player.releaseHook;
                if (Vector2.Distance(player.Center, projectile.Center) < 16 && !isHooked)
                {
                    projectile.Kill();
                }
            }
            if (isHooked)
            {
                player.GetModPlayer<JoostPlayer>().hoverBootsTimer = 0;
                player.rocketTime = player.rocketTimeMax;
                player.rocketDelay = 0;
                player.rocketFrame = false;
                player.canRocket = false;
                player.rocketRelease = false;
                player.fallStart = (int)(player.Center.Y / 16f);
                player.sandStorm = false;
                player.wingTime = 0;
                projectile.ai[0] = 2f;
                projectile.velocity = default(Vector2);
                projectile.timeLeft = 2;
                int num124 = (int)(projectile.position.X / 16f) - 1;
                int num125 = (int)((projectile.position.X + (float)projectile.width) / 16f) + 2;
                int num126 = (int)(projectile.position.Y / 16f) - 1;
                int num127 = (int)((projectile.position.Y + (float)projectile.height) / 16f) + 2;
                if (num124 < 0)
                {
                    num124 = 0;
                }
                if (num125 > Main.maxTilesX)
                {
                    num125 = Main.maxTilesX;
                }
                if (num126 < 0)
                {
                    num126 = 0;
                }
                if (num127 > Main.maxTilesY)
                {
                    num127 = Main.maxTilesY;
                }
                bool flag3 = true;
                for (int num128 = num124; num128 < num125; num128++)
                {
                    for (int num129 = num126; num129 < num127; num129++)
                    {
                        if (Main.tile[num128, num129] == null)
                        {
                            Main.tile[num128, num129] = new Tile();
                        }
                        Vector2 vector9;
                        vector9.X = (float)(num128 * 16);
                        vector9.Y = (float)(num129 * 16);
                        if (projectile.position.X + (float)(projectile.width / 2) > vector9.X && projectile.position.X + (float)(projectile.width / 2) < vector9.X + 16f && projectile.position.Y + (float)(projectile.height / 2) > vector9.Y && projectile.position.Y + (float)(projectile.height / 2) < vector9.Y + 16f && Main.tile[num128, num129].nactive() && (Main.tileSolid[(int)Main.tile[num128, num129].type] || Main.tile[num128, num129].type == 314))
                        {
                            flag3 = false;
                        }
                    }
                }
                if (flag3)
                {
                    isHooked = false;
                }
                retreat = false;
                player.velocity = player.DirectionTo(projectile.Center) * pullSpeed;
                if (Math.Abs(player.Center.X - projectile.Center.X) < 8)
                {
                    player.velocity.X = 0;
                    player.position.X = projectile.Center.X - player.width / 2;
                }
                if (Math.Abs(player.Center.Y - projectile.Center.Y) < 8)
                {
                    player.velocity.Y = 0;
                    player.position.Y = projectile.Center.Y - player.height / 2;
                }
                if (player.itemAnimation == 0)
                {
                    if (player.velocity.X > 0)
                    {
                        player.direction = 1;
                    }
                    if (player.velocity.X < 0)
                    {
                        player.direction = -1;
                    }
                }
                if (Vector2.Distance(player.Center, projectile.Center) > 30)
                {
                    /*player.position += player.velocity;
                    bool slope = false;
                    for (int i = (int)(player.position.X/16)-1; i < (int)((player.position.X+player.width)/16)+1;i++)
                    {
                        for (int j = (int)(player.position.Y / 16)-1; j < (int)((player.position.Y + player.height) / 16)+1; j++)
                        {
                            if (Main.tile[i,j].slope() > 0)
                            {
                                slope = true;
                                break;
                            }
                        }
                    }
                    if (slope)
                    {
                        player.position += player.velocity * 4;
                    }*/
                    if (Collision.SolidCollision(player.position, player.width, player.height))
                    {
                        pullTime -= 0.5f;
                    }
                    else
                    {
                        pullTime--;
                    }
                    if (pullTime <= 0)
                    {
                        pullTime = (int)(Vector2.Distance(player.Center, projectile.Center) / pullSpeed);
                    }
                    player.position = (projectile.Center + (projectile.DirectionTo(player.Center) * pullTime * pullSpeed)) - player.Size / 2;
                    if (projectile.soundDelay <= 0 && Collision.SolidCollision(player.position, player.width, player.height))
                    {
                        projectile.soundDelay = 20;
                        Main.PlaySound(15, (int)player.Center.X, (int)projectile.Center.Y, 1);
                    }
                }
                else
                {
                    pullTime = 0;
                }
                if (player.releaseJump)
                {
                    jump = true;
                }

                if (player.controlJump && jump)
                {
                    projectile.Kill();
                    player.wingTime = (float)player.wingTimeMax;
                    if (player.doubleJumpCloud)
                    {
                        player.jumpAgainCloud = true;
                    }
                    if (player.doubleJumpSandstorm)
                    {
                        player.jumpAgainSandstorm = true;
                    }
                    if (player.doubleJumpBlizzard)
                    {
                        player.jumpAgainBlizzard = true;
                    }
                    if (player.doubleJumpFart)
                    {
                        player.jumpAgainFart = true;
                    }
                    if (player.doubleJumpSail)
                    {
                        player.jumpAgainSail = true;
                    }
                    if (player.doubleJumpUnicorn)
                    {
                        player.jumpAgainUnicorn = true;
                    }
                    if (!player.controlDown && player.velocity.Y > -Player.jumpSpeed)
                    {
                        player.velocity.Y -= Player.jumpSpeed;
                        player.jump = Player.jumpHeight / 2;
                    }
                    player.grapCount = 0;
                    return false;
                }
            }
            else
            {
                projectile.ai[0] = 0f;
                int num111 = (int)(projectile.position.X / 16f) - 1;
                int num112 = (int)((projectile.position.X + (float)projectile.width) / 16f) + 2;
                int num113 = (int)(projectile.position.Y / 16f) - 1;
                int num114 = (int)((projectile.position.Y + (float)projectile.height) / 16f) + 2;
                if (num111 < 0)
                {
                    num111 = 0;
                }
                if (num112 > Main.maxTilesX)
                {
                    num112 = Main.maxTilesX;
                }
                if (num113 < 0)
                {
                    num113 = 0;
                }
                if (num114 > Main.maxTilesY)
                {
                    num114 = Main.maxTilesY;
                }
                if (!retreat)
                {
                    projectile.velocity = vel * 3;
                }
                for (int num115 = num111; num115 < num112; num115++)
                {
                    int num116 = num113;
                    while (num116 < num114)
                    {
                        if (Main.tile[num115, num116] == null)
                        {
                            Main.tile[num115, num116] = new Tile();
                        }
                        Vector2 vector8;
                        vector8.X = (float)(num115 * 16);
                        vector8.Y = (float)(num116 * 16);
                        if (projectile.position.X + (float)projectile.width > vector8.X && projectile.position.X < vector8.X + 16f && projectile.position.Y + (float)projectile.height > vector8.Y && projectile.position.Y < vector8.Y + 16f && Main.tile[num115, num116].nactive() && (Main.tileSolid[(int)Main.tile[num115, num116].type] || Main.tile[num115, num116].type == 314))
                        {
                            if (!retreat)
                            {
                                projectile.velocity = vel;
                            }
                            if (canGrab && !player.controlHook)
                            {
                                projectile.velocity.X = 0f;
                                projectile.velocity.Y = 0f;
                                Main.PlaySound(0, num115 * 16, num116 * 16, 1, 1f, 0f);
                                isHooked = true;
                                projectile.position.X = (float)(num115 * 16 + 8 - projectile.width / 2);
                                projectile.position.Y = (float)(num116 * 16 + 8 - projectile.height / 2);
                                projectile.netUpdate = true;
                            }
                            else
                            {
                                if (projectile.soundDelay <= 0)
                                {
                                    projectile.soundDelay = 20;
                                    Main.PlaySound(15, (int)projectile.position.X, (int)projectile.position.Y, 1);
                                }
                            }
                            break;
                        }
                        else
                        {
                            num116++;
                        }
                    }
                    if (isHooked)
                    {
                        break;
                    }
                }

            }

            return false;
        }
        public override bool PreDrawExtras(SpriteBatch spriteBatch)
        {
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Vector2 vector14 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
            float num84 = mountedCenter.X - vector14.X;
            float num85 = mountedCenter.Y - vector14.Y;
            float rotation13 = (float)Math.Atan2((double)num85, (double)num84) - 1.57f;
            bool flag11 = true;
            while (flag11)
            {
                float num86 = (float)Math.Sqrt((double)(num84 * num84 + num85 * num85));
                if (num86 < 30f)
                {
                    flag11 = false;
                }
                else if (float.IsNaN(num86))
                {
                    flag11 = false;
                }
                else
                {
                    num86 = 24f / num86;
                    num84 *= num86;
                    num85 *= num86;
                    vector14.X += num84;
                    vector14.Y += num85;
                    num84 = mountedCenter.X - vector14.X;
                    num85 = mountedCenter.Y - vector14.Y;
                    Color color15 = Lighting.GetColor((int)vector14.X / 16, (int)(vector14.Y / 16f));
                    SpriteEffects effects = SpriteEffects.None;
                    if (projectile.spriteDirection == -1)
                    {
                        effects = SpriteEffects.FlipHorizontally;
                    }
                    Main.spriteBatch.Draw(mod.GetTexture("Projectiles/CactusHookChain"), new Vector2(vector14.X - Main.screenPosition.X, vector14.Y - Main.screenPosition.Y), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, Main.chain30Texture.Width, Main.chain30Texture.Height)), color15, rotation13, new Vector2((float)Main.chain30Texture.Width * 0.5f, (float)Main.chain30Texture.Height * 0.5f), 1f, effects, 0f);
                }
            }
            return true;
        }
    }
}

