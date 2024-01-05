using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;
using Terraria.Utilities;
using JoostMod.Projectiles.Melee;
using JoostMod.Projectiles.Thrown;
using JoostMod.Projectiles.Hybrid;
using JoostMod.DamageClasses;

namespace JoostMod.Items.Weapons.Hybrid
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
            Item.DamageType = ModContent.GetInstance<MeleeThrowingHybrid>();
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
            Item.shoot = ModContent.ProjectileType<Naginata>();
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
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Naginata>()] > 0)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int Gilgwep = Main.rand.Next(7);
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Naginata>()] > 0 && Gilgwep == 6)
            {
                Gilgwep = Main.rand.Next(6);
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Axe>()] + player.ownedProjectileCounts[ModContent.ProjectileType<Axe2>()] > 0 && Gilgwep == 5)
            {
                Gilgwep = Main.rand.Next(5);
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<GilgSetFlail>()] > 0 && Gilgwep == 4)
            {
                Gilgwep = Main.rand.Next(4);
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<BusterSword>()] > 0 && Gilgwep == 3)
            {
                Gilgwep = Main.rand.Next(3);
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Gunblade>()] > 0 && Gilgwep == 2)
            {
                Gilgwep = Main.rand.Next(2);
            }
            if (Gilgwep == 6)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage * 3, knockback, player.whoAmI);
            }
            if (Gilgwep == 5)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 2f, velocity.Y * 2f, ModContent.ProjectileType<Axe>(), damage * 2, knockback * 1.2f, player.whoAmI);
            }
            if (Gilgwep == 4)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 4, velocity.Y * 4, ModContent.ProjectileType<GilgSetFlail>(), (int)(damage * 2.5f), knockback * 2, player.whoAmI);
            }
            if (Gilgwep == 3)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<BusterSword>(), (int)(damage * 2.5f), knockback * 3, player.whoAmI);
            }
            if (Gilgwep == 2)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<Gunblade>(), (int)player.GetDamage(DamageClass.Ranged).ApplyTo(damage), knockback, player.whoAmI);
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
                    Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle) * 2, baseSpeed * (float)Math.Cos(offsetAngle) * 2, ModContent.ProjectileType<Kunai>(), (int)(damage * 0.8f), knockback / 2, player.whoAmI);
                }
            }
            if (Gilgwep == 0)
            {
                float Speed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 1.5f, velocity.Y * 1.5f, ModContent.ProjectileType<Tomahawk>(), damage, knockback, player.whoAmI);
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 1.2f, velocity.Y * 1.2f, ModContent.ProjectileType<Tomahawk>(), damage, knockback, player.whoAmI, 1, Speed * 1.2f);

            }
            return false;
        }
        public override bool RangedPrefix()
        {
            return true;
        }

    }
}


