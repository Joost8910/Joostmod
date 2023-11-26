using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Thrown
{
    public class MechanicalSphere : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mechanical Sphere");
            Tooltip.SetDefault("'Unleash mechanical power'");
        }
        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 34;
            Item.height = 36;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 7;
            Item.value = 0;
            Item.rare = ItemRarityID.Pink;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("MechanicalSphere").Type;
            Item.shootSpeed = 6f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddIngredient(ItemID.SoulofSight, 7)
                .AddIngredient(ItemID.SoulofMight, 7)
                .AddIngredient(ItemID.SoulofFright, 7)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

