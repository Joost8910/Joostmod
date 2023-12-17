using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons.Melee
{
    public class ActualMace : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Actual Mace");
            Tooltip.SetDefault(//"Not to be confused with the flail labeled 'Mace'\n" +
                "Hold attack to charge the swing\n" +
                "Charged attacks reduce enemy defense\n" +
                "Can be upgraded with torches");
        }
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.width = 46;
            Item.height = 46;
            Item.useTime = 33;
            Item.useAnimation = 33;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6.4f;
            Item.value = 20000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item7;
            Item.channel = true;
            Item.useTurn = false;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.ActualMace>();
            Item.shootSpeed = 1f;
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
    }
}

