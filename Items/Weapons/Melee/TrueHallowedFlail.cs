using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons.Melee
{
    public class TrueHallowedFlail : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Incandescence");
        }
        public override void SetDefaults()
        {
            Item.damage = 76;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.width = 42;
            Item.height = 42;
            Item.useTime = 44;
            Item.useAnimation = 44;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 9;
            Item.value = 500000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.channel = true;
            Item.useTurn = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.TrueHallowedFlail>();
            Item.shootSpeed = 21f;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position.Y -= Item.scale * 50 - 50;
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
                .AddIngredient<HallowedFlail>()
                .AddIngredient<Materials.BrokenHeroFlail>()
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

    }
}

