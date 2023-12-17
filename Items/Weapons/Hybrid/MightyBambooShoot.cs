using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons.Hybrid
{
    public class MightyBambooShoot : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mighty Bamboo Shoot");
            Tooltip.SetDefault("Left click to swing\n" +
                "Right click to charge a seed barrage\n" +
                "Allows the collection of seeds for ammo");
        }
        public override void SetDefaults()
        {
            Item.damage = 17;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.CountsAsClass(DamageClass.Ranged);
            Item.width = 100;
            Item.height = 12;
            Item.noMelee = true;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.knockBack = 6;
            Item.value = 20000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item7;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 11f;
        }
        /*
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            add += (player.rangedDamage - 1f);
            mult *= player.rangedDamageMult;
        }
        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            crit += player.GetCritChance(DamageClass.Ranged);
            crit /= 2;
        }
        */
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            Player player = Main.player[Main.myPlayer];
            int dmg = list.FindIndex(x => x.Name == "Damage");
            list.RemoveAt(dmg);
            list.Insert(dmg, new TooltipLine(Mod, "Damage", player.GetWeaponDamage(Item) + " melee and ranged damage"));
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool? CanChooseAmmo(Item ammo, Player player)
        {
            return ammo.ammo == AmmoID.Dart;
        }
        public override bool CanUseItem(Player player)
        {
            /*
            if (player.altFunctionUse == 2)
            {
                Item.useAmmo = AmmoID.Dart;
            }
            else
            {
                Item.useAmmo = AmmoID.None;
            }
            */
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Hybrid.BambooShoot>()] < 1)
            {
                return base.CanUseItem(player);
            }
            return false;
        }
        public override int ChoosePrefix(UnifiedRandom rand)
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
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return false;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int mode = 0;
            if (player.altFunctionUse == 2)
            {
                mode = 1;
            }
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<Projectiles.Hybrid.BambooShoot>(), damage, knockback, player.whoAmI, mode, type);
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Blowpipe)
                .AddIngredient(ItemID.BreathingReed)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}

