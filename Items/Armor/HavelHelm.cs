using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class HavelHelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Havel's Helmet");
            Tooltip.SetDefault("8% increased melee crit Chance\n" +
                "10% reduced movement speed");
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = 225000;
            Item.rare = ItemRarityID.Pink;
            Item.defense = 18;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 8;
            player.moveSpeed *= 0.9f;
            player.maxRunSpeed *= 0.9f;
            player.GetModPlayer<JoostPlayer>().accRunSpeedMult *= 0.9f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<HavelArmor>() && legs.type == ModContent.ItemType<HavelLeggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Press the Armor Ability key to reduce damage taken at the cost of mobility\n" +
                "30 second duration";
            player.GetModPlayer<JoostPlayer>().havelArmor = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.EarthEssence>(50)
                .AddIngredient(ItemID.StoneBlock, 100)
                .AddRecipeGroup("JoostMod:AnyCobalt", 4)
                .AddRecipeGroup("JoostMod:AnyMythril", 4)
                .AddRecipeGroup("JoostMod:AnyAdamantite", 4)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}