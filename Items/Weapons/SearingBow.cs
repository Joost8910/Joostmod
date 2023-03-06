using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
    public class SearingBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Volcanic Longbow");
			Tooltip.SetDefault("Transforms wooden arrows into Volcanic Arrows\n"+
                "Volcanic Arrows deal 50% more damage and drop a trail of damaging lava droplets\n" +
                "Right clicking nocks additional arrows\n" + 
                "Can nock up to 5 arrows");
		}
		public override void SetDefaults()
		{
			Item.damage = 48;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 52;
			Item.height = 36;
			Item.useTime = 24;
			Item.useAnimation = 24;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 4.5f;
			Item.value = 250000;
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item7;
			Item.autoReuse = true;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.noMelee = true;
			Item.shoot = Mod.Find<ModProjectile>("SearingBow").Type;
			Item.shootSpeed = 13f;
            Item.useAmmo = AmmoID.Arrow;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[Item.shoot] > 0)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
        public override bool AltFunctionUse(Player player)
        {
            return (player.ownedProjectileCounts[Item.shoot] <= 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = Mod.Find<ModProjectile>("SearingBow").Type;
            return true;
        }
        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.MoltenFury)
				.AddIngredient<Materials.FireEssence>(50)
				.AddRecipeGroup("JoostMod:AnyCobalt", 4)
				.AddRecipeGroup("JoostMod:AnyMythril", 4)
				.AddRecipeGroup("JoostMod:AnyAdamantite", 4)
				.AddTile<Tiles.ElementalForge>()
				.Register();
		}
	}
}

