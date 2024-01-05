using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons.Melee
{
    public class TerraSpear : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Spear");
        }
        public override void SetDefaults()
        {
            Item.damage = 75;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 100;
            Item.height = 100;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.scale = 1.2f;
            Item.knockBack = 7;
            Item.value = 1000000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.TerraSpear>();
            Item.shootSpeed = 8f;
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
                .AddIngredient<TrueDarkLance>()
                .AddIngredient<TrueGungnir>()
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}


