using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

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
            Item.shoot = Mod.Find<ModProjectile>("Boomerain").Type;
            Item.shootSpeed = 7.5f;

        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Mod.Find<ModProjectile>("EarthenHammer").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("EarthWave").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("EarthWave1").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("EarthWave2").Type] <= 0 || player.ownedProjectileCounts[Mod.Find<ModProjectile>("GaleBoomerang").Type] <= 0 || player.ownedProjectileCounts[Mod.Find<ModProjectile>("Boomerain").Type] <= 0 || player.ownedProjectileCounts[Mod.Find<ModProjectile>("InfernalChakram").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("DousedChakram").Type] <= 0;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("EarthenHammer").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("EarthWave").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("EarthWave1").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("EarthWave2").Type] <= 0)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 1.2f, velocity.Y * 1.2f, Mod.Find<ModProjectile>("EarthenHammer").Type, (int)(damage * 1.44f), knockback * 2.16f, player.whoAmI);
            }
            else if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("GaleBoomerang").Type] <= 0)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("GaleBoomerang").Type, damage, knockback, player.whoAmI);
            }
            else if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Boomerain").Type] <= 0)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 2, velocity.Y * 2, Mod.Find<ModProjectile>("Boomerain").Type, (int)(damage * 0.65f), knockback * 1.16f, player.whoAmI);
            }
            else if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("InfernalChakram").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("DousedChakram").Type] <= 0)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 1.4f, velocity.Y * 1.4f, Mod.Find<ModProjectile>("InfernalChakram").Type, (int)(damage * 0.75f), knockback, player.whoAmI);
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


