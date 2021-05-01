using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons
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
			item.damage = 30;
			item.melee = true;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.width = 46;
			item.height = 46;
			item.useTime = 33;
			item.useAnimation = 33;
			item.useStyle = 1;
			item.knockBack = 6.4f;
			item.value = 20000;
			item.rare = 1;
			item.UseSound = SoundID.Item7;
			item.autoReuse = false;
			item.channel = true;
            item.useTurn = false;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("ActualMace");
			item.shootSpeed = 1f;
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
	}
}

