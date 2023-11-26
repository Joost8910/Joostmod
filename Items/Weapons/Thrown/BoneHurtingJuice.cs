using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Thrown
{
    public class BoneHurtingJuice : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bone Hurting Juice");
            Tooltip.SetDefault("Debuff damage steadily increases over time\n" +
                "Debuff deals double damage against skeletal creatures\n" +
                "'Ow, oof, ouch, my bones'");
        }
        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.DamageType = DamageClass.Throwing;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 20;
            Item.height = 24;
            Item.useTime = 27;
            Item.useAnimation = 27;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 3;
            Item.value = 50;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item106;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("BoneHurtingJuiceBottle").Type;
            Item.shootSpeed = 8f;
        }
        public override void AddRecipes()
        {
            CreateRecipe(20)
                .AddIngredient(ItemID.BottledWater, 20)
                .AddIngredient(ItemID.Blinkroot)
                .AddIngredient(ItemID.Deathweed)
                .AddTile(TileID.Bottles)
                .Register();
        }

    }
}

