using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;

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
            "Rapidly throws shurikens\n" +
            "Right click for an evasive jump that throws many shurikens\n" +
            "Hold UP during the jump to throw a giant shuriken\n" +
            "(4 Second Cooldown");
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
            item.shootSpeed = 10f;
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
                    line2.overrideColor = new Color(0, 255, (int)(51 + (Main.DiscoG * 0.75f)));
                }
            }
        }
        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            return false;
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
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 0)
            {
                item.reuseDelay = 0;
                item.useTime = 7;
                item.useAnimation = 7;
                item.useStyle = 1;
            }
            else
            {
                player.direction = Main.MouseWorld.X < player.Center.X ? -1 : 1;
                if (player.velocity.Y == 0 || player.wingTime > 0 || player.rocketTime > 0 || player.jump > 0)
                {
                    player.jump = 0;
                    player.velocity.Y = -player.gravDir * 12;
                    player.velocity.X = -player.direction * 6;
                    player.fallStart = (int)(player.position.Y / 16f);
                    Main.PlaySound(42, player.Center, 204);
                    if (player.immuneTime < 20)
                    {
                        player.immune = true;
                        player.immuneTime = 20;
                    }
                }

                item.reuseDelay = 40;
                item.useAnimation = 40;
                item.useTime = 1;
                item.useStyle = 5;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
            if (player.altFunctionUse != 0)
            {
                if (player.itemAnimation < 20)
                {
                    if (player.itemAnimation == 19 && (player.wingTime > 0 || player.rocketTime > 0 || player.jump > 0))
                    {
                        player.wingTime -= 20;
                        player.rocketTime -= 3;
                        player.velocity.Y = -player.gravDir * 6;
                    }
                    if (modPlayer.LegendCool <= 0 && player.controlUp && player.itemAnimation == 19)
                    {
                        player.itemAnimation = 0;
                        player.reuseDelay = 60;
                        modPlayer.LegendCool = 240;
                        Main.PlaySound(SoundID.Item19, position);
                        Projectile.NewProjectile(position.X, position.Y, speedX / 3.5f, speedY / 3.5f, mod.ProjectileType("GiantGnunderken"), damage * 3, knockBack * 3, player.whoAmI, 0.0f, 0.0f);
                    }
                    else
                    {

                        float spread = 90f * 0.0174f;
                        float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
                        double startAngle = Math.Atan2(speedX, speedY) - spread / 2;
                        double deltaAngle = spread / 20f;
                        double offsetAngle;
                        int dir = player.direction < 0 ? player.itemAnimation : 20 - player.itemAnimation;
                        Main.PlaySound(2, player.Center, 19);
                        offsetAngle = startAngle + deltaAngle * dir;
                        Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockBack, player.whoAmI);
                    }
                }
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
            return player.altFunctionUse == 0;
        }
    }
}

