using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
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
            Main.npcFrameCount[NPC.type] = 6;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            NPCID.Sets.TrailCacheLength[NPC.type] = 5;
        }
		public override void SetDefaults()
		{
			NPC.width = 110;
			NPC.height = 92;
			NPC.damage = 30;
			NPC.defense = 12;
			NPC.lifeMax = 1700;
			NPC.HitSound = SoundID.NPCHit28;
			NPC.DeathSound = SoundID.NPCDeath31;
			NPC.value = 0;
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1;
			NPC.noGravity = true;
            NPC.netAlways = true;
            NPC.noTileCollide = true;
		}
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * bossLifeScale + 1);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.SpawnTileY < Main.worldSurface*0.35f && !JoostWorld.downedRoc && JoostWorld.activeQuest.Contains(NPC.type) && !NPC.AnyNPCs(NPC.type) ? 0.15f : 0f;
        }
        public override void OnKill()
		{
            JoostWorld.downedRoc = true;
            NPC.DropItemInstanced(NPC.position, NPC.Size, Mod.Find<ModItem>("Roc").Type, 1, false);
            if (Main.expertMode && Main.rand.Next(100) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("EvilStone").Type, 1);
            }
        }
		public override void HitEffect(int hitDirection, double damage)
        {
            NPC.ai[0]++;
            if (NPC.life <= 0)
			{
				Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/Roc"), 1f);
				Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/Roc2"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/Roc2"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/Roc3"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/Roc3"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/Roc3"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/Roc3"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/Roc3"), 1f);
            }

        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            Player P = Main.player[NPC.target];
            if (NPC.velocity.Y > 0 || (NPC.ai[3] > 0 && NPC.ai[0] > 1))
            {
                NPC.frame.Y = 0;
            }
            else
            {
                NPC.frameCounter++;
                if (NPC.frameCounter > 5 + NPC.velocity.Y/3)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += 120;
                    if (NPC.frame.Y == 120)
                    {
                        SoundEngine.PlaySound(SoundID.Item32, NPC.position);
                    }
                }
                if (NPC.frame.Y >= 720)
                {
                    NPC.frame.Y = 0;
                }
            }
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return NPC.ai[3] != 2 && base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (NPC.ai[3] == 1)
            {
                NPC.ai[3] = 2;
                NPC.target = target.whoAmI;
            }
        }
        public override void AI()
		{
            if (NPC.Center.Y < 666)
            {
                NPC.directionY = 1;
                NPC.velocity.Y = 6;
            }
            Player P = Main.player[NPC.target];
            if (Vector2.Distance(NPC.Center, P.Center) > 2500 || NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(false);
                P = Main.player[NPC.target];
                if (!P.active || P.dead || Vector2.Distance(NPC.Center, P.Center) > 2500)
                {
                    NPC.ai[0] = 0;
                }
            }
            if (NPC.ai[0] < 1)
            {
                if (Main.rand.Next(100) == 0)
                {
                    NPC.direction *= -1;
                }
                if (NPC.velocity.X * NPC.direction< 5)
                {
                    NPC.velocity.X += NPC.direction * 0.3f;
                }
                if (NPC.velocity.Y == 0)
                {
                    NPC.directionY *= -1;
                }
                if (NPC.directionY == 1)
                {
                    if (NPC.velocity.Y < 3.75f)
                    {
                        NPC.velocity.Y += 0.1f;
                    }
                    else
                    {
                        NPC.directionY *= -1;
                    }
                }
                else
                {
                    if (NPC.velocity.Y > -4)
                    {
                        NPC.velocity.Y -= 0.15f;
                    }
                    else
                    {
                        NPC.directionY *= -1;
                    }
                }
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 1;
                NPC.life = NPC.life < NPC.lifeMax ? NPC.life + 1 + (int)((float)NPC.lifeMax * 0.001f) : NPC.lifeMax;
                if (Vector2.Distance(NPC.Center, P.Center) < 1600)
                {
                    NPC.ai[0]++;
                }
            }
            else
            {
                if (NPC.ai[3] > 0)
                {
                    NPC.damage = 1;
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    if (NPC.ai[3] == 1)
                    {
                        NPC.velocity = NPC.DirectionTo(P.Center) * 12;
                        NPC.direction = NPC.velocity.X > 0 ? 1 : -1;
                    }
                    if (NPC.ai[3] == 2)
                    {
                        NPC.ai[0]++;
                        if (NPC.direction == 1)
                        {
                            P.position = new Vector2(NPC.position.X + 74 * NPC.scale, NPC.position.Y + 72 * NPC.scale);
                        }
                        else
                        {
                            P.position = new Vector2(NPC.position.X + 16 * NPC.scale, NPC.position.Y + 72 * NPC.scale);
                        }
                        P.mount.Dismount(P);
                        NPC.velocity.Y = NPC.Center.Y < 666 ? 1 : -7;
                        NPC.velocity.X = NPC.direction * 10;
                        P.velocity = NPC.velocity;
                        if (!Collision.SolidCollision(NPC.position, NPC.width, NPC.height) && (NPC.ai[0] > 120 || Vector2.Distance(NPC.Center, P.Center) > 80 || NPC.Center.Y < 666))
                        {
                            P.fallStart = (int)(P.position.Y / 16);
                            NPC.ai[3] = 0;
                        }
                    }
                    else
                    {
                        NPC.ai[0] = 2;
                    }
                }
                else
                {
                    NPC.damage = 30 * (Main.expertMode ? 2 : 1);
                    NPC.ai[0]++;
                    NPC.ai[1]++;
                    NPC.ai[2]++;
                    if (NPC.ai[0] > 1500 && NPC.Center.Y < P.Center.Y - 160)
                    {
                        NPC.ai[3] = 1;
                    }
                    if (NPC.ai[2] > 480 && NPC.Center.Y < P.Center.Y)
                    {
                        NPC.velocity = Vector2.Zero;
                        NPC.direction = P.Center.X < NPC.Center.X ? -1 : 1;
                        NPC.spriteDirection = NPC.direction;
                        NPC.ai[1]++;
                        if (NPC.ai[2] > 720)
                        {
                            NPC.ai[2] = 0;
                        }
                    }
                    else
                    {
                        if (NPC.Center.X < P.Center.X - 300)
                        {
                            NPC.direction = 1;
                        }
                        if (NPC.Center.X > P.Center.X + 300)
                        {
                            NPC.direction = -1;
                        }
                        if (NPC.Center.Y < P.Center.Y - 200)
                        {
                            NPC.directionY = 1;
                        }
                        if (NPC.Center.Y > P.Center.Y + 50)
                        {
                            NPC.directionY = -1;
                        }
                        if (NPC.velocity.X * NPC.direction < 8)
                        {
                            NPC.velocity.X += NPC.direction * 0.3f;
                        }
                        if (NPC.velocity.Y * NPC.directionY < 6)
                        {
                            NPC.velocity.Y += NPC.directionY * 0.2f;
                        }
                    }
                    if (NPC.ai[1] > (Main.expertMode ? 60 : 90) && NPC.velocity.Y <= 0 && NPC.Center.Y < P.Center.Y)
                    {
                        SoundEngine.PlaySound(SoundID.Item39, NPC.position);
                        float Speed = 10f;
                        int damage = 15;
                        int type = Mod.Find<ModProjectile>("RocFeather").Type;
                        float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                        int numberProjectiles = 2 + Main.rand.Next(3);
                        if (Main.netMode != 1)
                        {
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1)).RotatedByRandom(MathHelper.ToRadians(30));                                                                                        // perturbedSpeed = perturbedSpeed * scale; 
                                Projectile.NewProjectile(NPC.Center.X, NPC.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 1, Main.myPlayer);
                            }
                        }
                        NPC.ai[1] = 0;
                    }
                }
            }
            NPC.netUpdate = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.ai[3] == 1 && NPC.ai[0] > 1)
            {
                Vector2 drawOrigin = new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, (TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type]) * 0.5f);
                SpriteEffects spriteEffects = SpriteEffects.None;
                if (NPC.spriteDirection == 1)
                {
                    spriteEffects = SpriteEffects.FlipHorizontally;
                }
                for (int i = 0; i < NPC.oldPos.Length; i++)
                {
                    Color color = NPC.GetAlpha(drawColor) * ((NPC.oldPos.Length - i) / (float)NPC.oldPos.Length) * 0.8f;
                    Vector2 drawPos = NPC.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(-14, NPC.gfxOffY - 23);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, (TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type]) * (NPC.frame.Y / 120), TextureAssets.Npc[NPC.type].Value.Width, TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type]));
                    spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, drawPos, rect, color, NPC.rotation, drawOrigin, NPC.scale, spriteEffects, 0f);
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
            rotation = NPC.rotation;
        }
    }
}

