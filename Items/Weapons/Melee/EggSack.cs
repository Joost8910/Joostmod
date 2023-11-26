using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Melee
{
    public class EggSack : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Egg Sack");
            Tooltip.SetDefault("'SPIDERS!'");
        }
        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 30;
            Item.height = 26;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2;
            Item.channel = true;
            Item.value = 50000;
            Item.rare = ItemRarityID.Pink;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = Mod.Find<ModProjectile>("EggSack").Type;
            Item.shootSpeed = 16f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SpiderFang, 7)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

