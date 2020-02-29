using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class SearingBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Volcanic Longbow");
			Tooltip.SetDefault("Transforms wooden arrows into Volcanic Arrows\n"+
                "Volcanic Arrows deal 50% more damage and drop a trail of damaging lava droplets\n" +
                "Right clicking nocks additional arrows\n" + 
                "Can nock up to 5 arrows");
		}
		public override void SetDefaults()
		{
			item.damage = 48;
			item.ranged = true;
			item.width = 52;
			item.height = 36;
			item.useTime = 24;
			item.useAnimation = 24;
			item.useStyle = 5;
			item.knockBack = 4.5f;
			item.value = 250000;
			item.rare = 5;
			item.UseSound = SoundID.Item7;
			item.autoReuse = true;
			item.noUseGraphic = true;
			item.channel = true;
			item.noMelee = true;
			item.shoot = mod.ProjectileType("SearingBow");
			item.shootSpeed = 13f;
            item.useAmmo = AmmoID.Arrow;
        }
        public override bool ConsumeAmmo(Player player)
        {
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[item.shoot] > 0)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
        public override bool AltFunctionUse(Player player)
        {
            return (player.ownedProjectileCounts[item.shoot] <= 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            type = mod.ProjectileType("SearingBow");
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "FireEssence", 50);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 4);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 4);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 4);
            recipe.AddTile(null, "ElementalForge");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

