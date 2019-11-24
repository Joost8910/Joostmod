using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons
{
	public class Gilgameshset : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gilgamesh's Weapon Set");
			Tooltip.SetDefault("'Too bad you don't have 8 arms'");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(48, 7));
		}
        public override void SetDefaults()
        {
            item.damage = 800;
            item.melee = true;
            item.thrown = true;
            item.width = 36;
            item.height = 60;
            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = 50000000;
            item.rare = -12;
            item.scale = 1f;
            item.expert = true;
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Naginata");
            item.shootSpeed = 8f;

        }
        /*
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            add += (player.thrownDamage - 1f);
            mult *= player.thrownDamageMult;
        }
        */
        public override void GetWeaponCrit(Player player, ref int crit)
        {
            crit += player.thrownCrit;
            crit /= 2;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            Player player = Main.player[Main.myPlayer];
            int dmg = list.FindIndex(x => x.Name == "Damage");
            list.RemoveAt(dmg);
            list.Insert(dmg, new TooltipLine(mod, "Damage", player.GetWeaponDamage(item) + " melee and throwing damage"));
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("Naginata")] > 0)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int Gilgwep = Main.rand.Next(7);
            if (player.ownedProjectileCounts[mod.ProjectileType("Naginata")] > 0 && Gilgwep == 6)
            {
                Gilgwep = Main.rand.Next(6);
            }
            if (player.ownedProjectileCounts[mod.ProjectileType("Axe")] + player.ownedProjectileCounts[mod.ProjectileType("Axe2")] > 0 && Gilgwep == 5)
            {
                Gilgwep = Main.rand.Next(5);
            }
            if (player.ownedProjectileCounts[mod.ProjectileType("Flail")] > 0 && Gilgwep == 4)
            {
                Gilgwep = Main.rand.Next(4);
            }
            if (player.ownedProjectileCounts[mod.ProjectileType("BusterSword")] > 0 && Gilgwep == 3)
            {
                Gilgwep = Main.rand.Next(3);
            }
            if (player.ownedProjectileCounts[mod.ProjectileType("Gunblade")] > 0 && Gilgwep == 2)
            {
                Gilgwep = Main.rand.Next(2);
            }
            if (Gilgwep == 6)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, (damage * 3), knockBack, player.whoAmI);
            }
            if (Gilgwep == 5)
            {
                Projectile.NewProjectile(position.X, position.Y, (speedX * 2f), (speedY * 2f), mod.ProjectileType("Axe"), (damage * 2), knockBack * 1.2f, player.whoAmI);
            }
            if (Gilgwep == 4)
            {
                Projectile.NewProjectile(position.X, position.Y, (speedX * 4), (speedY * 4), mod.ProjectileType("Flail"), (int)(damage * 2.5f), knockBack * 2, player.whoAmI);
            }
            if (Gilgwep == 3)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("BusterSword"), (int)(damage * 2.5f), knockBack * 3, player.whoAmI);
            }
            if (Gilgwep == 2)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("Gunblade"), (int)(damage * player.rangedDamage * player.rangedDamageMult * 2f), knockBack, player.whoAmI);
            }
            if (Gilgwep == 1)
            {
                float spread = 25f * 0.0174f;
                float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
                double startAngle = Math.Atan2(speedX, speedY) - spread / 2;
                double deltaAngle = spread / 2f;
                double offsetAngle;
                int i;
                for (i = 0; i < 3; i++)
                {
                    offsetAngle = startAngle + deltaAngle * i;
                    Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle) * 2, baseSpeed * (float)Math.Cos(offsetAngle) * 2, mod.ProjectileType("Kunai"), (int)(damage * 0.8f), knockBack / 2, player.whoAmI);
                }
            }
            if (Gilgwep == 0)
            {
                float Speed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
                Projectile.NewProjectile(position.X, position.Y, (speedX * 1.5f), (speedY * 1.5f), mod.ProjectileType("Tomahawk"), damage, knockBack, player.whoAmI);
                Projectile.NewProjectile(position.X, position.Y, (speedX * 1.2f), (speedY * 1.2f), mod.ProjectileType("Tomahawk"), damage, knockBack, player.whoAmI, 1, Speed * 1.2f);

            }
            return false;
        }
        public override int ChoosePrefix(UnifiedRandom rand)
        {
            if (Main.rand.NextBool(2))
            {
                switch (rand.Next(12))
                {
                    case 1:
                        return PrefixID.Rapid;
                    case 2:
                        return PrefixID.Hasty;
                    case 3:
                        return PrefixID.Intimidating;
                    case 4:
                        return PrefixID.Deadly2;
                    case 5:
                        return PrefixID.Staunch;
                    case 6:
                        return PrefixID.Awful;
                    case 7:
                        return PrefixID.Lethargic;
                    case 8:
                        return PrefixID.Awkward;
                    case 9:
                        return PrefixID.Powerful;
                    case 10:
                        return PrefixID.Frenzying;
                    case 11:
                        return PrefixID.Sighted;
                    default:
                        return PrefixID.Unreal;
                }
            }
            return base.ChoosePrefix(rand);
        }

    }
}


