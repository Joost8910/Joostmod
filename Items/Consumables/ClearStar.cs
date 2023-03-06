using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Consumables
{
    public class ClearStar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Weather Star - Clear");
            Tooltip.SetDefault("'Praise the sun!'");
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
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item9;
            Item.shoot = Mod.Find<ModProjectile>("ClearStar").Type;
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

