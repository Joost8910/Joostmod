using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Hostile;

namespace JoostMod.NPCs.Bosses
{
    [AutoloadBossHead]
	public class Gilgamesh : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gilgamesh");
			Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            NPCID.Sets.TrailCacheLength[NPC.type] = 6;
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[]
                {
                    BuffID.Confused
                }
            };
            NPCID.Sets.DebuffImmunitySets[Type] = debuffData;
        }
		public override void SetDefaults()
		{
			NPC.width = 84;
			NPC.height = 124;
			NPC.damage = 140;
			NPC.defense = 50;
			NPC.lifeMax = 280000;
			NPC.boss = true;
			NPC.lavaImmune = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath3;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1;
            if (!Main.dedServ)
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/ClashOnTheBigBridge");
            NPC.frameCounter = 0;
            SceneEffectPriority = SceneEffectPriority.BossHigh;
            NPC.noGravity = true;
        }

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.625f * bossLifeScale);
			NPC.damage = (int)(NPC.damage * 0.7f);
		}
        public override bool PreKill()
        {
            for (int i = 0; i < 15; i++)
            {
                Item.NewItem(NPC.GetSource_Death(), NPC.getRect(), ItemID.Heart);
            }
            return false;
        }

        public override void FindFrame(int frameHeight)
		{
			NPC.spriteDirection = NPC.direction;
            if (NPC.dontTakeDamage && NPC.ai[0] < 40)
            {
                NPC.frameCounter++;
                if (NPC.frameCounter > 4)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = (NPC.frame.Y + 148);
                    if (NPC.frame.Y > 148)
                    {
                        NPC.frame.Y = 0;
                    }
                }
            }
            else
            {
                if (NPC.velocity.Y == 0)
                {
                    if (NPC.velocity.X == 0)
                    {
                        NPC.frame.Y = 0;
                    }
                    else
                    {
                        NPC.frameCounter += Math.Abs(NPC.velocity.X);
                        if (NPC.frameCounter > 44)
                        {
                            NPC.frameCounter = 0;
                            NPC.frame.Y = (NPC.frame.Y + 148);
                        }
                        if (NPC.frame.Y <= 0)
                        {
                            NPC.frame.Y = 148;
                        }
                        if (NPC.frame.Y >= 592)
                        {
                            NPC.frame.Y = 148;
                        }
                    }
                }
                else
                {
                    if (NPC.velocity.Y > 0)
                    {
                        NPC.frame.Y = 296;
                    }
                    else
                    {
                        NPC.frame.Y = 444;
                    }
                }
            }
		}
        public override void AI()
        {
            var sauce = NPC.GetSource_FromAI();
            NPC.ai[0]++;
            NPC.TargetClosest(false);
            Player P = Main.player[NPC.target];
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(false);
                P = Main.player[NPC.target];
                if (!P.active || P.dead)
                {
                    NPC.velocity = new Vector2(0f, -100f);
                    NPC.active = false;
                }
            }
            if (NPC.velocity.X > 0)
            {
                NPC.direction = 1;
            }
            if (NPC.velocity.X < 0)
            {
                NPC.direction = -1;
            }
            NPC.netUpdate = true;
            float moveSpeed = NPC.Distance(P.Center) / 30;
            if (Math.Abs(P.Center.X - NPC.Center.X) < 70)
            {
                moveSpeed = 0;
            }
            if (Math.Abs(P.Center.X - NPC.Center.X) > 200)
            {
                moveSpeed = 7;
            }
            if (Math.Abs(P.Center.X - NPC.Center.X) > 300)
            {
                moveSpeed = 8;
            }
            if (Math.Abs(P.Center.X - NPC.Center.X) > 400)
            {
                moveSpeed = 9;
            }
            if (Math.Abs(P.Center.X - NPC.Center.X) > 500)
            {
                moveSpeed = 10;
            }
            if (Math.Abs(P.Center.X - NPC.Center.X) > 600)
            {
                moveSpeed = NPC.Distance(P.Center) / 60;
            }
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].type == Mod.Find<ModNPC>("Enkidu").Type && Main.npc[i].ai[1] >= 900)
                {
                    moveSpeed *= 0.7f;
                    break;
                }
            }
            NPC.ai[1]++;

            bool naginataReady = true;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.type == ModContent.ProjectileType<GilgNaginata1>() && projectile.active)
                {
                    NPC.direction = NPC.Center.X < P.Center.X ? 1 : -1;
                    naginataReady = false;
                    break;
                }
            }
            if (naginataReady && NPC.ai[2] <= 0 && NPC.ai[1] > 50)
            {
                if (NPC.Distance(P.MountedCenter) < 250)
                {
                    float Speed = 18f;
                    int damage = 50;
                    int type = ModContent.ProjectileType<GilgNaginata1>();
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    SoundEngine.PlaySound(SoundID.Item7, NPC.position);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(sauce, NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 7f, Main.myPlayer, NPC.whoAmI, 4f);
                    }
                    NPC.ai[1] = 0;
                    NPC.direction = NPC.Center.X < P.Center.X ? 1 : -1;
                }
                else if (NPC.ai[0] < 300)
                {
                    NPC.ai[2] = 1;
                }
            }
            if (NPC.ai[2] > 0)
            {
                NPC.direction = NPC.Center.X < P.Center.X ? 1 : -1;
                NPC.ai[1] = 30;
                NPC.ai[2]++;
                if (NPC.ai[2] == 45)
                {
                    float Speed = 12f + NPC.velocity.Length();
                    int damage = 40;
                    Vector2 arm = NPC.Center + new Vector2(-20 * NPC.direction, -32);
                    Vector2 pos = P.MountedCenter;
                    if (Math.Abs(NPC.Center.Y - P.MountedCenter.Y) < 200)
                    {
                        pos = PredictiveAim(Speed, arm);
                    }
                    int type = ModContent.ProjectileType<GilgTomahawk>();
                    float rotation = (float)Math.Atan2(arm.Y - pos.Y, arm.X - pos.X);
                    SoundEngine.PlaySound(SoundID.Item1, NPC.position);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(sauce, arm.X, arm.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 2f, Main.myPlayer, NPC.whoAmI, 4f);
                    }
                }
                if (NPC.ai[2] == 60)
                {
                    int damage = 40;
                    Vector2 pos = P.MountedCenter;
                    Vector2 arm = NPC.Center + new Vector2(17 * NPC.direction, -39);
                    float Speed = 12f + NPC.velocity.Length();
                    if (Main.expertMode || Math.Abs(NPC.Center.Y - P.MountedCenter.Y) < 200)
                    {
                        pos = PredictiveAim(Speed, arm);
                    }
                    int type = ModContent.ProjectileType<GilgTomahawk>();
                    float rotation = (float)Math.Atan2(arm.Y - pos.Y, arm.X - pos.X);
                    SoundEngine.PlaySound(SoundID.Item19, NPC.position);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(sauce, arm.X, arm.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 2f, Main.myPlayer, NPC.whoAmI, 4f);
                    }
                }
                if (NPC.ai[2] > 75)
                {
                    NPC.ai[2] = 0;
                }
            }
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.type == ModContent.ProjectileType<GilgNaginata1>() && projectile.active)
                {
                    naginataReady = false;
                    break;
                }
            }
            if (NPC.ai[0] > 240 && NPC.ai[2] <= 0 && naginataReady && (NPC.velocity.Y == 0 || (!Collision.CanHitLine(NPC.position, NPC.width, NPC.height, P.position, P.width, P.height) && NPC.ai[0] > 600)))
            {
                NPC.ai[0] = 0;
                NPC.ai[3] = 1;
                NPC.velocity.Y = -(float)Math.Sqrt(2 * 0.4f * Math.Abs((P.position.Y - 250) - (NPC.position.Y + NPC.height)));
                SoundEngine.PlaySound(SoundID.Item7, NPC.position);
            }
            if (NPC.ai[3] >= 1)
            {
                NPC.ai[3]++;
                NPC.ai[0] = 0;
                NPC.ai[1] = 0;
                if (NPC.velocity.Y < 0)
                {
                    NPC.noTileCollide = true;
                }
                else
                {
                    NPC.noTileCollide = false;
                }
                if (NPC.velocity.Y > 0 && NPC.ai[3] < 180)
                {
                    if (NPC.Center.Y < P.position.Y)
                    {
                        NPC.velocity.Y = (Math.Abs((P.position.Y) - (NPC.position.Y + NPC.height)) / 30) - (0.4f * 30 / 2);
                    }
                    else
                    {
                        NPC.velocity.Y = -(float)Math.Sqrt(2 * 0.4f * Math.Abs((P.position.Y) - (NPC.position.Y + NPC.height)));
                    }
                    SoundEngine.PlaySound(SoundID.Item7, NPC.position);
                    //npc.velocity.X = npc.Center.X < P.Center.X ? moveSpeed * 2.5f : moveSpeed * -2.5f;
                    //float vel = Math.Abs(PredictiveAim((Math.Abs(npc.Center.X - P.Center.X) / 40), npc.Center).X - npc.Center.X) / 40;
                    float vel = 1.5f * moveSpeed + Math.Abs(P.velocity.X);
                    NPC.velocity.X = NPC.Center.X < P.Center.X ? vel : -vel;
                    float Speed = 18f;
                    int damage = 60;
                    int type = ModContent.ProjectileType<GilgNaginata1>();
                    SoundEngine.PlaySound(SoundID.Item7, NPC.position);
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(sauce, NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 15f, Main.myPlayer, NPC.whoAmI, 4f);
                    }
                    NPC.ai[3] = 180;
                }
            }
            else
            {
                if (NPC.Center.Y > P.Center.Y + 100 && NPC.velocity.Y == 0)
                {
                    NPC.velocity.Y = -(float)Math.Sqrt(2 * 0.4f * Math.Abs(P.position.Y - (NPC.position.Y + NPC.height)));
                    SoundEngine.PlaySound(SoundID.Item7, NPC.position);
                }
                if (NPC.position.Y + NPC.height + NPC.velocity.Y < P.position.Y && NPC.velocity.Y >= 0)
                {
                    NPC.position.Y++;
                }
                if (NPC.velocity.Y == 0 && NPC.velocity.X == 0 && moveSpeed > 3)
                {
                    NPC.velocity.Y = -8f;
                }
                if (P.Center.X > NPC.Center.X + 10)
                {
                    NPC.velocity.X = moveSpeed;
                }
                if (P.Center.X < NPC.Center.X - 10)
                {
                    NPC.velocity.X = -moveSpeed;
                }
            }
            if (NPC.ai[3] >= 210)
            {
                NPC.ai[3] = 0;
                NPC.noTileCollide = false;
            }
            NPC.noGravity = true;
            NPC.velocity.Y += 0.4f;
            if (NPC.velocity.Y > 15)
            {
                NPC.velocity.Y = 15;
            }
        }
        private Vector2 PredictiveAim(float speed, Vector2 origin)
        {
            Player P = Main.player[NPC.target];
            Vector2 vel = (Math.Abs(NPC.Center.Y - P.MountedCenter.Y) < 200 ? new Vector2(P.velocity.X, 0) : P.velocity);
            Vector2 predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(P.MountedCenter, origin) / speed));
            predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(predictedPos, origin) / speed));
            predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(predictedPos, origin) / speed));
            return predictedPos;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (NPC.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            /*Texture2D armTex = Mod.Assets.Request<Texture2D>("NPCs/Bosses/Gilgamesh_BackArm").Value;
            int totalArmFrames = 3;
            int armFrame = 0;
            float armRotation = 0;
            Vector2 armOffset = new Vector2(17, -39);

            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.type == mod.ProjectileType("GilgNaginata1") && projectile.active)
                {
                    Vector2 pos = npc.Center + new Vector2(armOffset.X * npc.direction, armOffset.Y);
                    armRotation = (float)Math.Atan2(pos.Y - projectile.Center.Y, pos.X - projectile.Center.X) + (npc.direction == 1 ? 3.14f : 0);
                    if (projectile.timeLeft > 22 || projectile.timeLeft < 12)
                    {
                        armFrame = 1;
                    }
                    else
                    {
                        armFrame = 2;
                    }
                    break;
                }
            }
            
            Rectangle armRect = new Rectangle(0, armFrame * (armTex.Height / totalArmFrames), (armTex.Width), (armTex.Height / totalArmFrames));
            Vector2 armVect = new Vector2((float)armTex.Width / 2, (float)armTex.Height / (2 * totalArmFrames));

            if (npc.frame.Y == 148 || npc.frame.Y == 148 * 2)
            {
                armOffset.Y -= 2;
            }
            if (npc.frame.Y == 148 * 3)
            {
                armOffset.Y += 2;
            }
            armOffset = armOffset.RotatedBy(npc.rotation * npc.direction, Vector2.Zero);
            armRotation += npc.rotation;*/

            Texture2D shoulderTex = Mod.Assets.Request<Texture2D>("NPCs/Bosses/Gilgamesh_BackShoulder").Value;
            int totalShoulderFrames = 1;
            int shoulderFrame = 0;
            float shoulderRotation = 0;
            Vector2 shoulderOffset = new Vector2(17, -39);

            Rectangle shoulderRect = new Rectangle(0, shoulderFrame * (shoulderTex.Height / totalShoulderFrames), (shoulderTex.Width), (shoulderTex.Height / totalShoulderFrames));
            Vector2 shoulderVect = new Vector2((float)shoulderTex.Width / 2, (float)shoulderTex.Height / (2 * totalShoulderFrames));

            if (NPC.frame.Y == 148 )
            {
                shoulderOffset.Y -= 2;
                shoulderOffset.X += 2;
            }
            if (NPC.frame.Y == 148 * 2)
            {
                shoulderOffset.Y -= 2;
                shoulderOffset.X += 4;
            }
            if (NPC.frame.Y == 148 * 3)
            {
                shoulderOffset.Y += 2;
                shoulderOffset.X += 2;
            }

            Texture2D forearmTex = Mod.Assets.Request<Texture2D>("NPCs/Bosses/Gilgamesh_BackForearm").Value;
            int totalForearmFrames = 1;
            int forearmFrame = 0;
            float forearmRotation = 0;
            Vector2 forearmOffset = new Vector2(5, 19);

            Rectangle forearmRect = new Rectangle(0, forearmFrame * (forearmTex.Height / totalForearmFrames), (forearmTex.Width), (forearmTex.Height / totalForearmFrames));
            Vector2 forearmVect = new Vector2((float)forearmTex.Width / 2, (float)forearmTex.Height / (2 * totalForearmFrames));


            Texture2D handTex = Mod.Assets.Request<Texture2D>("NPCs/Bosses/Gilgamesh_BackHand").Value;
            int totalHandFrames = 3;
            int handFrame = 0;
            float handRotation = 0;
            Vector2 handOffset = new Vector2(11, 19);

            Vector2 shoulderPos = NPC.Center + new Vector2(NPC.scale * NPC.direction * shoulderOffset.X, NPC.scale * shoulderOffset.Y);
            Vector2 forearmPos = shoulderPos + new Vector2(NPC.scale * NPC.direction * forearmOffset.X, NPC.scale * forearmOffset.Y);
            Vector2 handPos = forearmPos + new Vector2(NPC.scale * NPC.direction * handOffset.X, NPC.scale * handOffset.Y);

            Player P = Main.player[NPC.target];
            if (NPC.ai[2] > 15)
            {
                handFrame = 1;
                Vector2 pos = P.MountedCenter;
                float Speed = 12f + NPC.velocity.Length();
                if (Main.expertMode || Math.Abs(NPC.Center.Y - P.MountedCenter.Y) < 200)
                {
                    pos = PredictiveAim(Speed, shoulderPos);
                }
                if (NPC.ai[2] < 35)
                {
                    shoulderRotation = 0.0174f * 7f * (NPC.ai[2] - 15) * -NPC.direction;
                    forearmRotation = 0.0174f * 4.5f * (NPC.ai[2] - 15) * -NPC.direction;
                }
                else if (NPC.ai[2] < 55)
                {
                    shoulderRotation = (shoulderPos - pos).ToRotation() + 1.57f;
                    forearmRotation = 0.0174f * 90 * -NPC.direction;
                    handFrame = 2;
                }
                else if (NPC.ai[2] < 65)
                {
                    //shoulderRotation = 0.0174f * (135 + (-1.5f * (npc.ai[2] - 45))) * -npc.direction;
                    shoulderRotation = (shoulderPos - pos).ToRotation() + 1.57f;
                    forearmRotation = 0.0174f * (90 + (-13.5f * (NPC.ai[2] - 55))) * -NPC.direction;
                    handFrame = 2;
                }
                else if (NPC.ai[2] < 75)
                {
                    forearmRotation = 0.0174f * 45 * NPC.direction;
                    shoulderRotation = (shoulderPos - pos).ToRotation() + 1.57f;
                }

                forearmRotation += shoulderRotation;
                handRotation += forearmRotation;
                forearmOffset = forearmOffset.RotatedBy(shoulderRotation * NPC.direction, Vector2.Zero);
                forearmPos = shoulderPos + new Vector2(NPC.scale * NPC.direction * forearmOffset.X, NPC.scale * forearmOffset.Y);
                handOffset = handOffset.RotatedBy(forearmRotation * NPC.direction, Vector2.Zero);
                handPos = forearmPos + new Vector2(NPC.scale * NPC.direction * handOffset.X, NPC.scale * handOffset.Y);
            }
            else
            {
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    Projectile projectile = Main.projectile[i];
                    if (projectile.type == ModContent.ProjectileType<GilgNaginata1>() && projectile.active)
                    {
                        Vector2 dir = projectile.velocity;
                        dir.Normalize();
                        Vector2 pos = (projectile.Center - dir * 20);

                        shoulderRotation = (((shoulderPos - pos).ToRotation() + (NPC.direction == 1 ? 3.14f : 0f)) * 2) + (NPC.direction == 1 ? -0.785f : 0.785f);

                        forearmOffset = forearmOffset.RotatedBy(shoulderRotation * NPC.direction, Vector2.Zero);
                        forearmPos = shoulderPos + new Vector2(NPC.scale * NPC.direction * forearmOffset.X, NPC.scale * forearmOffset.Y);
                        //forearmRotation = (forearmPos - projectile.Center).ToRotation();
                        handPos = forearmPos + new Vector2(NPC.scale * NPC.direction * handOffset.X, NPC.scale * handOffset.Y);
                        Vector2 handPos2 = handPos + (new Vector2(4 * NPC.direction, 6));
                        forearmRotation = (handPos2 - pos).ToRotation() + (NPC.direction == 1 ? 2.618f : 0.542f);

                        handOffset = handOffset.RotatedBy(forearmRotation * NPC.direction, Vector2.Zero);
                        handPos = forearmPos + new Vector2(NPC.scale * NPC.direction * handOffset.X, NPC.scale * handOffset.Y);
                        handRotation = (handPos - projectile.Center).ToRotation() + (NPC.direction == 1 ? 3.14f : 0);
                        break;
                    }
                }
            }
            if (NPC.dontTakeDamage && NPC.ai[0] < 40)
            {
                return false;
            }
            Rectangle handRect = new Rectangle(0, handFrame * (handTex.Height / totalHandFrames), (handTex.Width), (handTex.Height / totalHandFrames));
            Vector2 handVect = new Vector2((float)handTex.Width / 2, (float)handTex.Height / (2 * totalHandFrames));

            Color color = Lighting.GetColor((int)(NPC.Center.X / 16), (int)(NPC.Center.Y / 16));
            if (NPC.ai[3] >= 1)
            {
                Vector2 drawOrigin = new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, (TextureAssets.Npc[NPC.type].Value.Height * 0.5f) / Main.npcFrameCount[NPC.type]);
                for (int i = 0; i < NPC.oldPos.Length; i++)
                {
                    Color color2 = drawColor * ((NPC.oldPos.Length - i) / (float)NPC.oldPos.Length) * 0.8f;
                    Vector2 drawPos = NPC.oldPos[i] - Main.screenPosition + new Vector2(NPC.width / 2, NPC.height / 2) + new Vector2(0f, NPC.gfxOffY);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, (TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type]) * (NPC.frame.Y / 148), TextureAssets.Npc[NPC.type].Value.Width, TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type]));
                    spriteBatch.Draw(shoulderTex, shoulderPos - NPC.Center + drawPos, new Rectangle?(shoulderRect), color2, shoulderRotation, shoulderVect, NPC.scale, spriteEffects, 0f);
                    spriteBatch.Draw(forearmTex, forearmPos - NPC.Center + drawPos, new Rectangle?(forearmRect), color2, forearmRotation, forearmVect, NPC.scale, spriteEffects, 0f);
                    spriteBatch.Draw(handTex, handPos - NPC.Center + drawPos, new Rectangle?(handRect), color2, handRotation, handVect, NPC.scale, spriteEffects, 0f);
                    spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, drawPos, rect, color2, NPC.rotation, drawOrigin, NPC.scale, spriteEffects, 0f);
                }
            }
            spriteBatch.Draw(shoulderTex, shoulderPos - Main.screenPosition, new Rectangle?(shoulderRect), color, shoulderRotation, shoulderVect, NPC.scale, spriteEffects, 0f);
            spriteBatch.Draw(forearmTex, forearmPos - Main.screenPosition, new Rectangle?(forearmRect), color, forearmRotation, forearmVect, NPC.scale, spriteEffects, 0f);
            spriteBatch.Draw(handTex, handPos - Main.screenPosition, new Rectangle?(handRect), color, handRotation, handVect, NPC.scale, spriteEffects, 0f);
            return true;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (NPC.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Texture2D shoulderTex = Mod.Assets.Request<Texture2D>("NPCs/Bosses/Gilgamesh_FrontShoulder").Value;
            int totalShoulderFrames = 1;
            int shoulderFrame = 0;
            float shoulderRotation = 0;
            Vector2 shoulderOffset = new Vector2(-20, -32);

            Rectangle shoulderRect = new Rectangle(0, shoulderFrame * (shoulderTex.Height / totalShoulderFrames), (shoulderTex.Width), (shoulderTex.Height / totalShoulderFrames));
            Vector2 shoulderVect = new Vector2((float)shoulderTex.Width / 2, (float)shoulderTex.Height / (2 * totalShoulderFrames));

            if (NPC.frame.Y == 148)
            {
                shoulderOffset.Y -= 2;
                shoulderOffset.X += 2;
            }
            if (NPC.frame.Y == 148 * 2)
            {
                shoulderOffset.Y -= 2;
                shoulderOffset.X += 4;
            }
            if (NPC.frame.Y == 148 * 3)
            {
                shoulderOffset.Y += 2;
                shoulderOffset.X += 2;
            }

            Texture2D forearmTex = Mod.Assets.Request<Texture2D>("NPCs/Bosses/Gilgamesh_FrontForearm").Value;
            int totalForearmFrames = 1;
            int forearmFrame = 0;
            float forearmRotation = 0;
            Vector2 forearmOffset = new Vector2(-15, 9);

            Rectangle forearmRect = new Rectangle(0, forearmFrame * (forearmTex.Height / totalForearmFrames), (forearmTex.Width), (forearmTex.Height / totalForearmFrames));
            Vector2 forearmVect = new Vector2((float)forearmTex.Width / 2, (float)forearmTex.Height / (2 * totalForearmFrames));


            Texture2D handTex = Mod.Assets.Request<Texture2D>("NPCs/Bosses/Gilgamesh_FrontHand").Value;
            int totalHandFrames = 3;
            int handFrame = 0;
            float handRotation = 0;
            Vector2 handOffset = new Vector2(-10, 18);


            Vector2 shoulderPos = NPC.Center + new Vector2(NPC.scale * NPC.direction * shoulderOffset.X, NPC.scale * shoulderOffset.Y);
            Vector2 forearmPos = shoulderPos + new Vector2(NPC.scale * NPC.direction * forearmOffset.X, NPC.scale * forearmOffset.Y);
            Vector2 handPos = forearmPos + new Vector2(NPC.scale * NPC.direction * handOffset.X, NPC.scale * handOffset.Y);

            Player P = Main.player[NPC.target];
            if (NPC.ai[2] > 0)
            {
                handFrame = 1;
                Vector2 pos = P.MountedCenter;
                float Speed = 12f + NPC.velocity.Length();
                if (Math.Abs(NPC.Center.Y - P.MountedCenter.Y) < 200)
                {
                    pos = PredictiveAim(Speed, shoulderPos);
                }
                if (NPC.ai[2] < 20)
                {
                    shoulderRotation = 0.0174f * 7f * NPC.ai[2] * -NPC.direction;
                    forearmRotation = 0.0174f * 4.5f * NPC.ai[2] * -NPC.direction;
                }
                else if (NPC.ai[2] < 40)
                {
                    shoulderRotation = (shoulderPos - pos).ToRotation() + 1.57f;
                    forearmRotation = 0.0174f * 90 * -NPC.direction + (NPC.direction > 0 ? -0.785f : 0.785f);
                    handFrame = 2;
                }
                else if (NPC.ai[2] < 50)
                {
                    //shoulderRotation = 0.0174f * (135 + (-1.5f * (npc.ai[2] - 30))) * -npc.direction;
                    shoulderRotation = (shoulderPos - pos).ToRotation() + 1.57f;
                    forearmRotation = 0.0174f * (90 + (-13.5f * (NPC.ai[2] - 40))) * -NPC.direction + (NPC.direction > 0 ? -0.785f : 0.785f);
                    handRotation = 0.0174f * (-4.5f * (NPC.ai[2] - 40)) * -NPC.direction;
                    handFrame = 2;
                }
                else if (NPC.ai[2] < 60)
                {
                    shoulderRotation = (shoulderPos - pos).ToRotation() + 1.57f;
                    handRotation = 0.0174f * -45 * -NPC.direction;
                    forearmRotation = 0.0174f * 45 * NPC.direction + (NPC.direction > 0 ? -0.785f : 0.785f);
                }
                forearmRotation += shoulderRotation;
                handRotation += forearmRotation;
                forearmOffset = forearmOffset.RotatedBy(shoulderRotation * NPC.direction, Vector2.Zero);
                forearmPos = shoulderPos + new Vector2(NPC.scale * NPC.direction * forearmOffset.X, NPC.scale * forearmOffset.Y);
                handOffset = handOffset.RotatedBy(forearmRotation * NPC.direction, Vector2.Zero);
                handPos = forearmPos + new Vector2(NPC.scale * NPC.direction * handOffset.X, NPC.scale * handOffset.Y);
            }
            else
            {
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    Projectile projectile = Main.projectile[i];
                    if (projectile.type == ModContent.ProjectileType<GilgNaginata1>() && projectile.active)
                    {
                        Vector2 dir = projectile.velocity;
                        dir.Normalize();
                        Vector2 pos = (projectile.Center - dir * 60);

                        //shoulderRotation = (shoulderPos - pos).ToRotation() + (npc.direction == 1 ? 1.57f : 1.57f);
                        shoulderRotation = (((shoulderPos - pos).ToRotation() + (NPC.direction == 1 ? 2.618f : 0.542f)) * 2) + (NPC.direction == 1 ? -0.785f : 0.785f);

                        forearmOffset = forearmOffset.RotatedBy(shoulderRotation * NPC.direction, Vector2.Zero);
                        forearmPos = shoulderPos + new Vector2(NPC.scale * NPC.direction * forearmOffset.X, NPC.scale * forearmOffset.Y);
                        //forearmRotation = (forearmPos - projectile.Center).ToRotation();
                        handPos = forearmPos + new Vector2(NPC.scale * NPC.direction * handOffset.X, NPC.scale * handOffset.Y);
                        Vector2 handPos2 = handPos + (new Vector2(8 * NPC.direction, 6));
                        forearmRotation = (handPos2 - pos).ToRotation() + (NPC.direction == 1 ? 1.57f : 1.57f);

                        handOffset = handOffset.RotatedBy(forearmRotation * NPC.direction, Vector2.Zero);
                        handPos = forearmPos + new Vector2(NPC.scale * NPC.direction * handOffset.X, NPC.scale * handOffset.Y);
                        handRotation = (handPos - projectile.Center).ToRotation() + (NPC.direction == 1 ? 3.14f : 0);
                        break;
                    }
                }
            }
            Rectangle handRect = new Rectangle(0, handFrame * (handTex.Height / totalHandFrames), (handTex.Width), (handTex.Height / totalHandFrames));
            Vector2 handVect = new Vector2((float)handTex.Width / 2, (float)handTex.Height / (2 * totalHandFrames));

            Color color = Lighting.GetColor((int)(NPC.Center.X / 16), (int)(NPC.Center.Y / 16));

            if (NPC.dontTakeDamage && NPC.ai[0] < 40)
            {
                Texture2D tex = Mod.Assets.Request<Texture2D>("NPCs/Bosses/Gilgamesh_SpinToWin").Value;
                if (NPC.ai[0] < 20)
                {
                    tex = Mod.Assets.Request<Texture2D>("NPCs/Bosses/Gilgamesh2_SpinToWin").Value;
                }
                Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, (tex.Height * 0.5f) / 2);
                for (int i = 0; i < NPC.oldPos.Length; i++)
                {
                    Color color2 = drawColor * ((NPC.oldPos.Length - i) / (float)NPC.oldPos.Length) * 0.8f;
                    Vector2 drawPos = NPC.oldPos[i] - Main.screenPosition + new Vector2(NPC.width / 2, NPC.height / 2) + new Vector2(0f, (NPC.height / 2) - (drawOrigin.Y));
                    Rectangle? rect = new Rectangle?(new Rectangle(0, (tex.Height / 2) * ((NPC.frame.Y / 148) % 2), tex.Width, tex.Height / 2));
                    spriteBatch.Draw(tex, drawPos, rect, color2, NPC.rotation, drawOrigin, NPC.scale, spriteEffects, 0f);
                }
            }
            else
            {
                if (NPC.ai[3] >= 1)
                {
                    Vector2 drawOrigin = new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, (TextureAssets.Npc[NPC.type].Value.Height * 0.5f) / Main.npcFrameCount[NPC.type]);
                    for (int i = 0; i < NPC.oldPos.Length; i++)
                    {
                        Color color2 = drawColor * ((NPC.oldPos.Length - i) / (float)NPC.oldPos.Length) * 0.8f;
                        Vector2 drawPos = NPC.oldPos[i] - Main.screenPosition + new Vector2(NPC.width / 2, NPC.height / 2) + new Vector2(0f, NPC.gfxOffY);
                        Rectangle? rect = new Rectangle?(new Rectangle(0, (TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type]) * (NPC.frame.Y / 148), TextureAssets.Npc[NPC.type].Value.Width, TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type]));
                        spriteBatch.Draw(shoulderTex, shoulderPos - NPC.Center + drawPos, new Rectangle?(shoulderRect), color2, shoulderRotation, shoulderVect, NPC.scale, spriteEffects, 0f);
                        spriteBatch.Draw(forearmTex, forearmPos - NPC.Center + drawPos, new Rectangle?(forearmRect), color2, forearmRotation, forearmVect, NPC.scale, spriteEffects, 0f);
                        spriteBatch.Draw(handTex, handPos - NPC.Center + drawPos, new Rectangle?(handRect), color2, handRotation, handVect, NPC.scale, spriteEffects, 0f);
                    }
                }
                spriteBatch.Draw(shoulderTex, shoulderPos - Main.screenPosition, new Rectangle?(shoulderRect), color, shoulderRotation, shoulderVect, NPC.scale, spriteEffects, 0f);
                spriteBatch.Draw(forearmTex, forearmPos - Main.screenPosition, new Rectangle?(forearmRect), color, forearmRotation, forearmVect, NPC.scale, spriteEffects, 0f);
                spriteBatch.Draw(handTex, handPos - Main.screenPosition, new Rectangle?(handRect), color, handRotation, handVect, NPC.scale, spriteEffects, 0f);
            }
        }
        public override bool CheckDead()
        {
            if (NPC.ai[3] != -100)
            {
                NPC.ai[0] = 440;
                NPC.frame.Y = 0;
                NPC.velocity.X = 0;
                NPC.velocity.Y = 10;
                NPC.damage = 0;
                NPC.life = 1;
                NPC.dontTakeDamage = true;
                NPC.ai[3] = -100;
                NPC.netUpdate = true;
                return false;
            }
            else
            {
                if (JoostWorld.downedGilgamesh)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.Center.X, (int)NPC.Center.Y - 10, Mod.Find<ModNPC>("Gilgamesh2").Type, 0, 510, 0, 0, 1);
                    }
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.Center.X, (int)NPC.Center.Y - 10, Mod.Find<ModNPC>("Gilgamesh2").Type);
                    }
                }
                for (int i = 0; i < 80; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Smoke, 0, 0, 0, Color.OrangeRed, 2);
                }
            }
            return false;
        }
        public override bool PreAI()
        {
            if (NPC.ai[3] == -100)
            {
                NPC.dontTakeDamage = true;
                NPC.ai[0]--;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.velocity.X = 0;
                NPC.velocity.Y = 10;
                NPC.noTileCollide = false;
                if (NPC.ai[0] < 0)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead();
                }
                if (Main.netMode != NetmodeID.Server)
                {
                    if (NPC.ai[0] == 400)
                    {
                        Main.NewText("<Gilgamesh> Enough expository banter!", 225, 25, 25);
                    }
                    if (NPC.ai[0] == 300)
                    {
                        Main.NewText("Now, we fight like men! And ladies!", 225, 25, 25);
                    }
                    if (NPC.ai[0] == 200)
                    {
                        Main.NewText("And ladies who dress like men!", 225, 25, 25);
                    }
                    if (NPC.ai[0] == 100)
                    {
                        Main.NewText("For Gilgamesh...it is morphing time!", 225, 75, 25);
                    }
                }
                return false;
            }
            return base.PreAI();
        }
    }
}

