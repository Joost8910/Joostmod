//HAH! I did jousting lances before 1.4 did!
//Though now I gotta figure out what to do with this
//Gonna use the 1.4 jousting lance damage formula,
//will decide on behavioral changes later
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Melee
{
    public class UnicornLance : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unicorn Lance");
            Tooltip.SetDefault("Does more damage the faster you are moving");
        }
        public override void SetDefaults()
        {
            Item.damage = 42;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 50;
            Item.height = 50;
            Item.noMelee = true;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.reuseDelay = 10;
            Item.knockBack = 7;
            Item.value = 100000;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.shoot = Mod.Find<ModProjectile>("UnicornLance").Type;
            Item.shootSpeed = 18f;
        }


        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.UnicornHorn)
                .AddIngredient(ItemID.PixieDust, 10)
                .AddIngredient(ItemID.Pearlwood, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

