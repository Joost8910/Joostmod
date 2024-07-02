using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace JoostMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class AirMedallion : ModItem
    {
		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 24;
            Item.value = 225000;
            Item.rare = ItemRarityID.Pink;
			Item.accessory = true;
            Item.damage = 25;
            Item.DamageType = DamageClass.Summon;
        }
        public override bool WeaponPrefix()
        {
            return false;
        }
        public override bool MagicPrefix()
        {
            return false;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria")
                {
                    if (line2.Name == "ItemName")
                    {
                        line2.OverrideColor = new Color(230, 204, 128);
                    }
                    if (line2.Name == "Damage" || line2.Name == "CritChance" || line2.Name == "Knockback")
                    {
                        line2.OverrideColor = Color.DarkGray;
                    }
                }
            }
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
            player.moveSpeed *= 1.1f;
            player.maxRunSpeed *= 1.1f;
            player.GetModPlayer<JoostPlayer>().accRunSpeedMult *= 1.1f;
            player.GetModPlayer<JoostPlayer>().airMedallion = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.AirEssence>(50)
                .AddRecipeGroup(nameof(ItemID.CobaltBar), 3)
                .AddRecipeGroup(nameof(ItemID.MythrilBar), 3)
                .AddRecipeGroup(nameof(ItemID.AdamantiteBar), 3)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}