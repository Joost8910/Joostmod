using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class SwingyHook : ModProjectile
    {
        private float grappleRotation = 0f;
        private bool isHooked;
        private int grappleSwing = -1;
        private float maxDist;
        private int jump = 0;
        private bool retreat;
        private float controlSpeed = 0;
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.GemHookAmethyst);
            projectile.width = 18;
            projectile.height = 18;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Swingy Hook");
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
                if (Main.projectile[i].active && Main.projectile[i].owner == projectile.whoAmI && Main.projectile[i].type == projectile.type)
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
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(isHooked);
            writer.Write((short)grappleSwing);
            writer.Write(retreat);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            isHooked = reader.ReadBoolean();
            grappleSwing = reader.ReadInt16();
            retreat = reader.ReadBoolean();
        }
        public override float GrappleRange()
        {
            return 400f;
        }

        public override void NumGrappleHooks(Player player, ref int numHooks)
        {
            numHooks = 1;
        }
        
        public override void GrappleRetreatSpeed(Player player, ref float speed)
        {
            speed = 40f;
        }
        public override bool PreAI()
        {
            return false;
        }
        public override void PostAI()
        {
            Player player = Main.player[projectile.owner];
            if (Vector2.Distance(player.Center, projectile.Center) > (isHooked ? 420 : 320))
            {
                retreat = true;
                isHooked = false;
            }
            if (!player.active || player.dead || Vector2.Distance(player.Center, projectile.Center) > 1000)
            {
                projectile.Kill();
            }
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Vector2 vector6 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
            float num69 = mountedCenter.X - vector6.X;
            float num70 = mountedCenter.Y - vector6.Y;
            float num71 = (float)Math.Sqrt((double)(num69 * num69 + num70 * num70));
            projectile.rotation = (float)Math.Atan2((double)num70, (double)num69) - 1.57f;
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
                projectile.velocity = projectile.DirectionTo(player.Center) * 15;
                if (Vector2.Distance(player.Center, projectile.Center) < 16 && !isHooked)
                {
                    projectile.Kill();
                }
            }
            if (isHooked)
            {
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
                else
                {
                    grappleSwing = projectile.whoAmI;
                }
            }
            else if (!retreat)
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
                            maxDist = Vector2.Distance(player.Center, projectile.Center);
                            grappleSwing = projectile.whoAmI;
                            projectile.velocity.X = 0f;
                            projectile.velocity.Y = 0f;
                            Main.PlaySound(0, num115 * 16, num116 * 16, 1, 1f, 0f);
                            isHooked = true;
                            projectile.position.X = (float)(num115 * 16 + 8 - projectile.width / 2);
                            projectile.position.Y = (float)(num116 * 16 + 8 - projectile.height / 2);
                            projectile.damage = 0;
                            projectile.netUpdate = true;
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
            if (Main.myPlayer == projectile.owner)
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
            int tweak2 = 0;
            if (grappleSwing >= 0)
            {
                if (player.mount.Active)
                {
                    player.mount.Dismount(player);
                }
                float targetrotation = (float)Math.Atan2(((projectile.Center.Y - player.Center.Y) * player.direction), ((projectile.Center.X - player.Center.X) * player.direction));
                grappleRotation = targetrotation;
                player.GetModPlayer<JoostPlayer>(mod).hoverBootsTimer = 0;
                player.wingTime = 0f;
                player.rocketTime = player.rocketTimeMax;
                player.rocketDelay = 0;
                player.rocketFrame = false;
                player.canRocket = false;
                player.rocketRelease = false;
                player.fallStart = (int)(player.Center.Y / 16f);
                player.maxFallSpeed += 5f;
                if (player.velocity.Y != 0 && player.itemAnimation == 0)
                {
                    player.fullRotationOrigin = player.Center - player.position;
                    player.fullRotation = projectile.rotation;
                    if (player.gravDir == -1)
                    {
                        player.fullRotation = projectile.rotation + (float)Math.PI;
                    }
                }
                else
                {
                    player.fullRotation = 0;
                }

                player.sandStorm = false;

                Vector2 v = player.Center - projectile.Center;
                float dist = Vector2.Distance(player.Center, projectile.Center);
                bool up = (player.controlUp);
                bool down = (player.controlDown && maxDist < 320);
                float ndist = Vector2.Distance(player.Center + player.velocity, projectile.Center);
                float ddist = ndist - dist;
                float num4 = projectile.Center.X - player.Center.X;
                float num5 = projectile.Center.Y - player.Center.Y;
                float num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
                float num7 = ddist + player.gravity;
                player.maxFallSpeed += 10;
                if ((player.controlLeft || player.controlRight) && player.velocity.X < 15f && player.velocity.X > -15f && player.velocity.Y != 0)
                {
                    player.velocity.X *= 1.015f;
                }
                projectile.localAI[1] = (dist / maxDist) * 100;
                if (projectile.localAI[1] > 100f)
                {
                    projectile.localAI[1] = 100f;
                }
                if (up)
                {
                    controlSpeed++;
                    if (controlSpeed > 10)
                    {
                        controlSpeed = 10;
                    }
                    if (controlSpeed < 0)
                    {
                        controlSpeed = 0;
                    }
                    num7 = controlSpeed;
                    maxDist = dist;
                }
                if (down)
                {
                    controlSpeed--;
                    if (controlSpeed < -10)
                    {
                        controlSpeed = -10;
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
                if (up)
                {
                    player.velocity = vect;
                }
                else if (!down)
                {
                    controlSpeed = 0;
                    if (dist >= maxDist)
                    {
                        if (player.velocity.Y != 0)
                        {
                            player.maxRunSpeed += 12f;
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
                    grappleSwing = 0;
                    player.grapCount = 0;
                    return;
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.player[projectile.owner].fullRotation = 0;
        }
        public override bool PreDrawExtras(SpriteBatch spriteBatch)
        {
            Player player = Main.player[projectile.owner];
            Vector2 pos = player.Center;
            Vector2 value = projectile.Center + new Vector2(-4, -4) - (projectile.rotation - 1.57f).ToRotationVector2() * 12;
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
                        float num5 = Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y);
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
                        num5 = 1f - projectile.localAI[1] / 100f;
                        num4 *= num5;
                        if (projPosY > 0f)
                        {
                            projPosY *= 1f + num4;
                            projPosX *= 1f - num4;
                        }
                        else
                        {
                            num5 = Math.Abs(projectile.velocity.X) / 3f;
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
                    Color color2 = Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f));
                    Main.spriteBatch.Draw(Main.chain16Texture, new Vector2(value.X - Main.screenPosition.X + (float)Main.chain16Texture.Width * 0.5f, value.Y - Main.screenPosition.Y + (float)Main.chain16Texture.Height * 0.5f), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, Main.chain16Texture.Width, (int)num)), color2, rotation2, new Vector2((float)Main.chain16Texture.Width * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
                }
            }
            return false;
        }
    }
}

