using System;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Items.Placeable;
using JoostMod.Projectiles;

namespace JoostMod.NPCs
{
    public class Cactite : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactite");
            Main.npcFrameCount[NPC.type] = 8;
        }
        public override void SetDefaults()
        {
            NPC.width = 24;
            NPC.height = 46;
            NPC.damage = 0;
            NPC.defense = 0;
            if (Main.expertMode)
            {
                if (Main.hardMode)
                {
                    NPC.lifeMax = 220;
                }
                else
                {
                    NPC.lifeMax = 110;
                }
                NPC.defense = 5;
            }
            else
            {
                NPC.lifeMax = 55;
            }
            if (NPC.downedPlantBoss)
            {
                if (Main.expertMode)
                {
                    NPC.lifeMax = 330;
                }
                else
                {
                    NPC.lifeMax = 165;
                }
                NPC.defense = 10;
            }
            if (NPC.downedMoonlord)
            {
                if (Main.expertMode)
                {
                    NPC.lifeMax = 1320;
                }
                else
                {
                    NPC.lifeMax = 660;
                }
                NPC.defense = 20;
            }
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 0, 0, 60);
            NPC.knockBackResist = 0.3f;
            NPC.aiStyle = -1;
            NPC.frameCounter = 0;
            Banner = Mod.Find<ModNPC>("Cactoid").Type;
            BannerItem = Mod.Find<ModItem>("CactoidBanner").Type;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Cactus, 1, 3, 6));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Anniversary>(), 100));
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.type == Mod.Find<ModNPC>("Cactus Person").Type)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            NPC.frameCounter++;
            if (NPC.velocity.X != 0)
            {
                if (NPC.frameCounter >= 15 / (1 + Math.Abs(NPC.velocity.X)))
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = (NPC.frame.Y + 54);
                }
                if (NPC.frame.Y >= 216)
                {
                    NPC.frame.Y = 0;
                }
            }
            else
            {
                if (NPC.frameCounter >= 6)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = (NPC.frame.Y + 54);
                }
                if (NPC.frame.Y >= 432)
                {
                    NPC.frame.Y = 216;
                }
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                var sauce = NPC.GetSource_Death();
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("Cactite1").Type);
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("Cactite2").Type);
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("Cactite2").Type);
            }

            //The HitEffect hook is client side, these bits will need to be moved
            NPC.ai[2]++;
            for (int n = 0; n < 200; n++)
            {
                NPC N = Main.npc[n];
                if (N.active && N.Distance(NPC.Center) < 400 && Collision.CanHitLine(NPC.Center, 1, 1, N.Center, 1, 1) && (N.type == Mod.Find<ModNPC>("Cactite").Type || N.type == Mod.Find<ModNPC>("Cactoid").Type))
                {
                    N.target = NPC.target;
                    N.ai[2]++;
                    N.netUpdate = true;
                }
            }
            if (NPC.friendly)
            {
                NPC.ai[3] = 45;
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (target.GetModPlayer<JoostPlayer>().cactoidCommendationItem)
            {
                return false;
            }
            return base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override bool? CanBeHitByItem(Player player, Item item)
        {
            if (player.GetModPlayer<JoostPlayer>().cactoidCommendationItem)
            {
                return false;
            }
            return base.CanBeHitByItem(player, item);
        }
        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            if (Main.player[projectile.owner].GetModPlayer<JoostPlayer>().cactoidCommendationItem && projectile.type != Mod.Find<ModProjectile>("Manipulation").Type && projectile.friendly)
            {
                return false;
            }
            return base.CanBeHitByProjectile(projectile);
        }
        public override void AI()
        {
            if (!NPC.friendly && (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active))
            {
                NPC.TargetClosest(false);
            }
            Player player = Main.player[NPC.target];
            bool playerCactoid = (player.GetModPlayer<JoostPlayer>().cactoidCommendationItem || player.HasBuff(Mod.Find<ModBuff>("CactoidFriend").Type));
            bool cactusPersonNear = false;
            int cactusPerson = -1;
            if (!playerCactoid)
            {
                float num = 600f;
                for (int i = 0; i < 255; i++)
                {
                    if (Main.player[i].active && !Main.player[i].dead && !Main.player[i].ghost && (Main.player[i].GetModPlayer<JoostPlayer>().cactoidCommendationItem || Main.player[i].HasBuff(Mod.Find<ModBuff>("CactoidFriend").Type)))
                    {
                        float num4 = Math.Abs(Main.player[i].Center.X - NPC.Center.X + Math.Abs(Main.player[i].Center.Y - NPC.Center.Y));
                        if (num4 < num)
                        {
                            num = num4;
                            NPC.target = i;
                            playerCactoid = true;
                        }
                    }
                }
                for (int k = 0; k < 200; k++)
                {
                    NPC cactu = Main.npc[k];
                    if (cactu.active && cactu.type == Mod.Find<ModNPC>("Cactus Person").Type && NPC.Distance(cactu.Center) < 800)
                    {
                        cactusPersonNear = true;
                        cactusPerson = cactu.whoAmI;
                        break;
                    }
                }
            }
            if (playerCactoid || cactusPersonNear)
            {
                NPC.friendly = true;
            }
            else
            {
                NPC.friendly = false;
            }
            if (NPC.friendly)
            {
                float idleAccel = 0.05f;
                float viewDist = 600f;
                float chaseAccel = 10f;
                float inertia = 20f;
                NPC.dontCountMe = true;
                if (NPC.ai[3] > 0)
                {
                    NPC.dontTakeDamageFromHostiles = true;
                    NPC.ai[3]--;
                }
                else
                {
                    NPC.dontTakeDamageFromHostiles = false;
                }
                NPC.ai[2] = 0;
                if (Main.expertMode)
                {
                    if (Main.hardMode)
                    {
                        NPC.damage = 45;
                    }
                    else
                    {
                        NPC.damage = 30;
                    }
                }
                else
                {
                    NPC.damage = 15;
                }
                if (NPC.downedMoonlord)
                {
                    if (Main.expertMode)
                    {
                        NPC.damage = 120;
                    }
                    else
                    {
                        NPC.damage = 60;
                    }
                }
                if (NPC.localAI[1] % 15 == 0 && NPC.life < NPC.lifeMax)
                {
                    NPC.life++;
                }
                if (NPC.localAI[1] <= 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, Mod.Find<ModProjectile>("Cactite").Type, NPC.damage, 2, Main.myPlayer, NPC.whoAmI);
                    }
                    NPC.localAI[1] = 40;
                }
                NPC.localAI[1]--;

                int num = 1;
                for (int k = 0; k < NPC.whoAmI; k++)
                {
                    if (Main.npc[k].active && Main.npc[k].target == NPC.target && (Main.npc[k].type == NPC.type || Main.npc[k].type == Mod.Find<ModNPC>("Cactoid").Type))
                    {
                        num++;
                        if (num > 40)
                        {
                            num = 0;
                        }
                    }
                }
                if (NPC.velocity.X > 0f)
                {
                    NPC.direction = 1;
                }
                else if (NPC.velocity.X < 0f)
                {
                    NPC.direction = -1;
                }
                else if (NPC.velocity.Y == 0 && playerCactoid && Math.Abs(NPC.Center.X - (player.Center.X - ((10 + num * 40) * player.direction))) > 30)
                {
                    NPC.velocity.Y = -7;
                }
                if (playerCactoid)
                {
                    for (int k = 0; k < 200; k++)
                    {
                        NPC otherCactoid = Main.npc[k];
                        if (k != NPC.whoAmI && otherCactoid.friendly && otherCactoid.active && otherCactoid.target == NPC.target && (otherCactoid.type == NPC.type || otherCactoid.type == Mod.Find<ModNPC>("Cactoid").Type) && Math.Abs(NPC.position.X - otherCactoid.position.X) + Math.Abs(NPC.position.Y - otherCactoid.position.Y) < NPC.width)
                        {
                            if (NPC.position.X < otherCactoid.position.X)
                            {
                                NPC.velocity.X -= idleAccel;
                            }
                            else
                            {
                                NPC.velocity.X += idleAccel;
                            }
                        }
                    }
                }
                Vector2 targetPos = NPC.position;
                float targetDist = viewDist;
                bool target = false;
                if (player.HasMinionAttackTargetNPC && playerCactoid)
                {
                    NPC N = Main.npc[player.MinionAttackTargetNPC];
                    targetDist = Vector2.Distance(NPC.Center, targetPos);
                    targetPos = N.Center;
                    target = true;
                    NPC.ai[0] = 0;
                }
                else
                {
                    for (int k = 0; k < 200; k++)
                    {
                        NPC N = Main.npc[k];
                        if (N.active && !N.friendly && !N.dontTakeDamage && N.lifeMax > 5 && N.chaseable && !N.immortal)
                        {
                            float distance = Vector2.Distance(NPC.Center, N.Center);
                            if ((distance < targetDist || !target) && Collision.CanHitLine(NPC.position, NPC.width, NPC.height, N.position, N.width, N.height))
                            {
                                targetDist = distance;
                                targetPos = N.Center;
                                target = true;
                                NPC.ai[0] = 0;
                            }
                        }
                    }
                }
                if (Vector2.Distance(player.Center, NPC.Center) > (target ? 1500f : 750f) && playerCactoid)
                {
                    NPC.ai[0] = 1f;
                }
                if (cactusPerson > -1 && Vector2.Distance(Main.npc[cactusPerson].Center, NPC.Center) > 600f && cactusPersonNear && !playerCactoid)
                {
                    NPC.ai[0] = 1f;
                }


                if (target && NPC.ai[0] == 0f)
                {
                    Vector2 direction = targetPos - NPC.Center;
                    direction.Normalize();
                    NPC.direction = direction.X < 0 ? -1 : 1;
                    NPC.velocity.X = (NPC.velocity.X * inertia + direction.X * chaseAccel) / (inertia + 1);
                    if (Math.Abs(NPC.velocity.X) < 0.5f)
                    {
                        NPC.velocity.X = 0;
                    }
                    if (targetPos.Y + 60 < NPC.position.Y && NPC.velocity.Y == 0)
                    {
                        NPC.velocity.Y = -7;
                    }
                }
                else if (playerCactoid)
                {
                    Vector2 center = NPC.Center;
                    Vector2 direction = player.Center - center;
                    if (!Collision.CanHitLine(NPC.Center, 1, 1, player.Center, 1, 1) && direction.Length() >= 200f)
                    {
                        NPC.ai[0] = 1f;
                    }
                    float speed = 6f;
                    if (NPC.ai[0] == 1f)
                    {
                        speed = 15f;
                    }
                    direction.X -= ((10 + num * 40) * player.direction);
                    direction.Y -= 70f;
                    float distanceTo = direction.Length();
                    if (distanceTo > 200f && speed < 9f)
                    {
                        speed = 9f;
                    }
                    if (distanceTo < 200f && NPC.ai[0] == 1f && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                    {
                        NPC.ai[0] = 0f;
                    }
                    if (distanceTo > 48f)
                    {
                        direction.Normalize();
                        direction *= speed;
                        float temp = inertia / 2f;
                        NPC.velocity.X = (NPC.velocity.X * temp + direction.X) / (temp + 1);
                    }
                    else
                    {
                        NPC.direction = Main.player[NPC.target].direction;
                        NPC.velocity.X *= (float)Math.Pow(0.9, 40.0 / inertia);
                        if (Math.Abs(NPC.velocity.X) < 0.5f)
                        {
                            NPC.velocity.X = 0;
                        }
                    }
                }
                else if (cactusPerson > -1 && Vector2.Distance(Main.npc[cactusPerson].Center, NPC.Center) > 600f && cactusPersonNear)
                {
                    if (cactusPerson > -1 && NPC.Center.X > Main.npc[cactusPerson].Center.X)
                    {
                        NPC.direction = -1;
                    }
                    else
                    {
                        NPC.direction = 1;
                    }
                    if (NPC.velocity.X == 0 && NPC.velocity.Y == 0)
                    {
                        NPC.velocity.Y = -7;
                    }
                    NPC.velocity.X = NPC.direction * 4;
                }
                else
                {
                    if (cactusPerson > -1 && cactusPersonNear && Vector2.Distance(Main.npc[cactusPerson].Center, NPC.Center) < 400f)
                    {
                        NPC.ai[0] = 0;
                    }
                    NPC.ai[1] += 1 + Main.rand.Next(5);
                    if (NPC.ai[1] > 900)
                    {
                        if (NPC.velocity.X == 0 && NPC.velocity.Y == 0)
                        {
                            if (Main.rand.Next(4) == 0)
                            {
                                NPC.direction *= -1;
                            }
                            else
                            {
                                NPC.velocity.Y = -7;
                            }
                        }
                        NPC.velocity.X = NPC.direction * 2;
                    }
                    if (NPC.ai[1] > 2000 && NPC.velocity.Y == 0)
                    {
                        NPC.ai[1] = 0;
                        NPC.velocity.X = 0f;
                    }
                }
            }
            else
            {
                if (NPC.ai[2] < 1)
                {
                    NPC.aiStyle = -1;
                    NPC.damage = 0;
                    NPC.ai[1] += 1 + Main.rand.Next(5);
                    NPC.netUpdate = true;
                    if (NPC.direction == 0)
                    {
                        NPC.direction = -1;
                    }
                    if (NPC.ai[1] > 900)
                    {
                        if (NPC.velocity.X == 0 && NPC.velocity.Y == 0)
                        {
                            if (Main.rand.Next(4) == 0)
                            {
                                NPC.direction *= -1;
                            }
                            else
                            {
                                NPC.velocity.Y = -7;
                            }
                        }
                        NPC.velocity.X = NPC.direction * 2;
                    }
                    if (NPC.ai[1] > 2000 && NPC.velocity.Y == 0)
                    {
                        NPC.ai[1] = 0;
                        NPC.velocity.X = 0f;
                    }
                }
                else
                {
                    NPC.FaceTarget();
                    NPC.aiStyle = 26;
                    AIType = NPCID.Unicorn;
                    if (Main.expertMode)
                    {
                        if (Main.hardMode)
                        {
                            NPC.damage = 45;
                        }
                        else
                        {
                            NPC.damage = 30;
                        }
                    }
                    else
                    {
                        NPC.damage = 15;
                    }
                    if (NPC.downedMoonlord)
                    {
                        if (Main.expertMode)
                        {
                            NPC.damage = 120;
                        }
                        else
                        {
                            NPC.damage = 60;
                        }
                    }
                    NPC.ai[1] = 1000;
                    NPC.ai[2] = 1;
                    NPC.velocity.X = NPC.velocity.X * 0.98f;
                }
            }
            NPC.netUpdate = true;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.Player.ZoneBeach && !spawnInfo.Invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse && spawnInfo.SpawnTileY < Main.rockLayer && spawnInfo.Player.ZoneDesert && !spawnInfo.Player.ZoneCorrupt && !spawnInfo.Player.ZoneCrimson && !spawnInfo.Player.ZoneHallow ? (Main.hardMode ? 0.075f : 0.15f) : 0f;
        }

    }
}

