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
    public class EarthElemental : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earth Elemental");
            NPCID.Sets.TrailingMode[npc.type] = 0;
            NPCID.Sets.TrailCacheLength[npc.type] = 6;
        }
        public override void SetDefaults()
        {
            npc.width = 44;
            npc.height = 44;
            npc.damage = 30;
            npc.defense = 30;
            npc.lifeMax = 600;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.value = Item.buyPrice(0, 0, 7, 50);
            npc.knockBackResist = 0.05f;
            npc.aiStyle = 26;
            aiType = NPCID.Unicorn;
            banner = npc.type;
            bannerItem = mod.ItemType("EarthElementalBanner");

        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EarthEssence"), (Main.expertMode ? Main.rand.Next(12, 30) : Main.rand.Next(8, 20)));
            if (Main.rand.Next(50) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SecondAnniversary"), 1);
            }
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (npc.ai[2] >= 100 && (npc.ai[2] < 300 || npc.ai[2] > 400))
            {
                damage *= 2;
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/EarthElemental"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/EarthElemental"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/EarthElemental"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/EarthElemental"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/EarthElemental"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/EarthElemental"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/EarthElemental"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/EarthElemental"), 1f);
            }

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY];
            return !spawnInfo.playerInTown && ((!spawnInfo.invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse) || spawnInfo.spawnTileY > Main.worldSurface) && (tile.type == 1 || tile.type == 25 || tile.type == 117 || tile.type == 203 || tile.type == 357 || tile.type == 367 || tile.type == 368 || tile.type == 369) && Main.hardMode ? 0.06f : 0f;
        }
        public override void AI()
        {
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            if (npc.ai[2] < 400)
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
            npc.spriteDirection = npc.direction;
            npc.netUpdate = true;
            npc.rotation += (npc.velocity.X * 0.0174f * 2.5f);
            if (Main.expertMode)
            {
                npc.ai[1]++;
                if (npc.velocity.Y == 0 && npc.ai[1] > 300 && npc.ai[2] < 1 && Collision.CanHitLine(npc.Center, 1, 1, P.Center, 1, 1))
                {
                    npc.ai[2] = 1;
                }
                if (npc.ai[2] > 0)
                {
                    npc.knockBackResist = 0f;
                    npc.aiStyle = -1;
                    npc.ai[2]++;
                    npc.rotation += (npc.direction * 0.0174f * npc.ai[2]);
                    int freq = (int)((100 - npc.ai[2]) / 10) + 10;
                    if (npc.ai[2] == 90)
                    {
                        Main.PlaySound(2, (int)npc.Center.X, (int)npc.Center.Y, 22, 0.5f, -0.1f);
                    }
                    if (npc.ai[2] == 100)
                    {
                        if (npc.Center.Y > P.Center.Y + 100 && Math.Abs(npc.Center.X - P.Center.X) < 400)
                        {
                            npc.velocity.Y = -4;
                            npc.ai[2] = 300;
                        }
                        else
                        {
                            npc.velocity.X = 15 * npc.direction;
                        }
                    }
                    if (npc.ai[2] >= 300)
                    {
                        if (npc.velocity.Y == 0 && npc.oldVelocity.Y > 0)
                        {
                            if (npc.ai[2] < 400)
                            {
                                npc.velocity.X = 5 * npc.direction;
                                npc.velocity.Y = -18;
                                npc.ai[2] = 400;
                            }
                            else if (npc.ai[2] < 600)
                            {
                                npc.velocity.X = 4 * npc.direction;
                                npc.velocity.Y = -14;
                                npc.ai[2] = 600;
                            }
                            else if (npc.ai[2] < 800)
                            {
                                npc.velocity.X = 3 * npc.direction;
                                npc.velocity.Y = -10;
                                npc.ai[2] = 800;
                            }
                            else
                            {
                                npc.ai[1] = 0;
                                npc.ai[2] = 0;
                            }
                            Main.PlaySound(SoundID.Item10, npc.Center);
                        }
                        if (npc.velocity.X == 0 && npc.oldVelocity.X != 0)
                        {
                            npc.velocity.X = -npc.oldVelocity.X;
                            npc.direction = Math.Sign(npc.velocity.X);
                            Main.PlaySound(2, (int)npc.Center.X, (int)npc.Center.Y, 10, 0.5f);
                        }
                    }
                    else if (npc.ai[2] >= 100)
                    {
                        if (Collision.SolidCollision(npc.position + new Vector2(npc.velocity.X, 0), npc.width, npc.height))
                        {
                            npc.velocity.Y = -15;
                            Main.PlaySound(SoundID.Item10, npc.Center);
                        }
                        if (npc.ai[2] > 200 || npc.velocity.X == 0)
                        {
                            npc.ai[1] = 0;
                            npc.ai[2] = 0;
                            npc.velocity.X = -npc.oldVelocity.X;
                            npc.velocity.Y = -8;
                            Main.PlaySound(SoundID.Item10, npc.Center);
                        }
                    }
                    else
                    {
                        npc.velocity = Vector2.Zero;
                        if (npc.ai[2] % freq == 0)
                        {
                            Main.PlaySound(2, (int)npc.Center.X, (int)npc.Center.Y, 23, 0.5f, -0.2f);
                        }
                        Vector2 posi = new Vector2(npc.Center.X, npc.position.Y + npc.height + 4);
                        Point pos = posi.ToTileCoordinates();
                        Tile tile = Framing.GetTileSafely(pos.X, pos.Y);
                        if (tile.active())
                        {
                            Dust dust = Main.dust[WorldGen.KillTile_MakeTileDust(pos.X, pos.Y, tile)];
                            dust.velocity.Y = -npc.ai[2] / 30;
                            dust.velocity.X = -npc.direction * npc.ai[2] / 10;
                        }
                    }
                }
                else
                {
                    npc.aiStyle = 26;
                    npc.knockBackResist = 0.05f;
                }
            }
            npc.velocity.Y += 0.2f;
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
            if (npc.ai[2] >= 100 && (npc.ai[2] < 300 || npc.ai[2] > 400))
            {
                Texture2D tex = Main.npcTexture[npc.type];
                Vector2 drawOrigin = new Vector2((tex.Width / 2), (tex.Height / 2));
                for (int i = 0; i < npc.oldPos.Length; i++)
                {
                    Color color2 = drawColor * ((npc.oldPos.Length - i) / (float)npc.oldPos.Length) * 0.8f;
                    Vector2 drawPos = npc.oldPos[i] - Main.screenPosition + new Vector2(npc.width / 2, npc.height / 2) + new Vector2(0f, npc.gfxOffY);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height));
                    spriteBatch.Draw(tex, drawPos, rect, color2, npc.rotation, drawOrigin, npc.scale, effects, 0f);
                }
            }
            return base.PreDraw(spriteBatch, drawColor);
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
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
            Texture2D tex = ModContent.GetTexture("JoostMod/NPCs/EarthElemental_Pupils");
            Rectangle rect = new Rectangle(0, 0, tex.Width, tex.Height);
            Vector2 drawOrigin = new Vector2((tex.Width / 2), (tex.Height / 2));
            Vector2 offSet = npc.DirectionTo(Main.player[npc.target].Center) * 2;
            spriteBatch.Draw(tex, new Vector2(npc.position.X - Main.screenPosition.X + (float)(npc.width / 2) - (float)(tex.Width) / 2f + drawOrigin.X + offSet.X, npc.position.Y - Main.screenPosition.Y + npc.height - tex.Height + 4f + drawOrigin.Y + offSet.Y), new Rectangle?(rect), drawColor, npc.rotation, drawOrigin, 1f, effects, 0f);

        }
    }
}

