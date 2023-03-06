using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class CactusSpears : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactus Spears");
            Tooltip.SetDefault("'What's better than a spear? Five spears!'");
        }
		public override void SetDefaults()
		{
            Item.damage = 19;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 60;
			Item.height = 60;
			Item.useTime = 35;
			Item.useAnimation = 35;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.noUseGraphic = true;
			Item.knockBack = 4.5f;
			Item.value = 60000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("CactusSpear").Type;
			Item.shootSpeed = 7f;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Mod.Find<ModProjectile>("CactusSpear").Type] < 1;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 5;
            float rotation = MathHelper.ToRadians(60);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
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


