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
    public class FireElemental : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Elemental");
            Main.npcFrameCount[npc.type] = 8;
        }
        public override void SetDefaults()
        {
            npc.width = 60;
            npc.height = 62;
            npc.damage = 60;
            npc.defense = 18;
            npc.lifeMax = 250;
            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.value = Item.buyPrice(0, 0, 7, 50);
            npc.knockBackResist = 0.45f;
            npc.aiStyle = 22;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.frameCounter = 0;
            aiType = NPCID.Wraith;
            banner = npc.type;
            bannerItem = mod.ItemType("FireElementalBanner");

        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 2701, Main.rand.Next(10, 20));
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FireEssence"), Main.rand.Next(4, 10));
            if (Main.rand.Next(50) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SecondAnniversary"), 1);
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
            return !spawnInfo.playerInTown && spawnInfo.spawnTileY >= Main.maxTilesY - 250 && Main.hardMode ? 0.12f : 0f;

        }
        public override void AI()
        {
            npc.ai[0]++;
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            npc.netUpdate = true;
            if (npc.Center.X < P.Center.X) //Always face towards the player
            {
                npc.direction = 1;
            }
            else
            {
                npc.direction = -1;
            }

            npc.ai[3]++;
            if (npc.ai[3] > 400)
            {
                if (Main.netMode != 1)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.BurningSphere);
                }
                Main.PlaySound(SoundID.Item45, npc.Center);
                npc.ai[3] = 0;
            }

            if ((npc.ai[3] % 10) == 0)
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
            npc.spriteDirection = npc.direction;
            npc.frameCounter++;
            if (npc.frameCounter >= 10)
            {
                npc.frameCounter = 0;
                npc.frame.Y = (npc.frame.Y + 74);
            }
            if (npc.frame.Y >= 592)
            {
                npc.frame.Y = 0;
            }
        }


    }
}

