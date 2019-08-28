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
    public class DeathKnight : ModNPC
    {
        bool slash = false;
        int slashTimer = 0;
        int chance = 0;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(slash);
            writer.Write(slashTimer);
            writer.Write(chance);
        }
        
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            slash = reader.ReadBoolean();
            slashTimer = reader.ReadInt32();
            chance = reader.ReadInt32();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death Knight");
            Main.npcFrameCount[npc.type] = 8;
        }
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 68;
            npc.damage = 110;
            npc.defense = 60;
            npc.lifeMax = 4000;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.value = Item.buyPrice(0, 5, 0, 0);
            npc.knockBackResist = 0f;
            npc.aiStyle = 0;
            npc.frameCounter = 0;
            banner = npc.type;
            bannerItem = mod.ItemType("DeathKnightBanner");

        }
        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Deathbringer"));
                }
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SigilofSkulls"));
                }
            }
            else
            {
                if (Main.rand.Next(15) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Deathbringer"));
                }
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SigilofSkulls"));
                }
            }
            if (Main.rand.Next(20) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ThirdAnniversary"), 1);
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position + new Vector2(24, 10), npc.velocity, mod.GetGoreSlot("Gores/DeathKnight1"), 1f);
                Gore.NewGore(npc.position + new Vector2(40, 24), npc.velocity, mod.GetGoreSlot("Gores/DeathKnight2"), 1f);
                Gore.NewGore(npc.position + new Vector2(14, 24), npc.velocity, mod.GetGoreSlot("Gores/DeathKnight2"), 1f);
                Gore.NewGore(npc.position + new Vector2(26, 34), npc.velocity, mod.GetGoreSlot("Gores/DeathKnight3"), 1f);
                Gore.NewGore(npc.position + new Vector2(12, 44), npc.velocity, mod.GetGoreSlot("Gores/DeathKnight4"), 1f);
                Gore.NewGore(npc.position + new Vector2(42, 44), npc.velocity, mod.GetGoreSlot("Gores/DeathKnight4"), 1f);
                Gore.NewGore(npc.position + new Vector2(20, 66), npc.velocity, mod.GetGoreSlot("Gores/DeathKnight5"), 1f);
                Gore.NewGore(npc.position + new Vector2(28, 66), npc.velocity, mod.GetGoreSlot("Gores/DeathKnight5"), 1f);
                for (int k = 0; k < 50; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 109, 2.5f * (float)hitDirection, Main.rand.Next(-5, 5), 0, default(Color), 0.7f);
                }
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY];
            return (tile.type == 41 || tile.type == 43 || tile.type == 44) && spawnInfo.planteraDefeated && spawnInfo.spawnTileY >= Main.rockLayer && Main.hardMode ? 0.01f : 0f;
        }
        public override void AI()
        {
            npc.ai[0]++;
            npc.netUpdate = true;
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            if (chance != 0)
            {
                chance = Main.rand.Next(300);
            }
            else
            {
                if (Main.rand.Next(4) == 0)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 20, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), 0, default(Color), 0.7f);
                }
                if (npc.velocity.Y == 0 && Collision.CanHitLine(new Vector2(npc.Center.X, npc.Center.Y), 1, 1, new Vector2(P.Center.X, P.Center.Y), 1, 1))
                {
                    npc.velocity.Y = -(float)Math.Sqrt(2 * 0.3f * Math.Abs(P.position.Y - npc.Center.Y));
                    npc.velocity.X = (P.Center.X - npc.Center.X) / 30;
                    slash = true;
                    chance = 1;
                }
            }
            if (slash)
            {
                npc.rotation += 36 * npc.direction * 0.0174f;
                if (slashTimer % 10 == 0)
                {
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 0, mod.ProjectileType("Deathbringer"), 50, 15f, Main.myPlayer, npc.whoAmI);
                    }
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 7);
                }
                slashTimer++;
                if (npc.velocity.Y == 0)
                {
                    slash = false;
                    slashTimer = 0;
                }
            }
            else
            {
                npc.rotation = npc.velocity.X * 0.0174f * 1.5f;
            }
            if (npc.velocity.X == 0 && npc.velocity.Y == 0)
            {
                npc.velocity.Y = -8f;
            }
            if (P.Center.X < npc.Center.X && npc.velocity.X > -6.5f)
            {
                npc.velocity.X -= 0.2f;
            }
            if (npc.velocity.X < -6.5f)
            {
                npc.velocity.X = -6.5f;
            }
            if (npc.velocity.X > 6.5f)
            {
                npc.velocity.X = 6.5f;
            }
            if (P.Center.X > npc.Center.X && npc.velocity.X < 6.5f)
            {
                npc.velocity.X += 0.2f;
            }
            if (npc.life < npc.lifeMax / 2)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 109, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), 0, default(Color), 0.7f);
                if (npc.ai[0] % 180 == 0 && Main.netMode != 1)
                {
                    NPC.NewNPC((int)npc.Center.X + 60, (int)npc.Center.Y + 60, 34, 0, npc.whoAmI);
                    NPC.NewNPC((int)npc.Center.X - 60, (int)npc.Center.Y + 60, 34, 0, npc.whoAmI);
                    NPC.NewNPC((int)npc.Center.X + 60, (int)npc.Center.Y - 60, 34, 0, npc.whoAmI);
                    NPC.NewNPC((int)npc.Center.X - 60, (int)npc.Center.Y - 60, 34, 0, npc.whoAmI);
                }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frameCounter++;
            if (npc.velocity.Y == 0)
            {
                if (npc.frameCounter >= 10)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = (npc.frame.Y + 68);
                }
                if (npc.frame.Y >= 408)
                {
                    npc.frame.Y = 0;
                }
            }
            else
            {
                if (slash)
                {
                    npc.frame.Y = 476;
                }
                else
                {
                    npc.frame.Y = 408;
                }
            }
        }
    }
}

