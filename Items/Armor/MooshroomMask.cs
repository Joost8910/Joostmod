using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class MooshroomMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mooshroom Mask");
            Tooltip.SetDefault("MOOOO");
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 5000;
            Item.rare = ItemRarityID.LightRed;
            Item.vanity = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Mushroom, 10)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

    }
}