using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using JoostMod.Projectiles.Melee;
using JoostMod.Items;
using Terraria.ID;
using Terraria.GameContent;
using Microsoft.CodeAnalysis.Text;
using System;

namespace JoostMod.PlayerLayers
{
    public class ItemOverArm : PlayerDrawLayer
    {
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;
            if (player.HeldItem.TryGetGlobalItem<JoostGlobalItem>(out JoostGlobalItem jgi))
            {
                return jgi.drawOverArm && player.HeldItem.type != ItemID.None && (player.itemAnimation > 0 || player.HeldItem.holdStyle == 1);
            }
            return false;
        }
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.ArmOverItem);
        
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            Item item = drawPlayer.HeldItem;
            Texture2D tex = TextureAssets.Item[item.type].Value;

            DrawAnimation expr_D3 = Main.itemAnimations[item.type];
            Rectangle? sourceRect = new Rectangle?((expr_D3 != null) ? expr_D3.GetFrame(tex, -1) : tex.Frame(1, 1, 0, 0, 0, 0));
            Rectangle expr_F6A = sourceRect.Value;
            int width = expr_F6A.Width;
            int height = expr_F6A.Height;


            Vector2 vector10 = new Vector2((float)(width / 2), (float)(height / 2));
            Vector2 vector11 = Main.DrawPlayerItemPos(drawPlayer.gravDir, item.type);
            int num11 = (int)vector11.X;
            vector10.Y = vector11.Y;
            Vector2 origin = new Vector2((float)(-(float)num11), (float)(height / 2));
            if (drawPlayer.direction == -1)
            {
                origin = new Vector2((float)(width + num11), (float)(height / 2));
            }
            DrawData data = new DrawData(tex, new Vector2((float)((int)(drawInfo.ItemLocation.X - Main.screenPosition.X + vector10.X)), (float)((int)(drawInfo.ItemLocation.Y - Main.screenPosition.Y + vector10.Y))), sourceRect, item.GetAlpha(drawInfo.itemColor), drawPlayer.itemRotation, origin, drawPlayer.GetAdjustedItemScale(item), drawInfo.itemEffect, 0);
            drawInfo.DrawDataCache.Add(data);

            /*
            Rectangle frame = new Rectangle(0, 0, tex.Width, tex.Height);
            SpriteEffects effects = drawInfo.itemEffect;
            DrawData data = new DrawData(tex, new Vector2((int)(drawInfo.ItemLocation.X - Main.screenPosition.X + offsetX), (int)(drawInfo.ItemLocation.Y - Main.screenPosition.Y + offsetY)), new Rectangle?(frame), color, rot, origin, drawPlayer.GetAdjustedItemScale(item), effects, 0);

            drawInfo.DrawDataCache.Add(data);
            */
        }
    }
}
