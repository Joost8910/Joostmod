using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Melee;

namespace JoostMod.Items.Weapons.Melee
{
    public class ChanneledNail : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Channeled Nail");
            Tooltip.SetDefault("Hold attack to charge a great slash!\n" +
            "Unleash it forward while dashing for a long ranged dash slash!");
        }
        public override void SetDefaults()
        {
            Item.damage = 27;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 38;
            Item.height = 38;
            Item.noMelee = true;
            Item.useTime = 13;
            Item.useAnimation = 13;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.knockBack = 5;
            Item.value = 50000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item18;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.ChanneledNail>();
            Item.shootSpeed = 10f;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] + player.ownedProjectileCounts[ModContent.ProjectileType<ChanneledNail2>()] + player.ownedProjectileCounts[ModContent.ProjectileType<GreatSlash>()] + player.ownedProjectileCounts[ModContent.ProjectileType<DashSlash>()] < 1;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<SharpenedNail>()
                .AddIngredient(ItemID.DemoniteBar, 10)
                .AddIngredient(ItemID.Bone, 30)
                .AddTile(TileID.Anvils)
                .Register();
            CreateRecipe()
            .AddIngredient<SharpenedNail>()
            .AddIngredient(ItemID.CrimtaneBar, 10)
            .AddIngredient(ItemID.Bone, 30)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}

