using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Bosses
{
    [AutoloadBossHead]
    public class SAXMutant : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SA-X");
            Main.npcFrameCount[npc.type] = 9;
            NPCID.Sets.TrailingMode[npc.type] = 0;
            NPCID.Sets.TrailCacheLength[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 170;
            npc.damage = 125;
            npc.defense = 16;
            npc.lifeMax = 200000;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath9;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.noTileCollide = false;
            npc.noGravity = true;
            npc.buffImmune[mod.BuffType("InfectedRed")] = true;
            npc.buffImmune[mod.BuffType("InfectedGreen")] = true;
            npc.buffImmune[mod.BuffType("InfectedBlue")] = true;
            npc.buffImmune[mod.BuffType("InfectedYellow")] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/VsSAX");
            npc.frameCounter = 0;
            npc.scale = 0.625f;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.625f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.75f);
        }
        public override bool PreNPCLoot()
        {
            return false;
        }


        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((int)npc.localAI[0]);
            writer.Write((int)npc.localAI[1]);
            writer.Write((int)npc.localAI[2]);
            writer.Write((int)npc.localAI[3]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            npc.localAI[0] = reader.ReadInt32();
            npc.localAI[1] = reader.ReadInt32();
            npc.localAI[2] = reader.ReadInt32();
            npc.localAI[3] = reader.ReadInt32();
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            if (npc.localAI[2] > 0)
            {
                npc.frameCounter++;
                if (npc.localAI[2] < 6)
                {
                    npc.frame.Y = 0;
                }
                npc.frame.X = 540;
                if (npc.frameCounter >= 6)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = (npc.frame.Y + 200);
                    if (npc.frame.Y > 1600)
                    {
                        npc.frame.Y = 1600;
                    }
                }
            }
            else if (npc.localAI[3] >= 100)
            {
                npc.frameCounter++;
                if (npc.localAI[3] < 110 || npc.localAI[3] == 230 || npc.localAI[3] == 280)
                {
                    npc.frame.Y = 0;
                }
                npc.frame.X = 720;
                if (npc.localAI[3] >= 230)
                {
                    npc.frame.X = 900;
                }
                if (npc.localAI[3] >= 280)
                {
                    npc.frame.X = 1080;
                }
                if (npc.frameCounter >= 10 || (npc.frameCounter >= 6 && npc.localAI[3] > 240))
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = (npc.frame.Y + 200);
                    if (npc.frame.Y > 1600)
                    {
                        npc.frame.Y = 1600;
                    }
                }
            }
            else if (npc.ai[1] > 0)
            {
                npc.frameCounter++;
                if (npc.ai[1] == 1)
                {
                    npc.frame.Y = 0;
                }
                npc.frame.X = 360;
                if (npc.frameCounter >= 6)
                {
                    npc.frameCounter = 0;
                    if (npc.velocity.Y < 0)
                    {
                        npc.frame.Y = npc.frame.Y <= 1400 ? 1600 : 1400;
                    }
                    else
                    {
                        npc.frame.Y = (npc.frame.Y + 200);
                        if (npc.frame.Y > 1000)
                        {
                            npc.frame.Y = 0;
                        }
                        if (npc.ai[1] > 36)
                        {
                            npc.frame.Y = 1200;
                        }
                    }
                }
            }
            else if (npc.ai[2] > 0)
            {
                npc.frame.X = 180;
                npc.frameCounter++;
                if (npc.ai[2] < 10)
                {
                    npc.frame.Y = 800;
                }
                else if (npc.ai[2] < 40)
                {
                    if (npc.frameCounter >= 7)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = npc.frame.Y <= 1000 ? 1200 : 1000;
                    }
                }
                else if (npc.ai[2] < 100)
                {
                    if (npc.ai[2] == 100)
                    {
                        npc.frame.Y = 1400;
                    }
                    if (npc.frameCounter >= 7)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = npc.frame.Y <= 1400 ? 1600 : 1400;
                    }
                }
                else
                {
                    npc.frame.Y = 800;
                }
            }
            else if (npc.ai[3] > 0)
            {
                npc.frame.X = 180;
                if (npc.ai[3] < 7)
                {
                    npc.frame.Y = 0;
                }
                else if (npc.ai[3] < 14)
                {
                    npc.frame.Y = 200;
                }
                else if (npc.ai[3] < 21)
                {
                    npc.frame.Y = 400;
                }
                else
                {
                    npc.frame.Y = 600;
                }
            }
            else if (npc.ai[0] > 0)
            {
                npc.frame.X = 0;
                if (npc.ai[0] < 7)
                {
                    npc.frame.Y = 800;
                }
                else if (npc.ai[0] < 14)
                {
                    npc.frame.Y = 1000;
                }
                else if (npc.ai[0] < 21)
                {
                    npc.frame.Y = 1200;
                }
                else if (npc.ai[0] < 28)
                {
                    npc.frame.Y = 1400;
                }
                else
                {
                    npc.frame.Y = 1600;
                }
            }
            else
            {
                npc.frameCounter++;
                npc.frame.X = 0;
                if (npc.frameCounter >= 9)
                {
                    npc.frameCounter = 0;
                    if (npc.velocity.Y < 0)
                    {
                        npc.frame.X = 360;
                        npc.frame.Y = npc.frame.Y <= 1400 ? 1600 : 1400;
                    }
                    else
                    {
                        npc.frame.Y = (npc.frame.Y + 200);
                        if (npc.frame.Y > 600)
                        {
                            npc.frame.Y = 0;
                        }
                    }
                }
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (npc.localAI[0] > 60)
            {
                return base.CanHitPlayer(target, ref cooldownSlot);
            }
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (npc.localAI[0] > 60)
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode && (npc.ai[1] >= 100 || npc.ai[2] >= 20))
            {
                target.wingTime = 0;
                target.rocketTime = 0;
                target.mount.Dismount(target);
                if (npc.ai[1] >= 120)
                {
                    target.velocity.Y = 10;
                }
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.localAI[0] > 90 && npc.localAI[0] < 120 && npc.localAI[2] <= 0 && npc.velocity.Y == 0 && damage > 1000)
            {
                npc.localAI[2]++;
                npc.netUpdate = true;
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (npc.localAI[0] <= 60)
            {
                damage = (int)(damage * (npc.localAI[0] / 60f));
                crit = false;
            }
        }
        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (npc.localAI[0] <= 60)
            {
                damage = (int)(damage * (npc.localAI[0] / 60f));
                crit = false;
            }
        }
        public override void AI()
        {
            npc.localAI[0]++;
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(false);
                P = Main.player[npc.target];
                if (!P.active || P.dead)
                {
                    npc.velocity = new Vector2(0f, -100f);
                    npc.active = false;
                    npc.netUpdate = true;
                }
            }
            if (npc.localAI[0] <= 60)
            {
                if (npc.localAI[0] == 1)
                {
                    npc.direction = (int)npc.ai[0];
                    npc.ai[0] = 0;
                }
                npc.scale = 0.625f + (npc.localAI[0] * 0.00625f);
                npc.width = (int)(100 * npc.scale);
                npc.height = (int)(170 * npc.scale);
                npc.position.X -= 0.625f / 2f;
                npc.position.Y -= 1.0625f;
                npc.netUpdate = true;
            }
            else
            {
                npc.width = 118;
                npc.height = 170;
                npc.scale = 1;

                if (npc.Center.Y < 666)
                {
                    npc.velocity.Y = 6;
                }
                #region Death
                if (npc.localAI[3] >= 100)
                {
                    npc.noTileCollide = false;
                    npc.dontTakeDamage = true;
                    npc.velocity.X = 0;
                    npc.localAI[3]++;
                    if (npc.localAI[2] <= 0)
                    {
                        if (npc.velocity.Y == 0 || npc.localAI[3] > 102)
                        {
                            npc.localAI[3]++;
                            npc.velocity.Y = 0;
                        }
                        if (npc.localAI[3] == 110)
                        {
                            Main.PlaySound(4, npc.Center, 10);
                        }
                    }
                    if (npc.localAI[3] >= 330f)
                    {
                        npc.life = 0;
                        npc.HitEffect(0, 0);
                        npc.checkDead();
                    }
                    if (npc.localAI[3] < 102)
                    {
                        npc.velocity.Y += 0.3f;
                        if (npc.velocity.Y > 15)
                        {
                            npc.velocity.Y = 15;
                        }
                    }
                    #endregion
                }
                else
                {
                    npc.dontTakeDamage = false;
                    if (npc.localAI[2] <= 0)
                    {
                        if (npc.Center.X < P.Center.X)
                        {
                            npc.direction = 1;
                        }
                        else
                        {
                            npc.direction = -1;
                        }
                    }
                    if (npc.ai[0] > 0)
                    {
                        npc.ai[0]++;
                    }
                    if (npc.ai[1] > 0)
                    {
                        npc.ai[1]++;
                    }
                    if (npc.ai[2] > 0)
                    {
                        npc.ai[2]++;
                    }
                    if (npc.ai[3] > 0)
                    {
                        npc.ai[3]++;
                    }
                    if (npc.ai[0] <= 0 && npc.ai[1] <= 0 && npc.ai[2] <= 0 && npc.ai[3] <= 0 && npc.localAI[0] > 130 && npc.localAI[2] <= 0)
                    {
                        if (Main.rand.NextBool(2) && npc.Distance(P.MountedCenter) < 400)
                        {
                            npc.ai[0]++;
                            //Acid Spit
                        }
                        else if ((Main.rand.NextBool(3) || npc.Distance(P.MountedCenter) > 800) && Math.Abs(P.MountedCenter.Y - npc.Center.Y) < 100)
                        {
                            npc.ai[2]++;
                            //Dash Punch
                        }
                        else if (Main.rand.NextBool(2) || npc.Distance(P.MountedCenter) > 800)
                        {
                            npc.ai[1]++;
                            //Jump
                        }
                        else
                        {
                            npc.ai[3]++;
                            //Missiles
                        }
                        npc.netUpdate = true;
                    }

                    #region Jump
                    if (npc.velocity.Y > 0 && npc.ai[1] > 30)
                    {
                        npc.localAI[1] = 1;
                        npc.noTileCollide = false;
                        if (P.position.Y > npc.position.Y + npc.height)
                        {
                            npc.noTileCollide = true;
                        }
                    }
                    if (npc.velocity.Y == 0)
                    {
                        if (npc.localAI[1] > 0)
                        {
                            if (Main.netMode != 1)
                            {
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 8f, 0, mod.ProjectileType("SaxWave"), 100, 0f, Main.myPlayer);
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -8f, 0, mod.ProjectileType("SaxWave"), 100, 0f, Main.myPlayer);
                            }
                            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 14);
                            for (int i = 0; i < 60; i++)
                            {
                                int dustType = 1;
                                int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
                                Dust dust = Main.dust[dustIndex];
                                dust.velocity.X = dust.velocity.X + Main.rand.Next(-12, 12);
                                dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-12, -5);
                            }
                            npc.localAI[1] = 0;
                            npc.ai[1] = 0;
                            npc.frame.Y = 0;
                            npc.velocity.X = 0;
                            npc.localAI[0] = 110;
                        }
                    }
                    if (npc.ai[1] >= 40 && npc.ai[1] < 100)
                    {
                        Vector2 predictedPos = P.MountedCenter + P.velocity + (new Vector2(P.velocity.X, 0) * (Vector2.Distance(P.MountedCenter, npc.Center) / 30));
                        predictedPos = P.MountedCenter + P.velocity + (new Vector2(P.velocity.X, 0) * (Vector2.Distance(predictedPos, npc.Center) / 30));
                        predictedPos = P.MountedCenter + P.velocity + (new Vector2(P.velocity.X, 0) * (Vector2.Distance(predictedPos, npc.Center) / 30));
                        Vector2 jumpos = predictedPos + new Vector2(0f, -500f);
                        if (npc.velocity.Y == 0)
                        {
                            npc.velocity = npc.DirectionTo(jumpos) * 30;
                            npc.velocity.Y = -(float)Math.Sqrt(2 * 0.3f * Math.Abs((jumpos.Y) - (npc.position.Y + npc.height)));
                            npc.frame.Y = 1400;
                            Main.PlaySound(42, npc.Center, 214);
                        }
                        npc.noTileCollide = true;
                        if (Math.Abs(npc.Center.Y - jumpos.Y) < 200)
                        {
                            if (npc.velocity.X > 0)
                            {
                                npc.direction = 1;
                                if (npc.Center.X > P.Center.X)
                                {
                                    npc.ai[1] = 100;
                                }
                            }
                            else
                            {
                                npc.direction = -1;
                                if (npc.Center.X < P.Center.X)
                                {
                                    npc.ai[1] = 100;
                                }
                            }
                        }
                    }
                    if (npc.ai[1] == 100)
                    {
                        Vector2 predictedPos = P.MountedCenter + P.velocity + (new Vector2(P.velocity.X, 0) * (Vector2.Distance(P.MountedCenter, npc.Center) / 15));
                        predictedPos = P.MountedCenter + P.velocity + (new Vector2(P.velocity.X, 0) * (Vector2.Distance(predictedPos, npc.Center) / 15));
                        predictedPos = P.MountedCenter + P.velocity + (new Vector2(P.velocity.X, 0) * (Vector2.Distance(predictedPos, npc.Center) / 15));
                        npc.velocity = npc.DirectionTo(predictedPos) * 15;
                        npc.damage = (Main.expertMode ? 375 : 250);
                    }
                    if (npc.ai[1] > 100)
                    {
                        if (P.position.Y <= npc.position.Y + npc.height)
                        {
                            npc.noTileCollide = false;
                        }
                    }
                    npc.damage = (Main.expertMode ? 188 : 125);
                    npc.velocity.Y += 0.3f;
                    if (npc.velocity.Y > 15f)
                    {
                        npc.velocity.Y = 15f;
                    }

                    #endregion

                    #region Dash Punch
                    if (npc.ai[2] == 2)
                    {
                        Main.PlaySound(SoundID.Item13, npc.Center);
                    }
                    if (npc.ai[2] == 20)
                    {
                        Main.PlaySound(42, npc.Center, 212);
                    }
                    if (npc.ai[2] == 40)
                    {
                        float vel = 20;
                        if (Math.Abs(P.Center.X - npc.Center.X) > 800)
                        {
                            Vector2 predictedPos = P.MountedCenter + P.velocity + (new Vector2(P.velocity.X, 0) * (Vector2.Distance(P.MountedCenter, npc.Center) / 40));
                            predictedPos = P.MountedCenter + P.velocity + (new Vector2(P.velocity.X, 0) * (Vector2.Distance(predictedPos, npc.Center) / 40));
                            predictedPos = P.MountedCenter + P.velocity + (new Vector2(P.velocity.X, 0) * (Vector2.Distance(predictedPos, npc.Center) / 40));
                            vel = Math.Abs(predictedPos.X - npc.Center.X) / 40;
                        }
                        npc.velocity.X = npc.direction * vel;
                        Vector2 position = npc.Center + new Vector2(24 * npc.direction, -6);
                        Vector2 velocity = new Vector2(10 * npc.direction, 3);
                        int type = mod.ProjectileType("SAXMissile");
                        int damage = 40;
                        int numberProjectiles = 5;
                        float spread = 90;
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(spread));
                            float scale = 1f - (Main.rand.NextFloat() * .3f);
                            perturbedSpeed = perturbedSpeed * scale;
                            if (Main.netMode != 1)
                            {
                                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 4, Main.myPlayer);
                            }
                        }
                        Main.PlaySound(42, npc.Center, 216);
                        Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/MissileShoot"));
                    }
                    if (npc.ai[2] > 40 && npc.ai[2] < 100)
                    {
                        npc.velocity.Y = 0;
                        npc.noTileCollide = true;
                        if (npc.velocity.X > 0)
                        {
                            npc.direction = 1;
                            if (npc.position.X > P.position.X + P.width)
                            {
                                npc.ai[2] = 100;
                                npc.velocity.X = 0;
                            }
                        }
                        else
                        {
                            npc.direction = -1;
                            if (npc.position.X + npc.width < P.position.X)
                            {
                                npc.ai[2] = 100;
                                npc.velocity.X = 0;
                            }
                        }
                        npc.damage = (Main.expertMode ? 375 : 250);
                    }
                    npc.damage = (Main.expertMode ? 188 : 125);
                    if (npc.ai[2] > 100)
                    {
                        npc.velocity.X = 0;
                        npc.noTileCollide = false;
                    }
                    if (npc.ai[2] > 110)
                    {
                        npc.ai[2] = 0;
                        npc.localAI[0] = 110;
                    }
                    #endregion

                    #region Missiles
                    if (npc.ai[3] > 0)
                    {
                        Vector2 position = npc.Center;
                        Vector2 velocity = Vector2.Zero;
                        int type = mod.ProjectileType("SAXMissile");
                        int damage = 40;
                        int numberProjectiles = 4;
                        float spread = 75;
                        if (npc.ai[3] == 10)
                        {
                            position = npc.Center + new Vector2(-50 * npc.direction, -53);
                            velocity = new Vector2(-10 * npc.direction, -3);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(spread));
                                float scale = 1f - (Main.rand.NextFloat() * .3f);
                                perturbedSpeed = perturbedSpeed * scale;
                                if (Main.netMode != 1)
                                {
                                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 4, Main.myPlayer);
                                }
                            }
                            Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/MissileShoot"));
                        }
                        if (npc.ai[3] == 17)
                        {
                            position = npc.Center + new Vector2(-4 * npc.direction, -75);
                            velocity = new Vector2(3 * npc.direction, -10);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(spread));
                                float scale = 1f - (Main.rand.NextFloat() * .3f);
                                perturbedSpeed = perturbedSpeed * scale;
                                if (Main.netMode != 1)
                                {
                                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 4, Main.myPlayer);
                                }
                            }
                            Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/MissileShoot"));
                        }
                        if (npc.ai[3] == 24)
                        {
                            position = npc.Center + new Vector2(20 * npc.direction, -35);
                            velocity = new Vector2(10 * npc.direction, 3);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(spread));
                                float scale = 1f - (Main.rand.NextFloat() * .3f);
                                perturbedSpeed = perturbedSpeed * scale;
                                if (Main.netMode != 1)
                                {
                                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 4, Main.myPlayer);
                                }
                            }
                            Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/MissileShoot"));
                        }
                        if (npc.ai[3] > 28)
                        {
                            npc.ai[3] = 0;
                            npc.localAI[0] = 120;
                        }
                    }
                    #endregion

                    #region Acid Spit
                    if (npc.ai[0] == 28)
                    {
                        Vector2 position = npc.Center + new Vector2(30 * npc.direction, -9);
                        Vector2 velocity = new Vector2(15 * npc.direction, -4.5f);
                        int type = mod.ProjectileType("SAXSpit");
                        int damage = 30;
                        int numberProjectiles = 8;
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(90));
                            float scale = 1f - (Main.rand.NextFloat() * .3f);
                            perturbedSpeed = perturbedSpeed * scale;
                            if (Main.netMode != 1)
                            {
                                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 2, Main.myPlayer);
                            }
                        }
                        position = npc.Center + new Vector2(-42 * npc.direction, -70);
                        velocity = new Vector2(-10 * npc.direction, 3);
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(position.X, position.Y, velocity.X, velocity.Y, type, damage, 2, Main.myPlayer);
                        }
                        Main.PlaySound(4, npc.Center, 13);
                        Main.PlaySound(4, npc.Center, 9);
                    }
                    if (npc.ai[0] > 35)
                    {
                        npc.ai[0] = 0;
                        npc.localAI[0] = 120;
                    }
                    #endregion
                }

                #region Damaged
                if (npc.localAI[2] > 0)
                {
                    npc.localAI[2]++;
                    npc.velocity.X = 0;
                    if (npc.localAI[2] == 18)
                    {
                        Main.PlaySound(15, npc.Center, 0);
                    }
                    if (npc.localAI[2] > 54)
                    {
                        npc.localAI[2] = 0;
                        npc.localAI[0] = 150;
                    }
                }
                #endregion
            }
        }
        public override bool PreDraw(SpriteBatch spritebatch, Color drawColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (npc.direction == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                effects = SpriteEffects.None;
            }

            Color color = Lighting.GetColor((int)(npc.Center.X / 16), (int)(npc.Center.Y / 16));

            int xFrameCount = 7;
            Texture2D texture = Main.npcTexture[npc.type];
            Rectangle rectangle = new Rectangle(npc.frame.X, npc.frame.Y, (texture.Width / xFrameCount), (texture.Height / Main.npcFrameCount[npc.type]));
            Vector2 vector = new Vector2(((texture.Width / xFrameCount) / 2f), ((texture.Height / Main.npcFrameCount[npc.type]) / 2f));
            float yOffset = (((1f - npc.scale) * 12f) + 4f + 31.875f) - ((npc.scale - 0.625f) * 85);
            Vector2 drawPosition  = new Vector2(npc.position.X - Main.screenPosition.X + (float)(npc.width / 2) - (float)(texture.Width / xFrameCount) / 2f + vector.X, npc.position.Y - Main.screenPosition.Y + (float)npc.height - (float)(texture.Height / Main.npcFrameCount[npc.type]) + yOffset + vector.Y);
            if ((npc.ai[1] >= 100 || npc.ai[2] >= 40) && npc.localAI[3] < 100)
            {
                for (int i = 0; i < npc.oldPos.Length; i++)
                {
                    Color color2 = color * ((npc.oldPos.Length - i) / (float)npc.oldPos.Length) * 0.5f;
                    Vector2 drawPos = new Vector2(npc.oldPos[i].X - Main.screenPosition.X + (float)(npc.width / 2) - (float)(texture.Width / xFrameCount) / 2f + vector.X, npc.oldPos[i].Y - Main.screenPosition.Y + (float)npc.height - (float)(texture.Height / Main.npcFrameCount[npc.type]) + yOffset + vector.Y);
                    spritebatch.Draw(texture, drawPos, rectangle, color2, npc.rotation, vector, npc.scale, effects, 0f);
                }
            }
            spritebatch.Draw(texture, drawPosition, new Rectangle?(rectangle), color, npc.rotation, vector, npc.scale, effects, 0f);
            return false;
        }
        public override bool CheckDead()
        {
            if (npc.localAI[3] < 100 && npc.localAI[0] > 60)
            {
                npc.localAI[3] = 100;
                npc.localAI[2] = 1;
                npc.frameCounter = 0;
                npc.frame.Y = 0;
                npc.frame.X = 540;
                npc.velocity.X = 0;
                npc.velocity.Y = 10;
                npc.damage = 0;
                npc.life = npc.lifeMax;
                npc.dontTakeDamage = true;
                npc.netUpdate = true;
                npc.noTileCollide = false;
                return false;
            }
            if (Main.netMode != 1)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + 40, mod.NPCType("SAXCoreX"));
            }
            //Main.NewText("The SA-X shimmers and reverts to its true form!", 175, 75, 225);

            return true;
        }
    }
}

