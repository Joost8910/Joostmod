using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Bosses
{
    [AutoloadBossHead]
    public class SAXCoreX : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SA-X");
            Main.npcFrameCount[npc.type] = 17;
        }
        public override void SetDefaults()
        {
            npc.width = 72;
            npc.height = 72;
            npc.damage = 150;
            npc.defense = 20;
            npc.lifeMax = 100000;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCDeath43;
            npc.value = Item.buyPrice(20, 0, 0, 0);
            npc.knockBackResist = 0f;
            npc.aiStyle = 0;
            npc.coldDamage = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.buffImmune[mod.BuffType("InfectedRed")] = true;
            npc.buffImmune[mod.BuffType("InfectedGreen")] = true;
            npc.buffImmune[mod.BuffType("InfectedBlue")] = true;
            npc.buffImmune[mod.BuffType("InfectedYellow")] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            bossBag = mod.ItemType("XBag");
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/VsSAX");
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.75f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.75f);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SAX"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SAX"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SAX"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/SAX"), npc.scale);
            }
        }
        public override void NPCLoot()
        {
            JoostWorld.downedSAX = true;

            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("IceCoreX"), 1 + Main.rand.Next(2));
                if (Main.rand.Next(2) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SAXMusicBox"));
                }
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SAXMask"));
                }
                if (Main.rand.Next(5) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SAXTrophy"));
                }
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Vector2 vect = npc.velocity;
            vect.Normalize();
            Rectangle eye = new Rectangle((int)(npc.Center.X + (vect.X * 24)) - 16, (int)(npc.Center.Y + (vect.Y * 24)) - 16, 32, 32);
            if (npc.ai[0] < 56 || !projectile.Hitbox.Intersects(eye))
            {
                if (projectile.minion || projectile.sentry)
                {
                    damage = damage / 2;
                }
                else
                {
                    damage = damage / 5;
                }
                crit = false;
                npc.ai[2] += 1 + (damage / 2 * (Main.expertMode ? 2 : 1));
            }
            else
            {
                Main.PlaySound(SoundID.NPCHit18, npc.Center);
                damage = damage / 2;
                crit = true;
            }
        }
        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (npc.ai[0] < 56)
            {
                damage = damage / 4;
                crit = false;
                npc.ai[2] += 1 + (damage / 2 * (Main.expertMode ? 2 : 1));
            }
            else
            {
                Main.PlaySound(SoundID.NPCHit18, npc.Center);
            }
        }
        public override void AI()
        {
            npc.ai[0]++;
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(false);
                P = Main.player[npc.target];
                if (!P.active || P.dead)
                {
                    npc.velocity = new Vector2(0f, -100f);
                    npc.active = false;
                }
            }
            npc.direction = 1;
            npc.localAI[1] = 3;
            if (npc.Distance(P.MountedCenter) > 120)
            {
                npc.localAI[1] = 3 + (npc.Distance(P.MountedCenter) - 120) / 40;
            }
            if (npc.ai[0] < 56)
            {
                npc.velocity = npc.DirectionTo(P.Center) * npc.localAI[1];
                npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X);
            }
            if (npc.ai[0] % 5 == 0)
            {
                npc.localAI[0]++;
                if (npc.localAI[0] > 7)
                {
                    npc.localAI[0] = 0;
                }
            }
            if (npc.localAI[3] > 0 && npc.ai[0] > 90)
            {
                if (npc.ai[0] == 100)
                {
                    Main.PlaySound(2, npc.Center, 13);
                }
                if (npc.ai[0] % 12 == 0)
                {
                    Main.PlaySound(2, npc.Center, 15);
                }
                npc.velocity = Vector2.Zero;
                if (npc.ai[0] < 200)
                {
                    npc.localAI[2] += 0.1f;
                }
                Vector2 vect = npc.velocity;
                vect.Normalize();
                Vector2 pos = new Vector2((int)(npc.Center.X + (vect.X * 24)) - 16, (int)(npc.Center.Y + (vect.Y * 24)) - 16);
                float e = npc.ai[0] - 90;
                if (e > 40)
                {
                    e -= 40;
                    int dustType = 135;
                    int dustIndex = Dust.NewDust(pos, 36, 36, dustType);
                    Dust dust = Main.dust[dustIndex];
                    dust.scale *= 1.5f;
                    dust.noGravity = true;
                }
                if (e % 10 < e / 4)
                {
                    int dustType = 135;
                    int dustIndex = Dust.NewDust(pos, 36, 36, dustType);
                    Dust dust = Main.dust[dustIndex];
                    dust.noGravity = true;
                }
                npc.rotation = npc.ai[0] * 0.0174f * npc.localAI[2];
                if (npc.ai[0] == 180)
                {
                    Main.PlaySound(42, npc.Center, 44);
                }
                if (npc.ai[0] > 200)
                {
                    float speed = 12f;
                    int damage = 75;
                    int type = mod.ProjectileType("SAXBeam");
                    Main.PlaySound(SoundLoader.customSoundType, (int)npc.position.X, (int)npc.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/IceBeam"));
                    float rotation = npc.rotation;
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * speed)), (float)((Math.Sin(rotation) * speed)), type, damage, 0f, Main.myPlayer);
                    }
                }
                if (npc.ai[0] > 240)
                {
                    npc.ai[0] = -150;
                    npc.ai[3] = 0;
                    npc.localAI[2] = 0;
                    npc.localAI[3] = 0;
                }
            }
            else
            {
                if (npc.ai[0] >= 56)
                {
                    float speed = 24f;
                    Vector2 predictedPos = P.MountedCenter + P.velocity + (P.velocity * (Vector2.Distance(P.MountedCenter, npc.Center) / speed));
                    predictedPos = P.MountedCenter + P.velocity + (P.velocity * (Vector2.Distance(predictedPos, npc.Center) / speed));
                    predictedPos = P.MountedCenter + P.velocity + (P.velocity * (Vector2.Distance(predictedPos, npc.Center) / speed));
                    Vector2 dir = npc.DirectionTo(predictedPos);
                    npc.velocity = dir * npc.localAI[1];
                    npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X);
                }
                if (npc.ai[0] > 90 && npc.ai[0] < 200)
                {
                    if (npc.ai[0] % 18 == 0)
                    {
                        Main.PlaySound(2, npc.Center, 15);
                    }
                    Vector2 vect = npc.velocity;
                    vect.Normalize();
                    Vector2 pos = new Vector2((int)(npc.Center.X + (vect.X * 24)) - 16, (int)(npc.Center.Y + (vect.Y * 24)) - 16);
                    float e = npc.ai[0] - 90;
                    if (e > 40)
                    {
                        e -= 40;
                        int dustType = 135;
                        int dustIndex = Dust.NewDust(pos, 36, 36, dustType);
                        Dust dust = Main.dust[dustIndex];
                        dust.scale *= 1.5f;
                        dust.noGravity = true;
                    }
                    if (e % 10 < e / 4)
                    {
                        int dustType = 135;
                        int dustIndex = Dust.NewDust(pos, 36, 36, dustType);
                        Dust dust = Main.dust[dustIndex];
                        dust.noGravity = true;
                    }
                }
                if (npc.ai[0] == 200)
                {
                    float speed = 12f;
                    if (npc.velocity.Length() > speed)
                    {
                        speed += (npc.velocity.Length() - speed) / 2;
                    }
                    int damage = 100;
                    int type = mod.ProjectileType("SAXBeamCharged");
                    Main.PlaySound(SoundLoader.customSoundType, (int)npc.position.X, (int)npc.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/IceBeamCharged"));
                    float rotation = npc.rotation;
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * speed)), (float)((Math.Sin(rotation) * speed)), type, damage, 0f, Main.myPlayer);
                    }
                }
                if (npc.ai[0] >= 200)
                {
                    npc.velocity = Vector2.Zero;
                }
                if (npc.ai[0] > 240)
                {
                    npc.ai[0] = -150;
                    npc.ai[3]++;
                    if (npc.ai[3] > 20)
                    {
                        npc.localAI[3] = 1;
                    }
                }
            }
            npc.ai[2]++;
            if (npc.ai[2] > 400)
            {
                if (Main.netMode != 1)
                {
                    switch (Main.rand.Next(4))
                    {
                        case 1:
                            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("GreenXParasite"));
                            break;
                        case 2:
                            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("RedXParasite"));
                            break;
                        case 3:
                            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("IceXParasite"));
                            break;
                        default:
                            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("XParasite"));
                            break;
                    }
                        
                }
                npc.ai[2] = 0;
                npc.ai[3]++;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            Color color = Lighting.GetColor((int)(npc.Center.X / 16), (int)(npc.Center.Y / 16));
            Texture2D tex = mod.GetTexture("NPCs/Bosses/IceCoreX");
            Rectangle rect = new Rectangle(0, (int)npc.localAI[0] * 64, (tex.Width), (tex.Height / 8));
            Vector2 vect = new Vector2((float)tex.Width / 2, (float)tex.Height / 16);
            float rotation = 0;
            spriteBatch.Draw(tex, new Vector2(npc.position.X - Main.screenPosition.X + (float)(npc.width / 2) - (float)(tex.Width / 1) / 2f + vect.X - 2f, npc.position.Y - Main.screenPosition.Y + (float)(npc.height / 2) - (float)(tex.Height / 16) + 2f + vect.Y), new Rectangle?(rect), color, rotation, vect, npc.scale, effects, 0f);

            int xFrameCount = 1;
            Texture2D texture = Main.npcTexture[npc.type];
            Rectangle rectangle = new Rectangle(npc.frame.X, npc.frame.Y, (texture.Width / xFrameCount), (texture.Height / Main.npcFrameCount[npc.type]));
            Vector2 vector = new Vector2(((texture.Width / xFrameCount) / 2f), ((texture.Height / Main.npcFrameCount[npc.type]) / 2f));
            
            spriteBatch.Draw(texture, new Vector2(npc.position.X - Main.screenPosition.X + (float)(npc.width / 2) - (float)(texture.Width / xFrameCount) / 2f + vector.X, npc.position.Y - Main.screenPosition.Y + (float)npc.height - (float)(texture.Height / Main.npcFrameCount[npc.type]) + 12f + vector.Y), new Rectangle?(rectangle), color, npc.rotation, vector, npc.scale, effects, 0f);
            return false;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = -1;
            if (npc.ai[0] < 0)
            {
                npc.frame.Y = 0;
            }
            else if (npc.ai[0] < 90)
            {
                if (npc.ai[0] % 8 == 0)
                {
                    npc.frame.Y += 96;
                }
                if (npc.frame.Y > 96 * 7)
                {
                    npc.frame.Y = 96 * 7;
                }
            }
            else if (npc.ai[0] < 200)
            {
                if (npc.ai[0] % 8 == 0)
                {
                    npc.frame.Y += 96;
                }
                if (npc.frame.Y > 96 * 15)
                {
                    npc.frame.Y = 96 * 7;
                }
            }
            else
            {
                npc.frame.Y = 96 * 16;
                if (npc.ai[0] > 208)
                {
                    npc.frame.Y = 96 * 4;
                }
                if (npc.ai[0] > 216)
                {
                    npc.frame.Y = 96 * 3;
                }
                if (npc.ai[0] > 224)
                {
                    npc.frame.Y = 96 * 2;
                }
                if (npc.ai[0] > 232)
                {
                    npc.frame.Y = 96 * 1;
                }
            }
        }
    }
}

