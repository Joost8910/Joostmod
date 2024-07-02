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
            // DisplayName.SetDefault("Greater Elemental Weapon Set");
            // Tooltip.SetDefault("'Unleash the elements!'");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(8, 16));
        }
        public override void SetDefaults()
        {
            Item.damage = 45;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 46;
            Item.height = 64;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 7;
            Item.value = 900000;
            Item.rare = ItemRarityID.Pink;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.Boomerain>();
            Item.shootSpeed = 10f;

        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Thrown.EarthenHammer>()] + player.ownedProjectileCounts[ModContent.ProjectileType<EarthWave>()] + player.ownedProjectileCounts[ModContent.ProjectileType<EarthWave1>()] + player.ownedProjectileCounts[ModContent.ProjectileType<EarthWave2>()] <= 0 || player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Thrown.GaleBoomerang>()] <= 0 || player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Thrown.Boomerain>()] <= 0 || player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Thrown.InfernalChakram>()] + player.ownedProjectileCounts[ModContent.ProjectileType<DousedChakram>()] <= 0;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Thrown.GaleBoomerang>()] <= 0)
            {
                Rectangle rect = new Rectangle((int)Main.MouseWorld.X - 32, (int)Main.MouseWorld.Y - 32, 64, 64);
                int t = 0;
                for (int n = 0; n < 200; n++)
                {
                    NPC target = Main.npc[n];
                    if (target.active && !target.friendly && !target.immortal && rect.Intersects(target.getRect()))
                    {
                        t = n + 1;
                        for (int d = 0; d < 12; d++)
                        {
                            Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, DustID.SnowBlock, 0, 0f, 50, default, 1.5f);
                            dust.noGravity = true;
                            dust.velocity = target.DirectionTo(dust.position) * 3;
                            dust.position = target.Center;
                        }
                        break;
                    }
                }
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 0.95f, velocity.Y * 0.95f, ModContent.ProjectileType<Projectiles.Thrown.GaleBoomerang>(), (int)(damage * 40f / Item.damage), knockback, player.whoAmI, t, t);
            }
            else if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Thrown.InfernalChakram>()] + player.ownedProjectileCounts[ModContent.ProjectileType<DousedChakram>()] <= 0)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<Projectiles.Thrown.InfernalChakram>(), (int)(damage * 36f / Item.damage), knockback, player.whoAmI);
            }
            else if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Thrown.EarthenHammer>()] + player.ownedProjectileCounts[ModContent.ProjectileType<EarthWave>()] + player.ownedProjectileCounts[ModContent.ProjectileType<EarthWave1>()] + player.ownedProjectileCounts[ModContent.ProjectileType<EarthWave2>()] <= 0)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<Projectiles.Thrown.EarthenHammer>(), (int)(damage * 69f / Item.damage), knockback * 2.16f, player.whoAmI);
            }
            else if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Thrown.Boomerain>()] <= 0)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 1.4f, velocity.Y * 1.4f, ModContent.ProjectileType<Projectiles.Thrown.Boomerain>(), damage, knockback * 1.16f, player.whoAmI);
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


