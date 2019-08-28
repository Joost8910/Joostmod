using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
    public class Spectre : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spectre");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 120;
            npc.height = 192;
            npc.damage = 50;
            npc.defense = 0;
            npc.lifeMax = 7500;
            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.value = Item.buyPrice(0, 10, 0, 0);
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.frameCounter = 0;
            banner = npc.type;
            bannerItem = mod.ItemType("SpectreBanner");
            npc.behindTiles = false;
            npc.alpha = 150;
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1508, Main.rand.Next(4, 12));
            int chance = Main.expertMode ? 6 : 9;
            if (Main.rand.NextBool(chance))
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SoulGreatsword"), 1);
            }
            if (Main.rand.NextBool(chance))
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SoulArrow"), Main.rand.Next(500) + 500);
            }
            if (Main.rand.NextBool(chance))
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SoulSpear"), 1);
            }
            if (Main.rand.NextBool(chance))
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HomingSoulmass"), 1);
            }
            if (Main.rand.NextBool(chance))
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FocusSouls"), 1);
            }
            if (Main.rand.Next(20) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ThirdAnniversary"), 1);
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (Vector2.Distance(npc.Center, target.Center) > 40)
            {
                return false;
            }
            return base.CanHitPlayer(target, ref cooldownSlot);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 92, 2.5f * (float)hitDirection, Main.rand.Next(-5, 5), 0, default(Color), 0.7f);
                }
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY];
            return (tile.type == 41 || tile.type == 43 || tile.type == 44) && spawnInfo.planteraDefeated && spawnInfo.spawnTileY >= Main.rockLayer && Main.hardMode && !NPC.AnyNPCs(npc.type) ? 0.0075f : 0f;

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
            if (!(npc.ai[3] == 5 && npc.ai[0] > 100))
            {
                if (npc.Center.X < P.Center.X - 50)
                {
                    npc.direction = 1;
                }
                if (npc.Center.X > P.Center.X + 50)
                {
                    npc.direction = -1;
                }
            }
            if (npc.Center.Y < P.Center.Y)
            {
                npc.directionY = 1;
            }
            if (npc.Center.Y > P.Center.Y)
            {
                npc.directionY = -1;
            }
            if ((npc.ai[0] % 7) == 0)
            {
                int dustType = 92;
                int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
                Dust dust = Main.dust[dustIndex];
                dust.velocity.Y = dust.velocity.X - 5 * npc.direction;
                dust.velocity.Y = dust.velocity.Y + 5;
                dust.noGravity = true;
            }
            float speedX = 2f;
            float speedY = 1.5f;
            float Xlration = 0.1f;
            float Ylration = 0.05f;
            if (npc.ai[3] == 0)
            {
                if (npc.ai[0] == 26)
                {
                    if (Main.rand.NextBool(3) && Collision.CanHitLine(npc.Center, 1, 1, P.Center, 1, 1))
                    {
                        npc.ai[3] = 1;
                        npc.ai[0] = 0;
                    }
                }
                if (npc.ai[0] == 82)
                {
                    if (Main.rand.NextBool(2))
                    {
                        npc.ai[3] = 3;
                        if (Main.rand.NextBool(2))
                        {
                            npc.ai[0] = 0;
                        }
                        else
                        {
                            npc.ai[0] = 55;
                        }
                    }
                }
                if (npc.ai[0] == 138)
                {
                    if (Main.rand.NextBool(2) && Collision.CanHitLine(npc.Center, 1, 1, P.Center, 1, 1))
                    {
                        npc.ai[3] = 5;
                        npc.ai[0] = 0;
                    }
                }
                if (npc.ai[0] > 140)
                {
                    speedX = 5;
                    Xlration = 0.2f;
                    speedY = 4;
                    Ylration = 0.2f;
                    if (npc.Distance(P.Center) < 150 && P.Center.Y - npc.Center.Y < 75)
                    {
                        npc.ai[3] = 3;
                        npc.ai[0] = 160;
                    }
                }
            }
            if (npc.velocity.X * npc.direction < speedX)
            {
                npc.velocity.X += Xlration * npc.direction;
            }
            if (npc.velocity.Y * npc.directionY < speedY)
            {
                npc.velocity.Y += Ylration * npc.directionY;
            }
            if (npc.ai[3] == 1)
            {
                npc.velocity *= 0.9f;
                if (npc.ai[0] > 25)
                {
                    npc.ai[3] = 2;
                    npc.ai[0] = 0;
                }
                if (npc.ai[0] % 6 < npc.ai[0] / 4)
                {
                    int dustType = 92;
                    int dustIndex = Dust.NewDust(npc.Center, 18, 18, dustType);
                    Dust dust = Main.dust[dustIndex];
                    dust.noGravity = true;
                }
            }
            if (npc.ai[3] == 2)
            {
                npc.velocity *= 0.9f;
                if (npc.ai[0] == 0)
                {
                    Main.PlaySound(2, npc.Center, 8);
                }
                if (npc.ai[0] == 10)
                {
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center, npc.DirectionTo(P.Center) * 12, mod.ProjectileType("HostileSoulArrow"), 30, 0, Main.myPlayer, npc.target);
                    }
                    Main.PlaySound(2, npc.Center, 8);
                }
                if (npc.ai[0] > 25)
                {
                    npc.ai[3] = 0;
                    npc.ai[0] = 0;
                }
            }
            if (npc.ai[3] == 3)
            {
                if (npc.ai[0] == 0)
                {
                    Main.PlaySound(2, npc.Center, 8);
                }
                npc.velocity *= 0.9f;
                if (npc.ai[0] < 26)
                {
                    if (npc.ai[0] % 6 < npc.ai[0] / 4)
                    {
                        int dustType = 92;
                        Vector2 pos = npc.Center + (new Vector2((40 * npc.direction) - 10, -100));
                        int dustIndex = Dust.NewDust(pos, 18, 18, dustType);
                        Dust dust = Main.dust[dustIndex];
                        dust.noGravity = true;
                    }
                }
                if (npc.ai[0] == 26)
                {
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("HostileHomingSoulmass"), 25, 0, Main.myPlayer, npc.whoAmI, 0);
                        Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("HostileHomingSoulmass"), 25, 0, Main.myPlayer, npc.whoAmI, 45);
                        Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("HostileHomingSoulmass"), 25, 0, Main.myPlayer, npc.whoAmI, 90);
                        Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("HostileHomingSoulmass"), 25, 0, Main.myPlayer, npc.whoAmI, 135);
                        Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("HostileHomingSoulmass"), 25, 0, Main.myPlayer, npc.whoAmI, 180);
                    }
                    Main.PlaySound(2, npc.Center, 28);
                }
                if (npc.ai[0] > 26 && npc.ai[0] < 54)
                {
                    npc.velocity = Vector2.Zero;
                }
                if (npc.ai[0] == 54)
                {
                    npc.ai[0] = 0;
                    npc.ai[3] = 0;
                }
                if (npc.ai[0] == 55)
                {
                    Main.PlaySound(2, npc.Center, 8);
                }
                if (npc.ai[0] > 54 && npc.ai[0] < 135)
                {
                    float e = npc.ai[0] - 80;
                    if (e > 40)
                    {
                        e -= 40;
                        int dustType = 92;
                        Vector2 pos = npc.Center + (new Vector2((40 * npc.direction) - 10, -100));
                        int dustIndex = Dust.NewDust(pos, 18, 18, dustType);
                        Dust dust = Main.dust[dustIndex];
                        dust.scale *= 1.5f;
                        dust.noGravity = true;
                    }
                    if (e % 10 < e / 4)
                    {
                        int dustType = 92;
                        Vector2 pos = npc.Center + (new Vector2((40 * npc.direction) - 10, -100));
                        int dustIndex = Dust.NewDust(pos, 18, 18, dustType);
                        Dust dust = Main.dust[dustIndex];
                        dust.noGravity = true;
                    }
                }
                if (npc.ai[0] == 115)
                {
                    Main.PlaySound(42, npc.Center, 203);
                }
                if (npc.ai[0] == 135)
                {
                    Vector2 pos = npc.Center + (new Vector2((40 * npc.direction) - 10, -100));
                    float speed = 15;
                    Vector2 dir = Vector2.Normalize((P.Center + new Vector2(P.velocity.X * (Vector2.Distance(P.Center, npc.Center) / speed), P.velocity.Y * (Vector2.Distance(P.Center, npc.Center) / speed))) - pos);
                    dir *= speed;
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(pos, dir, mod.ProjectileType("HostileSoulSpear"), 45, 0, Main.myPlayer, npc.target);
                    }
                    Main.PlaySound(2, npc.Center, 28);
                }
                if (npc.ai[0] > 115 && npc.ai[0] < 155)
                {
                    npc.velocity = Vector2.Zero;
                }
                if (npc.ai[0] == 155)
                {
                    npc.ai[0] = 0;
                    npc.ai[3] = 0;
                }
                if (npc.ai[0] == 160)
                {
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("HostileSoulGreatsword"), 60, 0, Main.myPlayer, npc.whoAmI);
                    }
                    Main.PlaySound(2, npc.Center, 8);
                }
                if (npc.ai[0] == 200)
                {
                    npc.ai[0] = 0;
                    npc.ai[3] = 4;
                }
            }
            if (npc.ai[3] == 4)
            {
                npc.velocity = Vector2.Zero;
                if (npc.ai[0] == 22)
                {
                    npc.ai[0] = 0;
                    npc.ai[3] = 0;
                }
            }
            if (npc.ai[3] == 5)
            {
                npc.velocity *= 0.9f;
                if (npc.ai[0] > 60)
                {
                    npc.velocity = Vector2.Zero;
                }
                if (npc.ai[0] == 30)
                {
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("HostileFocusSouls"), 50, 0, Main.myPlayer, npc.whoAmI);
                    }
                    Main.PlaySound(2, npc.Center, 8);
                }
                if (npc.ai[0] == 230)
                {
                    npc.ai[0] = 0;
                    npc.ai[3] = 0;
                }
            }
            if (npc.ai[0] % 60 > 30)
            {
                npc.alpha = 150 + (int)npc.ai[0] % 30 * 2;
            }
            else
            {
                npc.alpha = 210 - (int)npc.ai[0] % 30 * 2;
            }
            if (npc.ai[0] > 300)
            {
                npc.ai[0] = 0;
            }
            npc.netUpdate = true;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            npc.frameCounter++;
            if (npc.frameCounter >= 7)
            {
                npc.frameCounter = 0;
                npc.frame.Y = (npc.frame.Y + 220);
            }
            if (npc.ai[3] == 4)
            {
                if (npc.ai[0] < 4)
                {
                    npc.frame.Y = 0;
                }
                else if (npc.ai[0] < 8)
                {
                    npc.frame.Y = 220;
                }
                else if (npc.ai[0] < 12)
                {
                    npc.frame.Y = 220 * 2;
                }
                else
                {
                    npc.frame.Y = 220 * 3;
                }
            }
            if (npc.frame.Y >= 880 || (npc.ai[3] > 0 && (npc.ai[0] <= 0 || npc.ai[0] == 55)))
            {
                npc.frame.Y = 0;
                npc.frameCounter = 0;
            }
            if (npc.ai[3] == 3 && ((npc.ai[0] >= 190 && npc.ai[0] <= 200) || (npc.ai[0] >= 135 && npc.ai[0] <= 165) || (npc.ai[0] >= 26 && npc.ai[0] <= 54)))
            {
                npc.frame.Y = 3 * 220;
                npc.frameCounter = 0;
            }
            npc.frame.X = (int)npc.ai[3] * 240;
        }
        public override bool PreDraw(SpriteBatch sb, Color drawColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (npc.spriteDirection == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                effects = SpriteEffects.None;
            }
            int xFrameCount = 6;
            Color alpha = Color.White;
            alpha.A = (byte)npc.alpha;
            Texture2D texture = Main.npcTexture[npc.type];
            Rectangle rect = new Rectangle((int)npc.frame.X, (int)npc.frame.Y, (texture.Width / xFrameCount), (texture.Height / Main.npcFrameCount[npc.type]));
            Vector2 vect = new Vector2((float)((texture.Width / xFrameCount) / 2), (float)((texture.Height / Main.npcFrameCount[npc.type]) / 2));
            sb.Draw(texture, new Vector2(npc.position.X - Main.screenPosition.X + (float)(npc.width / 2) - (float)(texture.Width / xFrameCount) / 2f + vect.X, npc.position.Y - Main.screenPosition.Y + (float)npc.height - (float)(texture.Height / Main.npcFrameCount[npc.type]) + 4f + vect.Y), new Rectangle?(rect), alpha, npc.rotation, vect, 1f, effects, 0f);
            return false;
        }
        public override bool CheckDead()
        {
            float targetX = npc.Center.X;
            float targetY = npc.Center.Y;
            if (Main.netMode != 1)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, 288, 0, npc.whoAmI, targetX, targetY);
                NPC.NewNPC((int)npc.Center.X + 42, (int)npc.Center.Y, 288, 0, npc.whoAmI, targetX, targetY);
                NPC.NewNPC((int)npc.Center.X - 42, (int)npc.Center.Y, 288, 0, npc.whoAmI, targetX, targetY);
                NPC.NewNPC((int)npc.Center.X + 30, (int)npc.Center.Y + 30, 288, 0, npc.whoAmI, targetX, targetY);
                NPC.NewNPC((int)npc.Center.X - 30, (int)npc.Center.Y + 30, 288, 0, npc.whoAmI, targetX, targetY);
                NPC.NewNPC((int)npc.Center.X - 30, (int)npc.Center.Y - 30, 288, 0, npc.whoAmI, targetX, targetY);
                NPC.NewNPC((int)npc.Center.X + 30, (int)npc.Center.Y - 30, 288, 0, npc.whoAmI, targetX, targetY);
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + 42, 288, 0, npc.whoAmI, targetX, targetY);
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 42, 288, 0, npc.whoAmI, targetX, targetY);
            }
            return true;
        }
    }
}

