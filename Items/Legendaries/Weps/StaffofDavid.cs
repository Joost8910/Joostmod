using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Utilities;
using JoostMod.Items.Legendaries;
using JoostMod.Projectiles.Magic;

namespace JoostMod.Items.Legendaries.Weps
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
            Item.damage = 100;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.channel = true;
            Item.mana = 30;
            Item.rare = ItemRarityID.Cyan;
            Item.width = 56;
            Item.height = 56;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item8;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.staff[Item.type] = true;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<DavidLaser>();
            Item.value = 300000;
            Item.GetGlobalItem<JoostGlobalItem>().glowmaskTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_Crystal");
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Item.GetGlobalItem<JoostGlobalItem>().glowmaskColor = new Color(90, 255, (int)(51 + Main.DiscoG * 0.75f));
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Item.GetGlobalItem<JoostGlobalItem>().glowmaskColor = new Color(90, 255, (int)(51 + Main.DiscoG * 0.75f));
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.RubyStaff)
                .AddIngredient<EvilStone>()
                .AddIngredient<SkullStone>()
                .AddIngredient<JungleStone>()
                .AddIngredient<InfernoStone>()
                .AddTile<Tiles.ShrineOfLegends>()
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.DiamondStaff)
                .AddIngredient<EvilStone>()
                .AddIngredient<SkullStone>()
                .AddIngredient<JungleStone>()
                .AddIngredient<InfernoStone>()
                .AddTile<Tiles.ShrineOfLegends>()
                .Register();
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(0, 255, (int)(51 + Main.DiscoG * 0.75f));
                }
            }
        }
        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<JoostPlayer>().legendOwn = true;
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage *= JoostGlobalItem.LegendaryDamage() * 0.02f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            return false;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Vector2 muzzleOffset = Vector2.Normalize(velocity) * 65;
                if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                {
                    position += muzzleOffset;
                }
                float spread = 90f * 0.0174f;
                float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
                double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
                double deltaAngle = spread / 2f;
                double offsetAngle;
                int i;
                for (i = 0; i < 3; i++)
                {
                    offsetAngle = startAngle + deltaAngle * i;
                    Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), ModContent.ProjectileType<DavidBolt>(), damage * 7, 3, player.whoAmI, i);
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
