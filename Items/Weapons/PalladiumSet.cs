using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace JoostMod.Items.Weapons
{
    public class PalladiumSet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Palladium Weapon Set");
            Tooltip.SetDefault("'ALL the palladium!'");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(48, 4));
        }
        public override void SetDefaults()
        {
            Item.damage = 38;
            Item.DamageType = DamageClass.Generic;
            Item.width = 58;
            Item.height = 58;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = 120000;
            Item.rare = ItemRarityID.Pink;
            Item.scale = 1f;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("PalladiumChainedchainsaw").Type;
            Item.shootSpeed = 1f;
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
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[212] > 0)
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
                Projectile.NewProjectile(source, position.X, position.Y, (velocity.X * 12), (velocity.Y * 12), 172, (damage), knockback, player.whoAmI);
            }
            if (wep == 2)
            {
                Projectile.NewProjectile(source, position.X, position.Y, (velocity.X * 8), (velocity.Y * 8), 212, (damage), knockback, player.whoAmI);
            }
            if (wep == 3)
            {
                float distance = player.Distance(Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY));
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * (distance / 30), velocity.Y * (distance / 30), Mod.Find<ModProjectile>("CrystalChunk").Type, (damage), 0, player.whoAmI);
            }
            if (wep == 0)
            {
                Projectile.NewProjectile(source, position.X, position.Y, (velocity.X * 12), (velocity.Y * 12), Mod.Find<ModProjectile>("PalladiumChainedchainsaw").Type, (damage), knockback, player.whoAmI);
            }

            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PalladiumPike)
                .AddIngredient(ItemID.PalladiumRepeater)
                .AddIngredient<PalladiumStaff>()
                .AddIngredient<PalladiumChainedchainsaw>(4)
                .AddTile(TileID.Anvils)
                .Register();
        }

    }
}


