using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class CactusBanner : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactus Banner");
			Tooltip.SetDefault("Rapidly summons miniature cactus worms from the ground that damage enemies");
		}
		public override void SetDefaults()
		{
			Item.damage = 17;
			Item.DamageType = DamageClass.Summon;
            Item.mana = 10;
			Item.width = 22;
			Item.height = 44;
			Item.useTime = 16;
			Item.useAnimation = 16;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.noMelee = true; 
			Item.knockBack = 2.5f;
			Item.value = 60000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item8;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("CactusSwarm").Type;
			Item.shootSpeed = 10f;
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, player.Center.X - 50, player.position.Y + (player.gravDir == -1 ? player.height : 0), 0.08f, 10, type, damage, knockback, player.whoAmI, player.gravDir);
            Projectile.NewProjectile(source, player.Center.X + 50, player.position.Y + (player.gravDir == -1 ? player.height : 0), -0.08f, 10, type, damage, knockback, player.whoAmI, player.gravDir);
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
				.AddIngredient<Materials.LusciousCactus>(10)
				.Register();
        }
    }
}


