using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Accessories
{
	[AutoloadEquip(EquipType.Shield)]
	public class HavelsGreatshield : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Havel's Greatshield");
			Tooltip.SetDefault("Reduces movement speed by 5%\n" +
                "Right click to block attacks in front of you\n" + 
                "Blocking an attack reduces its damage by an amount equal to the shield's damage\n" + 
                "Left click while blocking to shield bash");
		}
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 46;
            Item.value = 225000;
            Item.rare = ItemRarityID.Pink;
			Item.accessory = true;
			Item.defense = 5;
            Item.damage = 50;
            Item.knockBack = 8;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        }

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
            player.moveSpeed *= 0.95f;
            player.maxRunSpeed *= 0.95f;
            player.GetModPlayer<JoostPlayer>().accRunSpeedMult *= 0.95f;
            player.GetModPlayer<JoostPlayer>().havelShieldItem = Item;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria")
                {
                    if (line2.Name == "Damage" || line2.Name == "CritChance" || line2.Name == "Knockback")
                    {
                        line2.OverrideColor = Color.DarkGray;
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.EarthEssence>(50)
                .AddIngredient(ItemID.StoneBlock, 150)
                .AddRecipeGroup(nameof(ItemID.CobaltBar), 3)
                .AddRecipeGroup(nameof(ItemID.MythrilBar), 3)
                .AddRecipeGroup(nameof(ItemID.AdamantiteBar), 3)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}