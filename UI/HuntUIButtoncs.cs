using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace JoostMod.UI
{
    public class HuntUIButton : UIElement
    {
        public static Texture2D bgTex = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/NPCs/Town/HuntScroll");
        private HuntInfo hunt;
        private float scale = 1f;
        
        public HuntUIButton(HuntInfo h)
        {
            hunt = h;

            Width.Set(bgTex.Width * scale, 0);
            Height.Set(bgTex.Height * scale, 0);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Texture2D scrollTex = bgTex;
            CalculatedStyle dimensions = GetDimensions();
            Color bgColor = Color.LightGray * 0.5f;
            Color iconColor = Color.SandyBrown * 0.4f;
            if (hunt.available())
            {
                int item = Main.LocalPlayer.FindItem(hunt.item);
                if (item != -1)
                {
                    scrollTex = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/NPCs/Town/HuntScroll_HasItem");
                    if (IsMouseHovering)
                    {
                        bgColor = Color.White;
                        iconColor = Color.Goldenrod;
                    }
                    else
                    {
                        bgColor = Color.White * 0.8f;
                        iconColor = Color.Goldenrod * 0.8f;
                    }
                }
                else if (hunt.completed())
                {
                    scrollTex = (Texture2D)Mod.Assets.Request<Texture2D>("JoostMod/NPCs/Town/HuntScroll_Completed");
                    bgColor = Color.White * 0.85f;
                    iconColor = Color.Gray * 0.7f;
                }
                else
                {
                    if (IsMouseHovering)
                    {
                        bgColor = Color.LightGray;
                        iconColor = Color.SandyBrown * 0.9f;
                    }
                    if (JoostWorld.activeQuest.Contains(hunt.NPC))
                    {
                        bgColor = Color.White;
                        iconColor = Color.SandyBrown;
                    }
                }
            }
            else
            {
                bgColor = Color.Black * 0.5f;
                iconColor = Color.Black * 0.5f;
            }
            spriteBatch.Draw(scrollTex, dimensions.Position(), null, bgColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
            Main.instance.LoadNPC(hunt.NPC);
            Texture2D tex = TextureAssets.Npc[hunt.NPC].Value;
            Rectangle rect = tex.Frame(hunt.xFrameCount, Main.npcFrameCount[hunt.NPC], 0, 0); 

            float num = 0.6f;
            float num2 = scrollTex.Width * scale * 0.6f;
            if (rect.Width > num2 || rect.Height > num2)
            {
                if (rect.Width > rect.Height)
                {
                    num = num2 / rect.Width;
                }
                else
                {
                    num = num2 / rect.Height;
                }
            }
            Vector2 drawPos = dimensions.Position();
            drawPos.X += scrollTex.Width * scale / 2f - rect.Width * num / 2f;
            drawPos.Y += scrollTex.Height * scale / 2f - rect.Height * num / 2f;

            spriteBatch.Draw(tex, drawPos, new Rectangle?(rect), iconColor, 0f, Vector2.Zero, num, SpriteEffects.None, 0f);
        }
        public override void Click(UIMouseEvent evt)
        {
            Player player = Main.LocalPlayer;
            if (hunt.available())
            {
                int item = player.FindItem(hunt.item);
                if (item != -1)
                {
                    player.inventory[item].stack--;
                    if (player.inventory[item].stack <= 0)
                    {
                        player.inventory[item] = new Item();
                    }
                    Main.npcChatText = hunt.completeText;
                    JoostMod.instance.HideHuntUI();
                    SoundEngine.PlaySound(SoundID.Chat);
                    hunt.reward(player);
                    return;
                }
                else if (!hunt.completed())
                {
                    Main.npcChatText = hunt.questText;
                    JoostMod.instance.HideHuntUI();
                    SoundEngine.PlaySound(SoundID.Item1.WithPitchOffset(-0.5f), player.position);
                    if (!JoostWorld.activeQuest.Contains(hunt.NPC))
                    {
                        Main.NewText(Lang.GetNPCNameValue(hunt.NPC), 225, 25, 25);
                        Main.NewText("The Hunt Begins!", 225, 25, 25);
                        JoostWorld.activeQuest.Add(hunt.NPC);
                        if (Main.netMode == 1)
                        {
                            ModPacket packet = JoostMod.instance.GetPacket();
                            packet.Write((byte)JoostModMessageType.ActiveQuest);
                            packet.Write(hunt.NPC);
                            packet.Send();
                        }
                    }
                }
            }
        }
    }
}
