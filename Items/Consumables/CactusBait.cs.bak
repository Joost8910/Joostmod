using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Consumables
{
    public class CactusBait : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Succulent Cactus Meal");
            Tooltip.SetDefault("Summons the Alpha Cactus Worm while in the underground desert");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 44;
            Item.height = 44;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true; 
            Item.value = 75000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(106, 63, 202);
                }
            }
        }
        public override bool CanUseItem(Player player)
        {
            if (player.position.Y/16 > Main.worldSurface && !NPC.AnyNPCs(Mod.Find<ModNPC>("IdleCactusWorm").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("AlphaCactusWormHead").Type) && !NPC.AnyNPCs(Mod.Find<ModNPC>("GrandCactusWormHead").Type))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("AlphaCactusWormHead").Type);
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.SucculentCactus>(10)
                .AddIngredient(ItemID.Bone, 6)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}

