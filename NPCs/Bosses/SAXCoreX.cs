using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
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
            Main.npcFrameCount[NPC.type] = 17;
        }
        public override void SetDefaults()
        {
            NPC.width = 72;
            NPC.height = 72;
            NPC.damage = 150;
            NPC.defense = 20;
            NPC.lifeMax = 100000;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath43;
            NPC.value = Item.buyPrice(12, 50, 0, 0);
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 0;
            NPC.coldDamage = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.buffImmune[Mod.Find<ModBuff>("InfectedRed").Type] = true;
            NPC.buffImmune[Mod.Find<ModBuff>("InfectedGreen").Type] = true;
            NPC.buffImmune[Mod.Find<ModBuff>("InfectedBlue").Type] = true;
            NPC.buffImmune[Mod.Find<ModBuff>("InfectedYellow").Type] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("XBag").Type;
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/VsSAX");
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * bossLifeScale);
            NPC.damage = (int)(NPC.damage * 0.75f);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/SAX"), NPC.scale);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/SAX"), NPC.scale);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/SAX"), NPC.scale);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/SAX"), NPC.scale);
            }
        }
        public override void OnKill()
        {
            JoostWorld.downedSAX = true;

            if (Main.expertMode)
            {
                NPC.DropBossBags();
            }
            else
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("IceCoreX").Type, 1 + Main.rand.Next(2));
                if (Main.rand.Next(4) == 0)
                {
                    Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("SAXMusicBox").Type);
                }
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("SAXMask").Type);
                }
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("SAXTrophy").Type);
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("FifthAnniversary").Type, 1);
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Vector2 vect = NPC.velocity;
            vect.Normalize();
            Rectangle eye = new Rectangle((int)(NPC.Center.X + (vect.X * 24)) - 16, (int)(NPC.Center.Y + (vect.Y * 24)) - 16, 32, 32);
            if (NPC.ai[0] < 56 || !projectile.Hitbox.Intersects(eye))
            {
                if (projectile.minion || projectile.sentry)
                {
                    damage = damage / 2;
                }
                else if (Main.player[projectile.owner].heldProj == projectile.whoAmI)
                {
                    damage = damage / 3;
                }
                else
                {
                    damage = damage / 5;
                }
                crit = false;
                //npc.ai[2] += 1 + ((damage / 2) * (Main.expertMode ? 2 : 1));
                //npc.netUpdate = true;
            }
            else
            {
                SoundEngine.PlaySound(SoundID.NPCHit18, NPC.Center);
                damage = damage / 2;
                crit = true;
            }
        }
        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (NPC.ai[0] < 56)
            {
                damage = damage / 3;
                crit = false;
                //npc.ai[2] += 1 + ((damage / 2) * (Main.expertMode ? 2 : 1));
                //npc.netUpdate = true;
            }
            else
            {
                SoundEngine.PlaySound(SoundID.NPCHit18, NPC.Center);
            }
        }
        bool message = false;
        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            if (NPC.ai[0] < 56)
            {
                NPC.ai[2] += 1 + ((damage / 2) * (Main.expertMode ? 2 : 1));
                if (Main.netMode != 0)
                {
                    ModPacket netMessage = GetPacket(SAXCoreMessageType.ShellHit);
                    netMessage.Write((int)NPC.ai[2]);
                    if (Main.netMode == 1)
                    {
                        netMessage.Write(Main.myPlayer);
                    }
                    netMessage.Send();
                }
                if (!message)
                {
                    Main.NewText("It seems hitting it in the 'shell' isn't very effective...", Color.SkyBlue);
                    message = true;
                }
            }
        }
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            Vector2 vect = NPC.velocity;
            vect.Normalize();
            Rectangle eye = new Rectangle((int)(NPC.Center.X + (vect.X * 24)) - 16, (int)(NPC.Center.Y + (vect.Y * 24)) - 16, 32, 32);
            if (NPC.ai[0] < 56 || !projectile.Hitbox.Intersects(eye))
            {
                NPC.ai[2] += 1 + ((damage / 2) * (Main.expertMode ? 2 : 1));
                if (Main.netMode != 0)
                {
                    ModPacket netMessage = GetPacket(SAXCoreMessageType.ShellHit);
                    netMessage.Write((int)NPC.ai[2]);
                    if (Main.netMode == 1)
                    {
                        netMessage.Write(Main.myPlayer);
                    }
                    netMessage.Send();
                }
                if (!message)
                {
                    Main.NewText("It seems hitting it in the 'shell' isn't very effective...", Color.SkyBlue);
                    message = true;
                }
            }
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((int)NPC.localAI[0]);
            writer.Write((int)NPC.localAI[1]);
            writer.Write((int)NPC.localAI[2]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            NPC.localAI[0] = reader.ReadInt32();
            NPC.localAI[1] = reader.ReadInt32();
            NPC.localAI[2] = reader.ReadInt32();
        }

        private ModPacket GetPacket(SAXCoreMessageType type)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)JoostModMessageType.SAXCore);
            packet.Write(NPC.whoAmI);
            packet.Write((byte)type);
            return packet;
        }
        public void HandlePacket(BinaryReader reader)
        {
            SAXCoreMessageType type = (SAXCoreMessageType)reader.ReadByte();
            if (type == SAXCoreMessageType.ShellHit)
            {
                int ai2 = reader.ReadInt32();
                NPC.ai[2] = ai2;
                if (Main.netMode == 2)
                {
                    ModPacket netMessage = GetPacket(SAXCoreMessageType.ShellHit);
                    int ignore = reader.ReadInt32();
                    netMessage.Write(ai2);
                    netMessage.Send(-1, ignore);
                }
            }
        }
        public override void AI()
        {
            NPC.ai[0]++;
            Player P = Main.player[NPC.target];
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(false);
                P = Main.player[NPC.target];
                if (!P.active || P.dead)
                {
                    NPC.velocity = new Vector2(0f, -100f);
                    NPC.active = false;
                }
            }
            NPC.direction = 1;
            float moveSpeed = 3;
            if (NPC.Distance(P.MountedCenter) > 120)
            {
                moveSpeed = 3 + (NPC.Distance(P.MountedCenter) - 120) / 40;
            }
            if (NPC.ai[0] < 56)
            {
                NPC.velocity = NPC.DirectionTo(P.Center) * moveSpeed;
                NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X);
            }
            if (NPC.ai[0] % 5 == 0)
            {
                NPC.localAI[0]++;
                if (NPC.localAI[0] > 7)
                {
                    NPC.localAI[0] = 0;
                }
            }
            if (NPC.ai[1] > 0 && NPC.ai[0] > 90)
            {
                if (NPC.ai[0] == 100)
                {
                    SoundEngine.PlaySound(SoundID.Item13, NPC.Center);
                }
                if (NPC.ai[0] % 12 == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item15, NPC.Center);
                }
                NPC.velocity = Vector2.Zero;
                if (NPC.ai[0] < 200)
                {
                    NPC.localAI[1] += 0.1f;
                }
                Vector2 vect = NPC.velocity;
                vect.Normalize();
                Vector2 pos = new Vector2((int)(NPC.Center.X + (vect.X * 24)) - 16, (int)(NPC.Center.Y + (vect.Y * 24)) - 16);
                float e = NPC.ai[0] - 90;
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
                NPC.rotation = NPC.ai[0] * 0.0174f * NPC.localAI[1];
                if (NPC.ai[0] == 180)
                {
                    SoundEngine.PlaySound(SoundID.Trackable, NPC.Center);
                }
                if (NPC.ai[0] > 200)
                {
                    float speed = 12f;
                    int damage = 75;
                    int type = Mod.Find<ModProjectile>("SAXBeam").Type;
                    SoundEngine.PlaySound(SoundLoader.customSoundType, (int)NPC.position.X, (int)NPC.position.Y, Mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/IceBeam"));
                    float rotation = NPC.rotation;
                    if (Main.netMode != 1)
                    {
                        //Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * speed)), (float)((Math.Sin(rotation) * speed)), type, damage, 0f, Main.myPlayer, 1);
                        Projectile.NewProjectile(NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation - 0.0174f * 2.5f) * speed)), (float)((Math.Sin(rotation - 0.0174f * 2.5f) * speed)), type, damage, 0f, Main.myPlayer);
                        //Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * speed)), (float)((Math.Sin(rotation) * speed)), type, damage, 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation + 0.0174f * 2.5f) * speed)), (float)((Math.Sin(rotation + 0.0174f * 2.5f) * speed)), type, damage, 0f, Main.myPlayer);
                        //Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(rotation) * speed)), (float)((Math.Sin(rotation) * speed)), type, damage, 0f, Main.myPlayer, -1);
                    }
                }
                if (NPC.ai[0] > 240)
                {
                    NPC.ai[0] = -150;
                    NPC.ai[3] = 0;
                    NPC.localAI[1] = 0;
                    NPC.ai[1] = 0;
                }
            }
            else
            {
                if (NPC.ai[0] >= 56)
                {
                    float speed = 24f;
                    Vector2 predictedPos = P.MountedCenter + P.velocity + (P.velocity * (Vector2.Distance(P.MountedCenter, NPC.Center) / speed));
                    predictedPos = P.MountedCenter + P.velocity + (P.velocity * (Vector2.Distance(predictedPos, NPC.Center) / speed));
                    predictedPos = P.MountedCenter + P.velocity + (P.velocity * (Vector2.Distance(predictedPos, NPC.Center) / speed));
                    Vector2 dir = NPC.DirectionTo(predictedPos);
                    NPC.velocity = dir * moveSpeed;
                    NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X);
                }
                if (NPC.ai[0] > 90 && NPC.ai[0] < 200)
                {
                    if (NPC.ai[0] % 18 == 0)
                    {
                        SoundEngine.PlaySound(SoundID.Item15, NPC.Center);
                    }
                    Vector2 vect = NPC.velocity;
                    vect.Normalize();
                    Vector2 pos = new Vector2((int)(NPC.Center.X + (vect.X * 24)) - 16, (int)(NPC.Center.Y + (vect.Y * 24)) - 16);
                    float e = NPC.ai[0] - 90;
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
                if (NPC.ai[0] == 200)
                {
                    float speed = 12f;
                    if (NPC.velocity.Length() > speed)
                    {
                        speed += (NPC.velocity.Length() - speed) / 2;
                    }
                    int damage = 100;
                    int type = Mod.Find<ModProjectile>("SAXBeamCharged").Type;
                    SoundEngine.PlaySound(SoundLoader.customSoundType, (int)NPC.position.X, (int)NPC.position.Y, Mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/IceBeamCharged"));
                    float rotation = NPC.rotation;
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * speed)), (float)((Math.Sin(rotation) * speed)), type, damage, 0f, Main.myPlayer, 1);
                        Projectile.NewProjectile(NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * speed)), (float)((Math.Sin(rotation) * speed)), type, damage, 0f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * speed)), (float)((Math.Sin(rotation) * speed)), type, damage, 0f, Main.myPlayer, -1);
                    }
                }
                if (NPC.ai[0] >= 200)
                {
                    NPC.velocity = Vector2.Zero;
                }
                if (NPC.ai[0] > 240)
                {
                    NPC.ai[0] = -150;
                    NPC.ai[3]++;
                    if (NPC.ai[3] > 20)
                    {
                        NPC.ai[1] = 1;
                    }
                }
            }
            NPC.ai[2]++;
            if (NPC.ai[2] > 400)
            {
                if (Main.netMode != 1)
                {
                    switch (Main.rand.Next(4))
                    {
                        case 1:
                            NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("GreenXParasite").Type);
                            break;
                        case 2:
                            NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("RedXParasite").Type);
                            break;
                        case 3:
                            NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("IceXParasite").Type);
                            break;
                        default:
                            NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("XParasite").Type);
                            break;
                    }
                        
                }
                NPC.ai[2] = 1;
                NPC.ai[3]++;
            }
            if (NPC.localAI[2] < 90)
            {
                NPC.localAI[2]++;
                if (NPC.localAI[2] < 60)
                {
                    NPC.dontTakeDamage = true;
                    NPC.velocity = Vector2.Zero;
                    Vector2 rote = NPC.localAI[3].ToRotationVector2();
                    Vector2 move = P.Center - NPC.Center;
                    move.Normalize();
                    float home = 21f - (NPC.localAI[2] / 3);
                    Vector2 vecRot = ((home - 1f) * rote + move) / home;
                    NPC.rotation = (float)Math.Atan2(vecRot.Y, vecRot.X);
                    NPC.localAI[3] = NPC.rotation;
                }
                else
                {
                    NPC.dontTakeDamage = false;
                    moveSpeed = moveSpeed * ((NPC.localAI[2] - 60) / 30);
                    NPC.velocity = NPC.DirectionTo(P.Center) * moveSpeed;
                    NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X);
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            Color color = Lighting.GetColor((int)(NPC.Center.X / 16), (int)(NPC.Center.Y / 16));
            Texture2D tex = Mod.GetTexture("NPCs/Bosses/IceCoreX");
            Rectangle rect = new Rectangle(0, (int)NPC.localAI[0] * 64, (tex.Width), (tex.Height / 8));
            Vector2 vect = new Vector2((float)tex.Width / 2, (float)tex.Height / 16);
            float rotation = 0;
            spriteBatch.Draw(tex, new Vector2(NPC.position.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)(tex.Width / 1) / 2f + vect.X - 2f, NPC.position.Y - Main.screenPosition.Y + (float)(NPC.height / 2) - (float)(tex.Height / 16) + 2f + vect.Y), new Rectangle?(rect), color, rotation, vect, NPC.scale, effects, 0f);

            int xFrameCount = 1;
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Rectangle rectangle = new Rectangle(NPC.frame.X, NPC.frame.Y, (texture.Width / xFrameCount), (texture.Height / Main.npcFrameCount[NPC.type]));
            Vector2 vector = new Vector2(((texture.Width / xFrameCount) / 2f), ((texture.Height / Main.npcFrameCount[NPC.type]) / 2f));
            
            spriteBatch.Draw(texture, new Vector2(NPC.position.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)(texture.Width / xFrameCount) / 2f + vector.X, NPC.position.Y - Main.screenPosition.Y + (float)NPC.height - (float)(texture.Height / Main.npcFrameCount[NPC.type]) + 12f + vector.Y), new Rectangle?(rectangle), color, NPC.rotation, vector, NPC.scale, effects, 0f);
            return false;
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = -1;
            if (NPC.ai[0] < 0)
            {
                NPC.frame.Y = 0;
            }
            else if (NPC.ai[0] < 90)
            {
                if (NPC.ai[0] % 8 == 0)
                {
                    NPC.frame.Y += 96;
                }
                if (NPC.frame.Y > 96 * 7)
                {
                    NPC.frame.Y = 96 * 7;
                }
            }
            else if (NPC.ai[0] < 200)
            {
                if (NPC.ai[0] % 8 == 0)
                {
                    NPC.frame.Y += 96;
                }
                if (NPC.frame.Y > 96 * 15)
                {
                    NPC.frame.Y = 96 * 7;
                }
            }
            else
            {
                NPC.frame.Y = 96 * 16;
                if (NPC.ai[0] > 208)
                {
                    NPC.frame.Y = 96 * 4;
                }
                if (NPC.ai[0] > 216)
                {
                    NPC.frame.Y = 96 * 3;
                }
                if (NPC.ai[0] > 224)
                {
                    NPC.frame.Y = 96 * 2;
                }
                if (NPC.ai[0] > 232)
                {
                    NPC.frame.Y = 96 * 1;
                }
            }
        }
    }
    enum SAXCoreMessageType : byte
    {
        ShellHit
    }
}

