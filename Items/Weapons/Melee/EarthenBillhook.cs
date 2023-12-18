//TODO: Billhook is honestly kinda ugly, change to a cooler polearm
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using JoostMod.Projectiles.Melee;

namespace JoostMod.Items.Weapons.Melee
{
    public class EarthenBillhook : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earthen Billhook");
            Tooltip.SetDefault("Right click to swing, causing a boulder to launch upwards when grounded\n" +
                "Left click to thrust, hit the boulder to launch it");
        }
        public override void SetDefaults()
        {
            Item.damage = 69;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 92;
            Item.height = 90;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.reuseDelay = 2;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 7;
            Item.value = 250000;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item7;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.EarthenBillhook>();
            Item.shootSpeed = 7f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] <= 0;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = ItemUseStyleID.Swing;
            }
            else
            {
                Item.useStyle = ItemUseStyleID.Shoot;
            }
            if (player.ownedProjectileCounts[Item.shoot] > 0)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<EarthenBillhook2>(), (int)(damage * 1.15f), knockback, player.whoAmI);
                return false;
            }
            SoundEngine.PlaySound(SoundID.Item19, player.Center);
            return true;
        }
        public override int ChoosePrefix(UnifiedRandom rand)
        {
            if (Main.rand.NextBool(2))
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
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.EarthEssence>(50)
                .AddIngredient(ItemID.StoneBlock, 100)
                .AddRecipeGroup(nameof(ItemID.CobaltBar), 4)
                .AddRecipeGroup(nameof(ItemID.MythrilBar), 4)
                .AddRecipeGroup(nameof(ItemID.AdamantiteBar), 4)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
    }
}

