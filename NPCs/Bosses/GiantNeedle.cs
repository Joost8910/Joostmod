using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace JoostMod.NPCs.Bosses
{
	public class GiantNeedle : ModNPC
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Giant Needle");
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        }
		public override void SetDefaults()
		{
			NPC.width = 26;
			NPC.height = 26;
			NPC.damage = 80;
			NPC.defense = 50;
			NPC.lifeMax = 1000;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 0f;
			NPC.knockBackResist = 0f;
			NPC.aiStyle = 0;
			NPC.noTileCollide = true;
			NPC.noGravity = true;
		}
		public override void HitEffect(NPC.HitInfo hit)
		{
            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GiantNeedle").Type);
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
			if (NPC.ai[1] < 105)
				return false;
            return base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
			modifiers.FinalDamage *= NPC.ai[0];
		}

        public override void AI()
		{
			if (NPC.ai[0] < 1)
				NPC.ai[0] += 0.004f;
			Player P = Main.player[NPC.target];
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
			{
				NPC.TargetClosest(true);
				if ((NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active))
				{
					NPC.velocity.Y += 1f;
					if (NPC.timeLeft > 10)
					{
						NPC.timeLeft = 10;
                    }
                    return;
                }
			}
			NPC.netUpdate = true;
			/*if(npc.Center.X < P.Center.X)
			{
				npc.velocity.X = 10f;
			}
			else
			{
				npc.velocity.X = -10f;
			}*/

			NPC.ai[1] += 1;

			if (NPC.ai[1] < 80)
			{
				NPC.rotation = NPC.ai[1] / 2;
				NPC.velocity = NPC.DirectionTo(P.Center + new Vector2(NPC.ai[2], NPC.ai[3])) * 20;
			}
			else if (NPC.ai[1] < 105)
			{
				NPC.velocity = Vector2.Zero;
				if (NPC.ai[1] < 85)
				{
                    Vector2 predictedPos = JoostFunctions.PredictPlayerPosition(NPC.Center, 50, P, 20);
                    //P.Center + (P.velocity * 26)
                    NPC.rotation = (predictedPos - NPC.Center).ToRotation() + MathHelper.PiOver2;
				}
				else if (NPC.ai[1] < 95)
				{
					NPC.localAI[0] += 0.15f;
				}
				else
				{
					NPC.localAI[0] -= 0.225f;
				}
            }
            else
            {
				NPC.velocity = (NPC.rotation - MathHelper.PiOver2).ToRotationVector2() * 50;
                //NPC.localAI[0] -= 0.25f;
            }
            if (NPC.ai[1] >= 130)
            {
                NPC.ai[1] = 0;
				NPC.ai[2] *= -1;
                NPC.ai[3] *= -1;
                NPC.localAI[0] = 0;
            }
            /*
			if (NPC.ai[1] >= 200)
			{
				NPC.ai[1] = 0;
			}
			if (NPC.ai[1] % 100 < 85 && NPC.ai[1] % 100 > 15)
			{
				NPC.rotation = NPC.ai[1] / 2;
			}
			else
			{
				NPC.rotation = (P.Center - NPC.Center).ToRotation() + (90 * 0.0174f);
			}
			if (NPC.ai[1] >= 100)
			{
				NPC.velocity = NPC.DirectionTo(P.Center + new Vector2(NPC.ai[2], NPC.ai[3])) * 20;
			}
			else
			{
				NPC.velocity = NPC.DirectionTo(P.Center + new Vector2(-NPC.ai[2], -NPC.ai[3])) * 20;
			}
			*/
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
			if (NPC.localAI[0] > 0)
			{
				Vector2 vector = NPC.Center - Main.screenPosition;
				float rot = NPC.rotation - MathHelper.PiOver2;
				int num = 50;
				int num2 = 30 * num;
				Texture2D value = TextureAssets.Extra[178].Value;
				Vector2 vector2 = value.Frame(1, 1, 0, 0, 0, 0).Size() * new Vector2(0f, 0.5f);
				Vector2 vector3 = new Vector2((float)(num2 / value.Width), 2f);
				Vector2 vector4 = new Vector2((float)(num2 / value.Width) * 0.5f, 2f);
				Color color = Color.White * Utils.GetLerpValue(0, 1, NPC.localAI[0]);
				Main.spriteBatch.Draw(value, vector, default(Rectangle?), color, rot, vector2, vector4, 0, 0f);
				Main.spriteBatch.Draw(value, vector, default(Rectangle?), color * 0.3f, rot, vector2, vector3, 0, 0f);
			}
        }
    }
}

