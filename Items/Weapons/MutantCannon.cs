using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons
{
    [AutoloadEquip(EquipType.HandsOff)]
    public class MutantCannon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mutated Arm Cannons");
            Tooltip.SetDefault("Charge up to dash forward and fire a volley of missiles");
        }
        public override void SetDefaults()
        {
            Item.damage = 550;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.width = 76;
            Item.height = 48;
            Item.useTime = 55;
            Item.useAnimation = 55;
            Item.reuseDelay = 5;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 11;
            Item.value = 10000000;
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item13;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("MutantCannon").Type;
            Item.shootSpeed = 10f;
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
                .AddIngredient<Materials.IceCoreX>()
                .Register();
        }
    }
}

