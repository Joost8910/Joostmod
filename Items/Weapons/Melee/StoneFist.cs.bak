using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons.Melee
{
    [AutoloadEquip(EquipType.HandsOff)]
    public class StoneFist : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stone Fist");
            Tooltip.SetDefault("'The real fist of Fury'\n" +
                "Charges up a powerful punch\n" +
                "Right Click while charged to grab an enemy\n" +
                "Hold Right Click to pummel the grabbed enemy\n" +
                "Let go of Left Click to throw the grabbed enemy");
        }
        public override void SetDefaults()
        {
            Item.damage = 333;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 64;
            Item.height = 64;
            Item.useTime = 55;
            Item.useAnimation = 55;
            Item.reuseDelay = 5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 50;
            Item.value = 225000;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item13;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.Stonefist>();
            Item.shootSpeed = 10f;
        }
        public override bool MeleePrefix()
        {
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.EarthEssence>(50)
                .AddIngredient(ItemID.StoneBlock, 100)
                .AddRecipeGroup(nameof(ItemID.CobaltBar), 4)
                .AddRecipeGroup(nameof(ItemID.MythrilBar), 4)
                .AddRecipeGroup(nameof(ItemID.AdamantiteBar), 4)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}

