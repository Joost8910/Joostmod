using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Legendaries
{
    public class BrokenShrine : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shrine of Legends");
            // Tooltip.SetDefault("Used to craft legendary weapons");
            ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<ShrineOfLegends>();
        }
        public override void SetDefaults()
        {
            Item.maxStack = 9999;
            Item.width = 26;
            Item.height = 26;
            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = 0;
            Item.rare = ItemRarityID.Gray;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>("BrokenShrine").Type;
        }
    }
}

