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
        }
        public override void SetDefaults()
        {
            npc.width = 48;
            npc.height = 48;
            npc.damage = 30;
            npc.defense = 30;
            npc.lifeMax = 300;
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
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EarthEssence"), Main.rand.Next(4, 10));
            if (Main.rand.Next(50) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SecondAnniversary"), 1);
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
            return !spawnInfo.playerInTown && ((!spawnInfo.invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse) || spawnInfo.spawnTileY > Main.worldSurface) && (tile.type == 1 || tile.type == 25 || tile.type == 117 || tile.type == 203 || tile.type == 357 || tile.type == 367 || tile.type == 368 || tile.type == 369) && Main.hardMode ? 0.12f : 0f;

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
            if (npc.Center.X < P.Center.X)
            {
                npc.direction = 1;
            }
            else
            {
                npc.direction = -1;
            }
            npc.ai[1]++;
            npc.ai[2] += 6;
            npc.rotation -= (6 * npc.direction);
            if (npc.rotation >= 360)
            {
                npc.rotation = 0;
            }
            if (npc.rotation <= -360)
            {
                npc.rotation = 0;
            }

        }


    }
}

