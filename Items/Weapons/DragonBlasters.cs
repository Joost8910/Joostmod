using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons
{
    public class DragonBlasters : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dragon Blasters");
			Tooltip.SetDefault("Left and right click to fire each gun\n" +
                "Hold the attack down to charge a blast of fire\n" +
                "35% chance to not consume ammo");
		}
		public override void SetDefaults()
		{
			item.damage = 24;
			item.ranged = true;
			item.width = 52;
			item.height = 36;
			item.useTime = 11;
			item.useAnimation = 11;
			item.useStyle = 5;
			item.knockBack = 3;
			item.value = 250000;
			item.rare = 5;
			item.UseSound = SoundID.Item7;
			item.autoReuse = true;
			item.noUseGraphic = true;
			item.channel = true;
			item.noMelee = true;
			item.shoot = mod.ProjectileType("DragonBlaster");
			item.shootSpeed = 13f;
        }
        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() > 0.35f;
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
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, 1);
                return false;
            }
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
        public override void AddRecipes()
		{
				ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "FireEssence", 50);
			recipe.AddIngredient(ItemID.PhoenixBlaster, 2);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 4);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 4);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 4);
            recipe.AddTile(null, "ElementalForge");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

