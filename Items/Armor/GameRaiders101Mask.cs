using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class GameRaiders101Mask : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gameraiders101 Mask");
            Tooltip.SetDefault("'Great for impersonating youtubers!'");
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 10000;
            Item.rare = ItemRarityID.Red;
            Item.vanity = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<MooshroomMask>()
                .AddIngredient(ItemID.Sunglasses)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}