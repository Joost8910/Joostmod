using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons
{
    public class TerraFury : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Fury");
        }
        public override void SetDefaults()
        {
            Item.damage = 110;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.width = 60;
            Item.height = 64;
            Item.useTime = 44;
            Item.useAnimation = 44;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 10;
            Item.value = 1000000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.channel = true;
            Item.useTurn = true;
            Item.shoot = Mod.Find<ModProjectile>("TerraFury").Type;
            Item.shootSpeed = 28f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position.Y -= (Item.scale * 42) - 42;
            Projectile.NewProjectile(source, position.X, position.Y, -velocity.X, -velocity.Y, type, damage, knockback, player.whoAmI, 2f);
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
                .AddIngredient<TrueHallowedFlail>()
                .AddIngredient<TrueNightsFury>()
                .AddTile(TileID.MythrilAnvil)
                .Register();
            CreateRecipe()
                .AddIngredient<TrueHallowedFlail>()
                .AddIngredient<TrueBloodMoon>()
                .AddTile(TileID.MythrilAnvil)
                .Register();

        }

    }
}

