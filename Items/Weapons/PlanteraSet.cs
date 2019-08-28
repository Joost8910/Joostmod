using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace JoostMod.Items.Weapons
{
    public class PlanteraSet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plantera Weapon Set");
            Tooltip.SetDefault("'BRING IT ON YOU GIANT F---'");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(48, 4));
        }
        public override void SetDefaults()
        {
            item.damage = 70;
            item.width = 66;
            item.height = 70;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = 150000;
            item.rare = 5;
            item.scale = 1f;
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("ThornShower");
            item.shootSpeed = 6f;
            item.crit = 4;
        }
        public override void GetWeaponCrit(Player player, ref int crit)
        {
            crit += (player.meleeCrit + player.rangedCrit + player.magicCrit + player.thrownCrit) / 4;
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
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int wep = Main.rand.Next(4);
            if (wep == 1)
            {
                Terraria.Projectile.NewProjectile(position.X, position.Y, (speedX * 2), (speedY * 2), 133, (damage), knockBack, player.whoAmI);
            }
            if (wep == 2)
            {
                Terraria.Projectile.NewProjectile(position.X, position.Y, (speedX * 2), (speedY * 2), 483, (damage), knockBack, player.whoAmI);
            }
            if (wep == 3)
            {
                Terraria.Projectile.NewProjectile(position.X, position.Y, (speedX * 5), (speedY * 5), 150, (damage / 2), knockBack, player.whoAmI);
            }
            if (wep == 0)
            {
                Terraria.Projectile.NewProjectile(position.X, position.Y, (speedX * 3), (speedY * 3), mod.ProjectileType("ThornShower"), (damage), knockBack, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Seedler);
            recipe.AddIngredient(ItemID.GrenadeLauncher);
            recipe.AddIngredient(ItemID.NettleBurst);
            recipe.AddIngredient(null, "RoseWeave");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}


