using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Buffs;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using JoostMod.Items.Consumables;

namespace JoostMod.NPCs.Bosses.SAX
{
    public abstract class XParasite : ModNPC
    {
        protected int infectionBuffID = ModContent.BuffType<InfectedYellow>();
        protected int spriteFrameHeight = 34;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("X Parasite");
            Main.npcFrameCount[NPC.type] = 6;
            NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<InfectedYellow>()] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<InfectedBlue>()] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<InfectedGreen>()] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<InfectedRed>()] = true;
        }
        public override void SetDefaults()
        {
            NPC.width = 28;
            NPC.height = 28;
            NPC.damage = 50;
            NPC.defense = 5;
            NPC.lifeMax = 1000;
            NPC.HitSound = SoundID.NPCHit25;
            NPC.DeathSound = SoundID.NPCDeath28;
            NPC.value = 0;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.frameCounter = 0;
        }
        public override void FindFrame(int frameHeight)
        {
            frameHeight = spriteFrameHeight;
            NPC.frameCounter++;
            if (NPC.frameCounter >= 6)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
            }
            if (NPC.frame.Y >= frameHeight * 6)
            {
                NPC.frame.Y = 0;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(infectionBuffID, 1800);

            NPC.DeathSound = SoundID.NPCDeath19;
            NPC.life = 0;
            NPC.checkDead();
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                ModPacket netMessage = GetPacket();
                netMessage.Send();
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit)
        {
            target.AddBuff(infectionBuffID, 1800);
            NPC.life = 0;
            NPC.checkDead();
        }
        private ModPacket GetPacket()
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)JoostModMessageType.KillNPC);
            packet.Write(NPC.whoAmI);
            return packet;
        }
        private void SetStats()
        {
            NPC.scale = 0.7f + Main.rand.Next(9) * 0.05f;
            NPC.Size *= NPC.scale;
            NPC.damage = NPC.defDamage = (int)(NPC.damage * NPC.scale);
            NPC.life = NPC.lifeMax = (int)(NPC.life * NPC.scale);
            NPC.value = (int)(NPC.value * NPC.scale);
            NPC.npcSlots *= NPC.scale;
        }
        private ref float HomingStrength => ref NPC.ai[1];
        private ref float Speed => ref NPC.ai[2];
        private ref float TransShaderIntensity => ref NPC.localAI[0];

        public override void AI()
        {
            if (NPC.ai[0] == 0)
            {
                SetStats();
                Speed = 3f;
                NPC.velocity = (Main.rand.NextFloat() * MathHelper.TwoPi).ToRotationVector2() * Speed;
                HomingStrength = 16f + Main.rand.NextFloat() * 12f;
                NPC.netUpdate = true;
            }
            if (NPC.ai[0] < 45)
            {
                if (NPC.ai[0] % 2 == 0)
                {
                    TransShaderIntensity = (45 - NPC.ai[0]) * Main.rand.NextFloat() * 1.5f;
                }
                NPC.ai[0]++;
            }
            Player P = Main.player[NPC.target];
            if (!NPC.HasValidTarget)
            {
                NPC.TargetClosest(true);
            }

            if (NPC.ai[0] > 30)
            {
                if (NPC.HasValidTarget)
                {
                    Vector2 move = P.MountedCenter - NPC.Center;
                    if (Speed < 10f * (1 + (1 - NPC.scale)))
                    {
                        Speed += 0.1f;
                    }
                    float effectiveSpeed = Speed;
                    if (NPC.Distance(P.MountedCenter) > 600)
                    {
                        effectiveSpeed = Speed + MathHelper.Min(12, (NPC.Distance(P.MountedCenter) - 600) / 60);
                    }
                    if (move.Length() > effectiveSpeed && effectiveSpeed > 0)
                    {
                        move *= effectiveSpeed / move.Length();
                    }
                    float home = HomingStrength;
                    NPC.velocity = ((home - 1f) * NPC.velocity + move) / home;

                    if (NPC.velocity.Length() < effectiveSpeed && effectiveSpeed > 0)
                    {
                        NPC.velocity *= 1.02f;
                    }
                    if (NPC.velocity.Length() > effectiveSpeed)
                    {
                        NPC.velocity.Normalize();
                        NPC.velocity *= effectiveSpeed;
                    }
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            int frameHeight = texture.Height / Main.npcFrameCount[NPC.type];
            int frameWidth = texture.Width;
            Rectangle rectangle = new Rectangle(NPC.frame.X, NPC.frame.Y, texture.Width, texture.Height / Main.npcFrameCount[NPC.type]);
            Vector2 origin = new Vector2(texture.Width / 2f, texture.Height / Main.npcFrameCount[NPC.type] / 2f);
            Vector2 drawPos = new Vector2(NPC.position.X - Main.screenPosition.X + NPC.width / 2 - texture.Width / 2f + origin.X, NPC.position.Y - Main.screenPosition.Y + NPC.height - frameHeight + origin.Y + NPC.gfxOffY);

            Color lightColor = Lighting.GetColor((int)(NPC.Center.X / 16), (int)(NPC.Center.Y / 16));


            DrawData data = new DrawData(texture, drawPos, new Rectangle?(rectangle), lightColor, NPC.rotation, origin, NPC.scale, effects, 0f);
            if (NPC.ai[0] < 45)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.ZoomMatrix);

                MiscShaderData shaderData = GameShaders.Misc["JoostXTransform"];

                shaderData.UseImage0(TextureAssets.Npc[NPC.type]);
                shaderData.UseImage1(TextureAssets.Npc[NPC.type]);
                float intensity = TransShaderIntensity;
                shaderData.UseOpacity(intensity);

                shaderData.Apply(data);
                data.Draw(spriteBatch);

                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.ZoomMatrix);
            }
            else
            {
                data.Draw(spriteBatch);
            }

            return false;
        }
    }

    public class XParasiteYellow : XParasite
    {
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                var sauce = NPC.GetSource_Death();
                for (int i = 0; i < 4; i++)
                {
                    Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("XParasite").Type);
                }
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.GoldCoin));
        }
        
    }
    public class XParasiteRed : XParasite
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            infectionBuffID = ModContent.BuffType<InfectedRed>();
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                var sauce = NPC.GetSource_Death();
                for (int i = 0; i < 4; i++)
                {
                    Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("RedXParasite").Type);
                }
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Heart));
        }
    }
    public class XParasiteGreen : XParasite
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            infectionBuffID = ModContent.BuffType<InfectedGreen>();
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                var sauce = NPC.GetSource_Death();
                for (int i = 0; i < 4; i++)
                {
                    Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("GreenXParasite").Type);
                }
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EnergyFragment>()));
        }
    }
    public class XParasiteIce : XParasite
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 42;
            NPC.height = 42;
            NPC.damage = 70;
            NPC.defense = 15;
            NPC.lifeMax = 2000;
            NPC.knockBackResist = 0.1f;
            NPC.coldDamage = true;
            infectionBuffID = ModContent.BuffType<InfectedBlue>();
            spriteFrameHeight = 50;
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                var sauce = NPC.GetSource_Death();
                for (int i = 0; i < 4; i++)
                {
                    Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("IceXParasite").Type);
                }
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Star));
        }
    }
}

