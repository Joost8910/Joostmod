using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Melee
{
    public class CoiledNail : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Coiled Nail");
            Tooltip.SetDefault("Hold attack to charge a great slash!\n" +
            "Unleash it forward while dashing for a long ranged dash slash!\n" +
            "Unleash it while holding up or down for a spin attack!");
        }
        public override void SetDefaults()
        {
            Item.damage = 44;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 40;
            Item.height = 40;
            Item.noMelee = true;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.knockBack = 6;
            Item.value = 100000;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item18;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.shoot = Mod.Find<ModProjectile>("CoiledNail").Type;
            Item.shootSpeed = 12f;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("CoiledNail2").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("GreatSlash").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("DashSlash").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("SpinSlash").Type] < 1;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ChanneledNail>()
                .AddIngredient(ItemID.SoulofLight, 5)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

