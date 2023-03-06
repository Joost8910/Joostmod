using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
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
            Main.npcFrameCount[NPC.type] = 8;
        }
        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 68;
            NPC.damage = 110;
            NPC.defense = 60;
            NPC.lifeMax = 4000;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = Item.buyPrice(0, 5, 0, 0);
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 0;
            NPC.frameCounter = 0;
            Banner = NPC.type;
            BannerItem = Mod.Find<ModItem>("DeathKnightBanner").Type;

        }
        public override void OnKill()
        {
            if (Main.expertMode)
            {
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("Deathbringer").Type);
                }
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("SigilofSkulls").Type);
                }
            }
            else
            {
                if (Main.rand.Next(15) == 0)
                {
                    Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("Deathbringer").Type);
                }
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("SigilofSkulls").Type);
                }
            }
            if (Main.rand.Next(20) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("ThirdAnniversary").Type, 1);
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.position + new Vector2(24, 10), NPC.velocity, Mod.GetGoreSlot("Gores/DeathKnight1"), 1f);
                Gore.NewGore(NPC.position + new Vector2(40, 24), NPC.velocity, Mod.GetGoreSlot("Gores/DeathKnight2"), 1f);
                Gore.NewGore(NPC.position + new Vector2(14, 24), NPC.velocity, Mod.GetGoreSlot("Gores/DeathKnight2"), 1f);
                Gore.NewGore(NPC.position + new Vector2(26, 34), NPC.velocity, Mod.GetGoreSlot("Gores/DeathKnight3"), 1f);
                Gore.NewGore(NPC.position + new Vector2(12, 44), NPC.velocity, Mod.GetGoreSlot("Gores/DeathKnight4"), 1f);
                Gore.NewGore(NPC.position + new Vector2(42, 44), NPC.velocity, Mod.GetGoreSlot("Gores/DeathKnight4"), 1f);
                Gore.NewGore(NPC.position + new Vector2(20, 66), NPC.velocity, Mod.GetGoreSlot("Gores/DeathKnight5"), 1f);
                Gore.NewGore(NPC.position + new Vector2(28, 66), NPC.velocity, Mod.GetGoreSlot("Gores/DeathKnight5"), 1f);
                for (int k = 0; k < 50; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 109, 2.5f * (float)hitDirection, Main.rand.Next(-5, 5), 0, default(Color), 0.7f);
                }
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            return (tile.TileType == 41 || tile.TileType == 43 || tile.TileType == 44) && spawnInfo.PlanteraDefeated && spawnInfo.SpawnTileY >= Main.rockLayer && Main.hardMode ? 0.01f : 0f;
        }
        public override void AI()
        {
            NPC.ai[0]++;
            NPC.netUpdate = true;
            Player P = Main.player[NPC.target];
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            if (chance != 0)
            {
                chance = Main.rand.Next(300);
            }
            else
            {
                if (Main.rand.Next(4) == 0)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 20, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), 0, default(Color), 0.7f);
                }
                if (NPC.velocity.Y == 0 && Collision.CanHitLine(new Vector2(NPC.Center.X, NPC.Center.Y), 1, 1, new Vector2(P.Center.X, P.Center.Y), 1, 1))
                {
                    NPC.velocity.Y = -(float)Math.Sqrt(2 * 0.3f * Math.Abs(P.position.Y - NPC.Center.Y));
                    NPC.velocity.X = (P.Center.X - NPC.Center.X) / 30;
                    slash = true;
                    chance = 1;
                }
            }
            if (slash)
            {
                NPC.rotation += 36 * NPC.direction * 0.0174f;
                if (slashTimer % 10 == 0)
                {
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(NPC.Center.X, NPC.Center.Y, 0, 0, Mod.Find<ModProjectile>("Deathbringer").Type, 50, 15f, Main.myPlayer, NPC.whoAmI);
                    }
                    SoundEngine.PlaySound(SoundID.Item7, NPC.position);
                }
                slashTimer++;
                if (NPC.velocity.Y == 0)
                {
                    slash = false;
                    slashTimer = 0;
                }
            }
            else
            {
                NPC.rotation = NPC.velocity.X * 0.0174f * 1.5f;
            }
            if (NPC.velocity.X == 0 && NPC.velocity.Y == 0)
            {
                NPC.velocity.Y = -8f;
            }
            if (P.Center.X < NPC.Center.X && NPC.velocity.X > -6.5f)
            {
                NPC.velocity.X -= 0.2f;
            }
            if (NPC.velocity.X < -6.5f)
            {
                NPC.velocity.X = -6.5f;
            }
            if (NPC.velocity.X > 6.5f)
            {
                NPC.velocity.X = 6.5f;
            }
            if (P.Center.X > NPC.Center.X && NPC.velocity.X < 6.5f)
            {
                NPC.velocity.X += 0.2f;
            }
            if (NPC.life < NPC.lifeMax / 2)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, 109, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5), 0, default(Color), 0.7f);
                if (NPC.ai[0] % 180 == 0 && Main.netMode != 1)
                {
                    NPC.NewNPC((int)NPC.Center.X + 60, (int)NPC.Center.Y + 60, 34, 0, NPC.whoAmI);
                    NPC.NewNPC((int)NPC.Center.X - 60, (int)NPC.Center.Y + 60, 34, 0, NPC.whoAmI);
                    NPC.NewNPC((int)NPC.Center.X + 60, (int)NPC.Center.Y - 60, 34, 0, NPC.whoAmI);
                    NPC.NewNPC((int)NPC.Center.X - 60, (int)NPC.Center.Y - 60, 34, 0, NPC.whoAmI);
                }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            NPC.frameCounter++;
            if (NPC.velocity.Y == 0)
            {
                if (NPC.frameCounter >= 10)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = (NPC.frame.Y + 68);
                }
                if (NPC.frame.Y >= 408)
                {
                    NPC.frame.Y = 0;
                }
            }
            else
            {
                if (slash)
                {
                    NPC.frame.Y = 476;
                }
                else
                {
                    NPC.frame.Y = 408;
                }
            }
        }
    }
}

