using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class WaterWhip : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Tendril");
            Tooltip.SetDefault("Left click for a slapping tendril\n" + 
                "Damage dealt is based on how much the tendril moves\n" +
                "Right click for a grasping tendril\n" +
                "Grabs hit enemies and items");
        }
        public override void SetDefaults()
        {
            item.damage = 60;
            item.magic = true;
            item.width = 36;
            item.height = 36;
            item.mana = 10;
            item.channel = true;
            item.useStyle = 5;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.useTime = 26;
            item.useAnimation = 26;
            item.reuseDelay = 2;
            item.value = 225000;
            item.rare = 5;
            item.knockBack = 8;
            item.UseSound = SoundID.Item21;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("WaterWhip");
            item.shootSpeed = 5f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.UseSound = SoundID.Item21;
            }
            else
            {
                item.UseSound = SoundID.Item21;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                type = mod.ProjectileType("WaterWhip2");
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WaterEssence", 50);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 4);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 4);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 4);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();

        }

    }
}


