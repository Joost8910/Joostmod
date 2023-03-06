using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class PalladiumStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crystal Staff");
            Tooltip.SetDefault("Shoots a chunk of crystal at your mouse's location that shatters into 8 shards");
        }
		public override void SetDefaults()
		{
			Item.damage = 35;
			Item.DamageType = DamageClass.Magic;
			Item.width = 38;
			Item.height = 38;
			Item.noMelee = true;
			Item.useTime = 35;
			Item.useAnimation = 35;
			Item.autoReuse = true;
			Item.mana = 20;
			Item.staff[Item.type] = true; 
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
			Item.value = 40000;
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item43;
			Item.shoot = Mod.Find<ModProjectile>("CrystalChunk").Type;
			Item.shootSpeed = 1f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float distance = player.Distance(Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY));
            velocity.X = velocity.X * (distance / 30);
            velocity.Y = velocity.Y * (distance / 30);
            return true;
        }
        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.PalladiumBar, 10)
				.AddIngredient(ItemID.CrystalShard, 8)
				.AddTile(TileID.Anvils)
				.Register();
		}

	}
}

