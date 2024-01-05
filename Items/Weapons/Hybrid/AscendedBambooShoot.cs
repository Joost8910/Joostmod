using JoostMod.DamageClasses;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons.Hybrid
{
    public class AscendedBambooShoot : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ascended Bamboo Shoot");
            Tooltip.SetDefault("Left click to swing\n" +
                "Right click to charge a seed barrage\n" +
                "Allows the collection of seeds for ammo");
        }
        public override void SetDefaults()
        {
            Item.damage = 42;
            Item.DamageType = ModContent.GetInstance<MeleeRangedHybrid>();
            Item.width = 140;
            Item.height = 20;
            Item.noMelee = true;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.knockBack = 7.5f;
            Item.value = 250000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item7;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.shoot = 10;
            Item.shootSpeed = 15f;
        }
        //Using CountsAsClass() does not work as expected, it only returns a bool; it cannot be modified directly
        /*
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage.CombineWith(player.GetDamage(DamageClass.Ranged));
        }

        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            crit += player.GetCritChance(DamageClass.Ranged);
        }
        */
        /*
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            Player player = Main.player[Main.myPlayer];
            int dmg = list.FindIndex(x => x.Name == "Damage");
            list.RemoveAt(dmg);
            list.Insert(dmg, new TooltipLine(Mod, "Damage", player.GetWeaponDamage(Item) + " melee and ranged damage"));
        }
        */
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useAmmo = AmmoID.Dart;
            }
            else
            {
                Item.useAmmo = AmmoID.None;
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Hybrid.AscendedBambooShoot>()] < 1)
            {
                return base.CanUseItem(player);
            }
            return false;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return player.altFunctionUse == 2 && !player.ItemAnimationJustStarted;
        }
        public override bool NeedsAmmo(Player player)
        {
            return player.altFunctionUse == 2;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int mode = 0;
            if (player.altFunctionUse == 2)
            {
                mode = 1;
            }
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<Projectiles.Hybrid.AscendedBambooShoot>(), damage, knockback, player.whoAmI, mode);
            return false;
        }
        public override bool MeleePrefix()
        {
            return Main.rand.NextBool(2);
        }
        public override bool WeaponPrefix()
        {
            return false;
        }
        public override bool RangedPrefix()
        {
            return true;
        }
        /*public override int ChoosePrefix(UnifiedRandom rand)
        {
            if (Main.rand.NextBool(3))
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
            else if (Main.rand.NextBool(2))
            {
                switch (rand.Next(18))
                {
                    case 1:
                        return PrefixID.Large;
                    case 2:
                        return PrefixID.Massive;
                    case 3:
                        return PrefixID.Dangerous;
                    case 4:
                        return PrefixID.Savage;
                    case 5:
                        return PrefixID.Sharp;
                    case 6:
                        return PrefixID.Pointy;
                    case 7:
                        return PrefixID.Tiny;
                    case 8:
                        return PrefixID.Terrible;
                    case 9:
                        return PrefixID.Small;
                    case 10:
                        return PrefixID.Dull;
                    case 11:
                        return PrefixID.Unhappy;
                    case 12:
                        return PrefixID.Bulky;
                    case 13:
                        return PrefixID.Shameful;
                    case 14:
                        return PrefixID.Heavy;
                    case 15:
                        return PrefixID.Light;
                    case 16:
                        return Mod.Find<ModPrefix>("Impractically Oversized").Type;
                    case 17:
                        return Mod.Find<ModPrefix>("Miniature").Type;
                    default:
                        return PrefixID.Legendary;
                }
            }
            return base.ChoosePrefix(rand);
        }*/
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<MightyBambooShoot>()
                .AddIngredient(ItemID.SoulofMight, 15)
                .AddIngredient(ItemID.HallowedBar, 12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

