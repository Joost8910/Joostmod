using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using JoostMod.Items.Weapons.Thrown;

namespace JoostMod.Items.Weapons.Generic
{
    public class HellstoneSet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellfire Weapon Set");
            Tooltip.SetDefault("'Hot, HOT! Way too hot!'");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(48, 4));
        }
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Generic;
            Item.width = 32;
            Item.height = 38;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = 9000;
            Item.rare = ItemRarityID.Orange;
            Item.scale = 1f;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.HellstoneShuriken>();
            Item.shootSpeed = 12f;
            Item.crit = 4;
        }
        /*
        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            crit += (player.GetCritChance(DamageClass.Generic) + player.GetCritChance(DamageClass.Ranged) + player.GetCritChance(DamageClass.Magic) + player.GetCritChance(DamageClass.Throwing)) / 4;
        }
        */
        public override int ChoosePrefix(Terraria.Utilities.UnifiedRandom rand)
        {
            switch (rand.Next(24))
            {
                case 1:
                    return PrefixID.Agile;
                case 2:
                    return PrefixID.Annoying;
                case 3:
                    return PrefixID.Broken;
                case 4:
                    return PrefixID.Damaged;
                case 5:
                    return PrefixID.Deadly2;
                case 6:
                    return PrefixID.Demonic;
                case 7:
                    return PrefixID.Forceful;
                case 8:
                    return PrefixID.Godly;
                case 9:
                    return PrefixID.Hurtful;
                case 10:
                    return PrefixID.Keen;
                case 11:
                    return PrefixID.Lazy;
                case 12:
                    return PrefixID.Murderous;
                case 13:
                    return PrefixID.Nasty;
                case 14:
                    return PrefixID.Nimble;
                case 15:
                    return PrefixID.Quick;
                case 16:
                    return PrefixID.Ruthless;
                case 17:
                    return PrefixID.Shoddy;
                case 18:
                    return PrefixID.Slow;
                case 19:
                    return PrefixID.Sluggish;
                case 20:
                    return PrefixID.Strong;
                case 21:
                    return PrefixID.Superior;
                case 22:
                    return PrefixID.Unpleasant;
                case 23:
                    return PrefixID.Weak;
                default:
                    return PrefixID.Zealous;
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int wep = Main.rand.Next(4);
            if (wep == 1)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.Flamarang, damage, knockback, player.whoAmI);
            }
            if (wep == 2)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.HellfireArrow, damage, knockback, player.whoAmI);
            }
            if (wep == 3)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ProjectileID.BallofFire, damage, knockback, player.whoAmI);
            }
            if (wep == 0)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<Projectiles.Thrown.HellstoneShuriken>(), 1, knockback, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FlowerofFire)
                .AddIngredient(ItemID.MoltenFury)
                .AddIngredient(ItemID.Flamarang)
                .AddIngredient<HellstoneGlove>()
                .AddTile(TileID.Anvils)
                .Register();
        }

    }
}


