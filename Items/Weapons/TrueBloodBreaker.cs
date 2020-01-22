using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class TrueBloodBreaker : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Blood Breaker");
        }
        public override void SetDefaults()
        {
            item.damage = 110;
            item.melee = true;
            item.width = 64;
            item.height = 64;
            item.useTime = 10;
            item.useAnimation = 40;
            item.knockBack = 9;
            item.value = 500000;
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.hammer = 100;
            item.useStyle = 1;
            item.tileBoost = 2;
            item.autoReuse = true;
            item.useTurn = true;
        }
        public override bool UseItem(Player player)
        {
            if (player.itemAnimation <= item.useTime)
            {
                if (player.velocity.Y == 0)
                {
                    Main.PlaySound(42, player.position, 207 + Main.rand.Next(3));
                    Projectile.NewProjectile(player.Center.X + 64 * player.direction * item.scale, player.Center.Y - 40 * player.gravDir, 3f * player.direction, 0f, mod.ProjectileType("BloodWave"), item.damage, item.knockBack, player.whoAmI, player.gravDir);
                }
                else
                {
                    Main.PlaySound(42, player.position, 213 + Main.rand.Next(4));
                }
            }
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "BloodBreaker", 1);
            recipe.AddIngredient(null, "BrokenHeroHammer", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}


