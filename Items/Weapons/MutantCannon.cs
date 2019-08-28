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
            item.damage = 450;
            item.melee = true;
            item.noMelee = true;
            item.width = 76;
            item.height = 48;
            item.useTime = 55;
            item.useAnimation = 55;
            item.reuseDelay = 5;
            item.channel = true;
            item.noUseGraphic = true;
            item.useStyle = 5;
            item.knockBack = 11;
            item.value = 10000000;
            item.rare = 11;
            item.UseSound = SoundID.Item13;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("MutantCannon");
            item.shootSpeed = 10f;
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
                        return mod.PrefixType("Impractically Oversized");
                    case 17:
                        return mod.PrefixType("Miniature");
                    default:
                        return PrefixID.Legendary;
                }
            }
            return base.ChoosePrefix(rand);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "IceCoreX", 1);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

