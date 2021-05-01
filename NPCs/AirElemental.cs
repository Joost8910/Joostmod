using System;
using Microsoft.Xna.Framework;
using Terraria;
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
            npc.width = 24;
            npc.height = 54;
            npc.damage = 25;
            npc.defense = 0;
            npc.lifeMax = 350;
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
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.Ichor] = true;
            npc.alpha = 75;
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TinyTwister"), (Main.expertMode ? Main.rand.Next(12, 30) : Main.rand.Next(8, 20)));

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
            return !spawnInfo.playerInTown && !spawnInfo.invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse && spawnInfo.sky && Main.hardMode ? 0.18f : 0f;
        }
        public override void AI()
        {
            npc.ai[0]++;
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            if (npc.ai[0] == 220)
            {
                for (int d = 0; d < 20; d++)
                {
                    Dust dust = Dust.NewDustDirect(npc.Center - new Vector2(10, 10), 20, 20, 31, npc.velocity.X * 0.8f, npc.velocity.Y * 0.8f, 0, default(Color), 1.5f);
                    Vector2 vel = npc.Center - dust.position;
                    vel.Normalize();
                    dust.position -= vel * 30;
                    dust.velocity = vel * 2 + npc.velocity;
                    dust.noGravity = true;
                }
            }
            if (npc.ai[0] > 220 && npc.ai[0] % 3 == 0)
            {
                 Dust.NewDustDirect(npc.Center - new Vector2(10, 10), 20, 20, 31, npc.velocity.X * 0.8f, npc.velocity.Y * 0.8f, 0, default(Color), 1f);
            }
            if (npc.ai[3] > 10)
            {
                if (Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    npc.velocity *= 0.98f;
                }
                if (Main.expertMode) // Wind
                {
                    npc.ai[1]++;
                    if (npc.ai[1] > 1000)
                    {
                        if (npc.ai[1] % 2 == 0)
                        {
                            Dust.NewDustDirect(npc.position, npc.width, npc.height, 31, npc.velocity.X * 0.8f, npc.velocity.Y * 0.8f, 0, Color.White, 1.5f);
                        }
                        if (npc.velocity.Y * npc.directionY < 8)
                        {
                            npc.velocity.Y += 0.3f * npc.directionY;
                        }
                        if (Collision.CanHitLine(npc.Center, 1, 1, P.Center, 1, 1) && npc.ai[2] == 0 && Math.Abs(npc.Center.Y - P.Center.Y) < 40)
                        {
                            npc.ai[2]++;
                        }
                    }
                    if (npc.ai[2] > 0)
                    {
                        npc.ai[2]++;
                        npc.velocity *= 0.98f;

                        Vector2 position = npc.Center;
                        float speedX = ((7f + Main.rand.NextFloat() * 7f) * npc.direction) + (npc.direction * npc.velocity.X > 0 ? npc.velocity.X : 0);
                        position.X -= 1000 * npc.direction;
                        position.Y += Main.rand.Next(-8, 8) * 10;
                        if (npc.ai[2] % 2 == 0)
                        {
                            Dust.NewDustPerfect(position, 31, new Vector2(speedX * 3, 0), 0, Color.White, 2f);
                        }
                        if (npc.ai[2] % 16 == 0)
                        {
                            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 7, 1.8f, -0.8f);

                            if (Main.netMode != 1)
                            {
                                Projectile.NewProjectile(position.X, position.Y, speedX, 0, mod.ProjectileType("HostileWind"), 25, 10f);
                            }
                        }
                        if (npc.ai[2] > 200)
                        {
                            npc.ai[0] = 0;
                            npc.ai[1] = 0;
                            npc.ai[2] = 0;
                        }
                    }
                }
                if (npc.ai[0] >= 240 && Collision.CanHitLine(npc.Center - new Vector2(20, 20), 40, 40, P.Center, 1, 1) && npc.ai[2] == 0)
                {
                    float Speed = 8f;
                    int damage = 25;
                    int type = mod.ProjectileType("Gust");
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 1);
                    Vector2 dir = npc.DirectionTo(P.Center);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center, dir * Speed, type, damage, 10f);
                    }
                    for (int d = 0; d < 10; d++)
                    {
                        Dust.NewDust(npc.Center - new Vector2(10, 10), 20, 20, 31, dir.X * Speed, dir.Y * Speed, 0, default(Color), 2f);
                    }
                    npc.ai[0] = 0;
                    npc.velocity = dir * -10;
                }
                npc.direction = npc.Center.X < P.Center.X ? 1 : -1;
                npc.directionY = npc.Center.Y < P.Center.Y ? 1 : -1;
                float speed = 10;
                Vector2 vel = new Vector2(speed * npc.direction, speed * npc.directionY);
                if (vel.Length() > speed)
                {
                    vel *= speed / vel.Length();
                }
                if (npc.Distance(P.Center) < 300 && Collision.CanHitLine(npc.Center, 1, 1, P.Center, 1, 1))
                {
                    vel *= -1;
                }
                float home = 50f;
                if (vel != Vector2.Zero)
                {
                    npc.velocity = ((home - 1f) * npc.velocity + vel) / home;
                }
            }
            else
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 31, 0, 0, 0, Color.White, 1.5f);
            }
            if (npc.ai[3] < (Main.expertMode ? 90 : 120))
            {
                npc.ai[3]++;
            }
            else
            {
                if (npc.ai[0] % 2 == 0)
                {
                    Dust.NewDustPerfect(new Vector2(npc.Center.X, npc.Center.Y + (npc.height / 2)), 31, Vector2.Zero, 0, Color.White, 1).noGravity = true;
                }
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    Projectile proj = Main.projectile[i];
                    float collisionPoint = 0;
                    if (proj.active && proj.friendly && proj.damage > 0 && proj.CanHit(npc) && Collision.CheckAABBvLineCollision(npc.position, npc.Size, proj.Center, proj.Center + proj.velocity * 10 * (proj.extraUpdates + 1), proj.width, ref collisionPoint))
                    {
                        Vector2 vel = proj.velocity;
                        vel.Normalize();
                        npc.velocity = vel.RotatedBy(90 * 0.0174f) * 12;
                        npc.ai[3] = 0;
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 7);
                        break;
                    }
                }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter * (npc.velocity.Length() / 2 + 2) >= 20)
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

