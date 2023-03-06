using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class PowerBomb : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Power Bomb");
			Tooltip.SetDefault("Explodes into a powerful heat wave");
		}
		public override void SetDefaults()
		{
			Item.damage = 500;
			Item.DamageType = DamageClass.Throwing;
			Item.width = 16;
			Item.height = 16;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.knockBack = 0;
			Item.value = 10000000;
			Item.rare = ItemRarityID.Purple;
            Item.UseSound = new SoundStyle("JoostMod/Sounds/Custom/LayBomb");
            Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("PowerBomb").Type;
			Item.shootSpeed = 12f;
        }
        public override bool CanUseItem(Player player)
        {
            if ((player.ownedProjectileCounts[Item.shoot] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("PowerBombExplosion").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("PowerBombExplosion2").Type]) >= 1)
            {
                return false;
            }
            else return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float distance = player.Distance(Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY));
            velocity.Normalize();
            velocity.X = velocity.X * (distance / 60);
            velocity.Y = velocity.Y * (distance / 60);
            return true;
        }
        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<Materials.IceCoreX>()
				.Register();
		}

	}
}
