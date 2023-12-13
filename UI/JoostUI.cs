using JoostMod.Items.Rewards;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using JoostMod.Items;
using ReLogic.Content;
using Terraria.GameContent;
using JoostMod.NPCs.Town;

namespace JoostMod.UI
{
    public class JoostUI : ModSystem
    {
        internal UserInterface huntUserInterface;
        internal HuntUI huntUI;
        private GameTime _lastUpdateUiGameTime;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                huntUI = new HuntUI();
                huntUI.Activate();
                huntUserInterface = new UserInterface();
                huntUserInterface.SetState(null);
            }
        }
        internal void ShowHuntUI()
        {
            huntUserInterface?.SetState(huntUI);
        }
        internal void HideHuntUI()
        {
            huntUserInterface?.SetState(null);
        }
        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;
            if (huntUserInterface?.CurrentState != null)
            {
                huntUserInterface.Update(gameTime);
            }
            if (Main.LocalPlayer.talkNPC == -1 || Main.npc[Main.LocalPlayer.talkNPC].type != ModContent.NPCType<HuntMaster>())
            {
                HideHuntUI();
            }
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            Player player = Main.LocalPlayer;
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "JoostMod: HuntInterface",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && huntUserInterface?.CurrentState != null)
                        {
                            huntUserInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));
            }
            int i = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (i != -1)
            {
                layers.Insert(i + 1, new LegacyGameInterfaceLayer(
                    "JoostMod: Empty Heart",
                    delegate
                    {
                        DrawEmptyHeart(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }

        public void DrawEmptyHeart(SpriteBatch spriteBatch)
        {
            Mod mod = JoostMod.instance;
            Player player = Main.player[Main.myPlayer];
            if (player.GetModPlayer<JoostPlayer>().emptyHeart)
            {
                Texture2D texHeart = ModContent.Request<Texture2D>("JoostMod/Items/EmptyHeart").Value;
                if (player.whoAmI == Main.myPlayer && player.active && !player.ghost)
                {
                    float lifePerHeart = 1f;
                    float scale = 1f;
                    bool flag = false;
                    int alpha;
                    if ((float)Main.player[Main.myPlayer].statLife >= (float)lifePerHeart)
                    {
                        alpha = 255;
                        if ((float)Main.player[Main.myPlayer].statLife == (float)lifePerHeart)
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        float num7 = ((float)Main.player[Main.myPlayer].statLife) / lifePerHeart;
                        alpha = (int)(30f + 225f * num7);
                        if (alpha < 30)
                        {
                            alpha = 30;
                        }
                        scale = num7 / 4f + 0.75f;
                        if ((double)scale < 0.75)
                        {
                            scale = 0.75f;
                        }
                        if (num7 > 0f)
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        scale += Main.cursorScale - 1f;
                    }
                    int xOffset = 0;
                    int yOffset = 0;
                    //yOffset += 5;
                    int a = 255;
                    Main.spriteBatch.Draw(texHeart, new Vector2((float)(500 + xOffset + (Main.screenWidth - 800) + TextureAssets.Heart.Value.Width / 2), 32f + ((float)TextureAssets.Heart.Value.Height - (float)TextureAssets.Heart.Value.Height * scale) / 2f + (float)yOffset + (float)(TextureAssets.Heart.Value.Height / 2)), new Rectangle?(new Rectangle(0, 0, texHeart.Width, texHeart.Height)), new Color(alpha, alpha, alpha, a), 0f, new Vector2((float)(texHeart.Width / 2), (float)(texHeart.Height / 2)), scale, SpriteEffects.None, 0f);

                }
            }
        }
    }
}