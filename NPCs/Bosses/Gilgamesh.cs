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
	public class Gilgamesh : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gilgamesh");
			Main.npcFrameCount[npc.type] = 4;
            NPCID.Sets.TrailingMode[npc.type] = 0;
            NPCID.Sets.TrailCacheLength[npc.type] = 6;
        }
		public override void SetDefaults()
		{
			npc.width = 84;
			npc.height = 124;
			npc.damage = 140;
			npc.defense = 50;
			npc.lifeMax = 280000;
			npc.boss = true;
			npc.lavaImmune = true;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath3;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			npc.knockBackResist = 0f;
			npc.aiStyle = -1;
            npc.buffImmune[BuffID.Confused] = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ClashOnTheBigBridge");
			npc.frameCounter = 0;
            musicPriority = MusicPriority.BossHigh;
            npc.noGravity = true;
        }

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.625f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.7f);
		}
        public override bool PreNPCLoot()
        {
            for (int i = 0; i < 15; i++)
            {
                Item.NewItem(npc.getRect(), ItemID.Heart);
            }
            return false;
        }

        public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = npc.direction;
            if (npc.dontTakeDamage && npc.ai[0] < 40)
            {
                npc.frameCounter++;
                if (npc.frameCounter > 4)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = (npc.frame.Y + 148);
                    if (npc.frame.Y > 148)
                    {
                        npc.frame.Y = 0;
                    }
                }
            }
            else
            {
                if (npc.velocity.Y == 0)
                {
                    if (npc.velocity.X == 0)
                    {
                        npc.frame.Y = 0;
                    }
                    else
                    {
                        npc.frameCounter += Math.Abs(npc.velocity.X);
                        if (npc.frameCounter > 44)
                        {
                            npc.frameCounter = 0;
                            npc.frame.Y = (npc.frame.Y + 148);
                        }
                        if (npc.frame.Y <= 0)
                        {
                            npc.frame.Y = 148;
                        }
                        if (npc.frame.Y >= 592)
                        {
                            npc.frame.Y = 148;
                        }
                    }
                }
                else
                {
                    if (npc.velocity.Y > 0)
                    {
                        npc.frame.Y = 296;
                    }
                    else
                    {
                        npc.frame.Y = 444;
                    }
                }
            }
		}
        public override void AI()
        {
            npc.ai[0]++;
            npc.TargetClosest(false);
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
            if (npc.velocity.X > 0)
            {
                npc.direction = 1;
            }
            if (npc.velocity.X < 0)
            {
                npc.direction = -1;
            }
            npc.netUpdate = true;
            float moveSpeed = npc.Distance(P.Center) / 30;
            if (Math.Abs(P.Center.X - npc.Center.X) < 70)
            {
                moveSpeed = 0;
            }
            if (Math.Abs(P.Center.X - npc.Center.X) > 200)
            {
                moveSpeed = 7;
            }
            if (Math.Abs(P.Center.X - npc.Center.X) > 300)
            {
                moveSpeed = 8;
            }
            if (Math.Abs(P.Center.X - npc.Center.X) > 400)
            {
                moveSpeed = 9;
            }
            if (Math.Abs(P.Center.X - npc.Center.X) > 500)
            {
                moveSpeed = 10;
            }
            if (Math.Abs(P.Center.X - npc.Center.X) > 600)
            {
                moveSpeed = npc.Distance(P.Center) / 60;
            }
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].type == mod.NPCType("Enkidu") && Main.npc[i].ai[1] >= 900)
                {
                    moveSpeed *= 0.7f;
                    break;
                }
            }
            npc.ai[1]++;

            bool naginataReady = true;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.type == mod.ProjectileType("GilgNaginata1") && projectile.active)
                {
                    npc.direction = npc.Center.X < P.Center.X ? 1 : -1;
                    naginataReady = false;
                    break;
                }
            }
            if (naginataReady && npc.ai[2] <= 0 && npc.ai[1] > 50)
            {
                if (npc.Distance(P.MountedCenter) < 250)
                {
                    float Speed = 18f;
                    int damage = 50;
                    int type = mod.ProjectileType("GilgNaginata1");
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 7);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 7f, Main.myPlayer, npc.whoAmI, 4f);
                    }
                    npc.ai[1] = 0;
                    npc.direction = npc.Center.X < P.Center.X ? 1 : -1;
                }
                else if (npc.ai[0] < 300)
                {
                    npc.ai[2] = 1;
                }
            }
            if (npc.ai[2] > 0)
            {
                npc.direction = npc.Center.X < P.Center.X ? 1 : -1;
                npc.ai[1] = 30;
                npc.ai[2]++;
                if (npc.ai[2] == 45)
                {
                    float Speed = 12f + npc.velocity.Length();
                    int damage = 40;
                    Vector2 arm = npc.Center + new Vector2(-20 * npc.direction, -32);
                    Vector2 pos = P.MountedCenter;
                    if (Math.Abs(npc.Center.Y - P.MountedCenter.Y) < 200)
                    {
                        pos = PredictiveAim(Speed, arm);
                    }
                    int type = mod.ProjectileType("GilgTomahawk");
                    float rotation = (float)Math.Atan2(arm.Y - pos.Y, arm.X - pos.X);
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 1);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(arm.X, arm.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 2f, Main.myPlayer, npc.whoAmI, 4f);
                    }
                }
                if (npc.ai[2] == 60)
                {
                    int damage = 40;
                    Vector2 pos = P.MountedCenter;
                    Vector2 arm = npc.Center + new Vector2(17 * npc.direction, -39);
                    float Speed = 12f + npc.velocity.Length();
                    if (Main.expertMode || Math.Abs(npc.Center.Y - P.MountedCenter.Y) < 200)
                    {
                        pos = PredictiveAim(Speed, arm);
                    }
                    int type = mod.ProjectileType("GilgTomahawk");
                    float rotation = (float)Math.Atan2(arm.Y - pos.Y, arm.X - pos.X);
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 19);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(arm.X, arm.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 2f, Main.myPlayer, npc.whoAmI, 4f);
                    }
                }
                if (npc.ai[2] > 75)
                {
                    npc.ai[2] = 0;
                }
            }
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.type == mod.ProjectileType("GilgNaginata1") && projectile.active)
                {
                    naginataReady = false;
                    break;
                }
            }
            if (npc.ai[0] > 240 && npc.ai[2] <= 0 && naginataReady && (npc.velocity.Y == 0 || (!Collision.CanHitLine(npc.position, npc.width, npc.height, P.position, P.width, P.height) && npc.ai[0] > 600)))
            {
                npc.ai[0] = 0;
                npc.ai[3] = 1;
                npc.velocity.Y = -(float)Math.Sqrt(2 * 0.4f * Math.Abs((P.position.Y - 250) - (npc.position.Y + npc.height)));
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 7);
            }
            if (npc.ai[3] >= 1)
            {
                npc.ai[3]++;
                npc.ai[0] = 0;
                npc.ai[1] = 0;
                if (npc.velocity.Y < 0)
                {
                    npc.noTileCollide = true;
                }
                else
                {
                    npc.noTileCollide = false;
                }
                if (npc.velocity.Y > 0 && npc.ai[3] < 180)
                {
                    if (npc.Center.Y < P.position.Y)
                    {
                        npc.velocity.Y = (Math.Abs((P.position.Y) - (npc.position.Y + npc.height)) / 30) - (0.4f * 30 / 2);
                    }
                    else
                    {
                        npc.velocity.Y = -(float)Math.Sqrt(2 * 0.4f * Math.Abs((P.position.Y) - (npc.position.Y + npc.height)));
                    }
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 7);
                    //npc.velocity.X = npc.Center.X < P.Center.X ? moveSpeed * 2.5f : moveSpeed * -2.5f;
                    //float vel = Math.Abs(PredictiveAim((Math.Abs(npc.Center.X - P.Center.X) / 40), npc.Center).X - npc.Center.X) / 40;
                    float vel = 1.5f * moveSpeed + Math.Abs(P.velocity.X);
                    npc.velocity.X = npc.Center.X < P.Center.X ? vel : -vel;
                    float Speed = 18f;
                    int damage = 60;
                    int type = mod.ProjectileType("GilgNaginata1");
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 7);
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 15f, Main.myPlayer, npc.whoAmI, 4f);
                    }
                    npc.ai[3] = 180;
                }
            }
            else
            {
                if (npc.Center.Y > P.Center.Y + 100 && npc.velocity.Y == 0)
                {
                    npc.velocity.Y = -(float)Math.Sqrt(2 * 0.4f * Math.Abs(P.position.Y - (npc.position.Y + npc.height)));
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 7);
                }
                if (npc.position.Y + npc.height + npc.velocity.Y < P.position.Y && npc.velocity.Y >= 0)
                {
                    npc.position.Y++;
                }
                if (npc.velocity.Y == 0 && npc.velocity.X == 0 && moveSpeed > 3)
                {
                    npc.velocity.Y = -8f;
                }
                if (P.Center.X > npc.Center.X + 10)
                {
                    npc.velocity.X = moveSpeed;
                }
                if (P.Center.X < npc.Center.X - 10)
                {
                    npc.velocity.X = -moveSpeed;
                }
            }
            if (npc.ai[3] >= 210)
            {
                npc.ai[3] = 0;
                npc.noTileCollide = false;
            }
            npc.noGravity = true;
            npc.velocity.Y += 0.4f;
            if (npc.velocity.Y > 15)
            {
                npc.velocity.Y = 15;
            }
        }
        private Vector2 PredictiveAim(float speed, Vector2 origin)
        {
            Player P = Main.player[npc.target];
            Vector2 vel = (Math.Abs(npc.Center.Y - P.MountedCenter.Y) < 200 ? new Vector2(P.velocity.X, 0) : P.velocity);
            Vector2 predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(P.MountedCenter, origin) / speed));
            predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(predictedPos, origin) / speed));
            predictedPos = P.MountedCenter + P.velocity + (vel * (Vector2.Distance(predictedPos, origin) / speed));
            return predictedPos;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (npc.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            /*Texture2D armTex = mod.GetTexture("NPCs/Bosses/Gilgamesh_BackArm");
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

            Texture2D shoulderTex = mod.GetTexture("NPCs/Bosses/Gilgamesh_BackShoulder");
            int totalShoulderFrames = 1;
            int shoulderFrame = 0;
            float shoulderRotation = 0;
            Vector2 shoulderOffset = new Vector2(17, -39);

            Rectangle shoulderRect = new Rectangle(0, shoulderFrame * (shoulderTex.Height / totalShoulderFrames), (shoulderTex.Width), (shoulderTex.Height / totalShoulderFrames));
            Vector2 shoulderVect = new Vector2((float)shoulderTex.Width / 2, (float)shoulderTex.Height / (2 * totalShoulderFrames));

            if (npc.frame.Y == 148 )
            {
                shoulderOffset.Y -= 2;
                shoulderOffset.X += 2;
            }
            if (npc.frame.Y == 148 * 2)
            {
                shoulderOffset.Y -= 2;
                shoulderOffset.X += 4;
            }
            if (npc.frame.Y == 148 * 3)
            {
                shoulderOffset.Y += 2;
                shoulderOffset.X += 2;
            }

            Texture2D forearmTex = mod.GetTexture("NPCs/Bosses/Gilgamesh_BackForearm");
            int totalForearmFrames = 1;
            int forearmFrame = 0;
            float forearmRotation = 0;
            Vector2 forearmOffset = new Vector2(5, 19);

            Rectangle forearmRect = new Rectangle(0, forearmFrame * (forearmTex.Height / totalForearmFrames), (forearmTex.Width), (forearmTex.Height / totalForearmFrames));
            Vector2 forearmVect = new Vector2((float)forearmTex.Width / 2, (float)forearmTex.Height / (2 * totalForearmFrames));


            Texture2D handTex = mod.GetTexture("NPCs/Bosses/Gilgamesh_BackHand");
            int totalHandFrames = 3;
            int handFrame = 0;
            float handRotation = 0;
            Vector2 handOffset = new Vector2(11, 19);

            Vector2 shoulderPos = npc.Center + new Vector2(npc.scale * npc.direction * shoulderOffset.X, npc.scale * shoulderOffset.Y);
            Vector2 forearmPos = shoulderPos + new Vector2(npc.scale * npc.direction * forearmOffset.X, npc.scale * forearmOffset.Y);
            Vector2 handPos = forearmPos + new Vector2(npc.scale * npc.direction * handOffset.X, npc.scale * handOffset.Y);

            Player P = Main.player[npc.target];
            if (npc.ai[2] > 15)
            {
                handFrame = 1;
                Vector2 pos = P.MountedCenter;
                float Speed = 12f + npc.velocity.Length();
                if (Main.expertMode || Math.Abs(npc.Center.Y - P.MountedCenter.Y) < 200)
                {
                    pos = PredictiveAim(Speed, shoulderPos);
                }
                if (npc.ai[2] < 35)
                {
                    shoulderRotation = 0.0174f * 7f * (npc.ai[2] - 15) * -npc.direction;
                    forearmRotation = 0.0174f * 4.5f * (npc.ai[2] - 15) * -npc.direction;
                }
                else if (npc.ai[2] < 55)
                {
                    shoulderRotation = (shoulderPos - pos).ToRotation() + 1.57f;
                    forearmRotation = 0.0174f * 90 * -npc.direction;
                    handFrame = 2;
                }
                else if (npc.ai[2] < 65)
                {
                    //shoulderRotation = 0.0174f * (135 + (-1.5f * (npc.ai[2] - 45))) * -npc.direction;
                    shoulderRotation = (shoulderPos - pos).ToRotation() + 1.57f;
                    forearmRotation = 0.0174f * (90 + (-13.5f * (npc.ai[2] - 55))) * -npc.direction;
                    handFrame = 2;
                }
                else if (npc.ai[2] < 75)
                {
                    forearmRotation = 0.0174f * 45 * npc.direction;
                    shoulderRotation = (shoulderPos - pos).ToRotation() + 1.57f;
                }

                forearmRotation += shoulderRotation;
                handRotation += forearmRotation;
                forearmOffset = forearmOffset.RotatedBy(shoulderRotation * npc.direction, Vector2.Zero);
                forearmPos = shoulderPos + new Vector2(npc.scale * npc.direction * forearmOffset.X, npc.scale * forearmOffset.Y);
                handOffset = handOffset.RotatedBy(forearmRotation * npc.direction, Vector2.Zero);
                handPos = forearmPos + new Vector2(npc.scale * npc.direction * handOffset.X, npc.scale * handOffset.Y);
            }
            else
            {
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    Projectile projectile = Main.projectile[i];
                    if (projectile.type == mod.ProjectileType("GilgNaginata1") && projectile.active)
                    {
                        Vector2 dir = projectile.velocity;
                        dir.Normalize();
                        Vector2 pos = (projectile.Center - dir * 20);

                        shoulderRotation = (((shoulderPos - pos).ToRotation() + (npc.direction == 1 ? 3.14f : 0f)) * 2) + (npc.direction == 1 ? -0.785f : 0.785f);

                        forearmOffset = forearmOffset.RotatedBy(shoulderRotation * npc.direction, Vector2.Zero);
                        forearmPos = shoulderPos + new Vector2(npc.scale * npc.direction * forearmOffset.X, npc.scale * forearmOffset.Y);
                        //forearmRotation = (forearmPos - projectile.Center).ToRotation();
                        handPos = forearmPos + new Vector2(npc.scale * npc.direction * handOffset.X, npc.scale * handOffset.Y);
                        Vector2 handPos2 = handPos + (new Vector2(4 * npc.direction, 6));
                        forearmRotation = (handPos2 - pos).ToRotation() + (npc.direction == 1 ? 2.618f : 0.542f);

                        handOffset = handOffset.RotatedBy(forearmRotation * npc.direction, Vector2.Zero);
                        handPos = forearmPos + new Vector2(npc.scale * npc.direction * handOffset.X, npc.scale * handOffset.Y);
                        handRotation = (handPos - projectile.Center).ToRotation() + (npc.direction == 1 ? 3.14f : 0);
                        break;
                    }
                }
            }
            if (npc.dontTakeDamage && npc.ai[0] < 40)
            {
                return false;
            }
            Rectangle handRect = new Rectangle(0, handFrame * (handTex.Height / totalHandFrames), (handTex.Width), (handTex.Height / totalHandFrames));
            Vector2 handVect = new Vector2((float)handTex.Width / 2, (float)handTex.Height / (2 * totalHandFrames));

            Color color = Lighting.GetColor((int)(npc.Center.X / 16), (int)(npc.Center.Y / 16));
            if (npc.ai[3] >= 1)
            {
                Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, (Main.npcTexture[npc.type].Height * 0.5f) / Main.npcFrameCount[npc.type]);
                for (int i = 0; i < npc.oldPos.Length; i++)
                {
                    Color color2 = drawColor * ((npc.oldPos.Length - i) / (float)npc.oldPos.Length) * 0.8f;
                    Vector2 drawPos = npc.oldPos[i] - Main.screenPosition + new Vector2(npc.width / 2, npc.height / 2) + new Vector2(0f, npc.gfxOffY);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, (Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type]) * (npc.frame.Y / 148), Main.npcTexture[npc.type].Width, Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type]));
                    spriteBatch.Draw(shoulderTex, shoulderPos - npc.Center + drawPos, new Rectangle?(shoulderRect), color2, shoulderRotation, shoulderVect, npc.scale, spriteEffects, 0f);
                    spriteBatch.Draw(forearmTex, forearmPos - npc.Center + drawPos, new Rectangle?(forearmRect), color2, forearmRotation, forearmVect, npc.scale, spriteEffects, 0f);
                    spriteBatch.Draw(handTex, handPos - npc.Center + drawPos, new Rectangle?(handRect), color2, handRotation, handVect, npc.scale, spriteEffects, 0f);
                    spriteBatch.Draw(Main.npcTexture[npc.type], drawPos, rect, color2, npc.rotation, drawOrigin, npc.scale, spriteEffects, 0f);
                }
            }
            spriteBatch.Draw(shoulderTex, shoulderPos - Main.screenPosition, new Rectangle?(shoulderRect), color, shoulderRotation, shoulderVect, npc.scale, spriteEffects, 0f);
            spriteBatch.Draw(forearmTex, forearmPos - Main.screenPosition, new Rectangle?(forearmRect), color, forearmRotation, forearmVect, npc.scale, spriteEffects, 0f);
            spriteBatch.Draw(handTex, handPos - Main.screenPosition, new Rectangle?(handRect), color, handRotation, handVect, npc.scale, spriteEffects, 0f);
            return true;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (npc.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Texture2D shoulderTex = mod.GetTexture("NPCs/Bosses/Gilgamesh_FrontShoulder");
            int totalShoulderFrames = 1;
            int shoulderFrame = 0;
            float shoulderRotation = 0;
            Vector2 shoulderOffset = new Vector2(-20, -32);

            Rectangle shoulderRect = new Rectangle(0, shoulderFrame * (shoulderTex.Height / totalShoulderFrames), (shoulderTex.Width), (shoulderTex.Height / totalShoulderFrames));
            Vector2 shoulderVect = new Vector2((float)shoulderTex.Width / 2, (float)shoulderTex.Height / (2 * totalShoulderFrames));

            if (npc.frame.Y == 148)
            {
                shoulderOffset.Y -= 2;
                shoulderOffset.X += 2;
            }
            if (npc.frame.Y == 148 * 2)
            {
                shoulderOffset.Y -= 2;
                shoulderOffset.X += 4;
            }
            if (npc.frame.Y == 148 * 3)
            {
                shoulderOffset.Y += 2;
                shoulderOffset.X += 2;
            }

            Texture2D forearmTex = mod.GetTexture("NPCs/Bosses/Gilgamesh_FrontForearm");
            int totalForearmFrames = 1;
            int forearmFrame = 0;
            float forearmRotation = 0;
            Vector2 forearmOffset = new Vector2(-15, 9);

            Rectangle forearmRect = new Rectangle(0, forearmFrame * (forearmTex.Height / totalForearmFrames), (forearmTex.Width), (forearmTex.Height / totalForearmFrames));
            Vector2 forearmVect = new Vector2((float)forearmTex.Width / 2, (float)forearmTex.Height / (2 * totalForearmFrames));


            Texture2D handTex = mod.GetTexture("NPCs/Bosses/Gilgamesh_FrontHand");
            int totalHandFrames = 3;
            int handFrame = 0;
            float handRotation = 0;
            Vector2 handOffset = new Vector2(-10, 18);


            Vector2 shoulderPos = npc.Center + new Vector2(npc.scale * npc.direction * shoulderOffset.X, npc.scale * shoulderOffset.Y);
            Vector2 forearmPos = shoulderPos + new Vector2(npc.scale * npc.direction * forearmOffset.X, npc.scale * forearmOffset.Y);
            Vector2 handPos = forearmPos + new Vector2(npc.scale * npc.direction * handOffset.X, npc.scale * handOffset.Y);

            Player P = Main.player[npc.target];
            if (npc.ai[2] > 0)
            {
                handFrame = 1;
                Vector2 pos = P.MountedCenter;
                float Speed = 12f + npc.velocity.Length();
                if (Math.Abs(npc.Center.Y - P.MountedCenter.Y) < 200)
                {
                    pos = PredictiveAim(Speed, shoulderPos);
                }
                if (npc.ai[2] < 20)
                {
                    shoulderRotation = 0.0174f * 7f * npc.ai[2] * -npc.direction;
                    forearmRotation = 0.0174f * 4.5f * npc.ai[2] * -npc.direction;
                }
                else if (npc.ai[2] < 40)
                {
                    shoulderRotation = (shoulderPos - pos).ToRotation() + 1.57f;
                    forearmRotation = 0.0174f * 90 * -npc.direction + (npc.direction > 0 ? -0.785f : 0.785f);
                    handFrame = 2;
                }
                else if (npc.ai[2] < 50)
                {
                    //shoulderRotation = 0.0174f * (135 + (-1.5f * (npc.ai[2] - 30))) * -npc.direction;
                    shoulderRotation = (shoulderPos - pos).ToRotation() + 1.57f;
                    forearmRotation = 0.0174f * (90 + (-13.5f * (npc.ai[2] - 40))) * -npc.direction + (npc.direction > 0 ? -0.785f : 0.785f);
                    handRotation = 0.0174f * (-4.5f * (npc.ai[2] - 40)) * -npc.direction;
                    handFrame = 2;
                }
                else if (npc.ai[2] < 60)
                {
                    shoulderRotation = (shoulderPos - pos).ToRotation() + 1.57f;
                    handRotation = 0.0174f * -45 * -npc.direction;
                    forearmRotation = 0.0174f * 45 * npc.direction + (npc.direction > 0 ? -0.785f : 0.785f);
                }
                forearmRotation += shoulderRotation;
                handRotation += forearmRotation;
                forearmOffset = forearmOffset.RotatedBy(shoulderRotation * npc.direction, Vector2.Zero);
                forearmPos = shoulderPos + new Vector2(npc.scale * npc.direction * forearmOffset.X, npc.scale * forearmOffset.Y);
                handOffset = handOffset.RotatedBy(forearmRotation * npc.direction, Vector2.Zero);
                handPos = forearmPos + new Vector2(npc.scale * npc.direction * handOffset.X, npc.scale * handOffset.Y);
            }
            else
            {
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    Projectile projectile = Main.projectile[i];
                    if (projectile.type == mod.ProjectileType("GilgNaginata1") && projectile.active)
                    {
                        Vector2 dir = projectile.velocity;
                        dir.Normalize();
                        Vector2 pos = (projectile.Center - dir * 60);

                        //shoulderRotation = (shoulderPos - pos).ToRotation() + (npc.direction == 1 ? 1.57f : 1.57f);
                        shoulderRotation = (((shoulderPos - pos).ToRotation() + (npc.direction == 1 ? 2.618f : 0.542f)) * 2) + (npc.direction == 1 ? -0.785f : 0.785f);

                        forearmOffset = forearmOffset.RotatedBy(shoulderRotation * npc.direction, Vector2.Zero);
                        forearmPos = shoulderPos + new Vector2(npc.scale * npc.direction * forearmOffset.X, npc.scale * forearmOffset.Y);
                        //forearmRotation = (forearmPos - projectile.Center).ToRotation();
                        handPos = forearmPos + new Vector2(npc.scale * npc.direction * handOffset.X, npc.scale * handOffset.Y);
                        Vector2 handPos2 = handPos + (new Vector2(8 * npc.direction, 6));
                        forearmRotation = (handPos2 - pos).ToRotation() + (npc.direction == 1 ? 1.57f : 1.57f);

                        handOffset = handOffset.RotatedBy(forearmRotation * npc.direction, Vector2.Zero);
                        handPos = forearmPos + new Vector2(npc.scale * npc.direction * handOffset.X, npc.scale * handOffset.Y);
                        handRotation = (handPos - projectile.Center).ToRotation() + (npc.direction == 1 ? 3.14f : 0);
                        break;
                    }
                }
            }
            Rectangle handRect = new Rectangle(0, handFrame * (handTex.Height / totalHandFrames), (handTex.Width), (handTex.Height / totalHandFrames));
            Vector2 handVect = new Vector2((float)handTex.Width / 2, (float)handTex.Height / (2 * totalHandFrames));

            Color color = Lighting.GetColor((int)(npc.Center.X / 16), (int)(npc.Center.Y / 16));

            if (npc.dontTakeDamage && npc.ai[0] < 40)
            {
                Texture2D tex = mod.GetTexture("NPCs/Bosses/Gilgamesh_SpinToWin");
                if (npc.ai[0] < 20)
                {
                    tex = mod.GetTexture("NPCs/Bosses/Gilgamesh2_SpinToWin");
                }
                Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, (tex.Height * 0.5f) / 2);
                for (int i = 0; i < npc.oldPos.Length; i++)
                {
                    Color color2 = drawColor * ((npc.oldPos.Length - i) / (float)npc.oldPos.Length) * 0.8f;
                    Vector2 drawPos = npc.oldPos[i] - Main.screenPosition + new Vector2(npc.width / 2, npc.height / 2) + new Vector2(0f, (npc.height / 2) - (drawOrigin.Y));
                    Rectangle? rect = new Rectangle?(new Rectangle(0, (tex.Height / 2) * ((npc.frame.Y / 148) % 2), tex.Width, tex.Height / 2));
                    spriteBatch.Draw(tex, drawPos, rect, color2, npc.rotation, drawOrigin, npc.scale, spriteEffects, 0f);
                }
            }
            else
            {
                if (npc.ai[3] >= 1)
                {
                    Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, (Main.npcTexture[npc.type].Height * 0.5f) / Main.npcFrameCount[npc.type]);
                    for (int i = 0; i < npc.oldPos.Length; i++)
                    {
                        Color color2 = drawColor * ((npc.oldPos.Length - i) / (float)npc.oldPos.Length) * 0.8f;
                        Vector2 drawPos = npc.oldPos[i] - Main.screenPosition + new Vector2(npc.width / 2, npc.height / 2) + new Vector2(0f, npc.gfxOffY);
                        Rectangle? rect = new Rectangle?(new Rectangle(0, (Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type]) * (npc.frame.Y / 148), Main.npcTexture[npc.type].Width, Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type]));
                        spriteBatch.Draw(shoulderTex, shoulderPos - npc.Center + drawPos, new Rectangle?(shoulderRect), color2, shoulderRotation, shoulderVect, npc.scale, spriteEffects, 0f);
                        spriteBatch.Draw(forearmTex, forearmPos - npc.Center + drawPos, new Rectangle?(forearmRect), color2, forearmRotation, forearmVect, npc.scale, spriteEffects, 0f);
                        spriteBatch.Draw(handTex, handPos - npc.Center + drawPos, new Rectangle?(handRect), color2, handRotation, handVect, npc.scale, spriteEffects, 0f);
                    }
                }
                spriteBatch.Draw(shoulderTex, shoulderPos - Main.screenPosition, new Rectangle?(shoulderRect), color, shoulderRotation, shoulderVect, npc.scale, spriteEffects, 0f);
                spriteBatch.Draw(forearmTex, forearmPos - Main.screenPosition, new Rectangle?(forearmRect), color, forearmRotation, forearmVect, npc.scale, spriteEffects, 0f);
                spriteBatch.Draw(handTex, handPos - Main.screenPosition, new Rectangle?(handRect), color, handRotation, handVect, npc.scale, spriteEffects, 0f);
            }
        }
        public override bool CheckDead()
        {
            if (npc.ai[3] != -100)
            {
                npc.ai[0] = 440;
                npc.frame.Y = 0;
                npc.velocity.X = 0;
                npc.velocity.Y = 10;
                npc.damage = 0;
                npc.life = 1;
                npc.dontTakeDamage = true;
                npc.ai[3] = -100;
                npc.netUpdate = true;
                return false;
            }
            else
            {
                if (JoostWorld.downedGilgamesh)
                {
                    if (Main.netMode != 1)
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 10, mod.NPCType("Gilgamesh2"), 0, 510, 0, 0, 1);
                    }
                }
                else
                {
                    if (Main.netMode != 1)
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 10, mod.NPCType("Gilgamesh2"));
                    }
                }
                for (int i = 0; i < 80; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, DustID.Smoke, 0, 0, 0, Color.OrangeRed, 2);
                }
            }
            return false;
        }
        public override bool PreAI()
        {
            if (npc.ai[3] == -100)
            {
                npc.dontTakeDamage = true;
                npc.ai[0]--;
                npc.ai[1] = 0;
                npc.ai[2] = 0;
                npc.velocity.X = 0;
                npc.velocity.Y = 10;
                if (npc.ai[0] < 0)
                {
                    npc.life = 0;
                    npc.HitEffect(0, 0);
                    npc.checkDead();
                }
                if (npc.ai[0] == 400)
                {
                    Main.NewText("<Gilgamesh> Enough expository banter!", 225, 25, 25);
                }
                if (npc.ai[0] == 300)
                {
                    Main.NewText("Now, we fight like men! And ladies!", 225, 25, 25);
                }
                if (npc.ai[0] == 200)
                {
                    Main.NewText("And ladies who dress like men!", 225, 25, 25);
                }
                if (npc.ai[0] == 100)
                {
                    Main.NewText("For Gilgamesh...it is morphing time!", 225, 75, 25);
                }
                return false;
            }
            return base.PreAI();
        }
    }
}

