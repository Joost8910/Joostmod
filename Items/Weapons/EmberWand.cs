using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class EmberWand : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ember Wand");
            Tooltip.SetDefault("Fires a small flame");
        }
        public override void SetDefaults()
        {
            item.damage = 5;
            item.magic = true;
            item.width = 24;
            item.height = 24;
            item.mana = 3;
            item.useTime = 35;
            item.useAnimation = 35;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = 10;
            item.rare = 1;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Flame");
            item.shootSpeed = 7f;
        }
        public override void HoldItem(Player player)
        {
            if (Main.rand.Next(player.itemAnimation > 0 ? 40 : 80) == 0)
            {
                Dust.NewDust(new Vector2(player.itemLocation.X + 16f * player.direction, player.itemLocation.Y - 14f * player.gravDir), 4, 4, 6);
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Torch);
            recipe.AddRecipeGroup("Wood", 6);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }

    }
}


