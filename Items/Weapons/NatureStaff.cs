using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class NatureStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Staff of Nature");
			Tooltip.SetDefault("Summons a swirling shield of leaves\n" + "Right click to send the leaves outwards");
		}
		public override void SetDefaults()
		{
			item.damage = 44;
			item.summon = true;
			item.mana = 12;
			item.width = 48;
			item.height = 48;
			item.useTime = 38;
			item.useAnimation = 38;
			item.useStyle = 4;
			item.noMelee = true; 
			item.knockBack = 4.5f;
			item.value = 250000;
			item.rare = 5;
			item.autoReuse = true;
			item.UseSound = SoundID.Item78;
			item.shoot = mod.ProjectileType("Leaf2");
			item.shootSpeed = 9f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            bool create = true;
            for (int l = 0; l < 200; l++)
            {
                Projectile p = Main.projectile[l];
                if (p.type == type && p.active && p.owner == player.whoAmI && p.ai[1] != 1)
                {
                    if (player.altFunctionUse == 0)
                    {
                        p.Kill();
                    }
                    else
                    {
                        p.ai[1] = 1f;
                        create = false;
                    }
                    p.netUpdate = true;
                }
            }
            if (create)
			{
 				Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
				Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 45f, 0f);
				Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 90f, 0f);
				Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 135f, 0f);
				Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 180f, 0f);
				Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 225f, 0f);
				Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 270f, 0f);
				Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 315f, 0f);
			}
			return false;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "ForestWand");
			recipe.AddIngredient(ItemID.JungleSpores, 8);
			recipe.AddIngredient(ItemID.SoulofFright, 12);
			recipe.AddTile(TileID.MythrilAnvil); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}


