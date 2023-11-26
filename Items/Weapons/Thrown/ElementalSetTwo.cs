using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace JoostMod.Items.Weapons.Thrown
{
    public class ElementalSetTwo : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lesser Elemental Weapon Set");
            Tooltip.SetDefault("'Unleash the elements!'");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 16));
        }
        public override void SetDefaults()
        {
            Item.damage = 47;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 22;
            Item.height = 30;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.value = 350000;
            Item.rare = ItemRarityID.Pink;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("WaterBalloon").Type;
            Item.shootSpeed = 10f;

        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int wep = Main.rand.Next(4);
            if (wep == 0)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 0.8f, velocity.Y * 0.8f, Mod.Find<ModProjectile>("Fireball").Type, (int)(damage * 0.8f), knockback * 0.2f, player.whoAmI);
            }
            if (wep == 1)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("Tornade").Type, (int)(damage * 0.9f), knockback, player.whoAmI);
            }
            if (wep == 2)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 1.2f, velocity.Y * 1.2f, Mod.Find<ModProjectile>("WaterBalloon").Type, (int)(damage * 1f), knockback, player.whoAmI);
            }
            if (wep == 3)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("Rock").Type, (int)(damage * 3.5f), knockback * 2, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Rock>(999)
                .AddIngredient<Fireball>(999)
                .AddIngredient<Tornade>(999)
                .AddIngredient<WaterBalloon>(999)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }

    }
}


