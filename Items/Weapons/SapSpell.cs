using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class SapSpell : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Staff of Degeneration");
            Tooltip.SetDefault("Inflicts a damage over time effect on struck enemies\n" +
                "Homes in on the clicked target");
        }
        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.width = 36;
            Item.height = 34;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.mana = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.staff[Item.type] = true;
            Item.knockBack = 0f;
            Item.value = 20000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = false;
            Item.shoot = Mod.Find<ModProjectile>("SapSpell").Type;
            Item.shootSpeed = 12f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double baseAngle = Math.Atan2(velocity.X, velocity.Y);
            float spread = 60f * 0.0174f;
            double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
            if (!Collision.CanHitLine(position, 6, 6, Main.MouseWorld, 1, 1))
            {
                spread = 225f * 0.0174f;
                randomAngle = baseAngle + (Main.rand.Next(2) - 0.5f) * spread;
            }
            velocity.X = baseSpeed * (float)Math.Sin(randomAngle);
            velocity.Y = baseSpeed * (float)Math.Cos(randomAngle);
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, Main.MouseWorld.X, Main.MouseWorld.Y);
            return false;
        }
    }
}

