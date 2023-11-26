using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using JoostMod.Items.Weapons.Magic;
using JoostMod.Items.Weapons.Ranged;
using JoostMod.Items.Weapons.Thrown;

namespace JoostMod.Items.Weapons.Generic
{
    public class SAXSet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SA-X Weapon Set");
            Tooltip.SetDefault("'Cold and explosive'");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(48, 4));
        }
        public override void SetDefaults()
        {
            Item.damage = 500;
            Item.DamageType = DamageClass.Generic;
            Item.width = 76;
            Item.height = 50;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 8;
            Item.value = 10000000;
            Item.rare = ItemRarityID.Purple;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("IceBeam").Type;
            Item.shootSpeed = 16f;
            Item.crit = 4;
        }
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
            if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("PowerBomb").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("PowerBombExplosion").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("PowerBombExplosion2").Type] >= 1)
            {
                wep = Main.rand.Next(3);
            }
            if (wep == 1)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("SuperMissile").Type, (int)(damage * 1.6f), knockback, player.whoAmI);
                SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/SuperMissileShoot"), position);
            }
            if (wep == 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(60));
                    float scale = 1f - Main.rand.NextFloat() * .3f;
                    perturbedSpeed = perturbedSpeed * scale;
                    Projectile.NewProjectile(source, position, perturbedSpeed, Mod.Find<ModProjectile>("Missile").Type, (int)(damage * 0.53f), knockback, player.whoAmI);
                    SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/MissileShoot"), position);
                }
            }
            if (wep == 3)
            {
                float distance = player.Distance(Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY));
                velocity.Normalize();
                velocity.X = velocity.X * (distance / 60);
                velocity.Y = velocity.Y * (distance / 60);
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("PowerBomb").Type, damage, knockback, player.whoAmI);
                SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/LayBomb"), position);
            }
            if (wep == 0)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("IceBeam").Type, (int)(damage * 0.9f), knockback, player.whoAmI);
                SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/IceBeam"), position);
            }


            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<SuperMissileLauncher>()
                .AddIngredient<PowerBomb>()
                .AddIngredient<IceBeam>()
                .AddIngredient<MutantCannon>()
                .Register();
        }

    }
}


