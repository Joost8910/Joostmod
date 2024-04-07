using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using JoostMod.Projectiles.Magic;

namespace JoostMod.Items.Weapons.Magic
{
    public class DualityStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Staff of Duality");
            Tooltip.SetDefault("'Find your inner pieces'");
        }
        public override void SetDefaults()
        {
            Item.damage = 28;
            Item.mana = 4;
            Item.width = 48;
            Item.height = 48;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item8;
            Item.useStyle = ItemUseStyleID.Shoot;
            //Item.staff[item.type] = true; 
            Item.knockBack = 0;
            Item.value = 144000;
            Item.rare = ItemRarityID.Pink;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.channel = true;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<LightLaser>();
            Item.shootSpeed = 12f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position.X, position.Y, -velocity.X, -velocity.Y, ModContent.ProjectileType<DarkLaser>(), damage, knockback, player.whoAmI);
            return true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-24, 0);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LightShard, 1)
                .AddIngredient(ItemID.DarkShard, 1)
                .AddIngredient(ItemID.SoulofLight, 7)
                .AddIngredient(ItemID.SoulofNight, 7)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

