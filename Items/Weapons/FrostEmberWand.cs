using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class FrostEmberWand : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frostfire Wand");
            Tooltip.SetDefault("Fires a small frostflame");
        }
        public override void SetDefaults()
        {
            item.damage = 6;
            item.magic = true;
            item.width = 24;
            item.height = 24;
            item.mana = 3;
            item.useTime = 35;
            item.useAnimation = 35;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = 50;
            item.rare = 2;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("FrostFlame");
            item.shootSpeed = 9f;
        }
        public override void HoldItem(Player player)
        {
            if (Main.rand.Next(player.itemAnimation > 0 ? 40 : 80) == 0)
            {
                Dust.NewDust(new Vector2(player.itemLocation.X + 16f * player.direction, player.itemLocation.Y - 14f * player.gravDir), 4, 4, 67);
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IceTorch);
            recipe.AddIngredient(ItemID.BorealWood, 6);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }

    }
}


