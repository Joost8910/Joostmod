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
    public class AirElemental : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Air Elemental");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 36;
            npc.height = 62;
            npc.damage = 20;
            npc.defense = 18;
            npc.lifeMax = 175;
            npc.HitSound = SoundID.NPCHit30;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.value = Item.buyPrice(0, 0, 7, 50);
            npc.knockBackResist = 0.85f;
            npc.aiStyle = 0;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.frameCounter = 0;
            banner = npc.type;
            bannerItem = mod.ItemType("AirElementalBanner");

        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TinyTwister"), Main.rand.Next(4, 10));

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
                    Dust.NewDust(npc.position, npc.width, npc.height, 16, 2.5f * (float)hitDirection, Main.rand.Next(-5, 5), 0, default(Color), 0.7f);
                }
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.playerInTown && !spawnInfo.invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse && spawnInfo.sky && Main.hardMode ? 0.4f : 0f;
        }
        public override void AI()
        {
            npc.ai[0]++;
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            npc.ai[1]++;
            if (npc.ai[1] % 24 == 0)
            {
                if (Main.rand.Next(3) == 0)
                {
                    npc.velocity = npc.DirectionTo(P.Center) * 12;
                }
                else
                {
                    npc.velocity = npc.DirectionTo(P.Center) * -4;
                }
                npc.netUpdate = true;
            }
            if (npc.ai[1] >= 240 && Main.netMode != 1)
            {
                float Speed = 4f;
                int damage = 20;
                int type = mod.ProjectileType("Gust");
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 1);
                float rotation = (float)Math.Atan2(npc.Center.Y - (P.position.Y + (P.height * 0.5f)), npc.Center.X - (P.position.X + (P.width * 0.5f)));
                if (Main.netMode != 1)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 10f, Main.myPlayer);
                }
                npc.ai[1] = 0;

            }

        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter >= 10)
            {
                npc.frameCounter = 0;
                npc.frame.Y = (npc.frame.Y + 62);
            }
            if (npc.frame.Y >= 248)
            {
                npc.frame.Y = 0;
            }
        }


    }
}

