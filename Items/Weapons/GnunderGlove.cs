using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Weapons
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class GnunderGlove : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gnunderson's Glove");
            Tooltip.SetDefault("'Glove of the legendary Gnunderson'\n" +
            "Does more damage as you kill bosses throughout the game\n" +
            "Throws lots of shurikens\n" +
            "Right click to throw a giant shuriken");
        }
        public override void SetDefaults()
        {
            item.damage = 100;
            item.thrown = true;
            item.width = 30;
            item.height = 28;
            item.useTime = 7;
            item.useAnimation = 7;
            item.useStyle = 1;
            item.knockBack = 3;
            item.rare = 9;
            item.UseSound = SoundID.Item7;
            item.noMelee = true;
            item.autoReuse = true;
            item.noUseGraphic = true;
            item.shoot = mod.ProjectileType("Gnunderken");
            item.shootSpeed = 20f;
            item.value = 300000;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Leather, 10);
            recipe.AddIngredient(null, "EvilStone");
            recipe.AddIngredient(null, "SkullStone");
            recipe.AddIngredient(null, "JungleStone");
            recipe.AddIngredient(null, "InfernoStone");
            recipe.AddTile(null, "ShrineOfLegends");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(0, 255, (int)(51 + (Main.DiscoG * 0.5f)));
                }
            }
        }
        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<JoostPlayer>().legendOwn = true;
        }
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            mult *= JoostGlobalItem.LegendaryDamage() * 0.08f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
            if (player.altFunctionUse == 2 && modPlayer.GnunderCool <= 0)
            {
                modPlayer.GnunderCool = 240;
                Projectile.NewProjectile(position.X, position.Y, speedX / 8, speedY / 8, mod.ProjectileType("GiantGnunderken"), damage * 3, knockBack * 3, player.whoAmI, 0.0f, 0.0f);
            }
            else
            {
                float spread = 15f * 0.0174f;
                float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
                double baseAngle = Math.Atan2(speedX, speedY);
                double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
                speedX = baseSpeed * (float)Math.Sin(randomAngle);
                speedY = baseSpeed * (float)Math.Cos(randomAngle);
            }
            return player.altFunctionUse != 2;
        }
    }
}

