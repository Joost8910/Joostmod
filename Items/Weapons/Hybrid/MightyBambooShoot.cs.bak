using JoostMod.DamageClasses;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons.Hybrid
{
    public class MightyBambooShoot : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mighty Bamboo Shoot");
            Tooltip.SetDefault("Left click to swing\n" +
                "Right click to charge a seed barrage\n" +
                "Allows the collection of seeds for ammo");
        }
        public override void SetDefaults()
        {
            Item.damage = 17;
            Item.DamageType = ModContent.GetInstance<MeleeRangedHybrid>();
            Item.width = 100;
            Item.height = 12;
            Item.noMelee = true;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.knockBack = 6;
            Item.value = 20000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item7;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.shoot = ProjectileID.Seed;
            Item.shootSpeed = 11f;
            //Item.useAmmo = AmmoID.Dart;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        /*
        public override bool? CanChooseAmmo(Item ammo, Player player)
        {
            return ammo.ammo == AmmoID.Dart;
        }
        */
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useAmmo = AmmoID.Dart;
            }
            else
            {
                Item.useAmmo = AmmoID.None;
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Hybrid.BambooShoot>()] < 1)
            {
                return base.CanUseItem(player);
            }
            return false;
        }
        public override bool MeleePrefix()
        {
            return Main.rand.NextBool(2);
        }
        public override bool WeaponPrefix()
        {
            return false;
        }
        public override bool RangedPrefix()
        {
            return true;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return player.altFunctionUse == 2 && !player.ItemAnimationJustStarted;
        }
        public override bool NeedsAmmo(Player player)
        {
            return player.altFunctionUse == 2;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int mode = 0;
            if (player.altFunctionUse == 2)
            {
                mode = 1;
            }
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<Projectiles.Hybrid.BambooShoot>(), damage, knockback, player.whoAmI, mode);
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Blowpipe)
                .AddIngredient(ItemID.BreathingReed)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}

