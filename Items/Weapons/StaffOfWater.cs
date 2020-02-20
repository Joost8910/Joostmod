using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class StaffOfWater : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Staff of the sea");
            Tooltip.SetDefault("Creates multiple streams of water\n" + "Right click to fire bolts of water");
        }
        public override void SetDefaults()
        {
            item.damage = 35;
            item.magic = true;
            item.width = 64;
            item.height = 64;
            item.mana = 12;
            Item.staff[item.type] = true;
            item.useStyle = 5;
            item.noMelee = true;
            item.useTime = 5;
            item.useAnimation = 25;
            item.value = 225000;
            item.rare = 5;
            item.knockBack = 6;
            item.UseSound = SoundID.Item13;
            item.autoReuse = true;
            item.shoot = 22;
            item.shootSpeed = 15f;
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
                if (!player.CheckMana((int)(item.mana * 1.5f), false))
                {
                    return false;
                }
            }
            else
            {
                item.UseSound = SoundID.Item13;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float num = 3;
            float rotation = MathHelper.ToRadians(22.5f);
            position += Vector2.Normalize(new Vector2(speedX, speedY)) * 24f;
            int i;
            if (player.altFunctionUse == 2)
            {
                type = 27;
                if (player.itemAnimation >= item.useAnimation - 4)
                {
                    for (i = 0; i < num; i++)
                    {
                        Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (num - 1)));
                        Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X / 3, perturbedSpeed.Y / 3, type, (int)(damage * 1.1f), knockBack, player.whoAmI);
                    }
                    player.CheckMana(item.mana / 2, true);
                }
            }
            else
            {
                if (player.itemAnimation == item.useAnimation / 2)
                {
                    Main.PlaySound(item.UseSound, player.Center);
                }
                for (i = 0; i < num; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (num - 1)));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WaterEssence", 50);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 4);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 4);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 4);
            recipe.AddIngredient(ItemID.Sapphire, 3);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(this);
            recipe.AddRecipe();

        }

    }
}


