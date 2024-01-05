using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Dyes;
using Terraria.Graphics.Shaders;

namespace JoostMod.Shaders
{
    public class TeamOutlineShaderData : ArmorShaderData
    {

        private static bool isInitialized;

        private static ArmorShaderData[] dustShaderData;

        public TeamOutlineShaderData(Ref<Effect> shader, string passName) : base(shader, passName)
        {
            if (!isInitialized)
            {
                isInitialized = true;
                dustShaderData = new ArmorShaderData[Main.teamColor.Length];
                for (int i = 1; i < Main.teamColor.Length; i++)
                {
                    dustShaderData[i] = new ArmorShaderData(shader, passName).UseColor(Main.teamColor[i]);
                }
                dustShaderData[0] = new ArmorShaderData(shader, passName).UseColor(Color.White);
            }
        }

        //I had to recreate TeamArmorShaderData just to change this one function to allow drawing when not on a team
        public override void Apply(Entity entity, DrawData? drawData)
        {
            Player player = entity as Player;
            if (entity != null && entity is Projectile)
            {
                Projectile p = entity as Projectile;
                if (p != null) 
                    player = Main.player[p.owner];
            }
            if (player == null)
            {
                return;
            }
            Color c = Color.White;
            if (player.team > 0)
                c = Main.teamColor[player.team];
            base.UseColor(c);
            base.Apply(player, drawData);
        }

        public override ArmorShaderData GetSecondaryShader(Entity entity)
        {
            Player player = entity as Player;
            if (player == null)
            {
                return dustShaderData[0];
            }
            return dustShaderData[player.team];
        }
    }
}
