using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Utilities;
using JoostMod.Items.Weapons.Thrown;
using JoostMod.Items.Weapons.Magic;

namespace JoostMod.Items.Weapons.Generic
{
    public class AdamantiteSet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Adamantite Weapon Set");
            Tooltip.SetDefault("'ALL the adamantite!'");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(48, 4));
        }
        public override void SetDefaults()
        {
            Item.damage = 55;
            Item.DamageType = DamageClass.Generic;
            Item.width = 52;
            Item.height = 58;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = 150000;
            Item.rare = ItemRarityID.Pink;
            Item.scale = 1f;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.AdamantiteChainedchainsaw>();
            Item.shootSpeed = 6f;
            Item.crit = 4;
        }

        public override bool WeaponPrefix()
        {
            return true;
        }
        /*
        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            crit += (player.GetCritChance(DamageClass.Generic) + player.GetCritChance(DamageClass.Ranged) + player.GetCritChance(DamageClass.Magic) + player.GetCritChance(DamageClass.Throwing)) / 4;
        }
        */
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[66] > 0)
            {
                return false;
            }
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int wep = Main.rand.Next(4);
            if (wep == 1)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 3, velocity.Y * 3, 278, damage, knockback, player.whoAmI);
            }
            if (wep == 2)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 2, velocity.Y * 2, 66, damage, knockback, player.whoAmI);
            }
            if (wep == 3)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 3, velocity.Y * 3, ModContent.ProjectileType<Projectiles.Magic.GreenLaser>(), damage, knockback, player.whoAmI);
            }
            if (wep == 0)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 3, velocity.Y * 3, ModContent.ProjectileType<Projectiles.Thrown.AdamantiteChainedchainsaw>(), damage, knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AdamantiteGlaive)
                .AddIngredient(ItemID.AdamantiteRepeater)
                .AddIngredient<AdamantiteStaff>()
                .AddIngredient<AdamantiteChainedchainsaw>(4)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

    }
}


