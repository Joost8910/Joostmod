using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class SandGlove : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sandstorm Glove");
            Tooltip.SetDefault("Rapidly throws sand\nUses sand blocks as ammo");
        }
        public override void SetDefaults()
        {
            item.damage = 28;
            item.thrown = true;
            item.width = 28;
            item.height = 30;
            item.useTime = 7;
            item.useAnimation = 7;
            item.useStyle = 1;
            item.noMelee = true;
			item.noUseGraphic = true;
            item.knockBack = 4;
            item.value = 225000;
            item.rare = 5;
            item.UseSound = SoundID.Item19;
            item.autoReuse = true;
            item.shoot = ProjectileID.SandBallGun;
            item.shootSpeed = 12.5f;
            item.useAmmo = AmmoID.Sand;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float spread = 7.5f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
            double baseAngle = Math.Atan2(speedX, speedY);
            double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
            speedX = baseSpeed * (float)Math.Sin(randomAngle);
            speedY = baseSpeed * (float)Math.Cos(randomAngle);
            if (type == ProjectileID.SandBallGun)
            {
                type = mod.ProjectileType("SandBlock");
            }
            if (type == ProjectileID.EbonsandBallGun)
            {
                type = mod.ProjectileType("EbonSandBlock");
            }
            if (type == ProjectileID.PearlSandBallGun)
            {
                type = mod.ProjectileType("PearlSandBlock");
            }
            if (type == ProjectileID.CrimsandBallGun)
            {
                type = mod.ProjectileType("CrimSandBlock");
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesertCore", 1);
            recipe.AddIngredient(ItemID.AdamantiteBar, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesertCore", 1);
            recipe.AddIngredient(ItemID.TitaniumBar, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}


