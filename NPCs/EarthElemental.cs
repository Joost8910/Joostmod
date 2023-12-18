using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Items.Materials;
using Terraria.GameContent.ItemDropRules;
using JoostMod.Items.Placeable;

namespace JoostMod.NPCs
{
    public class EarthElemental : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earth Elemental");
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            NPCID.Sets.TrailCacheLength[NPC.type] = 6;
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[]
                {
                    BuffID.Poisoned,
                    BuffID.Venom
                }
            };
            NPCID.Sets.DebuffImmunitySets[Type] = debuffData;
        }
        public override void SetDefaults()
        {
            NPC.width = 44;
            NPC.height = 44;
            NPC.damage = 30;
            NPC.defense = 30;
            NPC.lifeMax = 600;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = Item.buyPrice(0, 0, 7, 50);
            NPC.knockBackResist = 0.05f;
            NPC.aiStyle = 26;
            AIType = NPCID.Unicorn;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<EarthElementalBanner>();

        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            //I think elementals dropping multiple stacks would make them more aesthetically pleasing to kill
            int essence = ModContent.ItemType<EarthEssence>();
            var parameters = new DropOneByOne.Parameters()
            {
                ChanceNumerator = 1,
                ChanceDenominator = 1,
                MinimumStackPerChunkBase = 2,
                MaximumStackPerChunkBase = 6,
                MinimumItemDropsCount = 8,
                MaximumItemDropsCount = 20,
            };
            var expertParamaters = parameters;
            expertParamaters.MinimumItemDropsCount = 12;
            expertParamaters.MaximumItemDropsCount = 30;
            npcLoot.Add(new DropBasedOnExpertMode(new DropOneByOne(essence, parameters), new DropOneByOne(essence, expertParamaters)));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SecondAnniversary>(), 50));
        }
        /*
        public override void OnKill()
        {
            Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<EarthEssence>(), (Main.expertMode ? Main.rand.Next(12, 30) : Main.rand.Next(8, 20)));
            if (Main.rand.Next(50) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<SecondAnniversary>(), 1);
            }
        }
        */
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (NPC.ai[2] >= 100 && (NPC.ai[2] < 300 || NPC.ai[2] > 400))
            {
                damage *= 2;
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                var sauce = NPC.GetSource_Death();
                for (int i = 0; i < 8; i++)
                    Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("EarthElemental").Type);
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            return !spawnInfo.PlayerInTown && ((!spawnInfo.Invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse) || spawnInfo.SpawnTileY > Main.worldSurface) && (tile.TileType == 1 || tile.TileType == 25 || tile.TileType == 117 || tile.TileType == 203 || tile.TileType == 357 || tile.TileType == 367 || tile.TileType == 368 || tile.TileType == 369) && Main.hardMode ? 0.06f : 0f;
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            if (NPC.ai[2] < 400)
            {
                if (NPC.Center.X < P.Center.X)
                {
                    NPC.direction = 1;
                }
                else
                {
                    NPC.direction = -1;
                }
            }
            NPC.spriteDirection = NPC.direction;
            NPC.netUpdate = true;
            NPC.rotation += (NPC.velocity.X * 0.0174f * 2.5f);
            if (Main.expertMode)
            {
                NPC.ai[1]++;
                if (NPC.velocity.Y == 0 && NPC.ai[1] > 300 && NPC.ai[2] < 1 && Collision.CanHitLine(NPC.Center, 1, 1, P.Center, 1, 1))
                {
                    NPC.ai[2] = 1;
                }
                if (NPC.ai[2] > 0)
                {
                    NPC.knockBackResist = 0f;
                    NPC.aiStyle = -1;
                    NPC.ai[2]++;
                    NPC.rotation += (NPC.direction * 0.0174f * NPC.ai[2]);
                    int freq = (int)((100 - NPC.ai[2]) / 10) + 10;
                    if (NPC.ai[2] == 90)
                    {
                        SoundEngine.PlaySound(SoundID.Item22.WithVolumeScale(0.5f).WithPitchOffset(-0.1f), NPC.Center);
                    }
                    if (NPC.ai[2] == 100)
                    {
                        if (NPC.Center.Y > P.Center.Y + 100 && Math.Abs(NPC.Center.X - P.Center.X) < 400)
                        {
                            NPC.velocity.Y = -4;
                            NPC.ai[2] = 300;
                        }
                        else
                        {
                            NPC.velocity.X = 15 * NPC.direction;
                        }
                    }
                    if (NPC.ai[2] >= 300)
                    {
                        if (NPC.velocity.Y == 0 && NPC.oldVelocity.Y > 0)
                        {
                            if (NPC.ai[2] < 400)
                            {
                                NPC.velocity.X = 5 * NPC.direction;
                                NPC.velocity.Y = -18;
                                NPC.ai[2] = 400;
                            }
                            else if (NPC.ai[2] < 600)
                            {
                                NPC.velocity.X = 4 * NPC.direction;
                                NPC.velocity.Y = -14;
                                NPC.ai[2] = 600;
                            }
                            else if (NPC.ai[2] < 800)
                            {
                                NPC.velocity.X = 3 * NPC.direction;
                                NPC.velocity.Y = -10;
                                NPC.ai[2] = 800;
                            }
                            else
                            {
                                NPC.ai[1] = 0;
                                NPC.ai[2] = 0;
                            }
                            SoundEngine.PlaySound(SoundID.Item10, NPC.Center);
                        }
                        if (NPC.velocity.X == 0 && NPC.oldVelocity.X != 0)
                        {
                            NPC.velocity.X = -NPC.oldVelocity.X;
                            NPC.direction = Math.Sign(NPC.velocity.X);
                            SoundEngine.PlaySound(SoundID.Item10.WithVolumeScale(0.5f), NPC.Center);
                        }
                    }
                    else if (NPC.ai[2] >= 100)
                    {
                        if (Collision.SolidCollision(NPC.position + new Vector2(NPC.velocity.X, 0), NPC.width, NPC.height))
                        {
                            NPC.velocity.Y = -15;
                            SoundEngine.PlaySound(SoundID.Item10, NPC.Center);
                        }
                        if (NPC.ai[2] > 200 || NPC.velocity.X == 0)
                        {
                            NPC.ai[1] = 0;
                            NPC.ai[2] = 0;
                            NPC.velocity.X = -NPC.oldVelocity.X;
                            NPC.velocity.Y = -8;
                            SoundEngine.PlaySound(SoundID.Item10, NPC.Center);
                        }
                    }
                    else
                    {
                        NPC.velocity = Vector2.Zero;
                        if (NPC.ai[2] % freq == 0)
                        {
                            SoundEngine.PlaySound(SoundID.Item23.WithVolumeScale(0.5f).WithPitchOffset(-0.2f), NPC.Center);
                        }
                        Vector2 posi = new Vector2(NPC.Center.X, NPC.position.Y + NPC.height + 4);
                        Point pos = posi.ToTileCoordinates();
                        Tile tile = Framing.GetTileSafely(pos.X, pos.Y);
                        if (tile.HasTile)
                        {
                            Dust dust = Main.dust[WorldGen.KillTile_MakeTileDust(pos.X, pos.Y, tile)];
                            dust.velocity.Y = -NPC.ai[2] / 30;
                            dust.velocity.X = -NPC.direction * NPC.ai[2] / 10;
                        }
                    }
                }
                else
                {
                    NPC.aiStyle = 26;
                    NPC.knockBackResist = 0.05f;
                }
            }
            NPC.velocity.Y += 0.2f;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (NPC.spriteDirection == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                effects = SpriteEffects.None;
            }
            if (NPC.ai[2] >= 100 && (NPC.ai[2] < 300 || NPC.ai[2] > 400))
            {
                Texture2D tex = TextureAssets.Npc[NPC.type].Value;
                Vector2 drawOrigin = new Vector2((tex.Width / 2), (tex.Height / 2));
                for (int i = 0; i < NPC.oldPos.Length; i++)
                {
                    Color color2 = drawColor * ((NPC.oldPos.Length - i) / (float)NPC.oldPos.Length) * 0.8f;
                    Vector2 drawPos = NPC.oldPos[i] - Main.screenPosition + new Vector2(NPC.width / 2, NPC.height / 2) + new Vector2(0f, NPC.gfxOffY);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height));
                    spriteBatch.Draw(tex, drawPos, rect, color2, NPC.rotation, drawOrigin, NPC.scale, effects, 0f);
                }
            }
            return true;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (NPC.spriteDirection == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                effects = SpriteEffects.None;
            }
            Texture2D tex = (Texture2D)ModContent.Request<Texture2D>("JoostMod/NPCs/EarthElemental_Pupils");
            Rectangle rect = new Rectangle(0, 0, tex.Width, tex.Height);
            Vector2 drawOrigin = new Vector2((tex.Width / 2), (tex.Height / 2));
            Vector2 offSet = NPC.DirectionTo(Main.player[NPC.target].Center) * 2;
            spriteBatch.Draw(tex, new Vector2(NPC.position.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)(tex.Width) / 2f + drawOrigin.X + offSet.X, NPC.position.Y - Main.screenPosition.Y + NPC.height - tex.Height + 4f + drawOrigin.Y + offSet.Y), new Rectangle?(rect), drawColor, NPC.rotation, drawOrigin, 1f, effects, 0f);

        }
    }
}

