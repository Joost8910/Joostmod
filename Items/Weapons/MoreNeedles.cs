using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class MoreNeedles : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("1000 Needles");
            Tooltip.SetDefault("Unleashes a storm of needles");
        }
        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 20;
            Item.width = 28;
            Item.height = 30;
            Item.useTime = 5;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1.5f;
            Item.value = 10000;
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("Needle2").Type;
            Item.shootSpeed = 16f;
        }
        /*
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage = 1;
        }
        */
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            Player player = Main.player[Main.myPlayer];
            int dmg = list.FindIndex(x => x.Name == "Damage");
            list.RemoveAt(dmg);
            list.Insert(dmg, new TooltipLine(Mod, "Damage", 1 + " magic damage"));
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Needles>()
                .AddIngredient(ItemID.SpellTome)
                .AddIngredient(ItemID.StyngerBolt, 10)
                .AddIngredient(ItemID.SpectreBar, 10)
                .AddIngredient<Needle>(1000)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            damage = 1;
            float spread = 15f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            /*double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
            double deltaAngle = spread / 5f;
            double offsetAngle;
            int i;
            for (i = 0; i < 5; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockback, player.whoAmI);
            }*/

            for (int i = 0; i < 5; i++)
            {
                double baseAngle = Math.Atan2(velocity.X, velocity.Y);
                double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
                Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(randomAngle), baseSpeed * (float)Math.Cos(randomAngle), type, damage, knockback, player.whoAmI);
            }

            SoundEngine.PlaySound(SoundID.Item7, position);
            return false;
        }
    }
}

