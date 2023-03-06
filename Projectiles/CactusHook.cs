using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
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
            Projectile.CloneDefaults(ProjectileID.GemHookAmethyst);
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.light = 0.2f;
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
            return 250f;
        }

        public override void NumGrappleHooks(Player player, ref int numHooks)
        {
            numHooks = 1;
        }
        
        public override bool PreAI()
        {
            if (vel == Vector2.Zero)
            {
                vel = Projectile.velocity * 0.5f;
            }
            Player player = Main.player[Projectile.owner];
            if (player.dead || (Vector2.Distance(player.Center, Projectile.Center) > GrappleRange() && !isHooked))
            {
                retreat = true;
                isHooked = false;
                canGrab = false;
            }
            if (Vector2.Distance(player.Center, Projectile.Center) > GrappleRange() * 3)
            {
                Projectile.Kill();
            }
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
            Vector2 vector6 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
            float num69 = mountedCenter.X - vector6.X;
            float num70 = mountedCenter.Y - vector6.Y;
            float num71 = (float)Math.Sqrt((double)(num69 * num69 + num70 * num70));
            Projectile.rotation = (float)Math.Atan2((double)num70, (double)num69) - 1.57f;
            Projectile.direction = (Projectile.rotation < 0 && Projectile.rotation > -3.14f) ? -1 : 1;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.soundDelay--;
            if (retreat)
            {
                Projectile.velocity = Projectile.DirectionTo(player.Center) * retreatSpeed;
                canGrab = player.releaseHook;
                if (Vector2.Distance(player.Center, Projectile.Center) < 16 && !isHooked)
                {
                    Projectile.Kill();
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
                Projectile.ai[0] = 2f;
                Projectile.velocity = default(Vector2);
                Projectile.timeLeft = 2;
                int num124 = (int)(Projectile.position.X / 16f) - 1;
                int num125 = (int)((Projectile.position.X + (float)Projectile.width) / 16f) + 2;
                int num126 = (int)(Projectile.position.Y / 16f) - 1;
                int num127 = (int)((Projectile.position.Y + (float)Projectile.height) / 16f) + 2;
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
                        if (Projectile.position.X + (float)(Projectile.width / 2) > vector9.X && Projectile.position.X + (float)(Projectile.width / 2) < vector9.X + 16f && Projectile.position.Y + (float)(Projectile.height / 2) > vector9.Y && Projectile.position.Y + (float)(Projectile.height / 2) < vector9.Y + 16f && Main.tile[num128, num129].HasUnactuatedTile && (Main.tileSolid[(int)Main.tile[num128, num129].TileType] || Main.tile[num128, num129].TileType == 314))
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
                player.velocity = player.DirectionTo(Projectile.Center) * pullSpeed;
                if (Math.Abs(player.Center.X - Projectile.Center.X) < 8)
                {
                    player.velocity.X = 0;
                    player.position.X = Projectile.Center.X - player.width / 2;
                }
                if (Math.Abs(player.Center.Y - Projectile.Center.Y) < 8)
                {
                    player.velocity.Y = 0;
                    player.position.Y = Projectile.Center.Y - player.height / 2;
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
                if (Vector2.Distance(player.Center, Projectile.Center) > 30)
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
                        pullTime = (int)(Vector2.Distance(player.Center, Projectile.Center) / pullSpeed);
                    }
                    player.position = (Projectile.Center + (Projectile.DirectionTo(player.Center) * pullTime * pullSpeed)) - player.Size / 2;
                    if (Projectile.soundDelay <= 0 && Collision.SolidCollision(player.position, player.width, player.height))
                    {
                        Projectile.soundDelay = 20;
                        SoundEngine.PlaySound(SoundID.WormDig, new Vector2(player.Center.X, Projectile.Center.Y));
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
                    Projectile.Kill();
                    player.wingTime = (float)player.wingTimeMax;
                    if (player.hasJumpOption_Cloud)
                    {
                        player.jumpAgainCloud = true;
                    }
                    if (player.hasJumpOption_Sandstorm)
                    {
                        player.jumpAgainSandstorm = true;
                    }
                    if (player.hasJumpOption_Blizzard)
                    {
                        player.jumpAgainBlizzard = true;
                    }
                    if (player.hasJumpOption_Fart)
                    {
                        player.jumpAgainFart = true;
                    }
                    if (player.hasJumpOption_Sail)
                    {
                        player.jumpAgainSail = true;
                    }
                    if (player.hasJumpOption_Unicorn)
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
                Projectile.ai[0] = 0f;
                int num111 = (int)(Projectile.position.X / 16f) - 1;
                int num112 = (int)((Projectile.position.X + (float)Projectile.width) / 16f) + 2;
                int num113 = (int)(Projectile.position.Y / 16f) - 1;
                int num114 = (int)((Projectile.position.Y + (float)Projectile.height) / 16f) + 2;
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
                    Projectile.velocity = vel * 3;
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
                        if (Projectile.position.X + (float)Projectile.width > vector8.X && Projectile.position.X < vector8.X + 16f && Projectile.position.Y + (float)Projectile.height > vector8.Y && Projectile.position.Y < vector8.Y + 16f && Main.tile[num115, num116].HasUnactuatedTile && (Main.tileSolid[(int)Main.tile[num115, num116].TileType] || Main.tile[num115, num116].TileType == 314))
                        {
                            if (!retreat)
                            {
                                Projectile.velocity = vel;
                            }
                            if (canGrab && !player.controlHook)
                            {
                                Projectile.velocity.X = 0f;
                                Projectile.velocity.Y = 0f;
                                SoundEngine.PlaySound(SoundID.Dig, new Vector2(num115 * 16, num116 * 16));
                                isHooked = true;
                                Projectile.position.X = (float)(num115 * 16 + 8 - Projectile.width / 2);
                                Projectile.position.Y = (float)(num116 * 16 + 8 - Projectile.height / 2);
                                Projectile.netUpdate = true;
                            }
                            else
                            {
                                if (Projectile.soundDelay <= 0)
                                {
                                    Projectile.soundDelay = 20;
                                    SoundEngine.PlaySound(SoundID.WormDig, Projectile.position);
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
        public override bool PreDrawExtras()
        {
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
            Vector2 vector14 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
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
                    if (Projectile.spriteDirection == -1)
                    {
                        effects = SpriteEffects.FlipHorizontally;
                    }
                    Main.spriteBatch.Draw(Mod.GetTexture("Projectiles/CactusHookChain"), new Vector2(vector14.X - Main.screenPosition.X, vector14.Y - Main.screenPosition.Y), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, TextureAssets.Chain30.Value.Width, TextureAssets.Chain30.Value.Height)), color15, rotation13, new Vector2((float)TextureAssets.Chain30.Value.Width * 0.5f, (float)TextureAssets.Chain30.Value.Height * 0.5f), 1f, effects, 0f);
                }
            }
            return true;
        }
    }
}

