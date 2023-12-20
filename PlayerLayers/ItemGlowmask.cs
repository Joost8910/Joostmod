using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using JoostMod.Items;
using Terraria.GameContent;
using Terraria.ID;

namespace JoostMod.PlayerLayers
{
    public class ItemGlowmask : PlayerDrawLayer
    {
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;
            if (player.HeldItem.TryGetGlobalItem<JoostGlobalItem>(out JoostGlobalItem jgi))
            {
                return jgi.glowmaskTex != null && player.HeldItem.type != ItemID.None && !player.HeldItem.noUseGraphic && (player.itemAnimation > 0 || player.HeldItem.holdStyle == 1);
            }
            return false;
        }
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.HeldItem);

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            Item item = drawPlayer.HeldItem;
            Texture2D tex = item.GetGlobalItem<JoostGlobalItem>().glowmaskTex;
            Color color = item.GetGlobalItem<JoostGlobalItem>().glowmaskColor;
            float rot = drawPlayer.itemRotation;
            if (Item.staff[item.type])
            {
                rot = drawPlayer.itemRotation + 0.785f * drawPlayer.direction;
            }
            int offsetX = 0;
            int offsetY = 0;
            Vector2 origin = new Vector2(0f, (float)TextureAssets.Item[item.type].Value.Height);
            if (drawPlayer.gravDir == -1f)
            {
                if (drawPlayer.direction == -1)
                {
                    rot += 1.57f;
                    origin = new Vector2((float)TextureAssets.Item[item.type].Value.Width, 0f);
                    offsetX -= TextureAssets.Item[item.type].Value.Width;
                }
                else
                {
                    rot -= 1.57f;
                    origin = Vector2.Zero;
                }
            }
            else if (drawPlayer.direction == -1)
            {
                origin = new Vector2((float)TextureAssets.Item[item.type].Value.Width, (float)TextureAssets.Item[item.type].Value.Height);
                offsetX -= TextureAssets.Item[item.type].Value.Width;
            }
            Rectangle frame = new Rectangle(0, 0, tex.Width, tex.Height);
            SpriteEffects effects = SpriteEffects.None;
            if (drawPlayer.direction == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            if (drawPlayer.gravDir == -1f)
            {
                effects |= SpriteEffects.FlipVertically;
            }
            DrawData data = new DrawData(tex, new Vector2((int)(drawInfo.ItemLocation.X - Main.screenPosition.X + origin.X + offsetX), (int)(drawInfo.ItemLocation.Y - Main.screenPosition.Y + offsetY)), new Rectangle?(frame), color, rot, origin, drawPlayer.HeldItem.scale, effects, 0);

            drawInfo.DrawDataCache.Add(data);
        }
    }
}
