using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace JoostMod.Items.Weapons
{
    public class JumboCactuarSet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jumbo Cactuar Weapon Set");
            Tooltip.SetDefault("'Very Prickly'");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(48, 4));
        }
        public override void SetDefaults()
        {
            Item.damage = 450;
            Item.DamageType = DamageClass.Generic;
            Item.width = 128;
            Item.height = 200;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 8;
            Item.value = 10000000;
            Item.rare = ItemRarityID.Purple;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("GiantNeedle").Type;
            Item.shootSpeed = 16f;
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
                Projectile.NewProjectile(source, position.X, position.Y, (velocity.X), (velocity.Y), Mod.Find<ModProjectile>("GiantArrow").Type, damage, knockback, player.whoAmI);
            }
            if (wep == 2)
            {
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(5));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, Mod.Find<ModProjectile>("GiantNeedle").Type, (int)(damage * 0.62f), knockback, player.whoAmI);
            }
            if (wep == 3)
            {
                float numberProjectiles = 2;
                float rotation = MathHelper.ToRadians(12);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X * 2, perturbedSpeed.Y * 2, Mod.Find<ModProjectile>("Splitend").Type, (int)(damage * 0.67f), knockback, player.whoAmI);
                }
            }
            if (wep == 0)
            {
                for (int i = 0; i < 12; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(15));
                    float scale = 1f - (Main.rand.NextFloat() * .3f);
                    perturbedSpeed = perturbedSpeed * scale;
                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, Mod.Find<ModProjectile>("Needle3").Type, (damage / 5), knockback, player.whoAmI);
                }
            }
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Jumbow>()
                .AddIngredient<GiantNeedle>()
                .AddIngredient<EvenMoreNeedles>()
                .AddIngredient<Mustache>()
                .Register();
        }

    }
}


