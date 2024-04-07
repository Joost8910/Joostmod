using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using JoostMod.Projectiles.Thrown;

namespace JoostMod.Items.Weapons.Thrown
{
    public class ElementalSet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Greater Elemental Weapon Set");
            Tooltip.SetDefault("'Unleash the elements!'");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(8, 16));
        }
        public override void SetDefaults()
        {
            Item.damage = 48;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 46;
            Item.height = 64;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 7;
            Item.value = 900000;
            Item.rare = ItemRarityID.Pink;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.Boomerain>();
            Item.shootSpeed = 7.5f;

        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Thrown.EarthenHammer>()] + player.ownedProjectileCounts[ModContent.ProjectileType<EarthWave>()] + player.ownedProjectileCounts[ModContent.ProjectileType<EarthWave1>()] + player.ownedProjectileCounts[ModContent.ProjectileType<EarthWave2>()] <= 0 || player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Thrown.GaleBoomerang>()] <= 0 || player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Thrown.Boomerain>()] <= 0 || player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Thrown.InfernalChakram>()] + player.ownedProjectileCounts[ModContent.ProjectileType<DousedChakram>()] <= 0;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Thrown.EarthenHammer>()] + player.ownedProjectileCounts[ModContent.ProjectileType<EarthWave>()] + player.ownedProjectileCounts[ModContent.ProjectileType<EarthWave1>()] + player.ownedProjectileCounts[ModContent.ProjectileType<EarthWave2>()] <= 0)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 1.2f, velocity.Y * 1.2f, ModContent.ProjectileType<Projectiles.Thrown.EarthenHammer>(), (int)(damage * 1.44f), knockback * 2.16f, player.whoAmI);
            }
            else if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Thrown.GaleBoomerang>()] <= 0)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<Projectiles.Thrown.GaleBoomerang>(), damage, knockback, player.whoAmI);
            }
            else if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Thrown.Boomerain>()] <= 0)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 2, velocity.Y * 2, ModContent.ProjectileType<Projectiles.Thrown.Boomerain>(), (int)(damage * 0.65f), knockback * 1.16f, player.whoAmI);
            }
            else if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Thrown.InfernalChakram>()] + player.ownedProjectileCounts[ModContent.ProjectileType<DousedChakram>()] <= 0)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 1.4f, velocity.Y * 1.4f, ModContent.ProjectileType<Projectiles.Thrown.InfernalChakram>(), (int)(damage * 0.75f), knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<EarthenHammer>()
                .AddIngredient<InfernalChakram>()
                .AddIngredient<GaleBoomerang>()
                .AddIngredient<Boomerain>()
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }

    }
}


