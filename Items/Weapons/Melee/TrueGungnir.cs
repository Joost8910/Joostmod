using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons.Melee
{
    public class TrueGungnir : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("True Gungnir");
        }
        public override void SetDefaults()
        {
            Item.damage = 61;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 80;
            Item.height = 80;
            Item.useTime = 29;
            Item.useAnimation = 29;
            Item.scale = 1.15f;
            Item.knockBack = 7;
            Item.value = 500000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.TrueGungnir>();
            Item.shootSpeed = 7f;
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
                .AddIngredient(ItemID.Gungnir)
                .AddIngredient(ItemID.ChlorophyteBar, 24)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}


