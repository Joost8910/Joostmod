using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Grappling
{
    public class EnchantedSwingyHook : ModProjectile
    {
        private float swingSpeed = 1.03f;
        private bool isHooked;
        private int grappleSwing = -1;
        private float maxDist;
        private int jump = 0;
        private bool retreat;
        private float controlSpeed = 0;
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.GemHookAmethyst);
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.light = 0.15f;


        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enchanted Swingy Hook");
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
            if (Vector2.Distance(player.Center, Projectile.Center) > 1000 || player.dead || !player.active)
            {
                Projectile.Kill();
            }
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
            float xDif = mountedCenter.X - Projectile.Center.X;
            float yDif = mountedCenter.Y - Projectile.Center.Y;
            Projectile.rotation = (float)Math.Atan2((double)yDif, (double)xDif) - 1.57f;
            /*if (!retreat && !isHooked)
            {
                projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
                projectile.velocity.Y = projectile.velocity.Y + 0.3f;
                if (projectile.velocity.Y > 18f)
                {
                    projectile.velocity.Y = 18f;
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

            int left = (int)(Projectile.position.X / 16f) - 1;
            int right = (int)((Projectile.position.X + Projectile.width) / 16f) + 2;
            int top = (int)(Projectile.position.Y / 16f) - 1;
            int bottom = (int)((Projectile.position.Y + Projectile.height) / 16f) + 2;
            if (left < 0)
            {
                left = 0;
            }
            if (right > Main.maxTilesX)
            {
                right = Main.maxTilesX;
            }
            if (top < 0)
            {
                top = 0;
            }
            if (bottom > Main.maxTilesY)
            {
                bottom = Main.maxTilesY;
            }

            if (isHooked)
            {
                Projectile.ai[0] = 2f;

                Projectile.velocity = default;
                Projectile.timeLeft = 2;
                bool flag3 = true;
                for (int i = left; i < right; i++)
                {
                    for (int j = top; j < bottom; j++)
                    {
                        if (Main.tile[i, j] == null)
                        {
                            //Vanilla grappling code still does this, why is it an error now?
                            //Main.tile[i, j] = new Tile();
                        }
                        Vector2 vector9;
                        vector9.X = i * 16;
                        vector9.Y = j * 16;
                        if (Projectile.position.X + Projectile.width / 2 > vector9.X && Projectile.position.X + Projectile.width / 2 < vector9.X + 16f && Projectile.position.Y + Projectile.height / 2 > vector9.Y && Projectile.position.Y + Projectile.height / 2 < vector9.Y + 16f && Main.tile[i, j].HasUnactuatedTile && (Main.tileSolid[Main.tile[i, j].TileType] || Main.tile[i, j].TileType == 314))
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
                    grappleSwing = Projectile.whoAmI;
                }
            }
            else if (!retreat)
            {
                Projectile.ai[0] = 0f;
                for (int i = left; i < right; i++)
                {
                    int j = top;
                    while (j < bottom)
                    {
                        if (Main.tile[i, j] == null)
                        {
                            //Main.tile[i, j] = new Tile();
                        }
                        Vector2 vector8;
                        vector8.X = i * 16;
                        vector8.Y = j * 16;
                        if (Projectile.position.X + Projectile.width > vector8.X && Projectile.position.X < vector8.X + 16f && Projectile.position.Y + Projectile.height > vector8.Y && Projectile.position.Y < vector8.Y + 16f && Main.tile[i, j].HasUnactuatedTile && (Main.tileSolid[Main.tile[i, j].TileType] || Main.tile[i, j].TileType == 314))
                        {
                            maxDist = Vector2.Distance(player.Center, Projectile.Center);
                            grappleSwing = Projectile.whoAmI;
                            Projectile.velocity.X = 0f;
                            Projectile.velocity.Y = 0f;
                            SoundEngine.PlaySound(SoundID.Dig, new Vector2(i * 16, j * 16));
                            isHooked = true;
                            Projectile.position.X = i * 16 + 8 - Projectile.width / 2;
                            Projectile.position.Y = j * 16 + 8 - Projectile.height / 2;
                            Projectile.damage = 0;
                            Projectile.netUpdate = true;
                            break;
                        }
                        else
                        {
                            j++;
                        }
                    }
                    if (isHooked)
                    {
                        break;
                    }
                }
            }
            //Isn't this just the UseGrapple code? What's the point in calling it every frame? commenting out for now
            /*
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
            */
            if (grappleSwing >= 0)
            {
                if (player.mount.Active)
                {
                    player.mount.Dismount(player);
                }
                player.GetModPlayer<JoostPlayer>().hoverBootsTimer = 0;
                player.wingTime = 0f;
                player.rocketTime = player.rocketTimeMax;
                player.rocketDelay = 0;
                player.rocketFrame = false;
                player.canRocket = false;
                player.rocketRelease = false;
                player.fallStart = (int)(player.Center.Y / 16f);
                player.sandStorm = false;
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

                float dist = Vector2.Distance(player.Center, Projectile.Center);
                bool up = player.controlUp;
                bool down = player.controlDown && maxDist < 420;
                float ndist = Vector2.Distance(player.Center + player.velocity, Projectile.Center);
                float ddist = ndist - dist;
                float xDist = Projectile.Center.X - player.Center.X;
                float yDist = Projectile.Center.Y - player.Center.Y;
                float num7 = ddist + player.gravity;
                player.maxFallSpeed += 15;
                if ((player.controlLeft || player.controlRight) && player.velocity.X < 30f && player.velocity.X > -30f && player.velocity.Y != 0)
                {
                    player.velocity.X *= swingSpeed;
                }
                Projectile.localAI[1] = dist / maxDist * 100;
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
                    maxDist = dist;
                }
                float num8;
                if (dist > num7)
                {
                    num8 = num7 / dist;
                }
                else
                {
                    num8 = 1f;
                }
                xDist *= num8;
                yDist *= num8;
                Vector2 vect = new Vector2(xDist, yDist);
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
                    player.RefreshMovementAbilities();
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
            Vector2 pos = player.Center + new Vector2(-4, 0);
            Vector2 value = Projectile.Center + new Vector2(-8, -4) - (Projectile.rotation - 1.57f).ToRotationVector2() * 12;
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
                    Color color2 = Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f));
                    Main.EntitySpriteDraw(TextureAssets.Chains[4].Value, new Vector2(value.X - Main.screenPosition.X + TextureAssets.Chains[4].Value.Width * 0.5f, value.Y - Main.screenPosition.Y + TextureAssets.Chains[4].Value.Height * 0.5f), new Rectangle?(new Rectangle(0, 0, TextureAssets.Chains[4].Value.Width, (int)num)), color2, rotation2, new Vector2(TextureAssets.Chains[4].Value.Width * 0.5f, 0f), 1f, SpriteEffects.None, 0);
                }
            }
            return false;
        }
    }
}

