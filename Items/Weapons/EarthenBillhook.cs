using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons
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
			item.damage = 64;
			item.melee = true;
			item.width = 92;
			item.height = 90;
			item.useTime = 36;
			item.useAnimation = 36;
			item.reuseDelay = 2;
			item.useStyle = 1;
			item.knockBack = 7;
			item.value = 250000;
			item.rare = 5;
			item.UseSound = SoundID.Item7;
			item.autoReuse = true;
            item.noUseGraphic = true;
            item.channel = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("EarthenBillhook");
            item.shootSpeed = 7f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return (player.ownedProjectileCounts[item.shoot] <= 0);
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useStyle = 1;
            }
            else
            {
                item.useStyle = 5;
            }
            if (player.ownedProjectileCounts[item.shoot] > 0)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(position, new Vector2(speedX, speedY), mod.ProjectileType("EarthenBillhook2"), damage, knockBack, player.whoAmI);
                return false;
            }
            Main.PlaySound(2, player.Center, 19);
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
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

