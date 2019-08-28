using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace JoostMod.Items.Weapons
{
	public class SAXSet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SA-X Weapon Set");
			Tooltip.SetDefault("'Cold and explosive'");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(48, 4));
		}
		public override void SetDefaults()
		{
			item.damage = 500;
			item.width = 76;
			item.height = 50;
			item.useTime = 24;
            item.useAnimation = 24;
			item.useStyle = 5;
			item.noMelee = true; 
			item.knockBack = 8;
			item.value = 10000000;
			item.rare = 11;
			item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("IceBeam");
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
            if (((player.ownedProjectileCounts[mod.ProjectileType("PowerBomb")] + player.ownedProjectileCounts[mod.ProjectileType("PowerBombExplosion")] + player.ownedProjectileCounts[mod.ProjectileType("PowerBombExplosion2")]) >= 1))
            {
                wep = Main.rand.Next(3);
            }
            if (wep == 1)
            {
                Projectile.NewProjectile(position.X, position.Y, (speedX), (speedY), mod.ProjectileType("SuperMissile"), (int)(damage * 1.6f), knockBack, player.whoAmI);
                Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/SuperMissileShoot"));
            }
            if (wep == 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(60));
                    float scale = 1f - (Main.rand.NextFloat() * .3f);
                    perturbedSpeed = perturbedSpeed * scale;
                    Projectile.NewProjectile(position, perturbedSpeed, mod.ProjectileType("Missile"), (int)(damage * 0.53f), knockBack, player.whoAmI);
                    Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/MissileShoot"));
                }
            }
            if (wep == 3)
            {
                float distance = player.Distance(Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY));
                Vector2 velocity = new Vector2(speedX, speedY);
                velocity.Normalize();
                speedX = velocity.X * (distance / 60);
                speedY = velocity.Y * (distance / 60);
                Projectile.NewProjectile(position.X, position.Y, (speedX), (speedY), mod.ProjectileType("PowerBomb"), (damage), knockBack, player.whoAmI);
                Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/LayBomb"));
            }
            if (wep == 0)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("IceBeam"), (int)(damage * 0.9f), knockBack, player.whoAmI);
                Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/IceBeam"));
            }


            return false;
        }
			public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "SuperMissileLauncher");
			recipe.AddIngredient(null, "PowerBomb");
			recipe.AddIngredient(null, "IceBeam");
			recipe.AddIngredient(null, "MutantCannon");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}


