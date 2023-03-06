using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons
{
    public class PetEyeball : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pet Eyeball");
        }
        public override void SetDefaults()
        {
            Item.damage = 76;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 42;
            Item.height = 50;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.reuseDelay = 5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 5;
            Item.value = 100000;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.noMelee = true;
            Item.shoot = Mod.Find<ModProjectile>("PetEyeball").Type;
            Item.shootSpeed = 18f;
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
                .AddIngredient(ItemID.Lens)
                .AddIngredient(ItemID.SoulofSight, 15)
                .AddIngredient(ItemID.SoulofFlight, 5)
                .AddIngredient(ItemID.Chain, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

