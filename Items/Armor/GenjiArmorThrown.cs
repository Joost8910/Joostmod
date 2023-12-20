using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class GenjiArmorThrown : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Veridian Genji Armor");
            Tooltip.SetDefault("20% Increased throwing crit chance\n" + "Max Life increased by 225");
        }
        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 50;
            Item.value = 10000000;
            Item.rare = ItemRarityID.Purple;
            Item.defense = 30;
        }
        public override void EquipFrameEffects(Player player, EquipType type)
        {
            player.GetModPlayer<JoostPlayer>().skirtTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_Skirt");
            player.GetModPlayer<JoostPlayer>().betterShoulderTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_BetterShoulder");
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(0, 255, 0);
                }
            }
        }
        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 225;
            player.GetCritChance(DamageClass.Throwing) += 20;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.GenjiToken>()
                .Register();
        }
    }
}