using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Melee
{
    public class NegativeThousandDegreeKnife : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("-1000'C Degrees Knife");
            Tooltip.SetDefault("'So cold its atoms can't move!'");
        }
        public override void SetDefaults()
        {
            Item.damage = 200;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 20;
            Item.height = 18;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 1;
            Item.value = 500000;
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.shoot = Mod.Find<ModProjectile>("NegativeThousandDegreeKnife").Type;
            Item.shootSpeed = 0f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LunarBar, 8)
                .AddIngredient(ItemID.FrostCore)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}

