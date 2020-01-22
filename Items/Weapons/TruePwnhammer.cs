using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class TruePwnhammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Pwnhammer");
		}
		public override void SetDefaults()
		{
			item.damage = 90;
			item.melee = true;
			item.width = 54;
			item.height = 52;
			item.useTime = 8;
			item.useAnimation = 24;
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
                    Projectile.NewProjectile(player.Center.X + 50 * player.direction * item.scale, player.Center.Y - 40 * player.gravDir, 3f * player.direction, 0f, mod.ProjectileType("LightWave"), item.damage, item.knockBack, player.whoAmI, player.gravDir);
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
			recipe.AddIngredient(ItemID.Pwnhammer);
			recipe.AddIngredient(null, "BrokenHeroHammer");
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}


