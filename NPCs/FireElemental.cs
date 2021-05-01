using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
    public class FireElemental : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Elemental");
            Main.npcFrameCount[npc.type] = 5;
        }
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 62;
            npc.damage = 40;
            npc.defense = 18;
            npc.lifeMax = 500;
            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.value = Item.buyPrice(0, 0, 7, 50);
            npc.knockBackResist = 0.35f;
            npc.aiStyle = 22;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.frameCounter = 0;
            aiType = NPCID.Wraith;
            banner = npc.type;
            bannerItem = mod.ItemType("FireElementalBanner");
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.lavaImmune = true;
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2701, Main.rand.Next(10, 20));
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FireEssence"), (Main.expertMode ? Main.rand.Next(12, 30) : Main.rand.Next(8, 20)));
            if (Main.rand.Next(50) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SecondAnniversary"), 1);
            }
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (npc.localAI[1] >= 40)
            {
                //crit = true;
                damage *= 2;
                target.AddBuff(BuffID.OnFire, 900);
            }
            else
            {
                target.AddBuff(BuffID.OnFire, 420);
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 127, 2.5f * (float)hitDirection, Main.rand.Next(-5, 5), 0, default(Color), 0.7f);
                }
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY];
            return !spawnInfo.playerInTown && spawnInfo.spawnTileY >= Main.maxTilesY - 250 && Main.hardMode ? 0.06f : 0f;

        }
        public override void AI()
        {
            npc.localAI[0]++;
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            if (Main.expertMode && npc.localAI[0] > 800 && npc.ai[3] < 370 && npc.ai[3] > 60)
            {
                npc.aiStyle = -1;
                npc.knockBackResist = 0f;
                if (npc.localAI[1] < 1)
                {
                    npc.targetRect = P.getRect();
                    npc.FaceTarget();
                    float speed = (Math.Abs(npc.Center.X - P.Center.X) - 200) / 5f;
                    if (speed < -6)
                    {
                        speed = -6;
                    }
                    if (speed > 6)
                    {
                        speed = 6;
                    }
                    npc.velocity.X = speed * npc.direction;
                    npc.velocity.Y = 4 * npc.directionY;
                    if (Math.Abs(npc.Center.Y - P.Center.Y) < 10 && Math.Abs(npc.Center.X - P.Center.X) < 400)
                    {
                        npc.localAI[1]++;
                        npc.netUpdate = true;
                    }
                }
                else
                {
                    if (npc.localAI[1] == 1)
                    {
                        Main.PlaySound(42, (int)npc.Center.X, (int)npc.Center.Y, 230, 1, -0.4f);
                    }
                    npc.velocity = Vector2.Zero;
                    npc.localAI[1]++;
                    if (npc.localAI[1] >= 40)
                    {
                        npc.width = 56;
                        if (npc.localAI[1] == 40)
                        {
                            Main.PlaySound(42, (int)npc.Center.X, (int)npc.Center.Y, 217, 1, 0.1f);
                        }
                        npc.velocity.X = 15 * npc.direction;
                        Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Fire, -npc.velocity.X, -npc.velocity.Y, 0, default(Color), 4f).noGravity = true;
                    }
                    if (npc.localAI[1] > 90)
                    {
                        npc.width = 32;
                        npc.localAI[1] = 0;
                        npc.localAI[0] = 0;
                    }
                }
            }
            else
            {
                npc.knockBackResist = 0.35f;
                npc.aiStyle = 22;
                npc.FaceTarget();
                Vector2 spherePos = new Vector2(npc.Center.X, npc.position.Y - 20);
                npc.ai[3]++;
                if (npc.ai[3] == 370)
                {
                    Main.PlaySound(SoundID.Item20, npc.Center);
                }
                if (npc.ai[3] > 370)
                {
                    npc.velocity *= 0.8f;
                    int amount = (int)(npc.ai[3] - 370);
                    for (int d = 0; d < amount; d++)
                    {
                        Dust dust = Dust.NewDustDirect(spherePos - new Vector2(5, 5), 10, 10, 6, npc.velocity.X * 0.8f, npc.velocity.Y * 0.8f, 0, default(Color), 1.5f);
                        Vector2 vel = spherePos - dust.position;
                        vel.Normalize();
                        dust.position -= vel * 12;
                        dust.velocity = vel + npc.velocity * 0.8f;
                        dust.noGravity = true;
                    }
                    npc.netUpdate = true;
                }
                if (npc.ai[3] > 400)
                {
                    if (Main.netMode != 1)
                    {
                        NPC.NewNPC((int)spherePos.X, (int)spherePos.Y, NPCID.BurningSphere);
                    }
                    Main.PlaySound(SoundID.Item45, npc.Center);
                    npc.ai[3] = 0;
                }
            }

            if (Collision.SolidCollision(npc.position, npc.width, npc.height))
            {
                npc.velocity *= 0.92f;
            }
            if ((npc.localAI[0] % 10) == 0)
            {
                int dustGen = Main.rand.Next(3);
                int dustType = 158;
                if (dustGen == 1)
                {
                    dustType = 6;
                }
                if (dustGen == 2)
                {
                    dustType = 127;
                }
                int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
                Dust dust = Main.dust[dustIndex];
                dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
                dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
                dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            frameHeight = 74;
            npc.spriteDirection = npc.direction;
            if (npc.localAI[1] > 0)
            {
                if (npc.localAI[1] < 20)
                {
                    npc.frame.X = 140 * 4;
                }
                else if (npc.localAI[1] < 40)
                {
                    npc.frame.X = 140 * 5;
                }
                else
                {
                    npc.frame.X = 140 * 6;
                }
                npc.frame.Y = frameHeight * ((int)(npc.localAI[1] / 4) % 5);
            }
            else
            {
                npc.frameCounter++;
                if (npc.frameCounter >= 5)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = (npc.frame.Y + frameHeight);
                }
                if (npc.frame.Y >= frameHeight * 5)
                {
                    npc.frame.Y = 0;
                }
                if (npc.ai[3] > 360)
                {
                    npc.frame.X = 140 * 3;
                }
                else
                {
                    npc.frame.X = 0;
                    if (npc.velocity.X * npc.direction > 1)
                    {
                        npc.frame.X = 140;
                    }
                    if (npc.velocity.X * npc.direction < -1)
                    {
                        npc.frame.X = 140 * 2;
                    }
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (npc.spriteDirection == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                effects = SpriteEffects.None;
            }
            int xFrameCount = 7;
            Color alpha = Color.White;
            alpha *= 0.8f;
            Texture2D texture = Main.npcTexture[npc.type];
            Rectangle rect = new Rectangle((int)npc.frame.X, (int)npc.frame.Y, (texture.Width / xFrameCount), (texture.Height / Main.npcFrameCount[npc.type]));
            Vector2 vect = new Vector2((float)((texture.Width / xFrameCount) / 2), (float)((texture.Height / Main.npcFrameCount[npc.type]) / 2));
            spriteBatch.Draw(texture, new Vector2(npc.position.X - Main.screenPosition.X + (float)(npc.width / 2) - (float)(texture.Width / xFrameCount) / 2f + vect.X, npc.position.Y - Main.screenPosition.Y + (float)npc.height - (float)(texture.Height / Main.npcFrameCount[npc.type]) + 4f + vect.Y), new Rectangle?(rect), alpha, npc.rotation, vect, 1f, effects, 0f);
            return false;
        }

    }
}

