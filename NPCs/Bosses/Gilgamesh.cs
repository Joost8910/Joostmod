using System;
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
            NPCID.Sets.TrailCacheLength[npc.type] = 8;
        }
		public override void SetDefaults()
		{
			npc.width = 116;
			npc.height = 148;
			npc.damage = 140;
			npc.defense = 40;
			npc.lifeMax = 280000;
			npc.boss = true;
			npc.lavaImmune = true;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath3;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			npc.knockBackResist = 0f;
			npc.aiStyle = -1;
            npc.buffImmune[BuffID.Ichor] = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ClashOnTheBigBridge");
			npc.frameCounter = 0;
            musicPriority = MusicPriority.BossHigh;
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
			if (npc.velocity.Y == 0)
			{
				npc.frameCounter += Math.Abs(npc.velocity.X);
				if (npc.frameCounter >= 80)
				{
					npc.frameCounter = 0;	
					npc.frame.Y = (npc.frame.Y + 148);		
				}
				if (npc.frame.Y >= 592)
				{
					npc.frame.Y = 0;	
				}
			}
            else
            {
                npc.frame.Y = 148;
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
            npc.direction = npc.velocity.X > 0 ? 1 : -1;
            npc.netUpdate = true;
            float moveSpeed = 7;
            if (npc.Distance(P.Center) > 200)
            {
                moveSpeed = 8;
            }
            if (npc.Distance(P.Center) > 400)
            {
                moveSpeed = 9;
            }
            if (npc.Distance(P.Center) > 600)
            {
                moveSpeed = 10;
            }
            if (npc.Distance(P.Center) > 800)
            {
                moveSpeed = npc.Distance(P.Center) / 50;
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
            if (npc.Distance(P.Center) < 250)
            {
                if ((npc.ai[1] % 30) == 0)
                {
                    float Speed = 15f;
                    int damage = 50;
                    int type = mod.ProjectileType("GilgNaginata");
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 1);
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 15f, Main.myPlayer, npc.whoAmI);
                    }
                }
                npc.direction = npc.Center.X < P.Center.X ? 1 : -1;
            }


            if (npc.ai[1] > 240 && (npc.velocity.Y == 0 || (!Collision.CanHitLine(npc.position, npc.width, npc.height, P.position, P.width, P.height) && npc.ai[1] > 600)) && npc.Center.Y > P.Center.Y + 100)
            {
                npc.ai[1] = 0;
                npc.ai[3] = 1;
                npc.velocity.X = 0;
                npc.velocity.Y = -(float)Math.Sqrt(2 * 0.3f * Math.Abs((P.position.Y - 250) - (npc.position.Y + npc.height)));
            }
            if (npc.ai[3] >= 1)
            {
                npc.ai[3]++;
                npc.ai[1] = 0;
                if (npc.velocity.Y < 0)
                {
                    npc.noTileCollide = true;
                }
                else
                {
                    npc.noTileCollide = false;
                }
                if (Math.Abs(npc.position.Y + npc.height - P.position.Y) < 80 && npc.ai[3] < 180)
                {
                    npc.velocity.Y = 0;
                    npc.velocity.X = npc.Center.X < P.Center.X ? moveSpeed * 2 : moveSpeed * -2;
                    float Speed = 15f;
                    int damage = 60;
                    int type = mod.ProjectileType("GilgNaginata");
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 1);
                    float rotation = (float)Math.Atan2(npc.Center.Y - P.Center.Y, npc.Center.X - P.Center.X);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 15f, Main.myPlayer, npc.whoAmI);
                    }
                    npc.ai[3] = 180;
                }
            }
            else
            {
                if (npc.Center.Y > P.Center.Y + 100 && npc.velocity.Y == 0)
                {
                    npc.velocity.Y = -(float)Math.Sqrt(2 * 0.3f * Math.Abs(P.position.Y - (npc.position.Y + npc.height)));
                }
                if (npc.Center.Y < P.Center.Y - 150)
                {
                    npc.position.Y++;
                }
                if (npc.velocity.Y >= 0 && npc.velocity.X == 0)
                {
                    npc.velocity.Y = -8f;
                }
                npc.velocity.X = npc.Center.X < P.Center.X ? moveSpeed : -moveSpeed;             
            }
            if (npc.ai[3] >= 210)
            {
                npc.ai[3] = 0;
                npc.noTileCollide = false;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.ai[3] >= 1)
            {
                Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
                SpriteEffects spriteEffects = SpriteEffects.None;
                if (npc.spriteDirection == 1)
                {
                    spriteEffects = SpriteEffects.FlipHorizontally;
                }
                for (int i = 0; i < npc.oldPos.Length; i++)
                {
                    Color color = drawColor * ((npc.oldPos.Length - i) / (float)npc.oldPos.Length) * 0.8f;
                    Vector2 drawPos = npc.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, (Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type]) * (npc.frame.Y / 148), Main.npcTexture[npc.type].Width, Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type]));
                    spriteBatch.Draw(Main.npcTexture[npc.type], drawPos, rect, color, npc.rotation, drawOrigin, npc.scale, spriteEffects, 0f);
                }
            }
            return true;
        }
        public override bool CheckDead()
        {
            if (Main.netMode != 1)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 10, mod.NPCType("Gilgamesh2"));
            }   
            Main.NewText("<Gilgamesh> Enough expository banter!", 225, 25, 25);
            Main.NewText("Now, we fight like men! And ladies!", 225, 25, 25);
            Main.NewText("And ladies who dress like men!", 225, 25, 25);
            Main.NewText("For Gilgamesh...it is morphing time!", 225, 75, 25);
            for (int i = 0; i < npc.width / 8; i++)
            {
                for (int j = 0; j < npc.height / 8; j++)
                {
                    Dust.NewDust(npc.position + new Vector2(i, j), 8, 8, DustID.Smoke, 0, 0, 0, Color.OrangeRed, 2);
                }
            }
            return true;
        }
	}
}

