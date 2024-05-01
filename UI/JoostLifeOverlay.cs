using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace JoostMod.UI
{
    public class JoostLifeOverlay : ModResourceOverlay
    {
        //Copied from ExampleMod

        // This field is used to cache vanilla assets used in the CompareAssets helper method further down in this file
        private Dictionary<string, Asset<Texture2D>> vanillaAssetCache = new();

        // These fields are used to cache the result of ModContent.Request<Texture2D>()
        private Asset<Texture2D> heartTexture, heartFillTexture, fancyPanelTexture, barsFillingTexture, barsPanelTexture;

        public override void PostDrawResource(ResourceOverlayDrawContext context)
        {
            Asset<Texture2D> asset = context.texture;

            string fancyFolder = "Images/UI/PlayerResourceSets/FancyClassic/";
            string barsFolder = "Images/UI/PlayerResourceSets/HorizontalBars/";

            if (!Main.LocalPlayer.GetModPlayer<JoostPlayer>().emptyHeart)
                return;

            // NOTE: CompareAssets is defined below this method's body
            if (asset == TextureAssets.Heart || asset == TextureAssets.Heart2)
            {
                // Draw over the Classic hearts
                DrawEmptyHeartClassic(context);
            }
            else if (CompareAssets(asset, fancyFolder + "Heart_Fill") || CompareAssets(asset, fancyFolder + "Heart_Fill_B"))
            {
                // Draw over the Fancy hearts
                DrawEmptyHeartFancyFill(context);
            }
            else if (CompareAssets(asset, fancyFolder + "Heart_Single_Fancy"))
            {
                // Draw over the Fancy hearts
                DrawEmptyHeartFancyPanel(context);
            }
            else if (CompareAssets(asset, barsFolder + "HP_Fill") || CompareAssets(asset, barsFolder + "HP_Fill_Honey"))
            {
                // Draw over the Bars life bars
                DrawEmptyHeartBarFill(context);
            }
            else if (CompareAssets(asset, barsFolder + "HP_Panel_Right"))
            {
                // Draw over the Bars life panel
                DrawEmptyHeartBarPanel(context);
            }
        }
        private bool CompareAssets(Asset<Texture2D> existingAsset, string compareAssetPath)
        {
            // This is a helper method for checking if a certain vanilla asset was drawn
            if (!vanillaAssetCache.TryGetValue(compareAssetPath, out var asset))
                asset = vanillaAssetCache[compareAssetPath] = Main.Assets.Request<Texture2D>(compareAssetPath);

            return existingAsset == asset;
        }

        private void DrawEmptyHeartClassic(ResourceOverlayDrawContext context)
        {
            context.texture = heartTexture ??= ModContent.Request<Texture2D>("JoostMod/Items/Accessories/EmptyHeart");
            context.source = context.texture.Frame();
            context.position -= new Vector2(4, 4);
            context.color = Color.White;
            context.Draw();
        }
        private void DrawEmptyHeartFancyFill(ResourceOverlayDrawContext context)
        {
            context.texture = heartFillTexture ??= ModContent.Request<Texture2D>("JoostMod/Items/Accessories/EmptyHeart_FancyFill");
            context.Draw();
        }
        private void DrawEmptyHeartFancyPanel(ResourceOverlayDrawContext context)
        {
            context.texture = fancyPanelTexture ??= ModContent.Request<Texture2D>("JoostMod/Items/Accessories/EmptyHeart_Fancy");
            context.Draw();
        }
        private void DrawEmptyHeartBarFill(ResourceOverlayDrawContext context)
        {
            context.texture = barsFillingTexture ??= ModContent.Request<Texture2D>("JoostMod/Items/Accessories/EmptyHeart_BarFill");
            context.Draw();
        }
        private void DrawEmptyHeartBarPanel(ResourceOverlayDrawContext context)
        {
            context.texture = barsPanelTexture ??= ModContent.Request<Texture2D>("JoostMod/Items/Accessories/EmptyHeart_Bar");
            context.source = context.texture.Frame();
            context.position -= new Vector2(2, 0);
            context.Draw();
        }
    }
}
