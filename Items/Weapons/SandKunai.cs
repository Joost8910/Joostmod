using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
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
            Item.damage = 44;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 28;
            Item.height = 30;
            Item.useTime = 3;
            Item.useAnimation = 15;
            Item.reuseDelay = 12;
            Item.useStyle = 6;
            Item.noMelee = true;
			Item.noUseGraphic = true;
            Item.knockBack = 3.5f;
            Item.value = 225000;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item7;
            Item.autoReuse = true;
            Item.shootSpeed = 13.5f;
            Item.consumable = true;
            Item.maxStack = 999;
            Item.shoot = Mod.Find<ModProjectile>("SandKunai").Type;
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
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 60f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
            double deltaAngle = spread / 18f;
            int dir = velocity.X > 0 ? player.itemAnimation : 17 - player.itemAnimation;
            double offsetAngle = startAngle + deltaAngle * dir;
            SoundEngine.PlaySound(SoundID.Item39, player.Center);
            Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockback, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe(999)
                .AddIngredient<Materials.DesertCore>()
                .AddRecipeGroup("JoostMod:AnyAdamantite")
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}


