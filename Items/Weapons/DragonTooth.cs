using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons
{
    public class DragonTooth : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dragon Tooth");
			Tooltip.SetDefault("Increases defense by 10 while held\n" + 
                "Hold attack to charge the swing\n" +
                "Unleash a charged swing while falling to do a plunging attack");
		}
		public override void SetDefaults()
		{
			Item.damage = 188;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 106;
			Item.height = 110;
			Item.useTime = 48;
			Item.useAnimation = 48;
			Item.reuseDelay = 2;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 15;
			Item.value = 250000;
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item7;
			Item.autoReuse = true;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.noMelee = true;
			Item.shoot = Mod.Find<ModProjectile>("DragonTooth").Type;
			Item.shootSpeed = 10f;
        }
        public override void HoldItem(Player player)
        {
            player.statDefense += 10;
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
                .AddRecipeGroup("JoostMod:AnyCobalt", 4)
                .AddRecipeGroup("JoostMod:AnyMythril", 4)
                .AddRecipeGroup("JoostMod:AnyAdamantite", 4)
                .AddTile<Tiles.ElementalForge>()
                .Register();
		}
	}
}

