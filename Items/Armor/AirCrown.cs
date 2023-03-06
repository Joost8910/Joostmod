using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class AirCrown : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crown of Wind");
            Tooltip.SetDefault("20% increased summon damage\n" +
                "Increases your max number of minions");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = 225000;
            Item.rare = ItemRarityID.Pink;
            Item.defense = 6;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.2f;
            player.maxMinions++;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("AirArmor").Type && legs.type == Mod.Find<ModItem>("AirLegs").Type;
        }
        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)/* tModPorter Note: Removed. In SetStaticDefaults, use ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true if you had drawHair set to true, and ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true if you had drawAltHair set to true */
        {
            drawAltHair = true;
        }

        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Press the Armor Ability key to sacrifice your minions\n" +
                "You gain greatly increased mobility and life regen\n" +
                "The duration is based on how many minions you sacrifice\n" +
                "You also gain brief invulnerability on activation";
            player.GetModPlayer<JoostPlayer>().airArmor = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.TinyTwister>(50)
                .AddRecipeGroup("JoostMod:AnyCobalt", 4)
                .AddRecipeGroup("JoostMod:AnyMythril", 4)
                .AddRecipeGroup("JoostMod:AnyAdamantite", 4)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}