using System;
using JoostMod.Items.Materials;
using JoostMod.Items.Placeable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
    public class FireElemental : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Elemental");
            Main.npcFrameCount[NPC.type] = 5;
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[]
                {
                    BuffID.OnFire,
                    BuffID.ShadowFlame,
                    BuffID.Poisoned,
                    BuffID.Venom
                }
            };
            NPCID.Sets.DebuffImmunitySets[Type] = debuffData;
        }
        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 62;
            NPC.damage = 40;
            NPC.defense = 18;
            NPC.lifeMax = 500;
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = Item.buyPrice(0, 0, 7, 50);
            NPC.knockBackResist = 0.35f;
            NPC.aiStyle = 22;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.frameCounter = 0;
            AIType = NPCID.Wraith;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<FireElementalBanner>();
            NPC.lavaImmune = true;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            //I think elementals dropping multiple stacks would make them more aesthetically pleasing to kill
            int essence = ModContent.ItemType<FireEssence>();
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
            npcLoot.Add(ItemDropRule.Common(ItemID.LivingFireBlock, 4, 20, 50));
        }
        /*
        public override void OnKill()
        {
            Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, 2701, Main.rand.Next(10, 20));
            Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<FireEssence>(), (Main.expertMode ? Main.rand.Next(12, 30) : Main.rand.Next(8, 20)));
            if (Main.rand.Next(50) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<SecondAnniversary>(), 1);
            }
        }
        */
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (NPC.localAI[1] >= 40)
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
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 127, 2.5f * (float)hitDirection, Main.rand.Next(-5, 5), 0, default(Color), 0.7f);
                }
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            return !spawnInfo.PlayerInTown && spawnInfo.SpawnTileY >= Main.maxTilesY - 250 && Main.hardMode ? 0.06f : 0f;

        }
        public override void AI()
        {
            NPC.localAI[0]++;
            Player P = Main.player[NPC.target];
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            if (Main.expertMode && NPC.localAI[0] > 800 && NPC.ai[3] < 370 && NPC.ai[3] > 60)
            {
                NPC.aiStyle = -1;
                NPC.knockBackResist = 0f;
                if (NPC.localAI[1] < 1)
                {
                    NPC.targetRect = P.getRect();
                    NPC.FaceTarget();
                    float speed = (Math.Abs(NPC.Center.X - P.Center.X) - 200) / 5f;
                    if (speed < -6)
                    {
                        speed = -6;
                    }
                    if (speed > 6)
                    {
                        speed = 6;
                    }
                    NPC.velocity.X = speed * NPC.direction;
                    NPC.velocity.Y = 4 * NPC.directionY;
                    if (Math.Abs(NPC.Center.Y - P.Center.Y) < 10 && Math.Abs(NPC.Center.X - P.Center.X) < 400)
                    {
                        NPC.localAI[1]++;
                        NPC.netUpdate = true;
                    }
                }
                else
                {
                    if (NPC.localAI[1] == 1)
                    {
                        SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/dd2_sky_dragons_fury_swing_1").WithPitchOffset(-0.4f), NPC.Center); //230
                    }
                    NPC.velocity = Vector2.Zero;
                    NPC.localAI[1]++;
                    if (NPC.localAI[1] >= 40)
                    {
                        NPC.width = 56;
                        if (NPC.localAI[1] == 40)
                        {
                            SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/dd2_phantom_phoenix_shot_0").WithPitchOffset(0.1f), NPC.Center); //217

                        }
                        NPC.velocity.X = 15 * NPC.direction;
                        Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Torch, -NPC.velocity.X, -NPC.velocity.Y, 0, default(Color), 4f).noGravity = true;
                    }
                    if (NPC.localAI[1] > 90)
                    {
                        NPC.width = 32;
                        NPC.localAI[1] = 0;
                        NPC.localAI[0] = 0;
                    }
                }
            }
            else
            {
                NPC.knockBackResist = 0.35f;
                NPC.aiStyle = 22;
                NPC.FaceTarget();
                Vector2 spherePos = new Vector2(NPC.Center.X, NPC.position.Y - 20);
                NPC.ai[3]++;
                if (NPC.ai[3] == 370)
                {
                    SoundEngine.PlaySound(SoundID.Item20, NPC.Center);
                }
                if (NPC.ai[3] > 370)
                {
                    NPC.velocity *= 0.8f;
                    int amount = (int)(NPC.ai[3] - 370);
                    for (int d = 0; d < amount; d++)
                    {
                        Dust dust = Dust.NewDustDirect(spherePos - new Vector2(5, 5), 10, 10, 6, NPC.velocity.X * 0.8f, NPC.velocity.Y * 0.8f, 0, default(Color), 1.5f);
                        Vector2 vel = spherePos - dust.position;
                        vel.Normalize();
                        dust.position -= vel * 12;
                        dust.velocity = vel + NPC.velocity * 0.8f;
                        dust.noGravity = true;
                    }
                    NPC.netUpdate = true;
                }
                if (NPC.ai[3] > 400)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)spherePos.X, (int)spherePos.Y, NPCID.BurningSphere);
                    }
                    SoundEngine.PlaySound(SoundID.Item45, NPC.Center);
                    NPC.ai[3] = 0;
                }
            }

            if (Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
            {
                NPC.velocity *= 0.92f;
            }
            if ((NPC.localAI[0] % 10) == 0)
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
                int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
                Dust dust = Main.dust[dustIndex];
                dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
                dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
                dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            frameHeight = 74;
            NPC.spriteDirection = NPC.direction;
            if (NPC.localAI[1] > 0)
            {
                if (NPC.localAI[1] < 20)
                {
                    NPC.frame.X = 140 * 4;
                }
                else if (NPC.localAI[1] < 40)
                {
                    NPC.frame.X = 140 * 5;
                }
                else
                {
                    NPC.frame.X = 140 * 6;
                }
                NPC.frame.Y = frameHeight * ((int)(NPC.localAI[1] / 4) % 5);
            }
            else
            {
                NPC.frameCounter++;
                if (NPC.frameCounter >= 5)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = (NPC.frame.Y + frameHeight);
                }
                if (NPC.frame.Y >= frameHeight * 5)
                {
                    NPC.frame.Y = 0;
                }
                if (NPC.ai[3] > 360)
                {
                    NPC.frame.X = 140 * 3;
                }
                else
                {
                    NPC.frame.X = 0;
                    if (NPC.velocity.X * NPC.direction > 1)
                    {
                        NPC.frame.X = 140;
                    }
                    if (NPC.velocity.X * NPC.direction < -1)
                    {
                        NPC.frame.X = 140 * 2;
                    }
                }
            }
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
            int xFrameCount = 7;
            Color alpha = Color.White;
            alpha *= 0.8f;
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Rectangle rect = new Rectangle((int)NPC.frame.X, (int)NPC.frame.Y, (texture.Width / xFrameCount), (texture.Height / Main.npcFrameCount[NPC.type]));
            Vector2 vect = new Vector2((float)((texture.Width / xFrameCount) / 2), (float)((texture.Height / Main.npcFrameCount[NPC.type]) / 2));
            spriteBatch.Draw(texture, new Vector2(NPC.position.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)(texture.Width / xFrameCount) / 2f + vect.X, NPC.position.Y - Main.screenPosition.Y + (float)NPC.height - (float)(texture.Height / Main.npcFrameCount[NPC.type]) + 4f + vect.Y), new Rectangle?(rect), alpha, NPC.rotation, vect, 1f, effects, 0f);
            return false;
        }

    }
}

