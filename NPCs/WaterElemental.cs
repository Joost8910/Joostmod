using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Items.Materials;
using Terraria.GameContent.ItemDropRules;
using JoostMod.Items.Placeable;

namespace JoostMod.NPCs
{
    public class WaterElemental : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Elemental");
            Main.npcFrameCount[NPC.type] = 6;
            NPCID.Sets.TrailingMode[NPC.type] = 3;
            NPCID.Sets.TrailCacheLength[NPC.type] = 6;
        }
        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 50;
            NPC.damage = 30;
            NPC.defense = 24;
            NPC.lifeMax = 400;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = Item.buyPrice(0, 0, 7, 50);
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.frameCounter = 0;
            Banner = NPC.type;
            BannerItem = Mod.Find<ModItem>("WaterElementalBanner").Type;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.alpha = 50;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            //I think elementals dropping multiple stacks would make them more aesthetically pleasing to kill
            int essence = ModContent.ItemType<WaterEssence>();
            var parameters = new DropOneByOne.Parameters()
            {
                ChanceNumerator = 1,
                ChanceDenominator = 1,
                MinimumStackPerChunkBase = 2,
                MaximumStackPerChunkBase = 6,
                MinimumItemDropsCount = 8,
                MaximumItemDropsCount = 20,
            };
            var expertParamaters = parameters;
            expertParamaters.MinimumItemDropsCount = 12;
            expertParamaters.MaximumItemDropsCount = 30;
            npcLoot.Add(new DropBasedOnExpertMode(new DropOneByOne(essence, parameters), new DropOneByOne(essence, expertParamaters)));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecondAnniversary>(), 50));
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (NPC.ai[1] >= 20)
            {
                damage *= 2;
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 30; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 103, 2.5f * (float)hitDirection, Main.rand.Next(-5, 5), 0, default(Color), 0.7f);
                }
            }
            for (int k = 0; k < 4; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, 103, 2.5f * (float)hitDirection, Main.rand.Next(-5, 5), 0, default(Color), 0.7f);
            }
            //npc.ai[2] += (float)(damage / 3);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.PlayerInTown && ((!spawnInfo.Invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse) || spawnInfo.SpawnTileY > Main.worldSurface) && spawnInfo.Water && Main.hardMode ? 0.065f : 0f;
        }
        public override void AI()
        {
            NPC.ai[0]++;
            Player P = Main.player[NPC.target];
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            NPC.direction = NPC.Center.X < P.Center.X ? 1 : -1;
            NPC.directionY = NPC.Center.Y < P.Center.Y ? 1 : -1;
            NPC.spriteDirection = 1;
            NPC.wet = false;
            NPC.wetCount = 0;
            NPC.rotation = 0;
            if (NPC.ai[1] > 0)
            {
                NPC.ai[1]++;
                float speed = Main.expertMode ? 15 : 10;
                NPC.height = 40;
                if (NPC.ai[1] == 20)
                {
                    SoundEngine.PlaySound(SoundID.Splash, NPC.Center);
                }
                if (NPC.ai[1] == 40)
                {
                    NPC.velocity = NPC.DirectionTo(P.Center) * speed;
                    NPC.knockBackResist = Main.expertMode ? 0f : 0.01f;
                    SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_book_staff_cast_0"), NPC.Center); //201
                }
                if (NPC.ai[1] > 40)
                {
                    if ((int)NPC.ai[1] % 6 == 0)
                    {
                        SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_ghastly_glaive_pierce_1"), NPC.Center); // 205
                    }
                    Vector2 vel = new Vector2(speed * NPC.direction, speed * NPC.directionY);
                    if (vel.Length() > speed)
                    {
                        vel *= speed / vel.Length();
                    }
                    if (NPC.velocity.X == 0 && Math.Abs(NPC.oldVelocity.X) > 1)
                    {
                        NPC.velocity.X = -NPC.oldVelocity.X;
                        SoundEngine.PlaySound(SoundID.SplashWeak, NPC.Center);
                    }
                    if (NPC.velocity.Y == 0 && Math.Abs(NPC.oldVelocity.Y) > 1)
                    {
                        NPC.velocity.Y = -NPC.oldVelocity.Y;
                        SoundEngine.PlaySound(SoundID.SplashWeak, NPC.Center);
                    }
                    float home = 50f;
                    if (vel != Vector2.Zero)
                    {
                        NPC.velocity = ((home - 1f) * NPC.velocity + vel) / home;
                    }
                    if (NPC.ai[1] > 100)
                    {
                        NPC.height = 50;
                        NPC.ai[0] = 0;
                        NPC.ai[1] = 0;
                        NPC.knockBackResist = Main.expertMode ? 0.45f : 0.5f;
                    }
                    NPC.rotation = NPC.velocity.ToRotation() - 90 * 0.0174f;
                }
                else 
                {
                    NPC.rotation = NPC.DirectionTo(P.Center).ToRotation() - 90 * 0.0174f;
                }
            }
            else if (NPC.ai[3] == 0)
            {
                if (NPC.velocity.X * NPC.direction < 4)
                {
                    NPC.velocity.X += 0.05f * NPC.direction;
                }
                if (NPC.velocity.Y * NPC.directionY < 3)
                {
                    NPC.velocity.Y += 0.1f * NPC.directionY;
                }
                if (NPC.ai[0] > 300 && Collision.CanHitLine(NPC.position, 1, 1, P.Center, 1, 1))
                {
                    NPC.ai[1]++;
                    NPC.velocity = Vector2.Zero;
                }
            }
            if (Main.expertMode)
            {
                NPC.ai[2]++;
                if (NPC.ai[2] > 500 && NPC.ai[0] > 60 && NPC.ai[1] == 0 && NPC.ai[3] == 0)
                {
                    NPC.ai[3] = 1;
                }
                if (NPC.ai[3] == 1)
                {
                    NPC.directionY = NPC.Center.Y < P.Center.Y - 150 ? 1 : -1;
                    if (NPC.velocity.X * NPC.direction < 6)
                    {
                        NPC.velocity.X += 0.15f * NPC.direction;
                    }
                    if (NPC.velocity.Y * NPC.directionY < 5)
                    {
                        NPC.velocity.Y += 0.3f * NPC.directionY;
                    }
                    if (NPC.Distance(new Vector2(P.Center.X, P.Center.Y - 150)) < 40 && Collision.CanHitLine(NPC.position, 1, 1, P.Center, 1, 1))
                    {
                        NPC.ai[3] = 2;
                        NPC.velocity.Y = 0;
                        SoundEngine.PlaySound(SoundID.Splash.WithPitchOffset(-0.6f), NPC.Center);
                    }
                    NPC.knockBackResist = 0f;
                }
                if (NPC.ai[3] >= 2)
                {
                    NPC.ai[3]++;
                    if (NPC.ai[3] < 80)
                    {
                        NPC.directionY = NPC.Center.Y < P.Center.Y - 150 ? 1 : -1;
                        if (NPC.velocity.X * NPC.direction < 2)
                        {
                            NPC.velocity.X += 0.5f * NPC.direction;
                        }
                        if (NPC.velocity.Y * NPC.directionY < 2)
                        {
                            NPC.velocity.Y += 0.3f * NPC.directionY;
                        }

                        NPC.scale = 1f + (NPC.ai[3] / 80f);
                        NPC.width = (int)(NPC.scale * 40);
                        NPC.height = (int)(NPC.scale * 50);
                        
                        for (int d = 0; d < NPC.ai[3]; d++)
                        {
                            Dust dust = Dust.NewDustDirect(NPC.Center - new Vector2(10, 10), 20, 20, 172, NPC.velocity.X * 0.8f, NPC.velocity.Y * 0.8f, 0, default(Color), NPC.scale);
                            Vector2 vel = NPC.Center - dust.position;
                            vel.Normalize();
                            dust.position -= vel * 60;
                            dust.velocity = vel * 10 + NPC.velocity;
                            dust.noGravity = true;
                        }
                    }
                    else
                    {
                        NPC.velocity.Y = 0;
                        if (NPC.velocity.X * NPC.direction < 6f)
                        {
                            NPC.velocity.X += 0.04f * NPC.direction;
                        }
                        NPC.scale = 2f - ((NPC.ai[3] - 80) / 110f);
                        NPC.width = (int)(NPC.scale * 40);
                        NPC.height = (int)(NPC.scale * 50);

                        if (NPC.ai[3] % 10 == 0)
                        {
                            SoundEngine.PlaySound(SoundID.Item13.WithPitchOffset(-0.3f), NPC.Center);
                        }
                        
                        if (NPC.ai[3] % 2 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            Vector2 pos = NPC.Center;
                            pos.X += Main.rand.NextFloat(-NPC.width / 2f, NPC.width / 2f);
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), pos, new Vector2(0, 10), ProjectileID.RainNimbus, 20, 1);
                        }
                        if (NPC.ai[3] > 190)
                        {
                            NPC.knockBackResist = 0.5f;
                            NPC.ai[2] = 0;
                            NPC.ai[3] = 0;
                            if (NPC.ai[0] > 200)
                            {
                                NPC.ai[0] = 0;
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

            NPC.frameCounter++;
            if (NPC.frameCounter >= 5)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y = (NPC.frame.Y + frameHeight);
            }
            if (NPC.frame.Y >= frameHeight * 6)
            {
                NPC.frame.Y = 0;
            }
            if (NPC.ai[3] > 50)
            {
                NPC.frameCounter++;
            }
            NPC.frame.X = 0;
            if (NPC.ai[1] > 0)
            {
                NPC.frame.X = frameWidth;
            }
            if (NPC.ai[1] > 10)
            {
                NPC.frame.X = frameWidth * 2;
            }
            if (NPC.ai[1] > 20)
            {
                NPC.frame.X = frameWidth * 3;
            }
            if (NPC.ai[1] > 40)
            {
                NPC.frameCounter++;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            int xFrameCount = 4;
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Rectangle rect = new Rectangle((int)NPC.frame.X, (int)NPC.frame.Y, (texture.Width / xFrameCount), (texture.Height / Main.npcFrameCount[NPC.type]));
            Vector2 vect = new Vector2((float)((texture.Width / xFrameCount) / 2), (float)((texture.Height / Main.npcFrameCount[NPC.type]) / 2));
            drawColor *= 0.8f;

            if (NPC.ai[1] >= 40)
            {
                for (int i = 0; i < NPC.oldPos.Length; i++)
                {
                    Color color2 = drawColor * ((NPC.oldPos.Length - i) / (float)NPC.oldPos.Length) * 0.8f;
                    Vector2 drawPos = NPC.oldPos[i] - Main.screenPosition + new Vector2(NPC.width / 2, NPC.height / 2) + new Vector2(0f, NPC.gfxOffY);
                    spriteBatch.Draw(texture, drawPos, rect, color2, NPC.oldRot[i], vect, NPC.scale, SpriteEffects.None, 0f);
                }
            }
            spriteBatch.Draw(texture, new Vector2(NPC.position.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)(texture.Width / xFrameCount) / 2f + vect.X, NPC.position.Y - Main.screenPosition.Y + (float)NPC.height - (float)(texture.Height / Main.npcFrameCount[NPC.type]) + 4f + vect.Y), new Rectangle?(rect), drawColor, NPC.rotation, vect, NPC.scale, SpriteEffects.None, 0f);

            Texture2D tex = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/NPCs/WaterElemental_Hand");
            int frame = NPC.frame.Y / 62;
            Rectangle rect2 = new Rectangle(0, frame * (tex.Height / 6), tex.Width, tex.Height / 6);
            Vector2 drawOrigin = new Vector2((tex.Width / 2), ((tex.Height / 6) / 2));

            Vector2 offSet = new Vector2(-23, -24);
            Vector2 offSet2 = new Vector2(23, -24);
            if (NPC.ai[3] > 2)
            {
                if (NPC.ai[3] < 65)
                {
                    float lerpAmount = NPC.ai[3] < 50 ? NPC.ai[3] / 50f : 1f;
                    offSet = Vector2.Lerp(offSet, offSet + new Vector2(10, -30), lerpAmount);
                    offSet2 = Vector2.Lerp(offSet2, offSet2 + new Vector2(-10, -30), lerpAmount);
                }
                else if (NPC.ai[3] < 180)
                {
                    float lerpAmount = NPC.ai[3] < 80 ? (NPC.ai[3] - 65) / 15f : 1f;
                    offSet = Vector2.Lerp(offSet + new Vector2(10, -30), offSet + new Vector2(-6, 50), lerpAmount);
                    offSet2 = Vector2.Lerp(offSet2 + new Vector2(-10, -30), offSet2 + new Vector2(6, 50), lerpAmount);
                }
                else
                {
                    float lerpAmount = NPC.ai[3] < 190 ? (NPC.ai[3] - 180) / 10f : 1f;
                    offSet = Vector2.Lerp(offSet + new Vector2(-6, 50), offSet, lerpAmount);
                    offSet2 = Vector2.Lerp(offSet2 + new Vector2(6, 50), offSet2, lerpAmount);
                }
            }

            offSet = offSet.RotatedBy(NPC.rotation);
            offSet2 = offSet2.RotatedBy(NPC.rotation);
            spriteBatch.Draw(tex, new Vector2(NPC.position.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)(tex.Width) / 2f + drawOrigin.X + offSet.X * NPC.scale, NPC.position.Y - Main.screenPosition.Y + NPC.height / 2 + offSet.Y * NPC.scale), new Rectangle?(rect2), drawColor, NPC.rotation, drawOrigin, NPC.scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(tex, new Vector2(NPC.position.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)(tex.Width) / 2f + drawOrigin.X + offSet2.X * NPC.scale, NPC.position.Y - Main.screenPosition.Y + NPC.height / 2 + offSet2.Y * NPC.scale), new Rectangle?(rect2), drawColor, NPC.rotation, drawOrigin, NPC.scale, SpriteEffects.FlipHorizontally, 0f);

            return false;
        }

    }
}

