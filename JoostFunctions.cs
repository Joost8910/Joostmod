using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;
using Terraria.GameContent.Creative;

namespace JoostMod
{
    public class JoostFunctions : ModSystem
    {
        public static float GameDamageMult()
        {
            if (Main.GameModeInfo.IsJourneyMode)
            {
                CreativePowers.DifficultySliderPower power = CreativePowerManager.Instance.GetPower<CreativePowers.DifficultySliderPower>();
                if (power != null && power.GetIsUnlocked())
                {
                    return power.StrengthMultiplierToGiveNPCs;
                }
            }
            return Main.GameModeInfo.EnemyDamageMultiplier;
        }
        public static bool MetroidModActive() => ModLoader.TryGetMod("MetroidMod", out _);
        public static Vector2 PredictPlayerPosition(Vector2 startPos, float shootSpeed, Player P, int extraTime = 0)
        {
            Vector2 tileVel = Collision.TileCollision(P.position, P.velocity, P.width, P.height);
            Vector2 predictedPos = P.position + tileVel;
            int t = extraTime + (int)(Vector2.Distance(predictedPos, startPos) / shootSpeed);
            for (int i = 0; i < t; i++)
            {
                predictedPos += Collision.TileCollision(predictedPos, P.velocity, P.width, P.height);
            }
            predictedPos += P.Size / 2;
            //Vector2 predictedPos = P.MountedCenter + P.velocity + (tileVel * (extraTime + Vector2.Distance(P.MountedCenter, startPos) / shootSpeed));
            //predictedPos = P.MountedCenter + P.velocity + (tileVel * (extraTime + Vector2.Distance(predictedPos, startPos) / shootSpeed));
            //predictedPos = P.MountedCenter + P.velocity + (tileVel * (extraTime + Vector2.Distance(predictedPos, startPos) / shootSpeed));
            return predictedPos;
        }
        public void PredictNPCPosition(Vector2 startPos, float shootSpeed, NPC npc, ref Vector2 targetPos, ref float targetDist)
        {
            Vector2 predictedVel = npc.velocity;
            Vector2 predictedPos = npc.position + predictedVel;
            float predictedTime = Vector2.Distance(npc.Center, startPos) / shootSpeed;
            for (int i = 0; i < predictedTime; i++)
            {
                PredictNPCGravity(npc, predictedPos, ref predictedVel);
                predictedPos += predictedVel;
            }

            predictedTime = Vector2.Distance(predictedPos, startPos) / shootSpeed;
            predictedVel = npc.velocity;
            predictedPos = npc.position;
            for (int i = 0; i < predictedTime; i++)
            {
                PredictNPCGravity(npc, predictedPos, ref predictedVel);
                predictedPos += predictedVel;
            }

            targetDist = Vector2.Distance(startPos, predictedPos);
            targetPos = predictedPos + new Vector2(npc.width / 2, npc.height / 2);
            //Dust.NewDustPerfect(targetPos, DustID.Adamantite, Vector2.Zero, 0, Color.Red, 3f).noGravity = true;
        }
        public void PredictNPCGravity(NPC npc, Vector2 predictedPos, ref Vector2 predictedVelocity)
        {
            if (!npc.noGravity)
            {
                float gravity = 0.3f;
                float maxFallSpeed = 10f;
                if (npc.type == NPCID.MushiLadybug)
                {
                    gravity = 0.1f;
                    maxFallSpeed = 3f;
                }
                else if (npc.type == NPCID.VortexRifleman && npc.ai[2] == 1f)
                {
                    gravity = 0.1f;
                    maxFallSpeed = 2f;
                }
                else if ((npc.type == NPCID.DD2OgreT2 || npc.type == NPCID.DD2OgreT3) && npc.ai[0] > 0f && npc.ai[1] == 2f)
                {
                    gravity = 0.45f;
                    maxFallSpeed = 32f;
                }
                else if (npc.type == NPCID.VortexHornet && npc.ai[2] == 1f)
                {
                    gravity = 0.1f;
                    maxFallSpeed = 4f;
                }
                else if (npc.type == NPCID.VortexHornetQueen)
                {
                    gravity = 0.1f;
                    maxFallSpeed = 3f;
                }
                else if (npc.type == NPCID.SandElemental)
                {
                    gravity = 0f;
                }
                float num = (float)(Main.maxTilesX / 4200);
                num *= num;
                float num2 = (float)((double)(npc.position.Y / 16f - (60f + 10f * num)) / (Main.worldSurface / 6.0));
                if ((double)num2 < 0.25)
                {
                    num2 = 0.25f;
                }
                if (num2 > 1f)
                {
                    num2 = 1f;
                }
                gravity *= num2;
                if (npc.wet)
                {
                    gravity = 0.2f;
                    maxFallSpeed = 7f;
                    if (npc.honeyWet)
                    {
                        gravity = 0.1f;
                        maxFallSpeed = 4f;
                    }
                }
                predictedVelocity.Y += gravity;
                if (predictedVelocity.Y > maxFallSpeed)
                {
                    predictedVelocity.Y = maxFallSpeed;
                }
            }
            if (!npc.noTileCollide)
            {
                predictedVelocity = Collision.TileCollision(predictedPos, predictedVelocity, npc.width, npc.height);
            }
        }
        public static bool EllipseCollision(Vector2 ellipsePos, Vector2 ellipseDim, Vector2 boxPos, Vector2 boxDim)
        {
            Vector2 ellipseCenter = ellipsePos + 0.5f * ellipseDim;
            float x = 0f; //ellipse center
            float y = 0f; //ellipse center
            if (boxPos.X > ellipseCenter.X)
            {
                x = boxPos.X - ellipseCenter.X; //left corner
            }
            else if (boxPos.X + boxDim.X < ellipseCenter.X)
            {
                x = boxPos.X + boxDim.X - ellipseCenter.X; //right corner
            }
            if (boxPos.Y > ellipseCenter.Y)
            {
                y = boxPos.Y - ellipseCenter.Y; //top corner
            }
            else if (boxPos.Y + boxDim.Y < ellipseCenter.Y)
            {
                y = boxPos.Y + boxDim.Y - ellipseCenter.Y; //bottom corner
            }
            float a = ellipseDim.X / 2f;
            float b = ellipseDim.Y / 2f;
            return x * x / (a * a) + y * y / (b * b) < 1; //point collision detection
        }
    }
}
