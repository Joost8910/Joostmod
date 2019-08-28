using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items
{
    public class DukeFishingGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Duke Fishgun");
            Tooltip.SetDefault("Fires many fishing hooks");
        }
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 26;
            item.damage = 50;
            item.ranged = true;
            item.knockBack = 0;
            item.useTime = 8;
            item.useAnimation = 8;
            item.useStyle = 1;
            item.value = 1000000;
            item.rare = 8;
            item.UseSound = SoundID.Item36;
            item.autoReuse = false;
            item.shoot = mod.ProjectileType("DukeFishHook");
            item.shootSpeed = 19f;
            item.fishingPole = 75;
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            if (JoostMod.instance.battleRodsLoaded)
            {
                mult *= BattleRodsFishingDamage / player.rangedDamage;
            }
        }
        public override void GetWeaponCrit(Player player, ref int crit)
        {
            if (JoostMod.instance.battleRodsLoaded)
            {
                crit += BattleRodsCrit - player.rangedCrit;
            }
        }
        public float BattleRodsFishingDamage
        {
            get { Player player = Main.player[Main.myPlayer]; return player.GetModPlayer<UnuBattleRods.FishPlayer>(ModLoader.GetMod("UnuBattleRods")).bobberDamage; }
        }
        public int BattleRodsCrit
        {
            get { Player player = Main.player[Main.myPlayer]; return player.GetModPlayer<UnuBattleRods.FishPlayer>(ModLoader.GetMod("UnuBattleRods")).bobberCrit; }
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            if (JoostMod.instance.battleRodsLoaded)
            {
                Player player = Main.player[Main.myPlayer];
                int dmg = list.FindIndex(x => x.Name == "Damage");
                list.RemoveAt(dmg);
                list.Insert(dmg, new TooltipLine(mod, "Damage", player.GetWeaponDamage(item) + " Fishing damage"));
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DukeFishRod", 1);
            recipe.AddIngredient(null, "SuperFishingGun", 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GoldenFishingGun", 1);
            recipe.AddIngredient(null, "DukeFishRod", 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MechanicalFishingGun", 1);
            recipe.AddIngredient(null, "DukeFishRod", 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FishingGun", 1);
            recipe.AddIngredient(null, "DukeFishRod", 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Boomstick, 1);
            recipe.AddIngredient(null, "DukeFishRod", 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float spread = 65f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
            double startAngle = Math.Atan2(speedX, speedY) - spread / 2;
            double deltaAngle = spread / 16f;
            double offsetAngle;
            int i;
            for (i = 0; i < 16; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}

