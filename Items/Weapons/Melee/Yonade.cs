using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Melee
{
    public class Yonade : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yonade");
            Tooltip.SetDefault("Drops and explodes on a critical hit");
        }
        public override void SetDefaults()
        {
            Item.damage = 23;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 30;
            Item.height = 30;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4.2f;
            Item.channel = true;
            Item.value = 80000;
            Item.rare = ItemRarityID.Orange;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = Mod.Find<ModProjectile>("Yonade").Type;
            Item.shootSpeed = 10f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Valor)
                .AddIngredient(ItemID.Grenade, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}

