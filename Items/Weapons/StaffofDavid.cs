using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons
{
    public class StaffofDavid : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Staff of David");
            Tooltip.SetDefault("'Staff of the legendary David'\n" +
            "Does more damage as you kill bosses throughout the game\n" +
            "Unleashes a defense-ignoring, rapidly damaging laser\n" +
            "Right click to fire three magical bolts");
        }
        public override void SetDefaults()
        {
            item.damage = 100;
            item.noMelee = true;
            item.magic = true;
            item.channel = true;
            item.mana = 30;
            item.rare = 9;
            item.width = 56;
            item.height = 56;
            item.useTime = 30;
            item.useAnimation = 30;
            item.UseSound = SoundID.Item8;
            item.useStyle = 5;
            Item.staff[item.type] = true;
            item.shootSpeed = 14f;
            item.shoot = mod.ProjectileType("DavidLaser");
            item.value = 300000;
            item.GetGlobalItem<JoostGlobalItem>().glowmaskTex = mod.GetTexture("Items/Weapons/StaffofDavidCrystal");
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            item.GetGlobalItem<JoostGlobalItem>().glowmaskColor = new Color(90, 255, (int)(51 + (Main.DiscoG * 0.75f)));
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            item.GetGlobalItem<JoostGlobalItem>().glowmaskColor = new Color(90, 255, (int)(51 + (Main.DiscoG * 0.75f)));
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.RubyStaff);
            recipe.AddIngredient(null, "EvilStone");
            recipe.AddIngredient(null, "SkullStone");
            recipe.AddIngredient(null, "JungleStone");
            recipe.AddIngredient(null, "InfernoStone");
            recipe.AddTile(null, "ShrineOfLegends");
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DiamondStaff);
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
        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<JoostPlayer>().legendOwn = true;
        }
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            mult *= JoostGlobalItem.LegendaryDamage() * 0.02f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            return false;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 65;
                if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                {
                    position += muzzleOffset;
                }
                float spread = 90f * 0.0174f;
                float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
                double startAngle = Math.Atan2(speedX, speedY) - spread / 2;
                double deltaAngle = spread / 2f;
                double offsetAngle;
                int i;
                for (i = 0; i < 3; i++)
                {
                    offsetAngle = startAngle + deltaAngle * i;
                    Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType("DavidBolt"), damage * 7, 3, player.whoAmI, i);
                }
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
