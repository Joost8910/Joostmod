using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons.Melee
{
    public class TrueDarkLance : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Dark Lance");
        }
        public override void SetDefaults()
        {
            Item.damage = 66;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 80;
            Item.height = 80;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.scale = 1.1f;
            Item.knockBack = 9;
            Item.value = 500000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.TrueDarkLance>();
            Item.shootSpeed = 5f;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
        public override bool MeleePrefix()
        {
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DarkLance)
                .AddIngredient<Materials.BrokenHeroSpear>()
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}


