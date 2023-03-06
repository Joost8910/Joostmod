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
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(48, 7));
		}
        public override void SetDefaults()
        {
            Item.damage = 800;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.CountsAsClass(DamageClass.Throwing);
            Item.width = 36;
            Item.height = 60;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.value = 50000000;
            Item.rare = ItemRarityID.Expert;
            Item.scale = 1f;
            Item.expert = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("Naginata").Type;
            Item.shootSpeed = 8f;

        }
        /*
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            add += (player.thrownDamage - 1f);
            mult *= player.thrownDamageMult;
        }
        */
        /*
        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            crit += player.GetCritChance(DamageClass.Throwing);
            crit /= 2;
        }
        */
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            Player player = Main.player[Main.myPlayer];
            int dmg = list.FindIndex(x => x.Name == "Damage");
            list.RemoveAt(dmg);
            list.Insert(dmg, new TooltipLine(Mod, "Damage", player.GetWeaponDamage(Item) + " melee and throwing damage"));
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Naginata").Type] > 0)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int Gilgwep = Main.rand.Next(7);
            if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Naginata").Type] > 0 && Gilgwep == 6)
            {
                Gilgwep = Main.rand.Next(6);
            }
            if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Axe").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("Axe2").Type] > 0 && Gilgwep == 5)
            {
                Gilgwep = Main.rand.Next(5);
            }
            if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Flail").Type] > 0 && Gilgwep == 4)
            {
                Gilgwep = Main.rand.Next(4);
            }
            if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("BusterSword").Type] > 0 && Gilgwep == 3)
            {
                Gilgwep = Main.rand.Next(3);
            }
            if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Gunblade").Type] > 0 && Gilgwep == 2)
            {
                Gilgwep = Main.rand.Next(2);
            }
            if (Gilgwep == 6)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, (damage * 3), knockback, player.whoAmI);
            }
            if (Gilgwep == 5)
            {
                Projectile.NewProjectile(source, position.X, position.Y, (velocity.X * 2f), (velocity.Y * 2f), Mod.Find<ModProjectile>("Axe").Type, (damage * 2), knockback * 1.2f, player.whoAmI);
            }
            if (Gilgwep == 4)
            {
                Projectile.NewProjectile(source, position.X, position.Y, (velocity.X * 4), (velocity.Y * 4), Mod.Find<ModProjectile>("Flail").Type, (int)(damage * 2.5f), knockback * 2, player.whoAmI);
            }
            if (Gilgwep == 3)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("BusterSword").Type, (int)(damage * 2.5f), knockback * 3, player.whoAmI);
            }
            if (Gilgwep == 2)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("Gunblade").Type, (int)(damage * player.GetDamage(DamageClass.Ranged) * player.GetDamage(DamageClass.Ranged) * 2f), knockback, player.whoAmI);
            }
            if (Gilgwep == 1)
            {
                float spread = 25f * 0.0174f;
                float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
                double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
                double deltaAngle = spread / 2f;
                double offsetAngle;
                int i;
                for (i = 0; i < 3; i++)
                {
                    offsetAngle = startAngle + deltaAngle * i;
                    Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle) * 2, baseSpeed * (float)Math.Cos(offsetAngle) * 2, Mod.Find<ModProjectile>("Kunai").Type, (int)(damage * 0.8f), knockback / 2, player.whoAmI);
                }
            }
            if (Gilgwep == 0)
            {
                float Speed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
                Projectile.NewProjectile(source, position.X, position.Y, (velocity.X * 1.5f), (velocity.Y * 1.5f), Mod.Find<ModProjectile>("Tomahawk").Type, damage, knockback, player.whoAmI);
                Projectile.NewProjectile(source, position.X, position.Y, (velocity.X * 1.2f), (velocity.Y * 1.2f), Mod.Find<ModProjectile>("Tomahawk").Type, damage, knockback, player.whoAmI, 1, Speed * 1.2f);

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


