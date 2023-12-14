using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using System;
using Terraria.ID;
using System.Linq;
using JoostMod.UI;
using JoostMod.NPCs.Town;
using Terraria.ModLoader.UI.Elements;
using FixedUIScrollbar = Terraria.ModLoader.UI.Elements.FixedUIScrollbar;

namespace JoostMod.UI
{
    class HuntUI : UIState
    {
        public UIPanel panel;
        public UIGrid huntGrid;
        public FixedUIScrollbar huntGridScrollbar;

        private bool needUpdate = true;
        public override void OnInitialize()
        {
            panel = new UIPanel();
            panel.Width.Set(500, 0);
            panel.Height.Set(400, 0);
            panel.HAlign = 0.5f;
            panel.VAlign = 0.25f;
            Append(panel);


            UIText header = new UIText("Hunts");
            header.HAlign = 0.5f;
            header.Top.Set(0, 0);
            panel.Append(header);

            huntGrid = new UIGrid();
            huntGrid.Top.Pixels = 40;
            huntGrid.Left.Pixels = 10;
            huntGrid.Width.Set(450, 0);
            huntGrid.Height.Set(320, 0);
            huntGrid.ListPadding = 10;
            huntGrid.OnScrollWheel += OnScroll;
            panel.Append(huntGrid);

            huntGridScrollbar = new FixedUIScrollbar(UserInterface.ActiveInstance);
            huntGridScrollbar.SetView(100f, 450f);
            huntGridScrollbar.Top.Pixels = 40;
            huntGridScrollbar.Height.Set(50, 0);
            huntGridScrollbar.HAlign = 1f;
            panel.Append(huntGridScrollbar);
            huntGrid.SetScrollbar(huntGridScrollbar);
        }
        internal static void OnScroll(UIScrollWheelEvent evt, UIElement listeningElement)
        {
            Main.LocalPlayer.ScrollHotbar(Terraria.GameInput.PlayerInput.ScrollWheelDelta / 100);
        }
        internal void NeedUpdate()
        {
            needUpdate = true;
        }

        internal void UpdateHuntList()
        {
            if (!needUpdate)
            {
                return;
            }
            needUpdate = false;
            huntGrid.Clear();
            
            foreach (HuntInfo hunt in JoostMod.instance.hunts)
            {
                if (hunt.showQuest())
                {
                    HuntUIButton bill = new HuntUIButton(hunt);
                    huntGrid.Add(bill);
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            UpdateHuntList();
            Vector2 mousePos = new Vector2(Main.mouseX, Main.mouseY);
            if (panel.ContainsPoint(mousePos))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Main.LocalPlayer.talkNPC == -1 || Main.npc[Main.LocalPlayer.talkNPC].type != ModContent.NPCType<HuntMaster>())
            {
                ModContent.GetInstance<JoostUI>().HideHuntUI();
                NeedUpdate();
            }
        }
    }
}
