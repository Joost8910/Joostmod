using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class SandKunai : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sandstorm Kunai");
            Tooltip.SetDefault("Throws a spread of multiple kunais");
        }
        public override void SetDefaults()
        {
            item.damage = 44;
            item.thrown = true;
            item.width = 28;
            item.height = 30;
            item.useTime = 3;
            item.useAnimation = 15;
            item.reuseDelay = 12;
            item.useStyle = 6;
            item.noMelee = true;
			item.noUseGraphic = true;
            item.knockBack = 3.5f;
            item.value = 225000;
            item.rare = 5;
            item.UseSound = SoundID.Item7;
            item.autoReuse = true;
            item.shootSpeed = 13.5f;
            item.consumable = true;
            item.maxStack = 999;
            item.shoot = mod.ProjectileType("SandKunai");
        }
        public override bool UseItemFrame(Player player)
        {
            if (player.reuseDelay <= 0)
            {
                player.bodyFrame.Y = player.bodyFrame.Height * 4;
                return true;
            }
            if ((double)player.itemAnimation < (double)player.itemAnimationMax * 0.333)
            {
                player.bodyFrame.Y = player.bodyFrame.Height * 3;
                return true;
            }
            if ((double)player.itemAnimation < (double)player.itemAnimationMax * 0.666)
            {
                player.bodyFrame.Y = player.bodyFrame.Height * 2;
                return true;
            }
            player.bodyFrame.Y = player.bodyFrame.Height;
            return true;
        }
        public override bool ConsumeItem(Player player)
        {
            return player.reuseDelay > 0;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float spread = 60f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
            double startAngle = Math.Atan2(speedX, speedY) - spread / 2;
            double deltaAngle = spread / 18f;
            int dir = speedX > 0 ? player.itemAnimation : 17 - player.itemAnimation;
            double offsetAngle = startAngle + deltaAngle * dir;
            Main.PlaySound(2, player.Center, 39);
            Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockBack, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesertCore", 1);
            recipe.AddIngredient(ItemID.AdamantiteBar, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 999);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesertCore", 1);
            recipe.AddIngredient(ItemID.TitaniumBar, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 999);
            recipe.AddRecipe();
        }
    }
}


