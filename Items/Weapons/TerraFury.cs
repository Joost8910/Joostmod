using Terraria;
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
            item.damage = 110;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.width = 60;
            item.height = 64;
            item.useTime = 44;
            item.useAnimation = 44;
            item.useStyle = 5;
            item.knockBack = 10;
            item.value = 200000;
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.channel = true;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("TerraFury");
            item.shootSpeed = 28f;
        }
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position.Y -= (item.scale * 42) - 42;
            Projectile.NewProjectile(position.X, position.Y, -speedX, -speedY, type, damage, knockBack, player.whoAmI, 2f);
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
			recipe.AddIngredient(null, "TrueHallowedFlail");
			recipe.AddIngredient(null, "TrueNightsFury");
			recipe.AddTile(TileID.MythrilAnvil); 
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "TrueHallowedFlail");
			recipe.AddIngredient(null, "TrueBloodMoon");
			recipe.AddTile(TileID.MythrilAnvil); 
			recipe.SetResult(this);
			recipe.AddRecipe();

		}

	}
}

