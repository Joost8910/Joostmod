using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Thrown
{
    public class GiantNeedle : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Giant Needle");
            Tooltip.SetDefault("'Better than a sharp stick in the eye.'\n" + "'No, definitely worse...'");
        }
        public override void SetDefaults()
        {
            Item.damage = 280;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 26;
            Item.height = 72;
            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 7;
            Item.value = 10000000;
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("GiantNeedle").Type;
            Item.shootSpeed = 16f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(5));
            velocity = perturbedSpeed;
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.Cactustoken>()
                .Register();
        }
    }
}

