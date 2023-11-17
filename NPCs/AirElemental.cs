using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using JoostMod.Items.Materials;
using JoostMod.Items.Placeable;
using JoostMod.Projectiles;

namespace JoostMod.NPCs
{
    public class AirElemental : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Air Elemental");
            Main.npcFrameCount[NPC.type] = 4;
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[]
                {
                    BuffID.Poisoned,
                    BuffID.Venom,
                    BuffID.Ichor
                }
            };
            NPCID.Sets.DebuffImmunitySets[Type] = debuffData;
        }
        public override void SetDefaults()
        {
            NPC.width = 24;
            NPC.height = 54;
            NPC.damage = 25;
            NPC.defense = 0;
            NPC.lifeMax = 350;
            NPC.HitSound = SoundID.NPCHit30;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = Item.buyPrice(0, 0, 7, 50);
            NPC.knockBackResist = 0.85f;
            NPC.aiStyle = 0;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.frameCounter = 0;
            Banner = NPC.type;
            BannerItem = Mod.Find<ModItem>("AirElementalBanner").Type;
            /*
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.Ichor] = true;
            */
            NPC.alpha = 75;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            //I think elementals dropping multiple stacks would make them more aesthetically pleasing to kill
            int essence = ModContent.ItemType<TinyTwister>();
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
            Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("TinyTwister").Type, (Main.expertMode ? Main.rand.Next(12, 30) : Main.rand.Next(8, 20)));

            if (Main.rand.Next(50) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("SecondAnniversary").Type, 1);
            }
        }
        */

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 16, 2.5f * (float)hitDirection, Main.rand.Next(-5, 5), 0, default(Color), 0.7f);
                }
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.PlayerInTown && !spawnInfo.Invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse && spawnInfo.Sky && Main.hardMode ? 0.18f : 0f;
        }
        public override void AI()
        {
            NPC.ai[0]++;
            Player P = Main.player[NPC.target];
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            if (NPC.ai[0] == 220)
            {
                for (int d = 0; d < 20; d++)
                {
                    Dust dust = Dust.NewDustDirect(NPC.Center - new Vector2(10, 10), 20, 20, 31, NPC.velocity.X * 0.8f, NPC.velocity.Y * 0.8f, 0, default(Color), 1.5f);
                    Vector2 vel = NPC.Center - dust.position;
                    vel.Normalize();
                    dust.position -= vel * 30;
                    dust.velocity = vel * 2 + NPC.velocity;
                    dust.noGravity = true;
                }
            }
            if (NPC.ai[0] > 220 && NPC.ai[0] % 3 == 0)
            {
                 Dust.NewDustDirect(NPC.Center - new Vector2(10, 10), 20, 20, 31, NPC.velocity.X * 0.8f, NPC.velocity.Y * 0.8f, 0, default(Color), 1f);
            }
            if (NPC.ai[3] > 10)
            {
                if (Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                {
                    NPC.velocity *= 0.98f;
                }
                if (Main.expertMode) // Wind
                {
                    NPC.ai[1]++;
                    if (NPC.ai[1] > 1000)
                    {
                        if (NPC.ai[1] % 2 == 0)
                        {
                            Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 31, NPC.velocity.X * 0.8f, NPC.velocity.Y * 0.8f, 0, Color.White, 1.5f);
                        }
                        if (NPC.velocity.Y * NPC.directionY < 8)
                        {
                            NPC.velocity.Y += 0.3f * NPC.directionY;
                        }
                        if (Collision.CanHitLine(NPC.Center, 1, 1, P.Center, 1, 1) && NPC.ai[2] == 0 && Math.Abs(NPC.Center.Y - P.Center.Y) < 40)
                        {
                            NPC.ai[2]++;
                        }
                    }
                    if (NPC.ai[2] > 0)
                    {
                        NPC.ai[2]++;
                        NPC.velocity *= 0.98f;

                        Vector2 position = NPC.Center;
                        float speedX = ((7f + Main.rand.NextFloat() * 7f) * NPC.direction) + (NPC.direction * NPC.velocity.X > 0 ? NPC.velocity.X : 0);
                        position.X -= 1000 * NPC.direction;
                        position.Y += Main.rand.Next(-8, 8) * 10;
                        if (NPC.ai[2] % 2 == 0)
                        {
                            Dust.NewDustPerfect(position, 31, new Vector2(speedX * 3, 0), 0, Color.White, 2f);
                        }
                        if (NPC.ai[2] % 16 == 0)
                        {
                            SoundEngine.PlaySound(SoundID.Item7.WithVolumeScale(1.8f).WithPitchOffset(-0.8f), NPC.position);

                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), position.X, position.Y, speedX, 0, ModContent.ProjectileType<HostileWind>(), 25, 10f);
                            }
                        }
                        if (NPC.ai[2] > 200)
                        {
                            NPC.ai[0] = 0;
                            NPC.ai[1] = 0;
                            NPC.ai[2] = 0;
                        }
                    }
                }
                if (NPC.ai[0] >= 240 && Collision.CanHitLine(NPC.Center - new Vector2(20, 20), 40, 40, P.Center, 1, 1) && NPC.ai[2] == 0)
                {
                    float Speed = 8f;
                    int damage = 25;
                    int type = Mod.Find<ModProjectile>("Gust").Type;
                    SoundEngine.PlaySound(SoundID.Item1, NPC.position);
                    Vector2 dir = NPC.DirectionTo(P.Center);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, dir * Speed, type, damage, 10f);
                    }
                    for (int d = 0; d < 10; d++)
                    {
                        Dust.NewDust(NPC.Center - new Vector2(10, 10), 20, 20, 31, dir.X * Speed, dir.Y * Speed, 0, default(Color), 2f);
                    }
                    NPC.ai[0] = 0;
                    NPC.velocity = dir * -10;
                }
                NPC.direction = NPC.Center.X < P.Center.X ? 1 : -1;
                NPC.directionY = NPC.Center.Y < P.Center.Y ? 1 : -1;
                float speed = 10;
                Vector2 vel = new Vector2(speed * NPC.direction, speed * NPC.directionY);
                if (vel.Length() > speed)
                {
                    vel *= speed / vel.Length();
                }
                if (NPC.Distance(P.Center) < 300 && Collision.CanHitLine(NPC.Center, 1, 1, P.Center, 1, 1))
                {
                    vel *= -1;
                }
                float home = 50f;
                if (vel != Vector2.Zero)
                {
                    NPC.velocity = ((home - 1f) * NPC.velocity + vel) / home;
                }
            }
            else
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, 31, 0, 0, 0, Color.White, 1.5f);
            }
            if (NPC.ai[3] < (Main.expertMode ? 90 : 120))
            {
                NPC.ai[3]++;
            }
            else
            {
                if (NPC.ai[0] % 2 == 0)
                {
                    Dust.NewDustPerfect(new Vector2(NPC.Center.X, NPC.Center.Y + (NPC.height / 2)), 31, Vector2.Zero, 0, Color.White, 1).noGravity = true;
                }
                //this is probably bad for performance...
                //optimize this later
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    Projectile proj = Main.projectile[i];
                    float collisionPoint = 0;
                    //projectile.CanHit doesnt exist anymore and im not certain what the replacement should be
                    if (proj.active && proj.friendly && proj.damage > 0 && /*(proj.CanHit(NPC) &&*/ Collision.CheckAABBvLineCollision(NPC.position, NPC.Size, proj.Center, proj.Center + proj.velocity * 10 * (proj.extraUpdates + 1), proj.width, ref collisionPoint))
                    {
                        Vector2 vel = proj.velocity;
                        vel.Normalize();
                        NPC.velocity = vel.RotatedBy(90 * 0.0174f) * 12;
                        NPC.ai[3] = 0;
                        SoundEngine.PlaySound(SoundID.Item7, NPC.position);
                        break;
                    }
                }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter * (NPC.velocity.Length() / 2 + 2) >= 20)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y = (NPC.frame.Y + 62);
            }
            if (NPC.frame.Y >= 248)
            {
                NPC.frame.Y = 0;
            }
        }


    }
}

