using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ZoraHelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zora Helm");
            Tooltip.SetDefault("18% increased magic crit chance");
            ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = 225000;
            Item.rare = ItemRarityID.Pink;
            Item.defense = 8;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Magic) += 18;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ZoraArmor>() && legs.type == ModContent.ItemType<ZoraGreaves>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Allows you to swim and breathe water\n" +
                "Press the Armor Ability key to do a watery spin attack\n" +
                "The spin attack is far less effective out of water";
            player.accFlipper = true;
            player.gills = true;
            if (!player.mount.Active)
                player.ignoreWater = true;
            player.GetModPlayer<JoostPlayer>().zoraArmor = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.WaterEssence>(50)
                .AddRecipeGroup("JoostMod:AnyCobalt", 4)
                .AddRecipeGroup("JoostMod:AnyMythril", 4)
                .AddRecipeGroup("JoostMod:AnyAdamantite", 4)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}