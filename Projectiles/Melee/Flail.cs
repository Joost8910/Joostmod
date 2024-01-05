using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public abstract class Flail : ModProjectile
    {
        protected int outTime = 15;
        protected float throwSpeed = 14f;
        protected float returnSpeed = 10f;
        protected float returnSpeedAfterHeld = 15f;
        protected int baseHitCD = 10;
        protected int swingHitCD = 12;
        protected int throwHitCD = 10;
        protected float swingSpeed = 1f;
        protected Texture2D chainTex = null;
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.active || player.dead || player.noItems || player.CCed || Vector2.Distance(Projectile.Center, player.Center) > 900f)
            {
                Projectile.Kill();
                return false;
            }
            if (Main.myPlayer == Projectile.owner && Main.mapFullscreen)
            {
                Projectile.Kill();
                return false;
            }

            Vector2 mountedCenter = player.MountedCenter;

            bool doFastThrowDust = false;
            bool updateRotation = true;
            bool tilesBlockHitCheck = false;


            //int outTime = 15; 
            //float throwSpeed = 24f; 
            float throwLimit = 800f;
            float returnAccel = 3f;
            //float returnSpeed = 16f;
            float returnAccelAfterHeld = 6f;
            //float returnSpeedAfterHeld = 48f;

            float oldReturnAccel = 1f;
            float oldReturnSpeed = 14f;
            int oldPullDist = 60;

            //int baseHitCD = 10;
            //int swingHitCD = 12;
            //int throwHitCD = 10;
            int bounceTime = outTime + 5;

            /* Vanilla Flail Stats
             * Ball o' Hurt
			outTime = 15;
			throwSpeed = 14f;
			returnSpeed = 10f;
			returnSpeedAfterHeld = 15f;

             * Blue Moon
			outTime = 15;
			throwSpeed = 16f;
			returnSpeed = 12f;
			returnSpeedAfterHeld = 16f;

             * Sunfury
            outTime = 15;
			throwSpeed = 17f;
			returnSpeed = 14f;
			returnSpeedAfterHeld = 18f;

             * Dao of Pow
            outTime = 13;
			throwSpeed = 21f;
			returnSpeed = 20f;
			returnSpeedAfterHeld = 24f;
			swingHitCD = 15;

             * The Meatball
            outTime = 15;
			throwSpeed = 15f;
			returnSpeed = 11f;
			returnSpeedAfterHeld = 16f;

             * Flower Pow
            outTime = 13;
			throwSpeed = 23f;
			swingHitCD = 15;

             * Mace
            outTime = 13;
			throwSpeed = 12f;
			returnSpeed = 8f;
			returnSpeedAfterHeld = 13f;
            
             * Drippler Crippler
            outTime = 13;
            throwSpeed = 22f;
            returnSpeed = 22f;
            returnSpeedAfterHeld = 26f;
            swingHitCD = 15;
            */

            float speedMult = player.GetTotalAttackSpeed(DamageClass.Melee);
            CheckStats(ref speedMult);
            float ts = throwSpeed * speedMult;
            oldReturnAccel *= speedMult;
            oldReturnSpeed *= speedMult;
            returnAccel *= speedMult;
            float rs = returnSpeed * speedMult;
            returnAccelAfterHeld *= speedMult;
            float rsah = returnSpeedAfterHeld * speedMult;
            float oldMaxDist = ts * outTime;
            float maxHeldDistance = oldMaxDist + 160f;
            Projectile.localNPCHitCooldown = baseHitCD;
            switch ((int)Projectile.ai[0])
            {
                case 0: //Swinging in circle
                    {
                        tilesBlockHitCheck = true;
                        if (Projectile.owner == Main.myPlayer)
                        {
                            Vector2 mouseWorld = Main.MouseWorld;
                            Vector2 vector = mountedCenter.DirectionTo(mouseWorld).SafeNormalize(Vector2.UnitX * player.direction);
                            player.ChangeDir(vector.X > 0f ? 1 : -1);
                            if (!player.channel)
                            {
                                Projectile.ai[0] = 1f;
                                Projectile.ai[1] = 0f;
                                Projectile.velocity = vector * ts + player.velocity;
                                Projectile.Center = mountedCenter;
                                Projectile.netUpdate = true;
                                Projectile.ResetLocalNPCHitImmunity();
                                Projectile.localNPCHitCooldown = throwHitCD;
                                ThrowEffects();
                                break;
                            }
                        }
                        Projectile.localAI[1] += swingSpeed;
                        Vector2 vector2 = new Vector2(0, player.gravDir).RotatedBy((double)(31.4159279f * (Projectile.localAI[1] / 60f) * player.direction * player.gravDir), default);
                        vector2.Y *= 0.8f;
                        if (vector2.Y * player.gravDir > 0f)
                        {
                            vector2.Y *= 0.5f;
                        }
                        Projectile.Center = mountedCenter + vector2 * 30f;
                        Projectile.velocity = Vector2.Zero;
                        Projectile.localNPCHitCooldown = swingHitCD;
                        SwingEffects();
                        break;
                    }
                case 1: //Thrown out
                    {
                        doFastThrowDust = true;
                        Projectile.ai[1]++;
                        bool flag3 = Projectile.ai[1] >= outTime;
                        flag3 |= Projectile.Distance(mountedCenter) >= throwLimit;
                        if (player.controlUseItem)
                        {
                            Projectile.ai[0] = 6f;
                            Projectile.ai[1] = 0f;
                            Projectile.netUpdate = true;
                            Projectile.velocity *= 0.2f;
                            if (Main.myPlayer == Projectile.owner)
                            {
                                ReachedPeakEffects();
                                //Projectile.NewProjectile(Projectile.GetProjectileSource_FromProjectile(), Projectile.Center, Projectile.velocity, 928, Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
                            }
                        }
                        else
                        {
                            if (flag3)
                            {
                                Projectile.ai[0] = 2f;
                                Projectile.ai[1] = 0f;
                                Projectile.netUpdate = true;
                                Projectile.velocity *= 0.3f;
                                if (Main.myPlayer == Projectile.owner)
                                {
                                    ReachedPeakEffects();
                                    //Projectile.NewProjectile(Projectile.GetProjectileSource_FromProjectile(), Projectile.Center, Projectile.velocity, 928, Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
                                }
                            }
                            player.ChangeDir(player.Center.X < Projectile.Center.X ? 1 : -1);
                            Projectile.localNPCHitCooldown = throwHitCD;
                        }
                        break;
                    }
                case 2: //Returning
                    {
                        Vector2 vector3 = Projectile.DirectionTo(mountedCenter).SafeNormalize(Vector2.Zero);
                        if (Projectile.Distance(mountedCenter) <= rs + Projectile.width / 2)
                        {
                            Projectile.Kill();
                            return false;
                        }
                        if (player.controlUseItem)
                        {
                            Projectile.ai[0] = 6f;
                            Projectile.ai[1] = 0f;
                            Projectile.netUpdate = true;
                            Projectile.velocity *= 0.2f;
                        }
                        else
                        {
                            Projectile.velocity *= 0.98f;
                            Projectile.velocity = Projectile.velocity.MoveTowards(vector3 * rs, returnAccel);
                            player.ChangeDir(player.Center.X < Projectile.Center.X ? 1 : -1);
                        }
                        break;
                    }
                case 3: //Old Behavior
                    if (!player.controlUseItem)
                    {
                        Projectile.ai[0] = 4f;
                        Projectile.ai[1] = 0f;
                        Projectile.netUpdate = true;
                    }
                    else
                    {
                        float dist = Projectile.Distance(mountedCenter);
                        Projectile.tileCollide = Projectile.ai[1] == 1f;
                        bool flag4 = dist <= oldMaxDist;
                        if (flag4 != Projectile.tileCollide)
                        {
                            Projectile.tileCollide = flag4;
                            Projectile.ai[1] = Projectile.tileCollide ? 1 : 0;
                            Projectile.netUpdate = true;
                        }
                        if (dist > oldPullDist)
                        {
                            if (dist >= oldMaxDist)
                            {
                                Projectile.velocity *= 0.5f;
                                Projectile.velocity = Projectile.velocity.MoveTowards(Projectile.DirectionTo(mountedCenter).SafeNormalize(Vector2.Zero) * oldReturnSpeed, oldReturnSpeed);
                            }
                            Projectile.velocity *= 0.98f;
                            Projectile.velocity = Projectile.velocity.MoveTowards(Projectile.DirectionTo(mountedCenter).SafeNormalize(Vector2.Zero) * oldReturnSpeed, oldReturnAccel);
                        }
                        else
                        {
                            if (Projectile.velocity.Length() < 6f)
                            {
                                Projectile.velocity.X = Projectile.velocity.X * 0.96f;
                                Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
                            }
                            if (player.velocity.X == 0f)
                            {
                                Projectile.velocity.X = Projectile.velocity.X * 0.96f;
                            }
                        }
                        player.ChangeDir(player.Center.X < Projectile.Center.X ? 1 : -1);
                    }
                    break;
                case 4: //Returning after being held in place
                    {
                        Projectile.tileCollide = false;
                        Vector2 vector4 = Projectile.DirectionTo(mountedCenter).SafeNormalize(Vector2.Zero);
                        if (Projectile.Distance(mountedCenter) <= rsah)
                        {
                            Projectile.Kill();
                            return false;
                        }
                        Projectile.velocity *= 0.98f;
                        Projectile.velocity = Projectile.velocity.MoveTowards(vector4 * rsah, returnAccelAfterHeld);
                        Vector2 target = Projectile.Center + Projectile.velocity;
                        Vector2 vector5 = mountedCenter.DirectionFrom(target).SafeNormalize(Vector2.Zero);
                        if (Vector2.Dot(vector4, vector5) < 0f)
                        {
                            Projectile.Kill();
                            return false;
                        }
                        player.ChangeDir(player.Center.X < Projectile.Center.X ? 1 : -1);
                        break;
                    }
                case 5: //Bouncing off tile
                    {
                        Projectile.ai[1]++;
                        if (Projectile.ai[1] >= bounceTime)
                        {
                            Projectile.ai[0] = 6f;
                            Projectile.ai[1] = 0f;
                            Projectile.netUpdate = true;
                        }
                        else
                        {
                            Projectile.localNPCHitCooldown = throwHitCD;
                            Projectile.velocity.Y = Projectile.velocity.Y + 0.6f;
                            Projectile.velocity.X = Projectile.velocity.X * 0.95f;
                            player.ChangeDir(player.Center.X < Projectile.Center.X ? 1 : -1);
                        }
                        break;
                    }
                case 6: //Held in place
                    if (!player.controlUseItem || Projectile.Distance(mountedCenter) > maxHeldDistance)
                    {
                        Projectile.ai[0] = 4f;
                        Projectile.ai[1] = 0f;
                        Projectile.netUpdate = true;
                    }
                    else
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y + 0.8f;
                        Projectile.velocity.X = Projectile.velocity.X * 0.95f;
                        player.ChangeDir(player.Center.X < Projectile.Center.X ? 1 : -1);
                    }
                    break;
            }
            ExtraBehavior(ref updateRotation);

            Projectile.direction = Projectile.velocity.X == 0 ? player.direction : Math.Sign(Projectile.velocity.X);
            Projectile.spriteDirection = Projectile.direction;
            Projectile.ownerHitCheck = tilesBlockHitCheck;
            if (updateRotation)
            {
                if (Projectile.velocity.Length() > 1f)
                {
                    Projectile.rotation = Projectile.velocity.ToRotation() + Projectile.velocity.X * 0.1f;
                }
                else
                {
                    Projectile.rotation += Projectile.velocity.X * 0.1f;
                }
            }
            Projectile.timeLeft = 2;
            player.heldProj = Projectile.whoAmI;
            player.SetDummyItemTime(2);
            player.itemRotation = Projectile.DirectionFrom(mountedCenter).ToRotation();
            if (Projectile.Center.X < mountedCenter.X)
            {
                player.itemRotation += 3.14159274f;
            }
            player.itemRotation = MathHelper.WrapAngle(player.itemRotation);
            //Projectile.AI_015_Flails_Dust(doFastThrowDust);
            Dust(doFastThrowDust);
            return false;
        }
        public virtual void CheckStats(ref float speedMult)
        {

        }
        public virtual void ThrowEffects()
        {

        }
        public virtual void SwingEffects()
        {

        }
        public virtual void ReachedPeakEffects()
        {

        }
        public virtual void ExtraBehavior(ref bool flag)
        {

        }
        public virtual void Dust(bool doFastThrowDust)
        {
            
        }
        public override bool PreDrawExtras()
        {
            if (chainTex != null)
            {
                Texture2D texture = chainTex;
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
                Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
                Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
                float num1 = texture.Height * Projectile.scale;
                Vector2 vector2_4 = playerCenter - position;
                playerCenter -= Vector2.Normalize(vector2_4) * num1 * 0.5f;
                position -= Vector2.Normalize(vector2_4) * num1 * 0.5f;
                float rotation = (float)Math.Atan2(vector2_4.Y, vector2_4.X) - 1.57f;
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
                        sourceRectangle.Height = (int)(vector2_4.Length());
                    }
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    vector2_4 = playerCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16.0));
                    color2 = Projectile.GetAlpha(color2);
                    Main.EntitySpriteDraw(texture, position - Main.screenPosition, new Rectangle?(sourceRectangle), color2, rotation, origin, Projectile.scale, SpriteEffects.None, 0);

                }
            }
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            if (Projectile.ai[0] == 1 || Projectile.ai[0] == 2)
            {
                for (int k = 0; k < Projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + Projectile.Size / 2 + new Vector2(0f, Projectile.gfxOffY);
                    Color color2 = Projectile.GetAlpha(lightColor) * (0.4f - k * 0.05f);
                    float scale = Projectile.scale * (1.2f - k * 0.05f);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type] * Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]));
                    Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, drawPos, rect, color2, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), scale, SpriteEffects.None, 0);
                }
            }

            Color color = Lighting.GetColor((int)(Projectile.Center.X / 16), (int)(Projectile.Center.Y / 16.0));
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, effects, 0);
            return false;
        }
        /*
        if (shootAtEnemies) //Flower Pow behavior
        {
            flag = false;
            float num21 = (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.01f;
            Projectile.rotation += ((Projectile.velocity.X > 0f) ? num21 : (0f - num21));
            if (Projectile.ai[0] == 0f)
            {
                Projectile.rotation += 0.418879032f * (float)player.direction;
            }
            float num22 = 600f;
            NPC nPC = null;
            if (Projectile.owner == Main.myPlayer)
            {
                Projectile.localAI[0] += 1f;
                if (Projectile.localAI[0] >= 20f)
                {
                    Projectile.localAI[0] = 17f;
                    for (int i = 0; i < 200; i++)
                    {
                        NPC nPC2 = Main.npc[i];
                        if (nPC2.CanBeChasedBy(Projectile, false))
                        {
                            float num23 = Projectile.Distance(nPC2.Center);
                            if (num23 < num22 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, nPC2.position, nPC2.width, nPC2.height))
                            {
                                nPC = nPC2;
                                num22 = num23;
                            }
                        }
                    }
                }
                if (nPC != null)
                {
                    Projectile.localAI[0] = 0f;
                    float num24 = 14f;
                    Vector2 center = Projectile.Center;
                    Vector2 velocity = center.DirectionTo(nPC.Center) * num24;
                    //Projectile.NewProjectile(Projectile.GetProjectileSource_FromProjectile(), center, velocity, 248, (int)((double)Projectile.damage / 1.5), Projectile.knockBack / 2f, Main.myPlayer, 0f, 0f);
                }
            }
        }
        */
    }
}
