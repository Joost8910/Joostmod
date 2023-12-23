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
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.GiantNeedle>();
            Item.shootSpeed = 16f;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(5));
            velocity = perturbedSpeed;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.Cactustoken>()
                .Register();
        }
    }
}

