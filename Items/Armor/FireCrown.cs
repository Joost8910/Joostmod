using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class FireCrown : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crown of Fire");
            Tooltip.SetDefault("18% increased ranged crit Chance");
            ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = 225000;
            Item.rare = ItemRarityID.Pink;
            Item.defense = 9;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Ranged) += 18;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("FireChest").Type && legs.type == Mod.Find<ModItem>("FireLeggings").Type;
        }
        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Press the Armor Ability key to become overflowing with fire\n" +
                "You deal increased ranged damage and gain increased movement speed, at the cost of rapidly losing life\n" +
                "You will also leave a trail of damaging fire if you have the Blazing Anklets equipped";
            player.GetModPlayer<JoostPlayer>().fireArmor = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.FireEssence>(50)
                .AddRecipeGroup("JoostMod:AnyCobalt", 4)
                .AddRecipeGroup("JoostMod:AnyMythril", 4)
                .AddRecipeGroup("JoostMod:AnyAdamantite", 4)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}