using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Melee
{
    public class SharpenedNail : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sharpened Nail");
            Tooltip.SetDefault("Hold attack to charge a great slash!");
        }
        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 34;
            Item.height = 34;
            Item.noMelee = true;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.knockBack = 4;
            Item.value = 25000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item18;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.shoot = Mod.Find<ModProjectile>("SharpenedNail").Type;
            Item.shootSpeed = 8f;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("GreatSlash").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("SharpenedNail2").Type] < 1;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<OldNail>()
                .AddIngredient(ItemID.SilverBar, 10)
                .AddIngredient(ItemID.Marble, 30)
                .AddTile(TileID.Anvils)
                .Register();
            CreateRecipe()
                .AddIngredient<OldNail>()
                .AddIngredient(ItemID.TungstenBar, 10)
                .AddIngredient(ItemID.Marble, 30)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}

