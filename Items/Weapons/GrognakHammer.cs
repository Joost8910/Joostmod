using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Weapons
{
    public class GrognakHammer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Warhammer of Grognak");
            Tooltip.SetDefault("'Warhammer of the legendary Grognak'\n" +
            "Does more damage as you kill bosses throughout the game\n" +
            "Right click to unleash a mighty shockwave attack\n" +
            "Does not function as a tool hammer");
        }
        public override void SetDefaults()
        {
            item.damage = 100;
            item.melee = true;
            item.width = 64;
            item.height = 62;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 7;
            item.rare = 9;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.value = 200000;
            item.shootSpeed = 9f;
            item.shoot = mod.ProjectileType("GrognakBeam");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GoldHammer);
            recipe.AddIngredient(null, "EvilStone");
            recipe.AddIngredient(null, "SkullStone");
            recipe.AddIngredient(null, "JungleStone");
            recipe.AddIngredient(null, "InfernoStone");
            recipe.AddTile(null, "ShrineOfLegends");
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PlatinumHammer);
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
            player.GetModPlayer<JoostPlayer>(mod).legendOwn = true;
        }
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            mult *= JoostGlobalItem.LegendaryDamage() * 0.18f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(player.position.X, player.position.Y, speedX, speedY, mod.ProjectileType("GrognakHammer"), damage * 2, knockBack, player.whoAmI);
                item.noMelee = true;
                item.noUseGraphic = true;
                return false;
            }
            else
            {
                item.noMelee = false;
                item.noUseGraphic = false;
                return true;
            }

        }
    }
}

