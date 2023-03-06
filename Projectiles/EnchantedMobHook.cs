using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class EnchantedMobHook : ModProjectile
    {
        private float grappleRotation = 0f;
        private bool isHooked;
        private int grappleSwing = -1;
        private float maxDist;
        private int jump = 0;
        private bool retreat;
        private float controlSpeed = 0;
        private float wingTime = -1;
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.GemHookAmethyst);
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.light = 0.15f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enchanted Grabby Swingy Hook");
        }

        public override bool? SingleGrappleHook(Player player)
        {
            return true;
        }
        
        public override void UseGrapple(Player player, ref int type)
        {
            int hooksOut = 0;
            int oldestHookIndex = -1;
            int oldestHookTimeLeft = 100000;
            for (int i = 0; i < 1000; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Projectile.whoAmI && Main.projectile[i].type == Projectile.type)
                {
                    hooksOut++;
                    if (Main.projectile[i].timeLeft < oldestHookTimeLeft)
                    {
                        oldestHookIndex = i;
                        oldestHookTimeLeft = Main.projectile[i].timeLeft;
                    }
                }
            }
            if (hooksOut > 1)
            {
                Main.projectile[oldestHookIndex].Kill();
            }
        }
        
        public override float GrappleRange()
        {
            return 600f;
        }

        public override void NumGrappleHooks(Player player, ref int numHooks)
        {
            numHooks = 1;
        }

        public override void GrappleRetreatSpeed(Player player, ref float speed)
        {
            speed = 40f;
        }
        int hitMob = -1;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(hitMob);
            writer.Write(isHooked);
            writer.Write((short)grappleSwing);
            writer.Write(retreat);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            hitMob = reader.ReadInt16();
            isHooked = reader.ReadBoolean();
            grappleSwing = reader.ReadInt16();
            retreat = reader.ReadBoolean();
        }
        public override bool PreAI()
        {
            return false;
        }
        public override void PostAI()
        {
            Player player = Main.player[Projectile.owner];
            if (Vector2.Distance(player.Center, Projectile.Center) > (isHooked ? 520 : 420))
            {
                retreat = true;
                isHooked = false;
            }
            if (!player.active || player.dead || Vector2.Distance(player.Center, Projectile.Center) > 1000)
            {
                Projectile.Kill();
            }
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
            Vector2 vector6 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
            float num69 = mountedCenter.X - vector6.X;
            float num70 = mountedCenter.Y - vector6.Y;
            float num71 = (float)Math.Sqrt((double)(num69 * num69 + num70 * num70));
            Projectile.rotation = (float)Math.Atan2((double)num70, (double)num69) - 1.57f;
            /*if (!retreat && !isHooked)
            {
                projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
                projectile.velocity.Y = projectile.velocity.Y + 0.3f;
                if (projectile.velocity.Y > 15f)
                {
                    projectile.velocity.Y = 15f;
                }
            }*/
            if (retreat) 
            {
                Projectile.velocity = Projectile.DirectionTo(player.Center) * 18;
                if (Vector2.Distance(player.Center, Projectile.Center) < 16 && !isHooked)
                {
                    Projectile.Kill();
                }
            }
            if (isHooked)
            {
                bool flag3 = true;
                Projectile.ai[0] = 2f;
                Projectile.timeLeft = 2;
                if (hitMob >= 0)
                {
                    NPC npc = Main.npc[hitMob];
                    Projectile.velocity = npc.velocity;
                    Projectile.position = npc.Center - (Projectile.Size / 2);
                    flag3 = false;
                    if (!npc.active || npc.life <= 0 || npc.friendly)
                    {
                        hitMob = -1;
                        flag3 = true;
                    }
                    Projectile.netUpdate = true;
                }
                else
                {
                    Projectile.velocity = default(Vector2);
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
                    flag3 = true;
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
                }
                if (flag3)
                {
                    isHooked = false;
                }
                else
                {
                    grappleSwing = Projectile.whoAmI;
                }
            }
            else if (!retreat)
            {
                Projectile.ai[0] = 0f;
                if (hitMob < 0)
                {
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (Projectile.getRect().Intersects(npc.getRect()) && !npc.friendly && npc.active)
                        {
                            hitMob = i;
                            maxDist = Vector2.Distance(player.Center, Projectile.Center);
                            grappleSwing = Projectile.whoAmI;
                            isHooked = true;
                            SoundEngine.PlaySound(npc.HitSound, Projectile.Center);
                            Projectile.netUpdate = true;
                            break;
                        }
                    }
                }
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
                            maxDist = Vector2.Distance(player.Center, Projectile.Center);
                            grappleSwing = Projectile.whoAmI;
                            Projectile.velocity.X = 0f;
                            Projectile.velocity.Y = 0f;
                            SoundEngine.PlaySound(SoundID.Dig, new Vector2(num115 * 16, num116 * 16));
                            isHooked = true;
                            Projectile.position.X = (float)(num115 * 16 + 8 - Projectile.width / 2);
                            Projectile.position.Y = (float)(num116 * 16 + 8 - Projectile.height / 2);
                            Projectile.damage = 0;
                            Projectile.netUpdate = true;
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
            if (Main.myPlayer == Projectile.owner)
            {
                int num117 = 0;
                int num118 = -1;
                int num119 = 100000;
                int num121 = 1;
                for (int num122 = 0; num122 < 1000; num122++)
                {
                    if (Main.projectile[num122].timeLeft < num119)
                    {
                        num118 = num122;
                        num119 = Main.projectile[num122].timeLeft;
                    }
                    num117++;
                }
                if (num117 > num121)
                {
                    Main.projectile[num118].Kill();
                }
            }
            if (grappleSwing >= 0)
            {
                if (player.mount.Active)
                {
                    player.mount.Dismount(player);
                }
                float targetrotation = (float)Math.Atan2(((Projectile.Center.Y - player.Center.Y) * player.direction), ((Projectile.Center.X - player.Center.X) * player.direction));
                grappleRotation = targetrotation;
                if (hitMob < 0)
                {
                    player.rocketTime = player.rocketTimeMax;
                }
                else if (wingTime < 0)
                {
                    wingTime = player.wingTime;
                }
                player.GetModPlayer<JoostPlayer>().hoverBootsTimer = 0;
                player.wingTime = 0f;
                player.rocketDelay = 0;
                player.rocketFrame = false;
                player.canRocket = false;
                player.rocketRelease = false;
                player.fallStart = (int)(player.Center.Y / 16f);
                if (player.velocity.Y != 0 && player.itemAnimation == 0)
                {
                    player.fullRotationOrigin = player.Center - player.position;
                    player.fullRotation = Projectile.rotation;
                    if (player.gravDir == -1)
                    {
                        player.fullRotation = Projectile.rotation + (float)Math.PI;
                    }
                }
                else
                {
                    player.fullRotation = 0;
                }

                player.sandStorm = false;

                Vector2 v = player.Center - Projectile.Center;
                float dist = Vector2.Distance(player.Center, Projectile.Center);
                bool up = (player.controlUp || (dist > 450 && hitMob >= 0)); 
                bool down = (player.controlDown && maxDist < 420);
                float ndist = Vector2.Distance(player.Center + player.velocity, Projectile.Center);
                float ddist = ndist - dist;
                float num4 = Projectile.Center.X - player.Center.X;
                float num5 = Projectile.Center.Y - player.Center.Y;
                float num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
                float num7 = ddist + player.gravity;
                player.maxFallSpeed += 15;
                if ((player.controlLeft || player.controlRight) && player.velocity.X < 30f && player.velocity.X > -30f && player.velocity.Y != 0)
                {
                    player.velocity.X *= 1.03f;
                }
                Projectile.localAI[1] = (dist / maxDist) * 100;
                if (Projectile.localAI[1] > 100f)
                {
                    Projectile.localAI[1] = 100f;
                }
                if (up)
                {
                    controlSpeed++;
                    if (controlSpeed > 15)
                    {
                        controlSpeed = 15;
                    }
                    if (controlSpeed < 0)
                    {
                        controlSpeed = 0;
                    }
                    if (hitMob >= 0)
                    {
                        if (Main.npc[hitMob].velocity.Length() > 10 && (controlSpeed >= 10 || dist > 350))
                        {
                            controlSpeed = Main.npc[hitMob].velocity.Length()+1;
                        }
                        /*if (dist > 400)
                        {
                            //Main.NewText(Main.npc[hitMob].position.X - Main.npc[hitMob].oldPosition.X, Color.DarkSeaGreen);
                            Vector2 hyoik = Main.npc[hitMob].position - Main.npc[hitMob].oldPosition;
                            //controlSpeed += hyoik.Length()+1;
                            player.position += player.DirectionTo(projectile.Center) * hyoik.Length();
                        }*/
                    }
                    num7 = controlSpeed;
                    maxDist = dist;
                }
                if (down)
                {
                    controlSpeed--;
                    if (controlSpeed < -15)
                    {
                        controlSpeed = -15;
                    }
                    if (controlSpeed > 0)
                    {
                        controlSpeed = 0;
                    }
                    num7 = controlSpeed;
                    maxDist -= controlSpeed;
                }
                float num8;
                if (num6 > num7)
                {
                    num8 = num7 / num6;
                }
                else
                {
                    num8 = 1f;
                }
                num4 *= num8;
                num5 *= num8;
                Vector2 vect = new Vector2(num4, num5);
                if (up || down)
                {
                    player.velocity = vect;
                }
                else
                {
                    controlSpeed = 0;
                    if (dist >= maxDist)
                    {
                        if (player.velocity.Y != 0)
                        {
                            player.maxRunSpeed += 15f;
                            player.runAcceleration *= 3f;
                        }
                        player.velocity += vect;
                    }
                }
                if (player.releaseJump)
                {
                    jump = 1;
                }

                if (player.controlJump && jump >= 1)
                {
                    Projectile.Kill();
                    player.wingTime = wingTime;
                    if (hitMob < 0)
                    {
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
                    }
                    if (!player.controlDown && player.velocity.Y > -Player.jumpSpeed)
                    {
                        player.velocity.Y -= Player.jumpSpeed;
                        player.jump = Player.jumpHeight / 2;
                    }
                    grappleSwing = 0;
                    player.grapCount = 0;
                    return;
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.player[Projectile.owner].fullRotation = 0;
        }
        public override bool PreDrawExtras()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 pos = player.Center + new Vector2(-3, 0);
            Vector2 value = Projectile.Center + new Vector2(-7, -6) - (Projectile.rotation - 1.57f).ToRotationVector2() * 12;
            float projPosX = pos.X - 4 - value.X;
            float projPosY = pos.Y - 4 - value.Y;
            Math.Sqrt((double)(projPosX * projPosX + projPosY * projPosY));
            float rotation2 = (float)Math.Atan2((double)projPosY, (double)projPosX) - 1.57f;
            bool flag2 = true;
            if (projPosX == 0f && projPosY == 0f)
            {
                flag2 = false;
            }
            else
            {
                float projPosXY = (float)Math.Sqrt((double)(projPosX * projPosX + projPosY * projPosY));
                projPosXY = 12f / projPosXY;
                projPosX *= projPosXY;
                projPosY *= projPosXY;
                value.X -= projPosX;
                value.Y -= projPosY;
                projPosX = pos.X - 4 - value.X;
                projPosY = pos.Y - 4 - value.Y;
            }
            while (flag2)
            {
                float num = 12f;
                float num2 = (float)Math.Sqrt((double)(projPosX * projPosX + projPosY * projPosY));
                float num3 = num2;
                if (float.IsNaN(num2) || float.IsNaN(num3))
                {
                    flag2 = false;
                }
                else
                {
                    if (num2 < 20f)
                    {
                        num = num2 - 8f;
                        flag2 = false;
                    }
                    num2 = 12f / num2;
                    projPosX *= num2;
                    projPosY *= num2;
                    value.X += projPosX;
                    value.Y += projPosY;
                    projPosX = pos.X - 4 - value.X;
                    projPosY = pos.Y - 4 - value.Y;
                    if (num3 > 12f)
                    {
                        float num4 = 0.3f;
                        if (isHooked)
                        {
                            num4 = 0.8f;
                        }
                        float num5 = Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y);
                        if (num5 > 16f)
                        {
                            num5 = 16f;
                        }
                        num5 = 1f - num5 / 16f;
                        num4 *= num5;
                        num5 = num3 / 80f;
                        if (num5 > 1f)
                        {
                            num5 = 1f;
                        }
                        num4 *= num5;
                        if (num4 < 0f)
                        {
                            num4 = 0f;
                        }
                        num5 = 1f - Projectile.localAI[1] / 100f;
                        num4 *= num5;
                        if (projPosY > 0f)
                        {
                            projPosY *= 1f + num4;
                            projPosX *= 1f - num4;
                        }
                        else
                        {
                            num5 = Math.Abs(Projectile.velocity.X) / 3f;
                            if (num5 > 1f)
                            {
                                num5 = 1f;
                            }
                            num5 -= 0.5f;
                            num4 *= num5;
                            if (num4 > 0f)
                            {
                                num4 *= 2f;
                            }
                            projPosY *= 1f + num4;
                            projPosX *= 1f - num4;
                        }
                    }
                    rotation2 = (float)Math.Atan2((double)projPosY, (double)projPosX) - 1.57f;
                    Texture2D tex = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/Projectiles/EnchantedMobHook_Chain");
                    Color color2 = Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f));
                    Main.spriteBatch.Draw(tex, new Vector2(value.X - Main.screenPosition.X + (float)tex.Width * 0.5f, value.Y - Main.screenPosition.Y + (float)tex.Height * 0.5f), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, tex.Width, (int)num)), color2, rotation2, new Vector2((float)tex.Width * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
                }
            }
            return false;
        }
    }
}

