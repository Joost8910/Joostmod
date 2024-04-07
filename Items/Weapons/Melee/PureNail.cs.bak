using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Melee;

namespace JoostMod.Items.Weapons.Melee
{
    public class PureNail : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pure Nail");
            Tooltip.SetDefault("'Crafted to perfection, this ancient nail reveals its true form'\n" +
                "Fires beams that deal half damage while at full health");
        }
        public override void SetDefaults()
        {
            Item.damage = 55;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 46;
            Item.height = 44;
            Item.noMelee = true;
            Item.useTime = 11;
            Item.useAnimation = 11;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.knockBack = 7;
            Item.value = 200000;
            Item.rare = ItemRarityID.LightPurple;
            Item.UseSound = SoundID.Item18;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.PureNail>();
            Item.shootSpeed = 15f;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] + player.ownedProjectileCounts[ModContent.ProjectileType<PureNail2>()] + player.ownedProjectileCounts[ModContent.ProjectileType<GreatSlash>()] + player.ownedProjectileCounts[ModContent.ProjectileType<DashSlash>()] + player.ownedProjectileCounts[ModContent.ProjectileType<SpinSlash>()] < 1;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.statLife >= player.statLifeMax2)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<PureBeam>(), damage / 2, 0, player.whoAmI);
            }
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<CoiledNail>()
                .AddIngredient(ItemID.SoulofMight, 10)
                .AddIngredient(ItemID.SoulofSight, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

