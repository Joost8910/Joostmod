using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
    public class WaterElemental : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Elemental");
            Main.npcFrameCount[npc.type] = 6;
            NPCID.Sets.TrailingMode[npc.type] = 3;
            NPCID.Sets.TrailCacheLength[npc.type] = 6;
        }
        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 50;
            npc.damage = 30;
            npc.defense = 24;
            npc.lifeMax = 400;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.value = Item.buyPrice(0, 0, 7, 50);
            npc.knockBackResist = 0.5f;
            npc.aiStyle = -1;
            npc.noGravity = true;
            npc.frameCounter = 0;
            banner = npc.type;
            bannerItem = mod.ItemType("WaterElementalBanner");
            npc.buffImmune[BuffID.OnFire] = true;
            npc.alpha = 50;
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WaterEssence"), (Main.expertMode ? Main.rand.Next(12, 30) : Main.rand.Next(8, 20)));
            if (Main.rand.Next(50) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SecondAnniversary"), 1);
            }
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (npc.ai[1] >= 20)
            {
                damage *= 2;
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int k = 0; k < 30; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 103, 2.5f * (float)hitDirection, Main.rand.Next(-5, 5), 0, default(Color), 0.7f);
                }
            }
            for (int k = 0; k < 4; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 103, 2.5f * (float)hitDirection, Main.rand.Next(-5, 5), 0, default(Color), 0.7f);
            }
            //npc.ai[2] += (float)(damage / 3);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.playerInTown && ((!spawnInfo.invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse) || spawnInfo.spawnTileY > Main.worldSurface) && spawnInfo.water && Main.hardMode ? 0.065f : 0f;
        }
        public override void AI()
        {
            npc.ai[0]++;
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            npc.direction = npc.Center.X < P.Center.X ? 1 : -1;
            npc.directionY = npc.Center.Y < P.Center.Y ? 1 : -1;
            npc.spriteDirection = 1;
            npc.wet = false;
            npc.wetCount = 0;
            npc.rotation = 0;
            if (npc.ai[1] > 0)
            {
                npc.ai[1]++;
                float speed = Main.expertMode ? 15 : 10;
                npc.height = 40;
                if (npc.ai[1] == 20)
                {
                    Main.PlaySound(19, npc.Center, 0);
                }
                if (npc.ai[1] == 40)
                {
                    npc.velocity = npc.DirectionTo(P.Center) * speed;
                    npc.knockBackResist = Main.expertMode ? 0f : 0.01f;
                    Main.PlaySound(42, npc.Center, 201);
                }
                if (npc.ai[1] > 40)
                {
                    if ((int)npc.ai[1] % 6 == 0)
                    {
                        Main.PlaySound(42, npc.Center, 205);
                    }
                    Vector2 vel = new Vector2(speed * npc.direction, speed * npc.directionY);
                    if (vel.Length() > speed)
                    {
                        vel *= speed / vel.Length();
                    }
                    if (npc.velocity.X == 0 && Math.Abs(npc.oldVelocity.X) > 1)
                    {
                        npc.velocity.X = -npc.oldVelocity.X;
                        Main.PlaySound(19, npc.Center, 1);
                    }
                    if (npc.velocity.Y == 0 && Math.Abs(npc.oldVelocity.Y) > 1)
                    {
                        npc.velocity.Y = -npc.oldVelocity.Y;
                        Main.PlaySound(19, npc.Center, 1);
                    }
                    float home = 50f;
                    if (vel != Vector2.Zero)
                    {
                        npc.velocity = ((home - 1f) * npc.velocity + vel) / home;
                    }
                    if (npc.ai[1] > 100)
                    {
                        npc.height = 50;
                        npc.ai[0] = 0;
                        npc.ai[1] = 0;
                        npc.knockBackResist = Main.expertMode ? 0.45f : 0.5f;
                    }
                    npc.rotation = npc.velocity.ToRotation() - 90 * 0.0174f;
                }
                else 
                {
                    npc.rotation = npc.DirectionTo(P.Center).ToRotation() - 90 * 0.0174f;
                }
            }
            else if (npc.ai[3] == 0)
            {
                if (npc.velocity.X * npc.direction < 4)
                {
                    npc.velocity.X += 0.05f * npc.direction;
                }
                if (npc.velocity.Y * npc.directionY < 3)
                {
                    npc.velocity.Y += 0.1f * npc.directionY;
                }
                if (npc.ai[0] > 300 && Collision.CanHitLine(npc.position, 1, 1, P.Center, 1, 1))
                {
                    npc.ai[1]++;
                    npc.velocity = Vector2.Zero;
                }
            }
            if (Main.expertMode)
            {
                npc.ai[2]++;
                if (npc.ai[2] > 500 && npc.ai[0] > 60 && npc.ai[1] == 0 && npc.ai[3] == 0)
                {
                    npc.ai[3] = 1;
                }
                if (npc.ai[3] == 1)
                {
                    npc.directionY = npc.Center.Y < P.Center.Y - 150 ? 1 : -1;
                    if (npc.velocity.X * npc.direction < 6)
                    {
                        npc.velocity.X += 0.15f * npc.direction;
                    }
                    if (npc.velocity.Y * npc.directionY < 5)
                    {
                        npc.velocity.Y += 0.3f * npc.directionY;
                    }
                    if (npc.Distance(new Vector2(P.Center.X, P.Center.Y - 150)) < 40 && Collision.CanHitLine(npc.position, 1, 1, P.Center, 1, 1))
                    {
                        npc.ai[3] = 2;
                        npc.velocity.Y = 0;
                        Main.PlaySound(19, (int)npc.Center.X, (int)npc.Center.Y, 0, 1, -0.6f);
                    }
                    npc.knockBackResist = 0f;
                }
                if (npc.ai[3] >= 2)
                {
                    npc.ai[3]++;
                    if (npc.ai[3] < 80)
                    {
                        npc.directionY = npc.Center.Y < P.Center.Y - 150 ? 1 : -1;
                        if (npc.velocity.X * npc.direction < 2)
                        {
                            npc.velocity.X += 0.5f * npc.direction;
                        }
                        if (npc.velocity.Y * npc.directionY < 2)
                        {
                            npc.velocity.Y += 0.3f * npc.directionY;
                        }

                        npc.scale = 1f + (npc.ai[3] / 80f);
                        npc.width = (int)(npc.scale * 40);
                        npc.height = (int)(npc.scale * 50);
                        
                        for (int d = 0; d < npc.ai[3]; d++)
                        {
                            Dust dust = Dust.NewDustDirect(npc.Center - new Vector2(10, 10), 20, 20, 172, npc.velocity.X * 0.8f, npc.velocity.Y * 0.8f, 0, default(Color), npc.scale);
                            Vector2 vel = npc.Center - dust.position;
                            vel.Normalize();
                            dust.position -= vel * 60;
                            dust.velocity = vel * 10 + npc.velocity;
                            dust.noGravity = true;
                        }
                    }
                    else
                    {
                        npc.velocity.Y = 0;
                        if (npc.velocity.X * npc.direction < 6f)
                        {
                            npc.velocity.X += 0.04f * npc.direction;
                        }
                        npc.scale = 2f - ((npc.ai[3] - 80) / 110f);
                        npc.width = (int)(npc.scale * 40);
                        npc.height = (int)(npc.scale * 50);

                        if (npc.ai[3] % 10 == 0)
                        {
                            Main.PlaySound(2, (int)npc.Center.X, (int)npc.Center.Y, 13, 1, -0.3f);
                        }
                        
                        if (npc.ai[3] % 2 == 0 && Main.netMode != 1)
                        {
                            Vector2 pos = npc.Center;
                            pos.X += Main.rand.NextFloat(-npc.width / 2f, npc.width / 2f);
                            Projectile.NewProjectile(pos, new Vector2(0, 10), ProjectileID.RainNimbus, 20, 1);
                        }
                        if (npc.ai[3] > 190)
                        {
                            npc.knockBackResist = 0.5f;
                            npc.ai[2] = 0;
                            npc.ai[3] = 0;
                            if (npc.ai[0] > 200)
                            {
                                npc.ai[0] = 0;
                            }
                        }
                    }
                }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            frameHeight = 62;
            int frameWidth = 60;

            npc.frameCounter++;
            if (npc.frameCounter >= 5)
            {
                npc.frameCounter = 0;
                npc.frame.Y = (npc.frame.Y + frameHeight);
            }
            if (npc.frame.Y >= frameHeight * 6)
            {
                npc.frame.Y = 0;
            }
            if (npc.ai[3] > 50)
            {
                npc.frameCounter++;
            }
            npc.frame.X = 0;
            if (npc.ai[1] > 0)
            {
                npc.frame.X = frameWidth;
            }
            if (npc.ai[1] > 10)
            {
                npc.frame.X = frameWidth * 2;
            }
            if (npc.ai[1] > 20)
            {
                npc.frame.X = frameWidth * 3;
            }
            if (npc.ai[1] > 40)
            {
                npc.frameCounter++;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            int xFrameCount = 4;
            Texture2D texture = Main.npcTexture[npc.type];
            Rectangle rect = new Rectangle((int)npc.frame.X, (int)npc.frame.Y, (texture.Width / xFrameCount), (texture.Height / Main.npcFrameCount[npc.type]));
            Vector2 vect = new Vector2((float)((texture.Width / xFrameCount) / 2), (float)((texture.Height / Main.npcFrameCount[npc.type]) / 2));
            drawColor *= 0.8f;

            if (npc.ai[1] >= 40)
            {
                for (int i = 0; i < npc.oldPos.Length; i++)
                {
                    Color color2 = drawColor * ((npc.oldPos.Length - i) / (float)npc.oldPos.Length) * 0.8f;
                    Vector2 drawPos = npc.oldPos[i] - Main.screenPosition + new Vector2(npc.width / 2, npc.height / 2) + new Vector2(0f, npc.gfxOffY);
                    spriteBatch.Draw(texture, drawPos, rect, color2, npc.oldRot[i], vect, npc.scale, SpriteEffects.None, 0f);
                }
            }
            spriteBatch.Draw(texture, new Vector2(npc.position.X - Main.screenPosition.X + (float)(npc.width / 2) - (float)(texture.Width / xFrameCount) / 2f + vect.X, npc.position.Y - Main.screenPosition.Y + (float)npc.height - (float)(texture.Height / Main.npcFrameCount[npc.type]) + 4f + vect.Y), new Rectangle?(rect), drawColor, npc.rotation, vect, npc.scale, SpriteEffects.None, 0f);

            Texture2D tex = ModContent.GetTexture("JoostMod/NPCs/WaterElemental_Hand");
            int frame = npc.frame.Y / 62;
            Rectangle rect2 = new Rectangle(0, frame * (tex.Height / 6), tex.Width, tex.Height / 6);
            Vector2 drawOrigin = new Vector2((tex.Width / 2), ((tex.Height / 6) / 2));

            Vector2 offSet = new Vector2(-23, -24);
            Vector2 offSet2 = new Vector2(23, -24);
            if (npc.ai[3] > 2)
            {
                if (npc.ai[3] < 65)
                {
                    float lerpAmount = npc.ai[3] < 50 ? npc.ai[3] / 50f : 1f;
                    offSet = Vector2.Lerp(offSet, offSet + new Vector2(10, -30), lerpAmount);
                    offSet2 = Vector2.Lerp(offSet2, offSet2 + new Vector2(-10, -30), lerpAmount);
                }
                else if (npc.ai[3] < 180)
                {
                    float lerpAmount = npc.ai[3] < 80 ? (npc.ai[3] - 65) / 15f : 1f;
                    offSet = Vector2.Lerp(offSet + new Vector2(10, -30), offSet + new Vector2(-6, 50), lerpAmount);
                    offSet2 = Vector2.Lerp(offSet2 + new Vector2(-10, -30), offSet2 + new Vector2(6, 50), lerpAmount);
                }
                else
                {
                    float lerpAmount = npc.ai[3] < 190 ? (npc.ai[3] - 180) / 10f : 1f;
                    offSet = Vector2.Lerp(offSet + new Vector2(-6, 50), offSet, lerpAmount);
                    offSet2 = Vector2.Lerp(offSet2 + new Vector2(6, 50), offSet2, lerpAmount);
                }
            }

            offSet = offSet.RotatedBy(npc.rotation);
            offSet2 = offSet2.RotatedBy(npc.rotation);
            spriteBatch.Draw(tex, new Vector2(npc.position.X - Main.screenPosition.X + (float)(npc.width / 2) - (float)(tex.Width) / 2f + drawOrigin.X + offSet.X * npc.scale, npc.position.Y - Main.screenPosition.Y + npc.height / 2 + offSet.Y * npc.scale), new Rectangle?(rect2), drawColor, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(tex, new Vector2(npc.position.X - Main.screenPosition.X + (float)(npc.width / 2) - (float)(tex.Width) / 2f + drawOrigin.X + offSet2.X * npc.scale, npc.position.Y - Main.screenPosition.Y + npc.height / 2 + offSet2.Y * npc.scale), new Rectangle?(rect2), drawColor, npc.rotation, drawOrigin, npc.scale, SpriteEffects.FlipHorizontally, 0f);

            return false;
        }

    }
}

