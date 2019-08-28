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
    public class WaterElemental : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Elemental");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 62;
            npc.damage = 30;
            npc.defense = 24;
            npc.lifeMax = 200;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.scale = 1.5f;
            npc.value = Item.buyPrice(0, 0, 7, 50);
            npc.knockBackResist = 0.85f;
            npc.aiStyle = 0;
            npc.noGravity = true;
            npc.frameCounter = 0;
            banner = npc.type;
            bannerItem = mod.ItemType("WaterElementalBanner");

        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WaterEssence"), Main.rand.Next(4, 10));
            if (Main.rand.Next(50) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SecondAnniversary"), 1);
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int k = 0; k < 30; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 103, 2.5f * (float)hitDirection, Main.rand.Next(-5, 5), 0, default(Color), 0.7f);
                }
            }
            for (int k = 0; k < 4; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 103, 2.5f * (float)hitDirection, Main.rand.Next(-5, 5), 0, default(Color), 0.7f);
            }
            //npc.ai[2] += (float)(damage / 3);
        }
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            npc.ai[2] += npc.ai[2] < 25 ? (knockback * 2.5f) : 0;
        }
        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            npc.ai[2] += npc.ai[2] < 25 ? (knockback * 2.5f) : 0;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.playerInTown && ((!spawnInfo.invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse) || spawnInfo.spawnTileY > Main.worldSurface) && spawnInfo.water && Main.hardMode ? 0.1f : 0f;
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
            npc.spriteDirection = 1;
            npc.ai[1]++;
            npc.ai[3] = (npc.ai[1] * 4) - 90;
            double rad = npc.ai[3] * (Math.PI / 180);
            double dist = 180;
            Vector2 aim = new Vector2((float)(Math.Cos(rad) * dist), (float)(Math.Sin(rad) * dist));
            if (npc.ai[2] <= 0)
            {
                npc.velocity = npc.velocity = npc.DirectionTo(P.Center + aim) * (5 + npc.ai[1] / 40);
            }
            else
            {
                npc.ai[2]--;
            }
            if (npc.ai[1] >= 360)
            {
                npc.ai[1] = 0;
                npc.velocity = new Vector2(0, 0);
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter >= 5)
            {
                npc.frameCounter = 0;
                npc.frame.Y = (npc.frame.Y + 62);
            }
            if (npc.frame.Y >= 372)
            {
                npc.frame.Y = 0;
            }
        }


    }
}

