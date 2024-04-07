using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Magic
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
            Item.damage = 35;
            Item.DamageType = DamageClass.Magic;
            Item.width = 64;
            Item.height = 64;
            Item.mana = 12;
            Item.staff[Item.type] = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.useTime = 6;
            Item.useAnimation = 30;
            Item.value = 225000;
            Item.rare = ItemRarityID.Pink;
            Item.knockBack = 6;
            Item.UseSound = SoundID.Item13;
            Item.autoReuse = true;
            Item.shoot = 22;
            Item.shootSpeed = 15f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.UseSound = SoundID.Item21;
                if (!player.CheckMana((int)(Item.mana * 1.5f), false))
                {
                    return false;
                }
            }
            else
            {
                Item.UseSound = SoundID.Item13;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float num = 3;
            float rotation = MathHelper.ToRadians(22.5f);
            position += Vector2.Normalize(velocity) * 24f;
            int i;
            if (player.altFunctionUse == 2)
            {
                type = 27;
                if (player.itemAnimation >= Item.useAnimation - 5)
                {
                    for (i = 0; i < num; i++)
                    {
                        Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (num - 1)));
                        Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X / 3, perturbedSpeed.Y / 3, type, (int)(damage * 1.2f), knockback, player.whoAmI);
                    }
                    player.CheckMana(Item.mana / 2, true);
                }
            }
            else
            {
                if (player.itemAnimation == Item.useAnimation / 2)
                {
                    SoundEngine.PlaySound(Item.UseSound, player.Center);
                }
                for (i = 0; i < num; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (num - 1)));
                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
                }
            }
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.WaterEssence>(50)
                .AddRecipeGroup(nameof(ItemID.CobaltBar), 4)
                .AddRecipeGroup(nameof(ItemID.MythrilBar), 4)
                .AddRecipeGroup(nameof(ItemID.AdamantiteBar), 4)
                .AddIngredient(ItemID.Sapphire, 3)
                .AddTile<Tiles.ElementalForge>()
                .Register();

        }

    }
}


