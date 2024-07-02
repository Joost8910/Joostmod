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
            // DisplayName.SetDefault("Lesser Elemental Weapon Set");
            // Tooltip.SetDefault("'Unleash the elements!'");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 16));
        }
        public override void SetDefaults()
        {
            Item.damage = 52;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 22;
            Item.height = 30;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 6;
            Item.value = 350000;
            Item.rare = ItemRarityID.Pink;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.WaterBalloon>();
            Item.shootSpeed = 10f;

        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int wep = Main.rand.Next(4);
            if (wep == 0)
            {
                //player.SetItemTime(player.itemTime + 8);
                //player.SetItemAnimation(player.itemAnimation + 8);
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 0.75f, velocity.Y * 0.75f, ModContent.ProjectileType<Projectiles.Thrown.Fireball>(), (int)(damage * 38f / Item.damage), knockback / Item.knockBack, player.whoAmI);
            }
            if (wep == 1)
            {
                Item.reuseDelay = 0;
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 1.2f, velocity.Y * 1.2f, ModContent.ProjectileType<Projectiles.Thrown.Tornade>(), (int)(damage * 36f / Item.damage), knockback * 4f / Item.knockBack, player.whoAmI);
            }
            if (wep == 2)
            {
                //player.SetItemTime(player.itemTime + 4);
                //player.SetItemAnimation(player.itemAnimation + 4);
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 1.5f, velocity.Y * 1.5f, ModContent.ProjectileType<Projectiles.Thrown.WaterBalloon>(), damage, knockback, player.whoAmI);
            }
            if (wep == 3)
            {
                //player.SetItemTime(player.itemTime + 13);
                //player.SetItemAnimation(player.itemAnimation + 13);
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 1.3f, velocity.Y * 1.3f, ModContent.ProjectileType<Projectiles.Thrown.Rock>(), (int)(damage * 180f / Item.damage), knockback * 10f / Item.knockBack, player.whoAmI);
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


