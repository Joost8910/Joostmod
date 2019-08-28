using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Bosses
{
    [AutoloadBossHead]
    public class SAX : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SA-X");
            Main.npcFrameCount[npc.type] = 22;
        }
        public override void SetDefaults()
        {
            npc.width = 36;
            npc.height = 78;
            npc.damage = 100;
            npc.defense = 50;
            npc.lifeMax = 200000;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.buffImmune[mod.BuffType("InfectedRed")] = true;
            npc.buffImmune[mod.BuffType("InfectedGreen")] = true;
            npc.buffImmune[mod.BuffType("InfectedBlue")] = true;
            npc.buffImmune[mod.BuffType("InfectedYellow")] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/VsSAX");
            npc.frameCounter = 0;
            npc.noGravity = true;
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
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (npc.ai[1] >= 1)
            {
                damage = damage / 2;
                crit = false;
            }
        }
        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (npc.ai[1] >= 1)
            {
                damage = damage / 2;
                crit = false;
            }
        }
        public override void AI()
        {
            if (npc.localAI[3] <= 0)
            {
                if (npc.Center.Y < 666)
                {
                    npc.velocity.Y = 6;
                }
                Player P = Main.player[npc.target];
                if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
                {
                    npc.TargetClosest(false);
                    P = Main.player[npc.target];
                    if (!P.active || P.dead)
                    {
                        npc.velocity = new Vector2(0f, -100f);
                        npc.active = false;
                    }
                }
                if (npc.frame.Y < 312 || (npc.frame.Y > 312 && npc.frame.Y < 702))
                {
                    npc.soundDelay = 0;
                }
                if ((npc.frame.Y == 312 || npc.frame.Y == 702) && npc.soundDelay <= 0 && npc.ai[1] <= 0 && npc.localAI[1] <= 0)
                {
                    Main.PlaySound(SoundLoader.customSoundType, (int)npc.position.X, (int)npc.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/SAXFootstep"), 0.6f, 0.2f);
                    npc.soundDelay = 1;
                }
                int yOff = -4;
                if (npc.frame.Y >= 0)
                {
                    yOff -= 2;
                }
                if (npc.frame.Y >= 78)
                {
                    yOff -= 2;
                }
                if (npc.frame.Y >= 156)
                {
                    yOff += 2;
                }
                if (npc.frame.Y >= 234)
                {
                    yOff += 2;
                }
                if (npc.frame.Y >= 390)
                {
                    yOff -= 2;
                }
                if (npc.frame.Y >= 624)
                {
                    yOff += 2;
                }
                Vector2 gunCenter = npc.Center + new Vector2(npc.scale * npc.direction * -2, npc.scale * yOff);
                if (npc.ai[0] <= 30)
                {
                    if (npc.Center.X < P.Center.X)
                    {
                        npc.direction = 1;
                    }
                    else
                    {
                        npc.direction = -1;
                    }
                    PredictiveAim(24f, gunCenter);
                }
                npc.spriteDirection = npc.direction;
                if (npc.velocity.X == 0)
                {
                    npc.ai[1] = 0;
                }
                if (npc.position.Y > P.Center.Y + 250 && npc.ai[1] <= 0 && npc.velocity.Y == 0 && ((npc.ai[0] > 40 && npc.ai[2] < 10) || (npc.ai[0] > 70 && npc.ai[2] >= 10 && npc.ai[2] < 12) || (npc.ai[0] > 145 && npc.ai[2] >= 12)))
                {
                    Vector2 predictedPos = P.MountedCenter + P.velocity + (P.velocity * (npc.Distance(P.MountedCenter) / 24));
                    predictedPos = P.MountedCenter + P.velocity + (P.velocity * (npc.Distance(predictedPos) / 24));
                    predictedPos = P.MountedCenter + P.velocity + (P.velocity * (npc.Distance(predictedPos) / 24));
                    if (P.velocity.X * npc.direction <= 0)
                    {
                        Vector2 vel = npc.DirectionTo(predictedPos) * (npc.Distance(predictedPos) / 24);
                        npc.velocity.X = vel.X;
                    }
                    else
                    {
                        npc.velocity.X = (7 * npc.direction) + P.velocity.X;
                    }
                    npc.velocity.Y = -(float)Math.Sqrt(2 * 0.3f * Math.Abs((P.MountedCenter.Y + P.velocity.Y) - (npc.position.Y + npc.height)));
                    npc.frame.Y = 702;
                    npc.ai[0] = 0;
                    npc.ai[1] = 1;
                    npc.ai[2] = Main.rand.Next(13);
                    Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/SAXJump"));
                }
                if (npc.velocity.Y == 0)
                {
                    if (npc.velocity.X == 0 && npc.oldVelocity.X != 0 && npc.ai[1] <= 0)
                    {
                        npc.velocity.Y = -9f;
                        npc.velocity.X = 6f * npc.direction;
                        Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/SAXJump"));
                    }
                    npc.ai[1] = 0;
                }
                if (npc.ai[1] > 250)
                {
                    npc.ai[1] = 0;
                }
                if (P.position.Y > npc.position.Y + npc.height && npc.localAI[1] <= 0)
                {
                    npc.position.Y++;
                }
                npc.netUpdate = true;
                if (npc.ai[1] <= 0)
                {
                    npc.ai[0]++;
                    if (Math.Abs(npc.Center.X - P.Center.X) > 80)
                    {
                        if (npc.velocity.Y == 0)
                        {
                            if ((npc.velocity.X < 7 && npc.direction > 0) || (npc.velocity.X > -7 && npc.direction < 0))
                            {
                                npc.velocity.X += 0.5f * npc.direction;
                            }
                        }
                        else
                        {
                            if (npc.velocity.X < 7 && npc.velocity.X > -7)
                            {
                                npc.velocity.X += 0.5f * npc.direction;
                            }
                        }
                    }
                    else if (npc.velocity.Y == 0)
                    {
                        npc.velocity.X = 0;
                        if (npc.ai[0] == 10 && npc.localAI[1] <= 0)
                        {
                            npc.localAI[1]++;
                        }
                    }
                    if (Math.Abs(npc.Center.X - P.Center.X) > 300)
                    {
                        if (npc.Center.X < P.Center.X)
                        {
                            npc.direction = 1;
                        }
                        else
                        {
                            npc.direction = -1;
                        }
                        //npc.velocity.X = (7 * npc.direction) + (P.velocity.X * npc.direction > 0 ? P.velocity.X : 0);
                        npc.velocity.X = (7 * npc.direction) * (Math.Abs(npc.Center.X - P.Center.X) / 300);
                    }
                }
                if (npc.ai[0] >= 30)
                {
                    npc.ai[1] = 0;
                    if (P.HasBuff(BuffID.Frozen) && npc.ai[0] == 30)
                    {
                        npc.ai[2] = 10;
                    }
                    if (npc.ai[2] < 10)
                    {
                        float vel = 12f;
                        if (npc.velocity.Length() > vel)
                        {
                            vel += (npc.velocity.Length() - vel) / 2;
                        }
                        if (npc.ai[0] < 30)
                        {
                            PredictiveAim(vel * 2, gunCenter);
                        }
                        if (npc.ai[0] == 30)
                        {
                            npc.ai[0]++;
                            int damage = 50;
                            int type = mod.ProjectileType("SAXBeam");
                            float rotation = npc.ai[3];
                            Vector2 aim = new Vector2((float)((Math.Cos(rotation)) * -1), (float)((Math.Sin(rotation)) * -1));
                            Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/IceBeam"));
                            if (Main.netMode != 1)
                            {
                                Projectile.NewProjectile(gunCenter + (aim * 10 * npc.scale), aim * vel, type, damage, 0f, Main.myPlayer);
                            }
                        }
                        if (npc.ai[0] > 45)
                        {
                            npc.ai[0] = 0;
                            npc.ai[2] += 1 + Main.rand.Next(4);
                            npc.netUpdate = true;
                        }
                    }
                    else if (npc.ai[2] >= 10 && npc.ai[2] < 12)
                    {
                        float vel = 14f;
                        if (npc.velocity.Length() > vel)
                        {
                            vel += (npc.velocity.Length() - vel) / 2;
                        }
                        if (npc.ai[0] < 60)
                        {
                            PredictiveAim(vel * 2, gunCenter);
                        }
                        if (npc.ai[0] == 30)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/MissileClick"));
                            npc.ai[0]++;
                        }
                        if (npc.ai[0] == 60)
                        {
                            npc.ai[0]++;
                            int damage = 75;
                            int type = mod.ProjectileType("SAXSuperMissile");
                            float rotation = npc.ai[3];
                            Vector2 aim = new Vector2((float)((Math.Cos(rotation)) * -1), (float)((Math.Sin(rotation)) * -1));
                            Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/SuperMissileShoot"));
                            if (Main.netMode != 1)
                            {
                                Projectile.NewProjectile(gunCenter + (aim * 26 * npc.scale), aim * vel, type, damage, 10, Main.myPlayer);
                            }
                        }
                        if (npc.ai[0] > 75)
                        {
                            npc.ai[0] = 0;
                            npc.ai[2] = 0;
                        }
                    }
                    else
                    {
                        float vel = 12f;
                        if (npc.velocity.Length() > vel)
                        {
                            vel += (npc.velocity.Length() - vel) / 2;
                        }
                        if (npc.ai[0] < 120)
                        {
                            PredictiveAim(vel * 2, gunCenter);
                        }
                        if (npc.ai[0] == 30)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ChargingStart"));
                            npc.ai[0]++;
                            npc.localAI[0] = 0;
                        }
                        if (npc.ai[0] == 55)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ChargingStart2"));
                            npc.ai[0]++;
                        }
                        if (npc.ai[0] >= 80 && npc.ai[0] % 15 == 5 && npc.ai[0] < 120)
                        {
                            Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ChargingLoop"));
                        }
                        if ((npc.ai[0] % 4) == 0 && npc.ai[0] < 78)
                        {
                            npc.localAI[0] = (npc.localAI[0] + 1) % 12;
                        }
                        if (npc.ai[0] >= 78 && (npc.ai[0] % 4) == 0)
                        {
                            npc.localAI[0] = (npc.localAI[0] % 3) + 10;
                        }
                        if (npc.ai[0] == 120)
                        {
                            npc.ai[0]++;
                            int damage = 100;
                            int type = mod.ProjectileType("SAXBeamCharged");
                            float rotation = npc.ai[3];
                            Vector2 aim = new Vector2((float)((Math.Cos(rotation)) * -1), (float)((Math.Sin(rotation)) * -1));
                            Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/IceBeamCharged"));
                            if (Main.netMode != 1)
                            {
                                Projectile.NewProjectile(gunCenter + (aim * 10 * npc.scale), aim * vel, type, damage, 0f, Main.myPlayer);
                            }
                        }
                        if (npc.ai[0] > 150)
                        {
                            npc.ai[0] = 0;
                            npc.ai[2] = 0;
                        }
                    }
                    if (npc.ai[0] > 150)
                    {
                        npc.ai[0] = 0;
                        npc.ai[2] = 0;
                    }
                }
                if (npc.localAI[1] >= 1)
                {
                    if (npc.localAI[1] == 1)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/MorphBall"));
                        npc.position.Y += 44;
                    }
                    npc.ai[0] = 0;
                    npc.ai[1] = 0;
                    npc.localAI[1]++;
                    npc.velocity.X = 0;
                    if (npc.velocity.Y < 0)
                    {
                        npc.velocity.Y = 0;
                    }
                    npc.height = 32;
                    npc.width = 32;
                    if (npc.localAI[1] == 30)
                    {
                        int damage = 150;
                        int type = mod.ProjectileType("SAXPowerBomb");
                        Main.PlaySound(SoundLoader.customSoundType, npc.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/LayBomb"));
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 0, type, damage, 0f, Main.myPlayer);
                        }
                    }
                    if (npc.localAI[1] > 250)
                    {
                        npc.localAI[1] = -5;
                        npc.width = 36;
                        npc.height = 78;
                        npc.position.Y -= 50;
                    }
                }
                if (npc.ai[1] >= 1)
                {
                    npc.ai[0] = 0;
                    npc.ai[1]++;
                    if (npc.velocity.Y > 0 && npc.position.Y > P.MountedCenter.Y + 30 && npc.Distance(P.MountedCenter) < 900)
                    {
                        Vector2 predictedPos = P.MountedCenter + P.velocity + (P.velocity * (npc.Distance(P.MountedCenter) / 24));
                        predictedPos = P.MountedCenter + P.velocity + (P.velocity * (npc.Distance(predictedPos) / 24));
                        predictedPos = P.MountedCenter + P.velocity + (P.velocity * (npc.Distance(predictedPos) / 24));
                        if (P.velocity.X * npc.direction <= 0)
                        {
                            Vector2 vel = npc.DirectionTo(predictedPos) * (npc.Distance(predictedPos) / 24);
                            npc.velocity.X = vel.X;
                        }
                        else
                        {
                            npc.velocity.X = (7 * npc.direction) + P.velocity.X;
                        }
                        if (npc.ai[1] < 150)
                        {
                            npc.velocity.Y = -(float)Math.Sqrt(2 * 0.3f * Math.Abs((P.MountedCenter.Y + P.velocity.Y) - (npc.position.Y + npc.height)));
                        }
                    }
                    if (npc.ai[1] < 150 && ((npc.Center.X > P.MountedCenter.X + 300 && npc.velocity.X > 0) || (npc.Center.X < P.MountedCenter.X - 300 && npc.velocity.X < 0)))
                    {
                        npc.velocity.X = (7 * npc.direction);
                    }
                    //Main.PlaySound(2, npc.Center, 7);
                    if (!Collision.CanHitLine(new Vector2(npc.Center.X, npc.position.Y), 1, 1, P.position, P.width, P.height) && npc.velocity.Y < 0)
                    {
                        npc.noTileCollide = true;
                    }
                    else
                    {
                        npc.noTileCollide = false;
                    }
                    npc.damage = Main.expertMode ? 300 : 200;
                }
                else
                {
                    npc.damage = Main.expertMode ? 150 : 100;
                }
                if ((npc.ai[1] % 15) == 1)
                {
                    Main.PlaySound(SoundLoader.customSoundType, (int)npc.position.X, (int)npc.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ScrewAttack"), 1, -0.1f);
                }
            }
            if (npc.localAI[3] > 0)
            {
                npc.dontTakeDamage = true;
                npc.velocity.X = 0;
                if (npc.velocity.Y == 0 || npc.localAI[3] > 2)
                {
                    npc.localAI[3]++;
                    npc.velocity.Y = 0;
                }
                if (npc.localAI[3] >= 198f)
                {
                    npc.life = 0;
                    npc.HitEffect(0, 0);
                    npc.checkDead();
                }
            }
            if (npc.localAI[3] < 2)
            {
                npc.velocity.Y += 0.3f;
                if (npc.velocity.Y > 10)
                {
                    npc.velocity.Y = 10;
                }
            }
        }
        private void PredictiveAim(float speed, Vector2 origin)
        {
            Player P = Main.player[npc.target];
            Vector2 vel = (Math.Abs(npc.Center.Y - P.MountedCenter.Y) < 200 ? new Vector2(P.velocity.X, 0) : P.velocity);
            Vector2 predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(P.MountedCenter, origin) / speed));
            predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(predictedPos, origin) / speed));
            predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(predictedPos, origin) / speed));
            if (predictedPos.X < npc.Center.X && npc.velocity.Y == 0)
            {
                if (npc.direction > 0)
                {
                    npc.velocity.X = -1;
                }
                npc.direction = -1;
            }
            if (predictedPos.X > npc.Center.X && npc.velocity.Y == 0)
            {
                if (npc.direction < 0)
                {
                    npc.velocity.X = 1;
                }
                npc.direction = 1;
            }
            npc.ai[3] = (float)Math.Atan2(origin.Y - predictedPos.Y, origin.X - predictedPos.X);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (npc.ai[1] >= 1)
            {
                npc.velocity.X = (-7 * npc.direction) + target.velocity.X;
                npc.ai[1] = 200;
                if (Main.expertMode)
                {
                    target.wingTime = 0;
                    target.rocketTime = 0;
                    target.mount.Dismount(target);
                    target.velocity.Y = 10;
                    npc.velocity.Y = 10;
                }
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if ((Math.Abs(target.MountedCenter.Y - npc.Center.Y) < (target.height / 2) + 25 && Math.Abs(target.MountedCenter.X - npc.Center.X) < (target.width / 2) + 25) && npc.localAI[1] <= 0)
            {
                return base.CanHitPlayer(target, ref cooldownSlot);
            }
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if ((Math.Abs(target.Center.Y - npc.Center.Y) < (target.height / 2) + 25 && Math.Abs(target.Center.X - npc.Center.X) < (target.width / 2) + 25) && npc.localAI[1] <= 0)
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frame.X = 0;
            if (npc.localAI[3] > 0)
            {
                if (npc.localAI[3] < 60)
                {
                    npc.frameCounter++;
                    if (npc.frameCounter > 15)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y += 126;
                        if (npc.frame.Y > 126)
                        {
                            npc.frame.Y = 0;
                        }
                    }
                }
                else if (npc.localAI[3] < 75)
                {
                    npc.frame.Y = 2 * 126;
                }
                else if (npc.localAI[3] < 90)
                {
                    npc.frame.Y = 3 * 126;
                }
                else if (npc.localAI[3] < 96)
                {
                    npc.frame.Y = 4 * 126;
                }
                else if (npc.localAI[3] < 102)
                {
                    npc.frame.Y = 3 * 126;
                }
                else if (npc.localAI[3] < 108)
                {
                    npc.frame.Y = 4 * 126;
                }
                else if (npc.localAI[3] < 114)
                {
                    npc.frame.Y = 5 * 126;
                }
                else if (npc.localAI[3] < 120)
                {
                    npc.frame.Y = 4 * 126;
                }
                else if (npc.localAI[3] < 126)
                {
                    npc.frame.Y = 5 * 126;
                }
                else if (npc.localAI[3] < 132)
                {
                    npc.frame.Y = 6 * 126;
                }
                else if (npc.localAI[3] < 138)
                {
                    npc.frame.Y = 5 * 126;
                }
                else if (npc.localAI[3] < 144)
                {
                    npc.frame.Y = 6 * 126;
                }
                else if (npc.localAI[3] < 150)
                {
                    npc.frame.Y = 7 * 126;
                }
                else if (npc.localAI[3] < 156)
                {
                    npc.frame.Y = 6 * 126;
                }
                else if (npc.localAI[3] < 162)
                {
                    npc.frame.Y = 7 * 126;
                }
                else if (npc.localAI[3] < 168)
                {
                    npc.frame.Y = 8 * 126;
                }
                else if (npc.localAI[3] < 174)
                {
                    npc.frame.Y = 7 * 126;
                }
                else if (npc.localAI[3] < 180)
                {
                    npc.frame.Y = 8 * 126;
                }
                else if (npc.localAI[3] < 186)
                {
                    npc.frame.Y = 9 * 126;
                }
                else if (npc.localAI[3] < 192)
                {
                    npc.frame.Y = 8 * 126;
                }
                else if (npc.localAI[3] < 198)
                {
                    npc.frame.Y = 9 * 126;
                }
            }
            else
            {
                if (npc.ai[1] <= 0 && npc.localAI[1] <= 0)
                {
                    if (npc.velocity.X == 0 && npc.velocity.Y == 0)
                    {
                        npc.frameCounter = 0;
                        if ((npc.ai[0] >= 30 && npc.ai[2] < 10) || (npc.ai[0] >= 60 && npc.ai[2] >= 10 && npc.ai[2] < 12) || (npc.ai[0] >= 120 && npc.ai[2] >= 12))
                        {
                            npc.frame.Y = 1482;
                        }
                        else
                        {
                            npc.frame.Y = 1404;
                        }
                        if (npc.ai[3] > 25 * (float)(Math.PI / 180) && npc.ai[3] < 155 * (float)(Math.PI / 180))
                        {
                            npc.frame.X = 72;
                        }
                        if (npc.ai[3] > 70 * (float)(Math.PI / 180) && npc.ai[3] < 110 * (float)(Math.PI / 180))
                        {
                            npc.frame.X = 144;
                        }
                    }
                    else
                    {
                        if (npc.velocity.Y == 0)
                        {
                            npc.frameCounter += Math.Abs(npc.velocity.X);
                            if (npc.frameCounter >= 10)
                            {
                                npc.frameCounter = 0;
                                npc.frame.Y = (npc.frame.Y + 78);
                            }
                            if (npc.frame.Y >= 780)
                            {
                                npc.frame.Y = 0;
                            }
                            if (npc.ai[3] > 25 * (float)(Math.PI / 180) && npc.ai[3] < 155 * (float)(Math.PI / 180))
                            {
                                npc.frame.X = 72;
                            }
                            if (npc.ai[3] > 70 * (float)(Math.PI / 180) && npc.ai[3] < 110 * (float)(Math.PI / 180))
                            {
                                npc.frame.X = 144;
                            }
                        }
                        else
                        {
                            npc.frame.X = 0;
                            npc.frame.Y = 1560;
                            if (npc.ai[3] < -25 * (float)(Math.PI / 180) && npc.ai[3] > -155 * (float)(Math.PI / 180))
                            {
                                npc.frame.X = 72;
                                npc.frame.Y = 1638;
                            }
                            if (npc.ai[3] < -70 * (float)(Math.PI / 180) && npc.ai[3] > -110 * (float)(Math.PI / 180))
                            {
                                npc.frame.X = 0;
                                npc.frame.Y = 1638;
                            }
                            if (npc.ai[3] > 25 * (float)(Math.PI / 180) && npc.ai[3] < 155 * (float)(Math.PI / 180))
                            {
                                npc.frame.X = 72;
                                npc.frame.Y = 1560;
                            }
                            if (npc.ai[3] > 60 * (float)(Math.PI / 180) && npc.ai[3] < 120 * (float)(Math.PI / 180))
                            {
                                npc.frame.X = 144;
                                npc.frame.Y = 1560;
                            }
                        }
                    }
                }
                else if (npc.localAI[1] <= 0)
                {
                    npc.frame.X = 0;
                    npc.frameCounter++;
                    if (npc.frameCounter >= 6)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = (npc.frame.Y + 78);
                    }
                    if (npc.frame.Y >= 1404)
                    {
                        npc.frame.Y = 780;
                    }
                }
                else
                {
                    npc.frame.X = 144;
                    npc.frameCounter++;
                    if (npc.frameCounter >= 6)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = (npc.frame.Y + 78);
                    }
                    if (npc.frame.Y >= 1404)
                    {
                        npc.frame.Y = 780;
                    }
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
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
            //Color alpha2 = npc.GetAlpha(buffColor);
            //Color color = new Color((int)((float)npc.color.R * ((float)alpha2.R / 255)), (int)((float)npc.color.G * ((float)alpha2.G / 255)), (int)((float)npc.color.B * ((float)alpha2.B / 255)));
            Texture2D tex = mod.GetTexture("NPCs/Bosses/SAX_ArmCannon");
            Rectangle rect = new Rectangle(0, 0, (tex.Width), (tex.Height / 2));
            Vector2 vect = new Vector2((float)tex.Width / 2, (float)tex.Height / 4);
            float rotation = npc.direction > 0 ? npc.ai[3] - (float)(Math.PI) : npc.ai[3];

            Texture2D tex2 = mod.GetTexture("NPCs/Bosses/SAX_ChargingBeam");
            Rectangle rect2 = new Rectangle(0, (int)npc.localAI[0] * 46, (tex2.Width), (tex2.Height / 13));
            Vector2 vect2 = new Vector2((float)tex2.Width / 2, (float)tex2.Height / 26);

            int yOff = -4;
            if (npc.frame.Y >= 0)
            {
                yOff -= 2;
            }
            if (npc.frame.Y >= 78)
            {
                yOff -= 2;
            }
            if (npc.frame.Y >= 156)
            {
                yOff += 2;
            }
            if (npc.frame.Y >= 234)
            {
                yOff += 2;
            }
            if (npc.frame.Y >= 390)
            {
                yOff -= 2;
            }
            if (npc.frame.Y >= 624)
            {
                yOff += 2;
            }
            int xOff = -2;
            if ((npc.ai[0] >= 30 && npc.ai[2] < 10) || (npc.ai[0] >= 60 && npc.ai[2] >= 10 && npc.ai[2] < 12) || (npc.ai[0] >= 120 && npc.ai[2] >= 12))
            {
                if (npc.frame.X < 144)
                {
                    xOff = -6;
                }
                else
                {
                    yOff += 2;
                }
                if (npc.velocity.X == 0)
                {
                    yOff += 2;
                }
            }
            if (npc.ai[3] > 25 * (float)(Math.PI / 180) && npc.ai[3] < 155 * (float)(Math.PI / 180))
            {
                xOff += 6;
                yOff -= 6;
            }
            if (npc.frame.X >= 144)
            {
                yOff -= 10;
            }
            if (npc.ai[2] >= 10 && npc.ai[2] < 12 && npc.ai[0] >= 30)
            {
                rect = new Rectangle(0, (tex.Height / 2), (tex.Width), (tex.Height / 2));
            }
            if (npc.ai[1] <= 0 && npc.localAI[1] <= 0 && npc.localAI[3] <= 0)
            {
                spriteBatch.Draw(tex, npc.Center - Main.screenPosition + new Vector2(npc.scale * npc.direction * xOff, npc.scale * yOff), new Rectangle?(rect), color, rotation, vect, npc.scale, effects, 0f);
            }
            if (npc.ai[2] >= 12 && npc.ai[0] >= 30 && npc.ai[0] < 120 && npc.localAI[3] <= 0)
            {
                spriteBatch.Draw(tex2, npc.Center - Main.screenPosition + new Vector2(npc.scale * npc.direction * xOff, npc.scale * yOff), new Rectangle?(rect2), Color.White, rotation, vect2, npc.scale, effects, 0f);
            }


            int xFrameCount = 3;
            Texture2D texture = Main.npcTexture[npc.type];
            Rectangle rectangle = new Rectangle(npc.frame.X, npc.frame.Y, (texture.Width / xFrameCount), (texture.Height / Main.npcFrameCount[npc.type]));
            Vector2 vector = new Vector2(((texture.Width / xFrameCount) / 2f), ((texture.Height / Main.npcFrameCount[npc.type]) / 2f));
            if (npc.localAI[3] > 0)
            {
                Texture2D tex3 = mod.GetTexture("NPCs/Bosses/SAX_Death");
                Rectangle rect3 = new Rectangle(0, npc.frame.Y, tex3.Width, (tex3.Height / 10));
                Vector2 vect3 = new Vector2((tex3.Width / 2f), ((tex3.Height / 10) / 2f));
                spriteBatch.Draw(tex3, new Vector2(npc.position.X - Main.screenPosition.X + (float)(npc.width / 2) - (float)(tex3.Width) / 2f + vect3.X, npc.position.Y - Main.screenPosition.Y + (float)npc.height - (float)(tex3.Height / 10) + 4f + vect3.Y), new Rectangle?(rect3), color, npc.rotation, vect3, npc.scale, effects, 0f);
            }
            else
            {
                spriteBatch.Draw(texture, new Vector2(npc.position.X - Main.screenPosition.X + (float)(npc.width / 2) - (float)(texture.Width / xFrameCount) / 2f + vector.X, npc.position.Y - Main.screenPosition.Y + (float)npc.height - (float)(texture.Height / Main.npcFrameCount[npc.type]) + 4f + vector.Y), new Rectangle?(rectangle), color, npc.rotation, vector, npc.scale, effects, 0f);
            }
            return false;
        }
        public override bool CheckDead()
        {
            //Main.NewText("The SA-X enlarges and mutates!", 175, 75, 225);
            if (npc.localAI[3] <= 0)
            {
                npc.localAI[3] = 1;
                npc.frame.Y = 0;
                npc.velocity.X = 0;
                npc.velocity.Y = 10;
                if (npc.height == 32)
                {
                    npc.position.Y -= 50;
                }
                npc.width = 36;
                npc.height = 78;
                npc.damage = 0;
                npc.life = npc.lifeMax;
                npc.dontTakeDamage = true;
                npc.netUpdate = true;
                return false;
            }
            if (Main.netMode != 1)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y+38, mod.NPCType("SAXMutant"));
            }
            return true;
        }
    }
}

