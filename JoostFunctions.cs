using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace JoostMod
{
    public class JoostFunctions : ModSystem
    {
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
    }
}
