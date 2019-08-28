using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class CobaltStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crystal Staff");
            Tooltip.SetDefault("Shoots a chunk of crystal at your mouse's location that shatters into 8 shards");
        }
		public override void SetDefaults()
		{
			item.damage = 35;
			item.magic = true;
			item.width = 38;
			item.height = 38;
			item.noMelee = true;
			item.useTime = 35;
			item.useAnimation = 35;
			item.autoReuse = true;
			item.mana = 20;
			Item.staff[item.type] = true; 
			item.useStyle = 5;
			item.knockBack = 0;
			item.value = 40000;
			item.rare = 4;
			item.UseSound = SoundID.Item43;
			item.shoot = mod.ProjectileType("CrystalChunk");
			item.shootSpeed = 1f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float distance = player.Distance(Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY));
            speedX = speedX * (distance / 30);
            speedY = speedY * (distance / 30);
            return true;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CobaltBar, 10);
			recipe.AddIngredient(ItemID.CrystalShard, 8);
			recipe.AddTile(TileID.Anvils); 
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}

