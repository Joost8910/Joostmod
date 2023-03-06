using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
    public class Spectre : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spectre");
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.width = 120;
            NPC.height = 192;
            NPC.damage = 50;
            NPC.defense = 0;
            NPC.lifeMax = 7500;
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = Item.buyPrice(0, 10, 0, 0);
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.frameCounter = 0;
            Banner = NPC.type;
            BannerItem = Mod.Find<ModItem>("SpectreBanner").Type;
            NPC.behindTiles = false;
            NPC.alpha = 150;
        }
        public override void OnKill()
        {
            Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, 1508, Main.rand.Next(4, 12));
            int chance = Main.expertMode ? 6 : 9;
            if (Main.rand.NextBool(chance))
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("SoulGreatsword").Type, 1);
            }
            if (Main.rand.NextBool(chance))
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("SoulArrow").Type, Main.rand.Next(500) + 500);
            }
            if (Main.rand.NextBool(chance))
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("SoulSpear").Type, 1);
            }
            if (Main.rand.NextBool(chance))
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("HomingSoulmass").Type, 1);
            }
            if (Main.rand.NextBool(chance))
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("FocusSouls").Type, 1);
            }
            if (Main.rand.Next(20) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("ThirdAnniversary").Type, 1);
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (Vector2.Distance(NPC.Center, target.Center) > 40)
            {
                return false;
            }
            return base.CanHitPlayer(target, ref cooldownSlot);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 92, 2.5f * (float)hitDirection, Main.rand.Next(-5, 5), 0, default(Color), 0.7f);
                }
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            return (tile.TileType == 41 || tile.TileType == 43 || tile.TileType == 44) && spawnInfo.PlanteraDefeated && spawnInfo.SpawnTileY >= Main.rockLayer && Main.hardMode && !NPC.AnyNPCs(NPC.type) ? 0.0075f : 0f;

        }
        public override void AI()
        {
            NPC.ai[0]++;
            Player P = Main.player[NPC.target];
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            NPC.netUpdate = true;
            if (!(NPC.ai[3] == 5 && NPC.ai[0] > 100))
            {
                if (NPC.Center.X < P.Center.X - 50)
                {
                    NPC.direction = 1;
                }
                if (NPC.Center.X > P.Center.X + 50)
                {
                    NPC.direction = -1;
                }
            }
            if (NPC.Center.Y < P.Center.Y)
            {
                NPC.directionY = 1;
            }
            if (NPC.Center.Y > P.Center.Y)
            {
                NPC.directionY = -1;
            }
            if ((NPC.ai[0] % 7) == 0)
            {
                int dustType = 92;
                int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
                Dust dust = Main.dust[dustIndex];
                dust.velocity.Y = dust.velocity.X - 5 * NPC.direction;
                dust.velocity.Y = dust.velocity.Y + 5;
                dust.noGravity = true;
            }
            float velocity.X = 2f;
            float velocity.Y = 1.5f;
            float Xlration = 0.1f;
            float Ylration = 0.05f;
            if (NPC.ai[3] == 0)
            {
                if (NPC.ai[0] == 26)
                {
                    if (Main.rand.NextBool(3) && Collision.CanHitLine(NPC.Center, 1, 1, P.Center, 1, 1))
                    {
                        NPC.ai[3] = 1;
                        NPC.ai[0] = 0;
                    }
                }
                if (NPC.ai[0] == 82)
                {
                    if (Main.rand.NextBool(2))
                    {
                        NPC.ai[3] = 3;
                        if (Main.rand.NextBool(2))
                        {
                            NPC.ai[0] = 0;
                        }
                        else
                        {
                            NPC.ai[0] = 55;
                        }
                    }
                }
                if (NPC.ai[0] == 138)
                {
                    if (Main.rand.NextBool(2) && Collision.CanHitLine(NPC.Center, 1, 1, P.Center, 1, 1))
                    {
                        NPC.ai[3] = 5;
                        NPC.ai[0] = 0;
                    }
                }
                if (NPC.ai[0] > 140)
                {
                    velocity.X = 5;
                    Xlration = 0.2f;
                    velocity.Y = 4;
                    Ylration = 0.2f;
                    if (NPC.Distance(P.Center) < 150 && P.Center.Y - NPC.Center.Y < 75)
                    {
                        NPC.ai[3] = 3;
                        NPC.ai[0] = 160;
                    }
                }
            }
            if (NPC.velocity.X * NPC.direction < velocity.X)
            {
                NPC.velocity.X += Xlration * NPC.direction;
            }
            if (NPC.velocity.Y * NPC.directionY < velocity.Y)
            {
                NPC.velocity.Y += Ylration * NPC.directionY;
            }
            if (NPC.ai[3] == 1)
            {
                NPC.velocity *= 0.9f;
                if (NPC.ai[0] > 25)
                {
                    NPC.ai[3] = 2;
                    NPC.ai[0] = 0;
                }
                if (NPC.ai[0] % 6 < NPC.ai[0] / 4)
                {
                    int dustType = 92;
                    int dustIndex = Dust.NewDust(NPC.Center, 18, 18, dustType);
                    Dust dust = Main.dust[dustIndex];
                    dust.noGravity = true;
                }
            }
            if (NPC.ai[3] == 2)
            {
                NPC.velocity *= 0.9f;
                if (NPC.ai[0] == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
                }
                if (NPC.ai[0] == 10)
                {
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(NPC.Center, NPC.DirectionTo(P.Center) * 12, Mod.Find<ModProjectile>("HostileSoulArrow").Type, 30, 0, Main.myPlayer, NPC.target);
                    }
                    SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
                }
                if (NPC.ai[0] > 25)
                {
                    NPC.ai[3] = 0;
                    NPC.ai[0] = 0;
                }
            }
            if (NPC.ai[3] == 3)
            {
                if (NPC.ai[0] == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
                }
                NPC.velocity *= 0.9f;
                if (NPC.ai[0] < 26)
                {
                    if (NPC.ai[0] % 6 < NPC.ai[0] / 4)
                    {
                        int dustType = 92;
                        Vector2 pos = NPC.Center + (new Vector2((40 * NPC.direction) - 10, -100));
                        int dustIndex = Dust.NewDust(pos, 18, 18, dustType);
                        Dust dust = Main.dust[dustIndex];
                        dust.noGravity = true;
                    }
                }
                if (NPC.ai[0] == 26)
                {
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(NPC.Center, Vector2.Zero, Mod.Find<ModProjectile>("HostileHomingSoulmass").Type, 25, 0, Main.myPlayer, NPC.whoAmI, 0);
                        Projectile.NewProjectile(NPC.Center, Vector2.Zero, Mod.Find<ModProjectile>("HostileHomingSoulmass").Type, 25, 0, Main.myPlayer, NPC.whoAmI, 45);
                        Projectile.NewProjectile(NPC.Center, Vector2.Zero, Mod.Find<ModProjectile>("HostileHomingSoulmass").Type, 25, 0, Main.myPlayer, NPC.whoAmI, 90);
                        Projectile.NewProjectile(NPC.Center, Vector2.Zero, Mod.Find<ModProjectile>("HostileHomingSoulmass").Type, 25, 0, Main.myPlayer, NPC.whoAmI, 135);
                        Projectile.NewProjectile(NPC.Center, Vector2.Zero, Mod.Find<ModProjectile>("HostileHomingSoulmass").Type, 25, 0, Main.myPlayer, NPC.whoAmI, 180);
                    }
                    SoundEngine.PlaySound(SoundID.Item28, NPC.Center);
                }
                if (NPC.ai[0] > 26 && NPC.ai[0] < 54)
                {
                    NPC.velocity = Vector2.Zero;
                }
                if (NPC.ai[0] == 54)
                {
                    NPC.ai[0] = 0;
                    NPC.ai[3] = 0;
                }
                if (NPC.ai[0] == 55)
                {
                    SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
                }
                if (NPC.ai[0] > 54 && NPC.ai[0] < 135)
                {
                    float e = NPC.ai[0] - 80;
                    if (e > 40)
                    {
                        e -= 40;
                        int dustType = 92;
                        Vector2 pos = NPC.Center + (new Vector2((40 * NPC.direction) - 10, -100));
                        int dustIndex = Dust.NewDust(pos, 18, 18, dustType);
                        Dust dust = Main.dust[dustIndex];
                        dust.scale *= 1.5f;
                        dust.noGravity = true;
                    }
                    if (e % 10 < e / 4)
                    {
                        int dustType = 92;
                        Vector2 pos = NPC.Center + (new Vector2((40 * NPC.direction) - 10, -100));
                        int dustIndex = Dust.NewDust(pos, 18, 18, dustType);
                        Dust dust = Main.dust[dustIndex];
                        dust.noGravity = true;
                    }
                }
                if (NPC.ai[0] == 115)
                {
                    SoundEngine.PlaySound(SoundID.Trackable, NPC.Center);
                }
                if (NPC.ai[0] == 135)
                {
                    Vector2 pos = NPC.Center + (new Vector2((40 * NPC.direction) - 10, -100));
                    float speed = 15;
                    Vector2 dir = Vector2.Normalize((P.Center + new Vector2(P.velocity.X * (Vector2.Distance(P.Center, NPC.Center) / speed), P.velocity.Y * (Vector2.Distance(P.Center, NPC.Center) / speed))) - pos);
                    dir *= speed;
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(pos, dir, Mod.Find<ModProjectile>("HostileSoulSpear").Type, 45, 0, Main.myPlayer, NPC.target);
                    }
                    SoundEngine.PlaySound(SoundID.Item28, NPC.Center);
                }
                if (NPC.ai[0] > 115 && NPC.ai[0] < 155)
                {
                    NPC.velocity = Vector2.Zero;
                }
                if (NPC.ai[0] == 155)
                {
                    NPC.ai[0] = 0;
                    NPC.ai[3] = 0;
                }
                if (NPC.ai[0] == 160)
                {
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(NPC.Center, Vector2.Zero, Mod.Find<ModProjectile>("HostileSoulGreatsword").Type, 60, 0, Main.myPlayer, NPC.whoAmI);
                    }
                    SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
                }
                if (NPC.ai[0] == 200)
                {
                    NPC.ai[0] = 0;
                    NPC.ai[3] = 4;
                }
            }
            if (NPC.ai[3] == 4)
            {
                NPC.velocity = Vector2.Zero;
                if (NPC.ai[0] == 22)
                {
                    NPC.ai[0] = 0;
                    NPC.ai[3] = 0;
                }
            }
            if (NPC.ai[3] == 5)
            {
                NPC.velocity *= 0.9f;
                if (NPC.ai[0] > 60)
                {
                    NPC.velocity = Vector2.Zero;
                }
                if (NPC.ai[0] == 30)
                {
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(NPC.Center, Vector2.Zero, Mod.Find<ModProjectile>("HostileFocusSouls").Type, 50, 0, Main.myPlayer, NPC.whoAmI);
                    }
                    SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
                }
                if (NPC.ai[0] == 230)
                {
                    NPC.ai[0] = 0;
                    NPC.ai[3] = 0;
                }
            }
            if (NPC.ai[0] % 60 > 30)
            {
                NPC.alpha = 150 + (int)NPC.ai[0] % 30 * 2;
            }
            else
            {
                NPC.alpha = 210 - (int)NPC.ai[0] % 30 * 2;
            }
            if (NPC.ai[0] > 300)
            {
                NPC.ai[0] = 0;
            }
            NPC.netUpdate = true;
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            NPC.frameCounter++;
            if (NPC.frameCounter >= 7)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y = (NPC.frame.Y + 220);
            }
            if (NPC.ai[3] == 4)
            {
                if (NPC.ai[0] < 4)
                {
                    NPC.frame.Y = 0;
                }
                else if (NPC.ai[0] < 8)
                {
                    NPC.frame.Y = 220;
                }
                else if (NPC.ai[0] < 12)
                {
                    NPC.frame.Y = 220 * 2;
                }
                else
                {
                    NPC.frame.Y = 220 * 3;
                }
            }
            if (NPC.frame.Y >= 880 || (NPC.ai[3] > 0 && (NPC.ai[0] <= 0 || NPC.ai[0] == 55)))
            {
                NPC.frame.Y = 0;
                NPC.frameCounter = 0;
            }
            if (NPC.ai[3] == 3 && ((NPC.ai[0] >= 190 && NPC.ai[0] <= 200) || (NPC.ai[0] >= 135 && NPC.ai[0] <= 165) || (NPC.ai[0] >= 26 && NPC.ai[0] <= 54)))
            {
                NPC.frame.Y = 3 * 220;
                NPC.frameCounter = 0;
            }
            NPC.frame.X = (int)NPC.ai[3] * 240;
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
            int xFrameCount = 6;
            Color alpha = Color.White;
            alpha.A = (byte)NPC.alpha;
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Rectangle rect = new Rectangle((int)NPC.frame.X, (int)NPC.frame.Y, (texture.Width / xFrameCount), (texture.Height / Main.npcFrameCount[NPC.type]));
            Vector2 vect = new Vector2((float)((texture.Width / xFrameCount) / 2), (float)((texture.Height / Main.npcFrameCount[NPC.type]) / 2));
            sb.Draw(texture, new Vector2(NPC.position.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)(texture.Width / xFrameCount) / 2f + vect.X, NPC.position.Y - Main.screenPosition.Y + (float)NPC.height - (float)(texture.Height / Main.npcFrameCount[NPC.type]) + 4f + vect.Y), new Rectangle?(rect), alpha, NPC.rotation, vect, 1f, effects, 0f);
            return false;
        }
        public override bool CheckDead()
        {
            float targetX = NPC.Center.X;
            float targetY = NPC.Center.Y;
            if (Main.netMode != 1)
            {
                NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, 288, 0, NPC.whoAmI, targetX, targetY);
                NPC.NewNPC((int)NPC.Center.X + 42, (int)NPC.Center.Y, 288, 0, NPC.whoAmI, targetX, targetY);
                NPC.NewNPC((int)NPC.Center.X - 42, (int)NPC.Center.Y, 288, 0, NPC.whoAmI, targetX, targetY);
                NPC.NewNPC((int)NPC.Center.X + 30, (int)NPC.Center.Y + 30, 288, 0, NPC.whoAmI, targetX, targetY);
                NPC.NewNPC((int)NPC.Center.X - 30, (int)NPC.Center.Y + 30, 288, 0, NPC.whoAmI, targetX, targetY);
                NPC.NewNPC((int)NPC.Center.X - 30, (int)NPC.Center.Y - 30, 288, 0, NPC.whoAmI, targetX, targetY);
                NPC.NewNPC((int)NPC.Center.X + 30, (int)NPC.Center.Y - 30, 288, 0, NPC.whoAmI, targetX, targetY);
                NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y + 42, 288, 0, NPC.whoAmI, targetX, targetY);
                NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y - 42, 288, 0, NPC.whoAmI, targetX, targetY);
            }
            return true;
        }
    }
}

