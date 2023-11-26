using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Tools.Rods
{
    public class LunarRod : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunar Rod");
            Tooltip.SetDefault("Fires multiple fishing hooks\n" + "Can fish up Lunar Fragments");
        }
        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 32;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = 100000;
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.shoot = Mod.Find<ModProjectile>("LunarFishHook2").Type;
            Item.shootSpeed = 17f;
            Item.fishingPole = 100;
        }
        public override void HoldItem(Player player)
        {
            player.GetModPlayer<JoostPlayer>().lunarRod = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FragmentNebula, 3)
                .AddIngredient(ItemID.FragmentSolar, 3)
                .AddIngredient(ItemID.FragmentVortex, 3)
                .AddIngredient(ItemID.FragmentStardust, 3)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 45f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 32;
            double deltaAngle = spread / 32f;
            double offsetAngle;
            int i;
            for (i = 0; i < 4; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockback, player.whoAmI);

            }

            return false;
        }
    }
}

