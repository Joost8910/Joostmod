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
            ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
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
            return body.type == ModContent.ItemType<AirArmor>() && legs.type == ModContent.ItemType<AirLegs>();
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
                .AddRecipeGroup(nameof(ItemID.CobaltBar), 4)
                .AddRecipeGroup(nameof(ItemID.MythrilBar), 4)
                .AddRecipeGroup(nameof(ItemID.AdamantiteBar), 4)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}