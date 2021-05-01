using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Hunts
{
    [AutoloadBossHead]
    public class Roc : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Roc");
            Main.npcFrameCount[npc.type] = 6;
            NPCID.Sets.TrailingMode[npc.type] = 0;
            NPCID.Sets.TrailCacheLength[npc.type] = 5;
        }
		public override void SetDefaults()
		{
			npc.width = 110;
			npc.height = 92;
			npc.damage = 30;
			npc.defense = 12;
			npc.lifeMax = 1700;
			npc.HitSound = SoundID.NPCHit28;
			npc.DeathSound = SoundID.NPCDeath31;
			npc.value = 0;
			npc.knockBackResist = 0f;
			npc.aiStyle = -1;
			npc.noGravity = true;
            npc.netAlways = true;
            npc.noTileCollide = true;
		}
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.7f * bossLifeScale + 1);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.spawnTileY < Main.worldSurface*0.35f && !JoostWorld.downedRoc && JoostWorld.activeQuest.Contains(npc.type) && !NPC.AnyNPCs(npc.type) ? 0.15f : 0f;
        }
        public override void NPCLoot()
		{
            JoostWorld.downedRoc = true;
            npc.DropItemInstanced(npc.position, npc.Size, mod.ItemType("Roc"), 1, false);
            if (Main.expertMode && Main.rand.Next(100) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EvilStone"), 1);
            }
        }
		public override void HitEffect(int hitDirection, double damage)
        {
            npc.ai[0]++;
            if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Roc"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Roc2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Roc2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Roc3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Roc3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Roc3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Roc3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Roc3"), 1f);
            }

        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            Player P = Main.player[npc.target];
            if (npc.velocity.Y > 0 || (npc.ai[3] > 0 && npc.ai[0] > 1))
            {
                npc.frame.Y = 0;
            }
            else
            {
                npc.frameCounter++;
                if (npc.frameCounter > 5 + npc.velocity.Y/3)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += 120;
                    if (npc.frame.Y == 120)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 32);
                    }
                }
                if (npc.frame.Y >= 720)
                {
                    npc.frame.Y = 0;
                }
            }
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return npc.ai[3] != 2 && base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (npc.ai[3] == 1)
            {
                npc.ai[3] = 2;
                npc.target = target.whoAmI;
            }
        }
        public override void AI()
		{
            if (npc.Center.Y < 666)
            {
                npc.directionY = 1;
                npc.velocity.Y = 6;
            }
            Player P = Main.player[npc.target];
            if (Vector2.Distance(npc.Center, P.Center) > 2500 || npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(false);
                P = Main.player[npc.target];
                if (!P.active || P.dead || Vector2.Distance(npc.Center, P.Center) > 2500)
                {
                    npc.ai[0] = 0;
                }
            }
            if (npc.ai[0] < 1)
            {
                if (Main.rand.Next(100) == 0)
                {
                    npc.direction *= -1;
                }
                if (npc.velocity.X * npc.direction< 5)
                {
                    npc.velocity.X += npc.direction * 0.3f;
                }
                if (npc.velocity.Y == 0)
                {
                    npc.directionY *= -1;
                }
                if (npc.directionY == 1)
                {
                    if (npc.velocity.Y < 3.75f)
                    {
                        npc.velocity.Y += 0.1f;
                    }
                    else
                    {
                        npc.directionY *= -1;
                    }
                }
                else
                {
                    if (npc.velocity.Y > -4)
                    {
                        npc.velocity.Y -= 0.15f;
                    }
                    else
                    {
                        npc.directionY *= -1;
                    }
                }
                npc.ai[1] = 0;
                npc.ai[2] = 0;
                npc.ai[3] = 1;
                npc.life = npc.life < npc.lifeMax ? npc.life + 1 + (int)((float)npc.lifeMax * 0.001f) : npc.lifeMax;
                if (Vector2.Distance(npc.Center, P.Center) < 1600)
                {
                    npc.ai[0]++;
                }
            }
            else
            {
                if (npc.ai[3] > 0)
                {
                    npc.damage = 1;
                    npc.ai[1] = 0;
                    npc.ai[2] = 0;
                    if (npc.ai[3] == 1)
                    {
                        npc.velocity = npc.DirectionTo(P.Center) * 12;
                        npc.direction = npc.velocity.X > 0 ? 1 : -1;
                    }
                    if (npc.ai[3] == 2)
                    {
                        npc.ai[0]++;
                        if (npc.direction == 1)
                        {
                            P.position = new Vector2(npc.position.X + 74 * npc.scale, npc.position.Y + 72 * npc.scale);
                        }
                        else
                        {
                            P.position = new Vector2(npc.position.X + 16 * npc.scale, npc.position.Y + 72 * npc.scale);
                        }
                        P.mount.Dismount(P);
                        npc.velocity.Y = npc.Center.Y < 666 ? 1 : -7;
                        npc.velocity.X = npc.direction * 10;
                        P.velocity = npc.velocity;
                        if (!Collision.SolidCollision(npc.position, npc.width, npc.height) && (npc.ai[0] > 120 || Vector2.Distance(npc.Center, P.Center) > 80 || npc.Center.Y < 666))
                        {
                            P.fallStart = (int)(P.position.Y / 16);
                            npc.ai[3] = 0;
                        }
                    }
                    else
                    {
                        npc.ai[0] = 2;
                    }
                }
                else
                {
                    npc.damage = 30 * (Main.expertMode ? 2 : 1);
                    npc.ai[0]++;
                    npc.ai[1]++;
                    npc.ai[2]++;
                    if (npc.ai[0] > 1500 && npc.Center.Y < P.Center.Y - 160)
                    {
                        npc.ai[3] = 1;
                    }
                    if (npc.ai[2] > 480 && npc.Center.Y < P.Center.Y)
                    {
                        npc.velocity = Vector2.Zero;
                        npc.direction = P.Center.X < npc.Center.X ? -1 : 1;
                        npc.spriteDirection = npc.direction;
                        npc.ai[1]++;
                        if (npc.ai[2] > 720)
                        {
                            npc.ai[2] = 0;
                        }
                    }
                    else
                    {
                        if (npc.Center.X < P.Center.X - 300)
                        {
                            npc.direction = 1;
                        }
                        if (npc.Center.X > P.Center.X + 300)
                        {
                            npc.direction = -1;
                        }
                        if (npc.Center.Y < P.Center.Y - 200)
                        {
                            npc.directionY = 1;
                        }
                        if (npc.Center.Y > P.Center.Y + 50)
                        {
                            npc.directionY = -1;
                        }
                        if (npc.velocity.X * npc.direction < 8)
                        {
                            npc.velocity.X += npc.direction * 0.3f;
                        }
                        if (npc.velocity.Y * npc.directionY < 6)
                        {
                            npc.velocity.Y += npc.directionY * 0.2f;
                        }
                    }
                    if (npc.ai[1] > (Main.expertMode ? 60 : 90) && npc.velocity.Y <= 0 && npc.Center.Y < P.Center.Y)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 39);
                        float Speed = 10f;
                        int damage = 15;
                        int type = mod.ProjectileType("RocFeather");
                        float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                        int numberProjectiles = 2 + Main.rand.Next(3);
                        if (Main.netMode != 1)
                        {
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1)).RotatedByRandom(MathHelper.ToRadians(30));                                                                                        // perturbedSpeed = perturbedSpeed * scale; 
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 1, Main.myPlayer);
                            }
                        }
                        npc.ai[1] = 0;
                    }
                }
            }
            npc.netUpdate = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.ai[3] == 1 && npc.ai[0] > 1)
            {
                Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, (Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type]) * 0.5f);
                SpriteEffects spriteEffects = SpriteEffects.None;
                if (npc.spriteDirection == 1)
                {
                    spriteEffects = SpriteEffects.FlipHorizontally;
                }
                for (int i = 0; i < npc.oldPos.Length; i++)
                {
                    Color color = npc.GetAlpha(drawColor) * ((npc.oldPos.Length - i) / (float)npc.oldPos.Length) * 0.8f;
                    Vector2 drawPos = npc.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(-14, npc.gfxOffY - 23);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, (Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type]) * (npc.frame.Y / 120), Main.npcTexture[npc.type].Width, Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type]));
                    spriteBatch.Draw(Main.npcTexture[npc.type], drawPos, rect, color, npc.rotation, drawOrigin, npc.scale, spriteEffects, 0f);
                }
            }
            return true;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
    }
}

