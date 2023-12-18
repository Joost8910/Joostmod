using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class FireChest : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Smoldering Breastplate");
            Tooltip.SetDefault("18% increased ranged damage");
            ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false;
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 18;
            Item.value = 300000;
            Item.rare = ItemRarityID.Pink;
            Item.defense = 16;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.18f;
        }
        public override void EquipFrameEffects(Player player, EquipType type)
        {
            if (player.handoff == -1)
                player.handoff = (sbyte)EquipLoader.GetEquipSlot(Mod, "FireChest", EquipType.HandsOff);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.FireEssence>(50)
                .AddRecipeGroup(nameof(ItemID.CobaltBar), 8)
                .AddRecipeGroup(nameof(ItemID.MythrilBar), 8)
                .AddRecipeGroup(nameof(ItemID.AdamantiteBar), 8)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}