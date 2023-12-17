using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Consumables
{
    public class SandstormStar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Weather Star - Sandstorm");
            Tooltip.SetDefault("'Darude - Sandstorm'");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 22;
            Item.height = 22;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noUseGraphic = true;
            Item.noMelee = true; 
            Item.value = 7500;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item9;
            Item.shoot = ModContent.ProjectileType<Projectiles.SandstormStar>();
            Item.shootSpeed = 15;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            velocity.X = 0;
            velocity.Y = -15;
            return true;
        }
    }
}

