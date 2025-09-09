using System;
using System.IO;
using JoostMod.Items.Armor;
using JoostMod.Items.Consumables;
using JoostMod.Items.Materials;
using JoostMod.Items.Placeable;
using JoostMod.NPCs.Mobs;
using JoostMod.Projectiles.Hostile;
using JoostMod.Projectiles.Thrown;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Bosses.JumboCactuar
{
    [AutoloadBossHead]
	public class JumboCactuar : ModNPC
	{
        static Point STOMP_HITBOX = new Point(108, 260);
        static Point HEAD_HITBOX = new Point(192, 192);
        static Point FALL_HITBOX = new Point(170, 344);
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Jumbo Cactuar");
			Main.npcFrameCount[NPC.type] = 5;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        }
		public override void SetDefaults()
		{
			NPC.width = 150;
			NPC.height = 300;
			NPC.scale = 2f;
			NPC.damage = 120;
			NPC.defense = 30;
			NPC.lifeMax = 330000;
			NPC.boss = true;
			NPC.lavaImmune = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = Item.buyPrice(7, 50, 0, 0);
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1;
			NPC.frameCounter = 0;
            if (!Main.dedServ)
                Music = Main.drunkWorld ? MusicLoader.GetMusicSlot(Mod, "Sounds/Music/DontBeAfraid") : MusicLoader.GetMusicSlot(Mod, "Sounds/Music/TheDecisiveBattle");
            //bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = ModContent.ItemType<JumboCactuarBag>();
            NPC.noTileCollide = true;
			NPC.noGravity = true;
            SceneEffectPriority = SceneEffectPriority.BossHigh;
        }

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * balance);
			NPC.damage = (int)(NPC.damage * 0.7f);
		}
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.SuperHealingPotion;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (NPC.ai[3] >= 510 && NPC.ai[3] < 520)
            {
                target.AddBuff(BuffID.Dazed, 120);

                if (Main.expertMode && target.position.Y > NPC.Center.Y)
                    target.position.Y = NPC.position.Y + NPC.height + 18;
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (NPC.ai[3] >= 360 && NPC.ai[3] < 510 && target.position.Y + target.height > NPC.position.Y + NPC.height - STOMP_HITBOX.Y)
            {
                return false;
            }
            if (NPC.ai[3] >= 1430 && target.position.Y + target.height < NPC.position.Y + NPC.height - FALL_HITBOX.Y)
            {
                return false;
            }
            if (NPC.ai[0] > 4500)
            {
                return false;
            }
            cooldownSlot = ImmunityCooldownID.Bosses;
            return base.CanHitPlayer(target, ref cooldownSlot);
        }
        private Rectangle HeadBox()
        {
            Rectangle headBox = new Rectangle(0, 0, HEAD_HITBOX.X, HEAD_HITBOX.Y);
            Vector2 rotPoint = new Vector2(NPC.Center.X, NPC.Center.Y - 84 + NPC.gfxOffY);
            Vector2 centerPoint = rotPoint + new Vector2(156 * NPC.direction, -256).RotatedBy(NPC.rotation);
            headBox.X = (int)(centerPoint.X + 16 * NPC.direction - headBox.Width / 2);
            headBox.Y = (int)(centerPoint.Y - headBox.Height / 2);
            return headBox;
        }
        private Rectangle StompBox()
        {
            Rectangle stompBox = new Rectangle(0, 0, STOMP_HITBOX.X, STOMP_HITBOX.Y);
            stompBox.X = (int)(NPC.position.X + (NPC.direction < 0 ? -stompBox.Width : NPC.width));
            stompBox.Y = (int)(NPC.position.Y + NPC.height - stompBox.Height);
            return stompBox;
        }
        private Rectangle FallBox()
        {
            Rectangle fallBox = new Rectangle(0, 0, FALL_HITBOX.X, FALL_HITBOX.Y);
            fallBox.X = (int)NPC.position.X - fallBox.Width;
            fallBox.Y = (int)NPC.position.Y + NPC.height - fallBox.Height;
            fallBox.Width = NPC.width + FALL_HITBOX.X * 2;
            return fallBox;
        }
        public override bool ModifyCollisionData(Rectangle victimHitbox, ref int immunityCooldownSlot, ref MultipliableFloat damageMultiplier, ref Rectangle npcHitbox)
        {
            Rectangle headBox = HeadBox();
            if (victimHitbox.Intersects(headBox))
            {
                npcHitbox = headBox;
                if (victimHitbox.Y < headBox.Center.Y)
                    damageMultiplier *= 1.15f;
            }

            if ((NPC.ai[3] >= 1430 && NPC.ai[3] < 1440) || (NPC.ai[0] >= 4300 && NPC.ai[0] < 4400))
            {
                Rectangle fallBox = new Rectangle(0, 0, FALL_HITBOX.X, FALL_HITBOX.Y);
                fallBox.X = (int)NPC.position.X - fallBox.Width;
                fallBox.Y = (int)NPC.position.Y + NPC.height - fallBox.Height;
                fallBox.Width = NPC.width + FALL_HITBOX.X * 2;
                if (victimHitbox.Intersects(fallBox))
                {
                    npcHitbox = fallBox;
                    //immunityCooldownSlot = 2;
                    damageMultiplier *= 1.5f;
                }
                if (NPC.ai[0] >= 4300 && NPC.ai[0] < 4400)
                {
                    damageMultiplier *= 2f;
                }
            }
            if (NPC.ai[3] >= 510 && NPC.ai[3] < 520)
            {
                Rectangle stompBox = new Rectangle(0, 0, STOMP_HITBOX.X, STOMP_HITBOX.Y);
                stompBox.X = (int)(NPC.position.X + (NPC.direction < 0 ? -stompBox.Width : NPC.width));
                stompBox.Y = (int)(NPC.position.Y + NPC.height - stompBox.Height);
                if (victimHitbox.Intersects(stompBox))
                {
                    npcHitbox = stompBox;
                    //immunityCooldownSlot = 2;
                    damageMultiplier *= 1.25f;
                }
            }
            return base.ModifyCollisionData(victimHitbox, ref immunityCooldownSlot, ref damageMultiplier, ref npcHitbox);
        }
        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            if (NPC.localAI[0] > 300)
            {
                modifiers.Defense.Flat += (int)(NPC.localAI[0] / 2);
            }
            if (NPC.life < NPC.lifeMax * 0.1f)
            {
                modifiers.FinalDamage *= 0.67f;
            }
            else if (NPC.life < NPC.lifeMax * 0.2f)
            {
                modifiers.FinalDamage *= 0.75f;
            }
            if (NPC.ai[0] > 4000 && NPC.ai[0] < 4500)
            {
                modifiers.FinalDamage *= 0.2f;
            }
        }
        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            if (NPC.localAI[0] > 300)
            {
                modifiers.SourceDamage *= 2;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D tex = TextureAssets.Npc[NPC.type].Value;
            Rectangle rect = NPC.frame;
            Vector2 origin = NPC.frame.Size() / 2;
            Vector2 drawPos = NPC.Center - screenPos + new Vector2(0f, NPC.gfxOffY);
            SpriteEffects effects = SpriteEffects.None;
            Vector2 scale = new Vector2(1, 1);
            if (NPC.ai[0] > 4030 && NPC.ai[0] <= 4110)
            {
                if (NPC.ai[0] < 4040)
                {
                    scale.Y = 1f - (NPC.ai[0] - 4030) * 0.025f;
                }
                else if (NPC.ai[0] < 4050)
                {
                    scale.Y = 0.75f;
                }
                else if (NPC.ai[0] < 4060)
                {
                    scale.Y = 0.75f - (NPC.ai[0] - 4050) * 0.025f;
                }
                else if (NPC.ai[0] < 4070)
                {
                    scale.Y = 0.5f;
                }
                else if (NPC.ai[0] < 4080)
                {
                    scale.Y = 0.5f - (NPC.ai[0] - 4070) * 0.025f;
                }
                else if (NPC.ai[0] < 4100)
                {
                    scale.Y = 0.25f;
                }
                else
                {
                    scale.Y = 0.25f + (NPC.ai[0] - 4100) * 0.075f;
                }
                drawPos.Y += rect.Height * (1f - scale.Y);
            }
            if (NPC.spriteDirection == 1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            drawColor = NPC.GetNPCColorTintedByBuffs(drawColor);
            DrawData data = new DrawData(tex, drawPos, new Rectangle?(rect), drawColor, NPC.rotation, origin, scale * NPC.scale, effects, 0);
            if (NPC.ai[0] > 4300 && NPC.ai[0] < 4500)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);

                MiscShaderData shaderData = GameShaders.Misc["JoostMeteor"];

                Texture2D fireTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_Meteor");
                Rectangle rect2 = new Rectangle(0, 0, fireTex.Width, fireTex.Height);
                Vector2 origin2 = rect2.Size() / 2;
                DrawData data2 = new DrawData(fireTex, drawPos, new Rectangle?(rect2), Color.White, NPC.rotation, origin2, scale * NPC.scale, effects, 0);

                shaderData.UseImage0(ModContent.Request<Texture2D>($"{Texture}_Meteor"));
                shaderData.Apply(data2);    
                data2.Draw(spriteBatch);

                shaderData.UseImage0(TextureAssets.Npc[NPC.type]);
                shaderData.Apply(data);
                data.Draw(spriteBatch);

                spriteBatch.End();
                spriteBatch.Begin();
            }
            else
            {
                data.Draw(spriteBatch);
            }


            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            bool showHitbox = false;
            if (showHitbox)
            {
                Texture2D tex = TextureAssets.Npc[NPC.type].Value;
                Rectangle rect = new Rectangle(92, 64, 2, 2);
                Vector2 drawOrigin = rect.Size() / 2;
                Color color = Color.Red;

                Rectangle headBox = HeadBox();


                for (int i = 0; i < headBox.Width; i += 2)
                {
                    Main.EntitySpriteDraw(tex, headBox.TopLeft() + new Vector2(i, 0) - screenPos, new Rectangle?(rect), color, 0f, drawOrigin, 1f, SpriteEffects.None);
                    Main.EntitySpriteDraw(tex, headBox.BottomLeft() + new Vector2(i, 0) - screenPos, new Rectangle?(rect), color, 0f, drawOrigin, 1f, SpriteEffects.None);
                }
                for (int j = 0; j < headBox.Height; j += 2)
                {
                    Main.EntitySpriteDraw(tex, headBox.TopLeft() + new Vector2(0, j) - screenPos, new Rectangle?(rect), color, 0f, drawOrigin, 1f, SpriteEffects.None);
                    Main.EntitySpriteDraw(tex, headBox.TopRight() + new Vector2(0, j) - screenPos, new Rectangle?(rect), color, 0f, drawOrigin, 1f, SpriteEffects.None);
                }

                if (NPC.ai[3] >= 510 && NPC.ai[3] < 520)
                {
                    Rectangle stompBox = StompBox();

                    for (int i = 0; i < stompBox.Width; i += 2)
                    {
                        Main.EntitySpriteDraw(tex, stompBox.TopLeft() + new Vector2(i, 0) - screenPos, new Rectangle?(rect), color, 0f, drawOrigin, 1f, SpriteEffects.None);
                        Main.EntitySpriteDraw(tex, stompBox.BottomLeft() + new Vector2(i, 0) - screenPos, new Rectangle?(rect), color, 0f, drawOrigin, 1f, SpriteEffects.None);
                    }
                    for (int j = 0; j < stompBox.Height; j += 2)
                    {
                        Main.EntitySpriteDraw(tex, stompBox.TopLeft() + new Vector2(0, j) - screenPos, new Rectangle?(rect), color, 0f, drawOrigin, 1f, SpriteEffects.None);
                        Main.EntitySpriteDraw(tex, stompBox.TopRight() + new Vector2(0, j) - screenPos, new Rectangle?(rect), color, 0f, drawOrigin, 1f, SpriteEffects.None);
                    }
                }
                if (NPC.ai[3] >= 1430 && NPC.ai[3] < 1440 || (NPC.ai[0] >= 4300 && NPC.ai[0] < 4400))
                {
                    Rectangle fallBox = FallBox();
                    for (int i = 0; i < fallBox.Width; i += 2)
                    {
                        Main.EntitySpriteDraw(tex, fallBox.TopLeft() + new Vector2(i, 0) - screenPos, new Rectangle?(rect), color, 0f, drawOrigin, 1f, SpriteEffects.None);
                        Main.EntitySpriteDraw(tex, fallBox.BottomLeft() + new Vector2(i, 0) - screenPos, new Rectangle?(rect), color, 0f, drawOrigin, 1f, SpriteEffects.None);
                    }
                    for (int j = 0; j < fallBox.Height; j += 2)
                    {
                        Main.EntitySpriteDraw(tex, fallBox.TopLeft() + new Vector2(0, j) - screenPos, new Rectangle?(rect), color, 0f, drawOrigin, 1f, SpriteEffects.None);
                        Main.EntitySpriteDraw(tex, fallBox.TopRight() + new Vector2(0, j) - screenPos, new Rectangle?(rect), color, 0f, drawOrigin, 1f, SpriteEffects.None);
                    }
                }
            }
        }

        public override void FindFrame(int frameHeight)
		{
			NPC.spriteDirection = NPC.direction;
			NPC.frameCounter++;
			if (NPC.ai[2] <= 0 && !(NPC.ai[1] % 400 >= 380 && NPC.ai[1] < 1500))
			{
				if (NPC.frameCounter >= 14 - Math.Abs(NPC.velocity.X))
				{
					NPC.frameCounter = 0;	
					NPC.frame.Y = (NPC.frame.Y + frameHeight);
                }
                if (NPC.ai[0] > 4000)
                {
                    NPC.frame.Y = frameHeight;
                }
                if (NPC.frame.Y >= frameHeight * 2 || (NPC.ai[3] >= 300 && NPC.ai[3] < 600) || (NPC.ai[3] >= 1330 && NPC.ai[3] < 1430) || NPC.ai[0] > 4100)
				{
					NPC.frame.Y = 0;	
				}
                if ((NPC.ai[3] >= 1430 && NPC.ai[3] < 1510) || (NPC.ai[0] >= 4220 && NPC.ai[0] < 4720))
                {
                    NPC.frame.Y = frameHeight * 4;
                }
			}
			if (NPC.ai[2] > 0 && NPC.ai[2] < 15)
			{
				NPC.frame.Y = frameHeight * 2;
			}
			if (NPC.ai[2] >= 15 || (NPC.ai[1] % 400 >= 380 && NPC.ai[1] < 1500))
			{
				if (NPC.frameCounter >= 4)
				{
					NPC.frameCounter = 0;	
					NPC.frame.Y = (NPC.frame.Y == 0 ? frameHeight * 3 : 0);		
				}
                if (NPC.frame.Y == frameHeight * 2 || NPC.frame.Y == frameHeight)
                {
                    NPC.frame.Y = frameHeight * 3;
                }
			}
		}
        public override void HitEffect(NPC.HitInfo hit)
		{
            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/JumboCactuarDie"), NPC.Center);

                var sauce = NPC.GetSource_Death();
                Gore.NewGore(sauce, NPC.position + new Vector2(NPC.direction > 0 ? NPC.width: 0, 0), NPC.velocity, Mod.Find<ModGore>("JumboCactuar1").Type, 2);
				for (int i = 0; i < 3; i++)
                {
                    Gore.NewGore(sauce, NPC.position + new Vector2((NPC.direction > 0 ? NPC.width : 0) - 40 + i * 40, 0), NPC.velocity, Mod.Find<ModGore>("GiantNeedle").Type, 2);
                }
                Gore.NewGore(sauce, new Vector2(NPC.Center.X - (NPC.width / 2 * NPC.direction), NPC.Center.Y + NPC.height / 5), NPC.velocity, Mod.Find<ModGore>("JumboCactuar2").Type, 2);
                Gore.NewGore(sauce, new Vector2(NPC.Center.X + (NPC.width / 2 * NPC.direction), NPC.Center.Y + NPC.height / 4), NPC.velocity, Mod.Find<ModGore>("JumboCactuar2").Type, 2);
                Gore.NewGore(sauce, new Vector2(NPC.Center.X + (NPC.width * 0.75f * NPC.direction), NPC.Center.Y - NPC.height / 6), NPC.velocity, Mod.Find<ModGore>("JumboCactuar2").Type, 2);
                Gore.NewGore(sauce, new Vector2(NPC.Center.X - (NPC.width / 3 * NPC.direction), NPC.Center.Y - NPC.height / 3), NPC.velocity, Mod.Find<ModGore>("JumboCactuar2").Type, 2);
            }
        }
		public override void OnKill()
		{
			JoostWorld.downedJumboCactuar = true;

            /*
                if (Main.expertMode)
                {
                    NPC.DropBossBags();
                }
                else
                {
                    Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Cactustoken>(), 1 + Main.rand.Next(2));
                    if (Main.rand.Next(4) == 0)
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<DecisiveBattleMusicBox>());
                    }
                    if (Main.rand.Next(7) == 0)
                    {
                        Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<JumboCactuarMask>());
                    }
                }
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<JumboCactuarTrophy>());
                }
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<FifthAnniversary>(), 1);
                }
            */
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<JumboCactuarBag>()));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<JumboCactuarTrophy>(), 10));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FifthAnniversary>(), 10));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Cactustoken>(), 1, 1, 2));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<JumboCactuarMask>(), 7));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsMasterMode(), ModContent.ItemType<JumboCactuarRelic>()));

            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
            notExpertRule.OnSuccess(ItemDropRule.ByCondition(new Conditions.DrunkWorldIsNotUp(), ModContent.ItemType<DecisiveBattleMusicBox>(), 4));
            notExpertRule.OnSuccess(ItemDropRule.ByCondition(new Conditions.DrunkWorldIsUp(), ModContent.ItemType<DontBeAfraidMusicBox>(), 4));
        }
        public override void AI()
		{
            var sauce = NPC.GetSource_FromAI();
            float maxFallSpeed = 20f;
            //NPC.netUpdate = true;
            bool faceTarget = NPC.ai[3] < 1360 && !(NPC.ai[3] >= 510 && NPC.ai[3] < 600) && NPC.timeLeft > 300;
			Player target = Main.player[NPC.target];
			if (!NPC.HasValidTarget || NPC.ai[3] == 300 || NPC.ai[3] == 1300)
			{
				NPC.TargetClosestUpgraded(faceTarget);
				target = Main.player[NPC.target];
            }
            if (faceTarget)
            {
                NPC.targetRect = target.getRect();
                NPC.FaceTarget();
            }
            //Main.NewText(NPC.targetRect);
            if (!NPC.HasValidTarget)
            {
                if (NPC.timeLeft > 200)
                {
                    NPC.timeLeft = 200;
                    NPC.direction *= -1;
                }
            }
            else if (NPC.life > NPC.lifeMax * 0.05f)
            {
                NPC.timeLeft = 750;
            }

            #region Pushbox
            foreach (Player P in Main.ActivePlayers)
            {
                if (P.active && !P.dead)
                {
                    if (NPC.ai[3] >= 1430 && NPC.ai[3] < 1440 && P.Hitbox.Intersects(FallBox()))
                    {
                        Vector2 push = new Vector2(0, (NPC.Center.Y + (NPC.height / 2) + 32 - P.position.Y - P.gravity));

                        Vector2 collide = Collision.TileCollision(P.position, push, P.width, P.height);
                        P.position += collide;
                        P.AddBuff(BuffID.Dazed, 120);
                    }
                    else if (P.Hitbox.Intersects(NPC.Hitbox))
                    {
                        int dirX = P.Center.X - NPC.Center.X > 0 ? 1 : -1;
                        int dirY = 0;
                        if (P.Center.Y < NPC.position.Y)
                        {
                            dirY = -1;
                        }
                        Vector2 push = new Vector2((NPC.Center.X + (NPC.width / 2 * dirX)) - (P.Center.X + (P.width / 2 * -dirX)), 0);
                        if (dirY != 0)
                        {
                            push = new Vector2(0, (NPC.Center.Y + (NPC.height / 2 * dirY)) - (P.Center.Y + (P.height / 2 * -dirY)) - P.gravity);
                        }

                        if (Collision.CanHitLine(P.position, P.width, P.height, P.position + push, P.width, P.height))
                        {
                            Vector2 collide = Collision.TileCollision(P.position, push, P.width, P.height);
                            P.position += collide;
                        } 
                    }
                }
            }
            #endregion

            bool desert = target.ZoneDesert;
            bool corrupt = target.ZoneCorrupt;
            bool crimson = target.ZoneCrimson;
            bool hallow = target.ZoneHallow;

            if (NPC.ai[3] < 1300 && NPC.ai[0] < 400 && NPC.velocity.Y == 0 && target.Center.Y < NPC.position.Y)
            {
                NPC.velocity.Y = -20.2f;
            }

            #region Tile Collision
            if (target.position.Y >= NPC.position.Y + NPC.height - 2 && Collision.SolidCollision(NPC.position, NPC.width, NPC.height) && NPC.timeLeft > 200 && NPC.ai[0] < 4000)
            {
                maxFallSpeed = 8f;
                NPC.velocity.Y += 0.2f;
                if (NPC.ai[3] >= 360 && NPC.ai[3] < 505)
                {
                    NPC.velocity.Y += 0.8f;
                    maxFallSpeed = 15f;
                }
                if (NPC.velocity.Y < 0)
                {
                    NPC.velocity.Y = 0;
                }
            }
            else 
            {
                Vector2 pos = new Vector2(NPC.position.X, NPC.position.Y + NPC.height - 64);
                int collide = 0;
                for (int i = 0; i < 16; i++)
                {
                    Vector2 pos2 = new Vector2(NPC.position.X + ((NPC.width / 16) * i), pos.Y);
                    if (Collision.SolidCollision(pos2, NPC.width / 16, 64))
                    {
                        collide++;
                    }
                }
                if ((collide > 5 || NPC.position.Y + NPC.height >= Main.maxTilesY * 16) && (NPC.ai[0] < 4100 || NPC.ai[0] > 4400))
                {
                    if (NPC.velocity.Y > 0f)
                    {
                        if (NPC.ai[3] >= 360 && NPC.ai[3] < 510 && target.Center.Y > NPC.position.Y + NPC.height)
                        {
                            NPC.velocity.Y += 1f;
                        }
                        else
                        {
                            NPC.velocity.Y = 0f;
                        }
                        if (target.position.Y < NPC.position.Y - 200 && NPC.ai[3] < 1300 && NPC.timeLeft > 200 && NPC.ai[0] < 4000)
                        {
                            NPC.velocity.Y = -(float)Math.Sqrt(2 * 0.5f * Math.Abs(target.position.Y - (NPC.position.Y + NPC.height)));
                        }
                    }
                    else if (NPC.ai[3] < 1300 && NPC.ai[0] < 4000)
                    {
                        float maxSpeed = 6f;
                        if (NPC.ai[3] >= 360 && NPC.ai[3] < 510 && target.Center.Y < NPC.position.Y + NPC.height)
                        {
                            NPC.velocity.Y -= 0.5f;
                            maxSpeed = 10f;
                        }
                        if (NPC.timeLeft <= 200)
                            maxSpeed = 12f;
                        if (Main.getGoodWorld)
                            maxSpeed *= 1.4f;
                        NPC.velocity.Y -= 0.125f;
                        if (NPC.velocity.Y < -maxSpeed)
                        {
                            NPC.velocity.Y = -maxSpeed;
                        }
                        if (target.position.Y < NPC.position.Y - 400 && NPC.velocity.Y > -8 && NPC.timeLeft > 200)
                        {
                            NPC.velocity.Y = -8f;
                        }
                    }
                }
                else
                {
                    /*if (npc.velocity.Y == 0)
                    {
                        npc.velocity.Y -= 17.77f;
                    }*/
                    NPC.velocity.Y += 0.5f;
                }
            }
            #endregion
			
			if (NPC.ai[2] <= 0 && NPC.ai[3] < 300 && NPC.timeLeft > 200 && NPC.ai[0] < 4000) //Not using 10000 needles or stomp or running away
			{
                float runSpeed = 5.6f;
                if (NPC.life <= NPC.lifeMax * 0.1f)
                    runSpeed = 2.5f;
                else if (Main.getGoodWorld)
                    runSpeed = 8f;
                if (target.Center.X < NPC.position.X && NPC.velocity.X > -runSpeed)
				{
					NPC.velocity.X -= 0.4f;
                    NPC.FaceTarget();
				}
				else if(target.Center.X > NPC.position.X + NPC.width && NPC.velocity.X < runSpeed) 
				{
					NPC.velocity.X += 0.4f;
                    NPC.FaceTarget();
                }
                else
                {
                    NPC.velocity.X *= 0.9f;
                }
				if(NPC.velocity.X < -runSpeed)
				{
					NPC.velocity.X = -runSpeed;
				}
				if(NPC.velocity.X > runSpeed)
				{
					NPC.velocity.X = runSpeed;
				}
				//npc.position.X += npc.velocity.X;
			}
			if (NPC.ai[2] < 15 && (NPC.ai[3] < 300 || NPC.ai[1] % 400 >= 380))
			{
				NPC.ai[1]++;
			}
			
			if (!desert)
			{
                float scale = Math.Min(NPC.localAI[0] / 100f, 3f);
                Dust.NewDustDirect(target.position, target.width, target.height, 41, 0, 0, 0, Color.Green, scale);

                NPC.localAI[0]++;
			}
			else
			{
                NPC.localAI[0] = 0;
			}
            if (NPC.localAI[1] < 30)
            {
                if (Math.Abs(NPC.Center.X - target.Center.X) < Main.screenWidth / 2 && Math.Abs(NPC.Center.Y - target.Center.Y) < Main.screenHeight / 2)
                {
                    NPC.localAI[1] = 30;
                }
                if (NPC.localAI[1] == 30)
                {
                    SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/JumboCactuarAppear"), NPC.Center);
                }
            }
            if (NPC.localAI[3] < 100)
            {
                if (NPC.ai[3] < 300 && NPC.life <= NPC.lifeMax * 0.2f)
                {
                    NPC.ai[3] = 0;
                    if (NPC.ai[2] <= 0)
                    {
                        NPC.ai[1] = 0;
                        if (NPC.ai[0] < 4000)
                        {
                            NPC.ai[0] = 4000;
                        }
                    }
                }
            }
            else if (NPC.life <= NPC.lifeMax * 0.1f)
            {
                NPC.localAI[0] = 0;
                if (NPC.ai[3] < 300)
                {
                    NPC.ai[3] = 0;
                    if (NPC.ai[2] <= 0)
                    {
                        if (NPC.ai[0] < 4000)
                        {
                                if (NPC.ai[1] >= 1500)
                                    NPC.ai[1] = 0;
                                if (NPC.localAI[1] < 600)
                                {
                                    NPC.localAI[1] = 600;
                                    if (Main.netMode != NetmodeID.Server)
                                        Main.NewText("Jumbo Cactuar is hesitating...", Color.DarkOliveGreen);
                                }
                                if (NPC.timeLeft > 200 && NPC.life <= NPC.lifeMax * 0.05f)
                                {
                                    NPC.timeLeft = 200;
                                    NPC.direction *= -1;
                                }
                        }
                    }
                }
            }

            if (NPC.localAI[0] > 300) //Enraged
            {
                NPC.rotation += 10 * NPC.direction;
                //NPC.damage = 300;
                //defense = (int)(NPC.localAI[0] / 2);
                NPC.velocity = NPC.DirectionTo(target.Center) * (NPC.localAI[0] / 15f);
                //npc.noTileCollide = true;
                NPC.ai[3] = 0;
                if (NPC.localAI[1] < 300)
                {
                    if (Main.netMode != NetmodeID.Server)
                        Main.NewText("Jumbo Cactuar enrages as you leave the desert!", Color.DarkOliveGreen);

                    SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/JumboCactuarAppear").WithPitchOffset(-0.3f), NPC.Center);
                    NPC.localAI[1] = 300;
                }
            }
            else
            {
                NPC.rotation = 0;
                //NPC.damage = 150;
                //NPC.defense = 30;

                // NPC.ai[2] is above 0 when using 10000 needles
                if (NPC.ai[2] <= 0 && NPC.ai[0] < 4000)
                {
                    if (NPC.ai[3] < 300)
                    {
                        NPC.ai[3]++;

                        if (Math.Abs(target.Center.X - NPC.Center.X) > 2500)
                        {
                            NPC.ai[3] = 300;
                        }
                        else if (Math.Abs(target.Center.X - NPC.Center.X) > 1000)
                        {
                            NPC.ai[3]++;
                        }
                        if (NPC.ai[3] == 300 && Main.rand.NextBool(3) && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            NPC.ai[3] = 1300;
                            if (NPC.velocity.Y < 0)
                            {
                                NPC.velocity.Y = 0;
                            }
                            NPC.netUpdate = true;
                        }
                    }
                }
                else
                {
                    NPC.ai[3] = 0;
                }
                if (NPC.ai[1] % 400 < 380) // Standard needles check
                {
                    if (NPC.ai[3] >= 1300)
                    {
                        #region Ker Plunk
                        if (NPC.velocity.Y == 0)
                        {
                            NPC.velocity.X = 0;
                        }
                        NPC.ai[3]++;
                        if (NPC.ai[3] > 1330)
                            NPC.localAI[0]--;
                        float rot = 0;
                        if (NPC.ai[3] < 1330) //Tilt back
                        {
                            if (NPC.ai[3] <= 1303 && (NPC.velocity.Y != 0 || !Collision.SolidCollision(NPC.position, NPC.width, NPC.height)))
                            {
                                NPC.ai[3] = 1300;
                                NPC.velocity.Y += 0.5f;
                            }
                            if ((target.position.Y + target.velocity.Y < NPC.position.Y - 196 || Math.Abs(target.Center.X - NPC.Center.X) > 1600) && (NPC.velocity.Y == 0 || Collision.SolidCollision(NPC.position, NPC.width, NPC.height)))
                            {
                                NPC.velocity.Y = -(float)Math.Sqrt(2 * 0.5f * Math.Abs(target.position.Y - (NPC.position.Y + NPC.height) - 300));
                                NPC.ai[3] = 1330;
                            }
                            
                            rot = (NPC.ai[3] - 1300) * -0.2f;
                            NPC.position.X -= 1.27f * NPC.direction;
                        }
                        else if (NPC.ai[3] < 1392) //Tilt to neutral
                        {
                            if (NPC.ai[3] < 1350)
                            {
                                rot = -6 + ((NPC.ai[3] - 1330) * -0.15f);
                                NPC.position.X -= 0.895f * NPC.direction;
                            }
                            else if (NPC.ai[3] < 1360)
                            {
                                rot = -9;
                            }
                            else if (NPC.ai[3] < 1380)
                            {
                                rot = -9 + ((NPC.ai[3] - 1360) * 0.2f);
                                NPC.position.X += 1.27f * NPC.direction;
                            }
                            else
                            {
                                rot = -5 + ((NPC.ai[3] - 1380) * 0.4f);
                                NPC.position.X += 1.53f * NPC.direction;
                            }

                            if (NPC.velocity.Y != 0 || !Collision.SolidCollision(NPC.position, NPC.width, NPC.height) || Math.Abs(target.Center.X - NPC.Center.X) > 1600)
                            {
                                float maxSpeed = 35f;
                                float accel = 2.4f;
                                if (NPC.velocity.Y > 0)
                                {
                                    accel = 0.3f;
                                }
                                if (Math.Abs(target.Center.X - NPC.Center.X) > 1600)
                                {
                                    NPC.ai[3] = 1330;
                                    maxSpeed *= 1.5f;
                                    accel = 4.8f;
                                    if (NPC.velocity.Y == 0 || Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                                    {
                                        NPC.velocity.Y = -(float)Math.Sqrt(2 * 0.5f * Math.Abs(target.position.Y - (NPC.position.Y + NPC.height) - Math.Abs(target.Center.X - NPC.Center.X) / 3));
                                    }
                                }
                                if (target.Center.X < NPC.position.X - FALL_HITBOX.X && NPC.velocity.X > -maxSpeed && NPC.velocity.X <= 0.5)
                                {
                                    NPC.velocity.X -= accel;
                                    if (NPC.velocity.X < 0)
                                    {
                                        NPC.direction = -1;
                                    }
                                }
                                else if (target.Center.X > NPC.position.X + NPC.width + FALL_HITBOX.X && NPC.velocity.X < maxSpeed && NPC.velocity.X >= -0.5)
                                {
                                    NPC.velocity.X += accel;
                                    if (NPC.velocity.X > 0) 
                                    {
                                        NPC.direction = 1;
                                    }
                                }
                                else
                                {
                                    NPC.velocity.X *= 0.7f;
                                }
                                if (NPC.velocity.X < -maxSpeed)
                                {
                                    NPC.velocity.X = -maxSpeed;
                                }
                                if (NPC.velocity.X > maxSpeed)
                                {
                                    NPC.velocity.X = maxSpeed;
                                }
                            }
                        }
                        else if (NPC.ai[3] < 1420) //Tilt forward
                        {
                            if (NPC.ai[3] < 1410)
                                NPC.ai[3] = 1410;
                            rot = 0f + (NPC.ai[3] - 1410) * 0.6f;
                            NPC.position.X += 4f * NPC.direction;
                        }
                        else if (NPC.ai[3] < 1430) //Tilt forward
                        {
                            rot = 6 + (NPC.ai[3] - 1420) * 1.2f;
                            NPC.position.X += 8f * NPC.direction;
                            if (NPC.ai[3] == 1420)
                            {
                                if (Main.rand.NextBool(20))
                                    SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/slidewhistle").WithPitchOffset(-0.2f), NPC.Center);
                                else
                                    SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/dd2_monk_staff_swing_1").WithPitchOffset(-1f), NPC.Center); //214

                            }
                        }
                        else if (NPC.ai[3] < 1438) //Ker
                        {
                            rot = 18 + (NPC.ai[3] - 1430) * 5.25f;
                            NPC.position.X += 10f * NPC.direction;
                        }
                        else if (NPC.ai[3] < 1490) //Plunk
                        {
                            rot = 60;
                            if (NPC.ai[3] <= 1439 && (NPC.velocity.Y != 0 || !Collision.SolidCollision(NPC.position, NPC.width, NPC.height)))
                            {
                                NPC.velocity.Y += 2f;
                                maxFallSpeed = 40;
                                if (!(Collision.SolidCollision(NPC.position + new Vector2(0, NPC.height - 32), NPC.width, 32) && target.position.Y - 8 < NPC.position.Y + NPC.height))
                                {
                                    NPC.ai[3] = 1438;
                                }
                            }
                            if (NPC.ai[3] == 1439)
                            {
                                SoundEngine.PlaySound(SoundID.Item88.WithPitchOffset(-0.5f), NPC.Center);
                                for (int i = 0; i < 100; i++)
                                {
                                    int dustType = 32;
                                    Vector2 dustPos = new Vector2(NPC.position.X, NPC.position.Y + NPC.height - 16);
                                    int dustIndex = Dust.NewDust(dustPos, NPC.width, 32, dustType, 0, 0, 0, default, 2f);
                                    Dust dust = Main.dust[dustIndex];
                                    dust.velocity.X = dust.velocity.X + Main.rand.Next(-20, 20);
                                    dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-20, -5);
                                }
                                if (Main.netMode != NetmodeID.MultiplayerClient)
                                {
                                    int damage = 60;

                                    Vector2 pos = new Vector2(NPC.Center.X + ((NPC.width / 2 + FALL_HITBOX.X) * NPC.direction), NPC.position.Y + NPC.height);
                                    Projectile.NewProjectile(NPC.GetSource_FromAI(), pos.X, pos.Y, 12f * NPC.direction, 0, ModContent.ProjectileType<JumboCactuarWave>(), damage, 10, -1, 80, 2f, Main.expertMode ? 10f : 6f);

                                    Vector2 pos2 = new Vector2(NPC.Center.X - ((NPC.width / 2 + FALL_HITBOX.X) * NPC.direction), NPC.position.Y + NPC.height);
                                    Projectile.NewProjectile(NPC.GetSource_FromAI(), pos2.X, pos2.Y, 12f * -NPC.direction, 0, ModContent.ProjectileType<JumboCactuarWave>(), damage - 10, 10, -1, 80, 1.5f, Main.expertMode ? 7f : 5f);
                                }
                            }
                        }
                        else
                        {
                            rot = 60 - (NPC.ai[3] - 1490) * 1.5f;
                            NPC.velocity.X = -4.75f * NPC.direction;
                            if (NPC.ai[3] > 1530)
                            {
                                NPC.ai[3] = 0;
                            }
                        }

                        if (rot < 0)
                        {
                            NPC.gfxOffY = -rot * 1.33f;
                        }
                        else if (rot < 12)
                        {
                            NPC.gfxOffY = -rot * 0.5f;
                        }
                        else if (rot <= 18)
                        {
                            NPC.gfxOffY = -6 + (rot - 12);
                        }
                        else
                        {
                            NPC.gfxOffY = (rot - 18) * 6.19f;
                        }
                        NPC.rotation = MathHelper.ToRadians(rot) * NPC.direction;
                        #endregion
                    }
                    else if (NPC.ai[3] >= 300)
                    {
                        #region Stomp
                        NPC.ai[3]++;
                        float rot = 0;
                        if (NPC.ai[3] < 360)
                        {
                            NPC.velocity.X = 0;
                            rot = NPC.ai[3] - 300;
                        }
                        else if (NPC.ai[3] < 500)
                        {
                            if (target.position.Y + target.velocity.Y - 40 < NPC.Center.Y && (NPC.velocity.Y >= 0 || Collision.SolidCollision(NPC.position, NPC.width, NPC.height)))
                            {
                                NPC.velocity.Y = -(float)Math.Sqrt(2 * 0.5f * Math.Abs(target.position.Y - (NPC.position.Y + NPC.height)));
                            }
                            rot = 60;
                            float maxSpeed = 26f;
                            float accel = 1.1f;
                            if (target.Center.X < NPC.position.X && NPC.velocity.X > -maxSpeed && NPC.velocity.X <= 0.5)
                            {
                                NPC.velocity.X -= accel;
                            }
                            else if (target.Center.X > NPC.position.X + NPC.width && NPC.velocity.X < maxSpeed && NPC.velocity.X >= -0.5)
                            {
                                NPC.velocity.X += accel;
                            }
                            else
                            {
                                NPC.velocity.X *= 0.9f;
                            }
                            if (NPC.velocity.X < -maxSpeed)
                            {
                                NPC.velocity.X = -maxSpeed;
                            }
                            if (NPC.velocity.X > maxSpeed)
                            {
                                NPC.velocity.X = maxSpeed;
                            }
                            if (Math.Sign(NPC.velocity.X) * (target.Center.X - NPC.Center.X) > 1600)
                            {
                                NPC.ai[3] = 360;
                            }

                            if (Math.Abs(target.Center.X - NPC.Center.X) < NPC.width / 2 + STOMP_HITBOX.X + Math.Abs(NPC.velocity.X) && target.Center.Y < NPC.position.Y + NPC.height && target.Center.Y > NPC.position.Y)
                            {
                                NPC.ai[3] = 500;
                            }
                        }
                        else if (NPC.ai[3] < 510)
                        {
                            NPC.velocity.X *= 0.5f;
                            if (NPC.ai[3] < 507)
                            {
                                rot = 60;
                            }
                            else
                            {
                                rot = 60 - ((NPC.ai[3] - 507) * 10);
                                if (NPC.velocity.Y < 10)
                                {
                                    NPC.velocity.Y = 10;
                                }
                            }
                            if (NPC.ai[3] == 507)
                            {
                                SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/dd2_monk_staff_swing_0").WithPitchOffset(-0.2f), NPC.Center); // 216
                            }
                        }
                        else
                        {
                            NPC.velocity.X = 0f;
                            if (NPC.ai[3] == 510)
                            {
                                if (Collision.SolidCollision(NPC.position + new Vector2(0, NPC.height), NPC.width, 160))
                                {
                                    if (Main.expertMode && Main.netMode != NetmodeID.MultiplayerClient)
                                    {
                                        int damage = 50;
                                        Vector2 pos = new Vector2(NPC.Center.X + (NPC.width / 2 + STOMP_HITBOX.X) * NPC.direction, NPC.position.Y + NPC.height);
                                        Projectile.NewProjectile(NPC.GetSource_FromAI(), pos.X, pos.Y, 12f * NPC.direction, 0, ModContent.ProjectileType<JumboCactuarWave>(), damage, 10, -1, 90, 1f, 1f);
                                    }
                                    for (int i = 0; i < 100; i++)
                                    {
                                        int dustType = 32;
                                        Vector2 pos = new Vector2(NPC.position.X + (NPC.direction < 0 ? -STOMP_HITBOX.X : NPC.width), NPC.position.Y + NPC.height - 16);
                                        int dustIndex = Dust.NewDust(pos, STOMP_HITBOX.X, 32, dustType);
                                        Dust dust = Main.dust[dustIndex];
                                        dust.velocity.X = 0;
                                        dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-12, -1);
                                    }

                                    SoundEngine.PlaySound(SoundID.Item70, NPC.Center);
                                }

                            }

                            if (NPC.ai[3] < 560)
                            {
                                rot = 30;
                            }
                            else if (NPC.ai[3] < 575)
                            {
                                rot = 30 + (NPC.ai[3] - 560);
                                NPC.velocity.X = NPC.direction * -3;
                            }
                            else if (NPC.ai[3] < 590)
                            {
                                rot = 45;
                                NPC.velocity.X = NPC.direction * -5;
                            }
                            else
                            {
                                rot = 45 - (NPC.ai[3] - 590) * 2.25f;
                                NPC.velocity.X *= 0.95f;
                            }
                            if (NPC.ai[3] >= 610)
                            {
                                NPC.ai[3] = 0;
                            }
                        }
                        if (rot <= 30)
                        {
                            NPC.gfxOffY = rot * 2.33f;
                        }
                        else if (rot <= 60)
                        {
                            NPC.gfxOffY = 36 - (rot - 30) * 0.8f;
                        }
                        NPC.rotation = MathHelper.ToRadians(rot) * -NPC.direction;
                        #endregion
                    }

                    //Main.NewText(NPC.velocity.X);
                }

                /* Old jump code
                if (NPC.ai[2] <= 0 && Vector2.Distance(target.Center, NPC.Center) > 500)
				{
					NPC.ai[3] += 1+Main.rand.Next(3);
					if (Vector2.Distance(target.Center, NPC.Center) > 2000)
					{
						NPC.ai[3] = NPC.ai[3] < 600 ? 600 : 0;
					}
					else if (Vector2.Distance(target.Center, NPC.Center) > 1000)
					{
						NPC.ai[3] += NPC.ai[3] < 600 ? 2 : 0;
					}
				}
				else
				{
					NPC.ai[3] = 0;
				}
				if (NPC.ai[3] >= 600 && NPC.ai[3] < 900)
				{
					Vector2 jumpos = target.Center + new Vector2(0f, -900f);
					NPC.velocity = NPC.DirectionTo(jumpos) * 30;
					//npc.noTileCollide = true;
					if (Math.Abs(NPC.Center.X - jumpos.X) < 150 && Math.Abs(NPC.Center.Y - jumpos.Y) < 200)
					{
						NPC.ai[3] = 900;
					}
				}
				if (NPC.ai[3] >= 1000 && NPC.ai[3] < 1100)
				{
					NPC.velocity = NPC.DirectionTo(target.Center + new Vector2(target.velocity.X * 20, 0f)) * 20;
					//npc.noTileCollide = false;
				}
                */
            }

            if (NPC.ai[0] >= 4000)
            {
                NPC.localAI[0] = 0;
                NPC.ai[3] = 0;
                NPC.ai[2] = 0;
                NPC.ai[1] = 0;

                #region Meteor Jump
                NPC.ai[0]++;

                if (NPC.ai[0] == 4030)
                    SoundEngine.PlaySound(SoundID.Item129.WithPitchOffset(0.7f), NPC.Center);
                if (NPC.ai[0] == 4050)
                    SoundEngine.PlaySound(SoundID.Item129.WithPitchOffset(0.8f), NPC.Center);
                if (NPC.ai[0] == 4070)
                    SoundEngine.PlaySound(SoundID.Item129.WithPitchOffset(0.9f), NPC.Center);

                if (NPC.ai[0] == 4100)
                {
                    //NPC.velocity.Y = -(float)Math.Sqrt(2 * 0.5f * Math.Abs(666 - (NPC.position.Y + NPC.height)));
                    SoundEngine.PlaySound(SoundID.Item150.WithPitchOffset(-0.85f), NPC.Center);
                }
                if (NPC.ai[0] > 4100)
                {
                    bool doMovement = false;

                    if (NPC.position.Y + NPC.height > Math.Min(0, target.position.Y - 19000) && NPC.ai[0] < 4200)
                    {
                        NPC.velocity.Y = -80;
                        if (NPC.ai[0] > 4110)
                            NPC.ai[0] = 4110;
                    }
                    else
                    {
                        float rot = 60;
                        maxFallSpeed = 80f;
                        if (NPC.ai[0] < 4200)
                            NPC.ai[0] = 4200;

                        if (NPC.ai[0] < 4230)
                        {
                            rot = (NPC.ai[0] - 4200) * 2;
                        }
                        else if (NPC.ai[0] < 4302)
                        {
                            if (target.position.Y - NPC.position.Y + NPC.height > 23000)
                            {
                                NPC.velocity.Y += 1f;
                                NPC.ai[0] = 4300;
                                if (Math.Abs(target.Center.X - NPC.Center.X) > 150)
                                    doMovement = true;
                                else
                                    NPC.velocity.X *= 0.7f;
                            }
                            if (Math.Abs(target.Center.X - NPC.Center.X) > 4000 && NPC.velocity.Y > 0)
                            {
                                NPC.velocity.Y = 0;
                                doMovement = true;
                                NPC.ai[0] = 4300;
                            }
                        }
                        /*
                        Main.NewText(target.position.Y - NPC.position.Y + NPC.height, Color.MistyRose);
                        Main.NewText(NPC.velocity.Y, Color.Goldenrod);
                        */
                        if (NPC.ai[0] == 4302)
                        {
                            SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/airplane_fall"), new Vector2(target.Center.X, target.position.Y - 600));
                        }

                        if (NPC.ai[0] > 4302 && NPC.ai[0] <= 4400 && (NPC.velocity.Y != 0 || !Collision.SolidCollision(NPC.position, NPC.width, NPC.height)))
                        {
                            if (NPC.position.Y + NPC.height < target.position.Y - 4000 && Math.Abs(target.Center.X - NPC.Center.X) > 150)
                                doMovement = true;
                            else
                                NPC.velocity.X *= 0.7f;
                            
                            if (!(Collision.SolidCollision(NPC.position + new Vector2(0, NPC.height - 64), NPC.width, 64) && target.Center.Y < NPC.position.Y + NPC.height))
                                NPC.ai[0] = 4399;
                        }
                        if (NPC.ai[0] == 4400)
                        {
                            NPC.velocity.Y = 0;
                            NPC.ai[0] = 4500;
                            SoundEngine.PlaySound(SoundID.Item74.WithPitchOffset(-1f), NPC.Center);
                            for (int i = 0; i < 200; i++)
                            {
                                int dustType = 32;
                                Vector2 dustPos = new Vector2(NPC.position.X - FALL_HITBOX.X, NPC.position.Y + NPC.height - FALL_HITBOX.Y);
                                int dustIndex = Dust.NewDust(dustPos, NPC.width + FALL_HITBOX.X, FALL_HITBOX.Y, dustType, 0, 0, 0, default, 2f + Main.rand.NextFloat());
                                Dust dust = Main.dust[dustIndex];
                                dust.velocity.X = dust.velocity.X + Main.rand.Next(-30, 30);
                                dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-20, -5);
                            }
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Rectangle rect = new Rectangle((int)NPC.position.X - FALL_HITBOX.X, (int)NPC.position.Y + NPC.height - FALL_HITBOX.Y, NPC.width + FALL_HITBOX.X, FALL_HITBOX.Y);
                                var source = NPC.GetSource_FromAI();
                                float numberProjectiles = 9;
                                float rotation = MathHelper.ToRadians(60);
                                int damage = 75;
                                int type = ModContent.ProjectileType<JumboCactuarFireball>();
                                float speed = 12;
                                for (int i = 0; i < numberProjectiles; i++)
                                {
                                    Vector2 perturbedSpeed = new Vector2(0, -speed).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                                    Projectile.NewProjectile(source, rect.X + rect.Width * (i / (numberProjectiles - 1)), rect.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 5);
                                }
                                speed = 18;
                                numberProjectiles = 6;
                                rotation = MathHelper.ToRadians(20);
                                for (int i = 0; i < numberProjectiles; i++)
                                {
                                    Vector2 perturbedSpeed = new Vector2(-speed, -speed).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                                    Projectile.NewProjectile(source, rect.X + rect.Width * (i / (numberProjectiles - 1)), rect.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 5);
                                }
                                for (int i = 0; i < numberProjectiles; i++)
                                {
                                    Vector2 perturbedSpeed = new Vector2(speed, -speed).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                                    Projectile.NewProjectile(source, rect.X + rect.Width * (i / (numberProjectiles - 1)), rect.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 5);
                                }

                                numberProjectiles = 13;
                                rotation = MathHelper.ToRadians(65);
                                damage = 65;
                                type = ModContent.ProjectileType<SandClump>();
                                speed = 42;
                                for (int i = 0; i < numberProjectiles; i++)
                                {
                                    Vector2 perturbedSpeed = new Vector2(0, -speed).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                                    Projectile.NewProjectile(source, rect.X + rect.Width * (i / (numberProjectiles - 1)), rect.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 5);
                                }

                                numberProjectiles = 16;
                                rotation = MathHelper.ToRadians(70);
                                damage = 52;
                                type = ModContent.ProjectileType<SandBall>();
                                speed = 28;
                                for (int i = 0; i < numberProjectiles; i++)
                                {
                                    Vector2 perturbedSpeed = new Vector2(0, -speed).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                                    Projectile.NewProjectile(source, rect.X + rect.Width * (i / (numberProjectiles - 1)), rect.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 5, -1, 1);
                                }

                                speed = 16;
                                numberProjectiles = 17;
                                rotation = MathHelper.ToRadians(80);
                                for (int i = 0; i < numberProjectiles; i++)
                                {
                                    Vector2 perturbedSpeed = new Vector2(0, -speed).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                                    Projectile.NewProjectile(source, rect.X + rect.Width * (i / (numberProjectiles - 1)), rect.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 5);
                                }

                                damage = 55;
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), rect.X, rect.Y, -12f, 0, ModContent.ProjectileType<JumboCactuarWave>(), damage, 10, -1, 150, 2f, 1f);
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), rect.X + rect.Width, rect.Y, 12f, 0, ModContent.ProjectileType<JumboCactuarWave>(), damage, 10, -1, 150, 2f, 1f);

                            }
                        }
                        if (NPC.ai[0] > 4690)
                        {
                            rot = 60 - (NPC.ai[0] - 4690) * 1.5f;
                            NPC.velocity.X = -4.75f * NPC.direction;
                            if (NPC.ai[0] > 4730)
                            {
                                NPC.ai[0] = 0;
                                NPC.localAI[3] = 100;
                            }
                        }
                        else if (NPC.ai[0] >= 4500)
                        {
                            NPC.velocity.X = 0;
                        }

                        if (rot < 0)
                        {
                            NPC.gfxOffY = -rot * 1.33f;
                        }
                        else if (rot < 12)
                        {
                            NPC.gfxOffY = -rot * 0.5f;
                        }
                        else if (rot <= 18)
                        {
                            NPC.gfxOffY = -6 + (rot - 12);
                        }
                        else
                        {
                            NPC.gfxOffY = (rot - 18) * 6.19f;
                        }
                        NPC.rotation = MathHelper.ToRadians(rot) * NPC.direction;
                    }

                    if (doMovement)
                    {
                        float maxSpeed = 30f;
                        float accel = 1.8f;
                        if (NPC.position.Y + NPC.height < target.position.Y - 12000)
                        {
                            maxSpeed *= 2;
                            accel *= 2;
                        }
                        if (NPC.position.Y + NPC.height > target.position.Y - 8000 && !Main.expertMode)
                        {
                            maxSpeed *= 0.75f;
                        }
                        if (NPC.position.Y + NPC.height > target.position.Y - 6000)
                        {
                            maxSpeed *= 0.5f;
                        }
                        if (Math.Abs(target.Center.X - NPC.Center.X) > 1600)
                        {
                            maxSpeed += (Math.Abs(target.Center.X - NPC.Center.X) - 1600) / 200f;
                            accel += (Math.Abs(target.Center.X - NPC.Center.X) - 1600) / 800f;
                        }
                        if (target.Center.X < NPC.Center.X && NPC.velocity.X > -maxSpeed)
                        {
                            NPC.velocity.X -= accel;
                            if (NPC.velocity.X < 0)
                            {
                                NPC.direction = -1;
                            }
                        }
                        else if (target.Center.X > NPC.Center.X && NPC.velocity.X < maxSpeed)
                        {
                            NPC.velocity.X += accel;
                            if (NPC.velocity.X > 0)
                            {
                                NPC.direction = 1;
                            }
                        }
                        else
                        {
                            NPC.velocity.X *= 0.7f;
                        }
                        if (NPC.velocity.X < -maxSpeed)
                        {
                            NPC.velocity.X = -maxSpeed;
                        }
                        if (NPC.velocity.X > maxSpeed)
                        {
                            NPC.velocity.X = maxSpeed;
                        }
                    }
                }
                else
                {
                    NPC.velocity.X = 0;
                    if (NPC.velocity.Y < 0)
                    {
                        NPC.velocity.Y = 0;
                    }
                }
                #endregion
            }
            else if (Main.expertMode)
            {
                #region Minion Summons
                NPC.ai[0]++;
                if (NPC.ai[0] >= 700)
                {
                    if (Main.rand.NextBool(4) && NPC.localAI[2] <= 0)
                    {
                        NPC.localAI[2]++;
                    }
                    if (NPC.localAI[2] > 0) //Cactuars
                    {
                        NPC.localAI[2]++;
                        if (NPC.localAI[2] % 15 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            int spwn = 800 + Main.rand.Next(200);
                            if (corrupt)
                            {
                                int yOff = Main.rand.Next(100);
                                if (!Collision.SolidCollision(new Vector2(target.Center.X - spwn, target.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                                {
                                    NPC.NewNPC(sauce, (int)target.Center.X - spwn, (int)target.position.Y - yOff, ModContent.NPCType<CorruptCactuar>(), 0, 0f, 0f, 0, 0, target.whoAmI);
                                }
                                if (!Collision.SolidCollision(new Vector2(target.Center.X + spwn, target.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                                {
                                    NPC.NewNPC(sauce, (int)target.Center.X + spwn, (int)target.position.Y - yOff, ModContent.NPCType<CorruptCactuar>(), 0, 0f, 0f, 0, 0, target.whoAmI);
                                }
                            }
                            else if (crimson)
                            {
                                int yOff = Main.rand.Next(100);
                                if (!Collision.SolidCollision(new Vector2(target.Center.X - spwn, target.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                                {
                                    NPC.NewNPC(sauce, (int)target.Center.X - spwn, (int)target.position.Y - yOff, ModContent.NPCType<CrimsonCactuar>(), 0, 0f, 0f, 0, 0, target.whoAmI);
                                }
                                if (!Collision.SolidCollision(new Vector2(target.Center.X + spwn, target.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                                {
                                    NPC.NewNPC(sauce, (int)target.Center.X + spwn, (int)target.position.Y - yOff, ModContent.NPCType<CrimsonCactuar>(), 0, 0f, 0f, 0, 0, target.whoAmI);
                                }
                            }
                            else if (hallow)
                            {
                                int yOff = Main.rand.Next(100);
                                if (!Collision.SolidCollision(new Vector2(target.Center.X - spwn, target.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                                {
                                    NPC.NewNPC(sauce, (int)target.Center.X - spwn, (int)target.position.Y - yOff, ModContent.NPCType<HallowedCactuar>(), 0, 0f, 0f, 0, 0, target.whoAmI);
                                }
                                if (!Collision.SolidCollision(new Vector2(target.Center.X + spwn, target.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                                {
                                    NPC.NewNPC(sauce, (int)target.Center.X + spwn, (int)target.position.Y - yOff, ModContent.NPCType<HallowedCactuar>(), 0, 0f, 0f, 0, 0, target.whoAmI);
                                }
                            }
                            else
                            {
                                int yOff = Main.rand.Next(100);
                                if (!Collision.SolidCollision(new Vector2(target.Center.X - spwn, target.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                                {
                                    NPC.NewNPC(sauce, (int)target.Center.X - spwn, (int)target.position.Y - yOff, ModContent.NPCType<Cactuar>(), 0, 0f, 0f, 0, 0, target.whoAmI);
                                }
                                if (!Collision.SolidCollision(new Vector2(target.Center.X + spwn, target.position.Y - yOff) - new Vector2(9, 20), 18, 40))
                                {
                                    NPC.NewNPC(sauce, (int)target.Center.X + spwn, (int)target.position.Y - yOff, ModContent.NPCType<Cactuar>(), 0, 0f, 0f, 0, 0, target.whoAmI);
                                }
                            }
                        }
                        if (NPC.localAI[2] > 180)
                        {
                            NPC.localAI[2] = 0;
                            NPC.ai[0] = 0;
                        }
                    }
                    else // Giant Needles
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            float rot = (MathHelper.PiOver2 * Main.rand.NextFloat()) + (MathHelper.PiOver2 * Main.rand.NextFloat());
                            Vector2 offL = (rot - MathHelper.Pi / 6).ToRotationVector2() * 350;
                            Vector2 off = rot.ToRotationVector2() * 350;
                            Vector2 offR = (rot + MathHelper.Pi / 6).ToRotationVector2() * 350;
                            if (NPC.direction == -1)
                            {
                                NPC.NewNPC(sauce, (int)NPC.position.X - 40, (int)NPC.position.Y - 70, ModContent.NPCType<GiantNeedle>(), 0, 0f, 0f, offL.X - Main.rand.Next(70), offL.Y - Main.rand.Next(-50, 50), target.whoAmI);
                                NPC.NewNPC(sauce, (int)NPC.position.X, (int)NPC.position.Y - 100, ModContent.NPCType<GiantNeedle>(), 0, 0f, 5f, off.X + Main.rand.Next(-30, 30), off.Y + Main.rand.Next(-50, 50), target.whoAmI);
                                NPC.NewNPC(sauce, (int)NPC.position.X + 40, (int)NPC.position.Y - 110, ModContent.NPCType<GiantNeedle>(), 0, 0f, 10f, offR.X + Main.rand.Next(70), offR.Y + Main.rand.Next(-50, 70), target.whoAmI);
                            }
                            else
                            {
                                NPC.NewNPC(sauce, (int)NPC.position.X + 160, (int)NPC.position.Y - 110, ModContent.NPCType<GiantNeedle>(), 0, 0f, 0f, offL.X - Main.rand.Next(70), offL.Y + Main.rand.Next(-50, 50), target.whoAmI);
                                NPC.NewNPC(sauce, (int)NPC.position.X + 200, (int)NPC.position.Y - 100, ModContent.NPCType<GiantNeedle>(), 0, 0f, 5f, off.X + Main.rand.Next(-30, 30), off.Y + Main.rand.Next(-50, 50), target.whoAmI);
                                NPC.NewNPC(sauce, (int)NPC.position.X + 240, (int)NPC.position.Y - 70, ModContent.NPCType<GiantNeedle>(), 0, 0f, 10f, offR.X + Main.rand.Next(70), offR.Y + Main.rand.Next(-50, 50), target.whoAmI);
                            }
                        }
                        NPC.ai[0] = 0;
                    }
                }
                #endregion
            }

            #region Needles    
            if (NPC.ai[1] < 1500 && NPC.ai[0] < 4000)
			{
				if (NPC.ai[2] >= 15)
				{
					NPC.ai[1] = 0;
				}
				NPC.ai[2] = 0;
				if (NPC.ai[1] % 400 >= 380 && NPC.ai[3] < 300)
				{
					float Speed = 8f;
                    if (Main.expertMode && NPC.Distance(target.Center) > 1200)
                    {
                        Speed += (NPC.Distance(target.Center) - 1200) / 300f;
                    }
					Vector2 vector8 = new Vector2(NPC.Center.X + (Main.rand.Next(-15, 15) * 15), NPC.Center.Y + (Main.rand.Next(-20, 15) * 15));
					int damage = 10;
					int type = ModContent.ProjectileType<CactusNeedle>();
					SoundEngine.PlaySound(SoundID.Item1, NPC.position);
					float rotation = (float)Math.Atan2(vector8.Y - target.Center.Y, vector8.X - target.Center.X);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(sauce, vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);
                    }
				}
			}
            #endregion

            #region 10000 Needles
            if (NPC.ai[1] >= 1500 && NPC.ai[1] % 17 == 0)
			{
				NPC.ai[2]++;
			}
			if (NPC.ai[2] > 0)
			{
				int b = 255 - (int)(NPC.ai[1] - 1500);
				NPC.color = new Color(255, 255, b);
				NPC.defense = 30 + (int)(NPC.ai[1] - 1500);
				NPC.velocity.X = 0;
				if (NPC.ai[2] < 15)
				{
					NPC.velocity.X = NPC.ai[1] % (15 - NPC.ai[2]) < (15 - NPC.ai[2])/2 ? 8 : -8;
				}
				else
				{
					NPC.velocity.X = NPC.ai[1] % 30 < 15 ? 8 : -8;
				}
			}
			if (NPC.ai[2] >= 15)
			{
				float Speed = 8f;
                if (Main.expertMode && NPC.Distance(target.Center) > 1500)
                {
                    Speed += (NPC.Distance(target.Center) - 1500) / 300f;
                }
                int damage = 10;
				int type = ModContent.ProjectileType<CactusNeedle>();
				SoundEngine.PlaySound(SoundID.Item1, NPC.position);
				float rotation = (float)Math.Atan2(NPC.Center.Y - target.Center.Y, NPC.Center.X - target.Center.X);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(sauce, NPC.Center.X + (Main.rand.Next(-20, 20) * 20), NPC.Center.Y + (Main.rand.Next(-20, 15) * 30), (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);
                }
                NPC.ai[1]--;
            }
            #endregion

            if (NPC.velocity.Y > maxFallSpeed)
            {
                NPC.velocity.Y = maxFallSpeed;
            }

            NPC.scale = 2;
            if (NPC.timeLeft <= 200)
            {
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
                NPC.localAI[0] = 0;
                float runSpeed = 8.75f;
                if (Main.getGoodWorld)
                    runSpeed = 11.5f;
                NPC.velocity.X += 0.6f * NPC.direction;
                if (NPC.velocity.X < -runSpeed)
                {
                    NPC.velocity.X = -runSpeed;
                }
                if (NPC.velocity.X > runSpeed)
                {
                    NPC.velocity.X = runSpeed;
                }
                if (!NPC.HasValidTarget)
                {
                    NPC.timeLeft--;
                }
                else
                {
                    if (Math.Abs(NPC.Center.X - target.Center.X) > 1300)
                    {
                        NPC.timeLeft--;
                    }
                    else if (NPC.life > NPC.lifeMax * 0.05f)
                    {
                        NPC.timeLeft = 750;
                    }
                }
                /*
                if (NPC.timeLeft < 150)
                {
                    NPC.velocity = new Vector2(0, -2f);
                    NPC.scale = 2 * (NPC.timeLeft / 150f);
                    NPC.rotation = NPC.timeLeft * 6;
                    NPC.direction = -1;
                }
                */
                if (NPC.timeLeft <= 0)
                {
                    NPC.active = false;
                    if (Main.netMode != NetmodeID.Server)
                        Main.NewText("Escaped...", Color.DarkOliveGreen);
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NPC.netSkip = -1;
                        NPC.life = 0;
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, NPC.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                    }
                }
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}

