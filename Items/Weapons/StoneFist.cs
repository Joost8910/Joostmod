using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons
{
    [AutoloadEquip(EquipType.HandsOff)]
    public class StoneFist : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stone Fist");
			Tooltip.SetDefault("'The real fist of Fury'\n" +
                "Charges up a powerful punch\n" +
                "Right Click while charged to grab an enemy" +
                "Hold Right Click to pummel the grabbed enemy\n" +
                "Let go of Left Click to throw the grabbed enemy");
		}
		public override void SetDefaults()
		{
			item.damage = 333;
			item.melee = true;
			item.width = 64;
			item.height = 64;
			item.useTime = 55;
			item.useAnimation = 55;
			item.reuseDelay = 5;
			item.useStyle = 5;
			item.knockBack = 50;
			item.value = 225000;
			item.rare = 5;
			item.UseSound = SoundID.Item13;
			item.autoReuse = true;
			item.noUseGraphic = true;
			item.channel = true;
			item.noMelee = true;
			item.shoot = mod.ProjectileType("Stonefist");
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
			recipe.AddIngredient(null, "EarthEssence", 50);
			recipe.AddIngredient(ItemID.StoneBlock, 100);
            recipe.AddRecipeGroup("JoostMod:AnyCobalt", 4);
            recipe.AddRecipeGroup("JoostMod:AnyMythril", 4);
            recipe.AddRecipeGroup("JoostMod:AnyAdamantite", 4);
            recipe.AddTile(null, "ElementalForge");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

