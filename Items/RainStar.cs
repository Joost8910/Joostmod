using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items
{
    public class RainStar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Weather Star - Rain");
            Tooltip.SetDefault("'Make it rain!'");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 22;
            item.height = 22;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 4;
            item.noUseGraphic = true;
            item.noMelee = true; 
            item.value = 7500;
            item.rare = 1;
            item.UseSound = SoundID.Item9;
            item.shoot = mod.ProjectileType("RainStar");
            item.shootSpeed = 15;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            speedX = 0;
            speedY = -15;
            return true;
        }
    }
}

