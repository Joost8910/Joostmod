using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class TerraFirma : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Firma");
        }
        public override void SetDefaults()
        {
            item.damage = 140;
            item.melee = true;
            item.width = 64;
            item.height = 58;
            item.useTime = 5;
            item.useAnimation = 25;
            item.knockBack = 11;
            item.value = 200000;
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.hammer = 120;
            item.useStyle = 1;
            item.tileBoost = 7;
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
                    Projectile.NewProjectile(player.Center.X + 64 * player.direction * item.scale, player.Center.Y - 40 * player.gravDir, 10f, 0f, mod.ProjectileType("TerraWave"), item.damage, item.knockBack, player.whoAmI, player.gravDir);
                    Projectile.NewProjectile(player.Center.X + 64 * player.direction * item.scale, player.Center.Y - 40 * player.gravDir, -10f, 0f, mod.ProjectileType("TerraWave"), item.damage, item.knockBack, player.whoAmI, player.gravDir);
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
            recipe.AddIngredient(null, "TrueNightsWrath", 1);
            recipe.AddIngredient(null, "TruePwnhammer", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TrueBloodBreaker", 1);
            recipe.AddIngredient(null, "TruePwnhammer", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}


