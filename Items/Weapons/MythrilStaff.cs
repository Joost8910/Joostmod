using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class MythrilStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Twilight Staff");
            Tooltip.SetDefault("Spins around you firing bolts of light and night");
        }
        public override void SetDefaults()
        {
            item.damage = 40;
            item.magic = true;
            item.width = 54;
            item.height = 54;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useTime = 15;
            item.useAnimation = 15;
            item.reuseDelay = 5;
            item.autoReuse = true;
            item.mana = 5;
            item.channel = true;
            item.useStyle = 5;
            item.knockBack = 3;
            item.value = 60000;
            item.rare = 4;
            item.UseSound = SoundID.Item8;
            item.shoot = mod.ProjectileType("MythrilStaff");
            item.shootSpeed = 0;
            item.useTurn = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MythrilBar, 10);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}

