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
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(48, 4));
		}
		public override void SetDefaults()
		{
			item.damage = 450;
			item.width = 128;
			item.height = 200;
			item.useTime = 12;
			item.useAnimation = 12;
			item.useStyle = 5;
			item.noMelee = true; 
			item.knockBack = 8;
			item.value = 10000000;
			item.rare = 11;
			item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("GiantNeedle");
			item.shootSpeed = 16f;
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
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int wep = Main.rand.Next(4);
			if (wep == 1)
			{
				Projectile.NewProjectile(position.X, position.Y, (speedX), (speedY), mod.ProjectileType("GiantArrow"), damage, knockBack, player.whoAmI);
			}
			if (wep == 2)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("GiantNeedle"), (int)(damage * 0.62f), knockBack, player.whoAmI);
			}
			if (wep == 3)
            {
                float numberProjectiles = 2;
                float rotation = MathHelper.ToRadians(12);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X*2, perturbedSpeed.Y*2, mod.ProjectileType("Splitend"), (int)(damage * 0.67f), knockBack, player.whoAmI);
                }
			}
			if (wep == 0)
            {
                for (int i = 0; i < 12; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                    float scale = 1f - (Main.rand.NextFloat() * .3f);
                    perturbedSpeed = perturbedSpeed * scale;
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("Needle3"), (damage / 5), knockBack, player.whoAmI);
                }
			}		
			return false;
		}
			public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Jumbow");
			recipe.AddIngredient(null, "GiantNeedle");
			recipe.AddIngredient(null, "EvenMoreNeedles");
			recipe.AddIngredient(null, "Mustache");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}


