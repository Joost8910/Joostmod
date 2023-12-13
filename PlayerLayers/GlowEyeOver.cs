using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JoostMod.PlayerLayers
{
    public class GlowEyeOver : PlayerDrawLayer
    {
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            var jp = drawInfo.drawPlayer.GetModPlayer<JoostPlayer>();
            return jp.glowContacts && jp.glowEyeType > 0;
        }
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Head);

        public override bool IsHeadLayer => true;
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            SpriteBatch spriteBatch = Main.spriteBatch;
            Texture2D tex = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/Items/GlowingContacts_Face");
            Rectangle frame = drawPlayer.bodyFrame;
            float rot = drawPlayer.headRotation;
            Vector2 drawPos = drawPlayer.bodyPosition;
            Vector2 origin = drawInfo.headVect;
            Color color = drawPlayer.GetImmuneAlphaPure(Color.White, drawInfo.shadow);
            int eyeType = drawPlayer.GetModPlayer<JoostPlayer>().glowEyeType;
            if (eyeType == 2)
            {
                tex = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/Items/GlowingContacts_Alt");
            }
            if (eyeType == 3)
            {
                tex = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/Items/GlowingContacts_Helm");
            }
            if (eyeType == 4)
            {
                tex = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/Items/GlowingContacts_HelmAlt");
            }
            if (eyeType == 5)
            {
                tex = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/Items/GlowingContacts_Genji");
            }
            if (drawPlayer.GetModPlayer<JoostPlayer>().glowEyeNoGlow)
            {
                color = drawPlayer.GetImmuneAlphaPure(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), Color.White), drawInfo.shadow);
            }

            SpriteEffects effects = SpriteEffects.None;
            if (drawPlayer.direction == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            if (drawPlayer.gravDir == -1f)
            {
                effects |= SpriteEffects.FlipVertically;
            }
            DrawData data = new DrawData(tex, new Vector2((float)((int)(drawInfo.Position.X - Main.screenPosition.X - (float)(frame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawPlayer.height - (float)frame.Height + 4f))) + drawPos + origin, new Rectangle?(frame), color, rot, origin, 1f, effects, 0);
            data.shader = drawPlayer.GetModPlayer<JoostPlayer>().glowEyeDye;
            drawInfo.DrawDataCache.Add(data);
        }
    }
}
