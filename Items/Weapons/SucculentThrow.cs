using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class SucculentThrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Succulent Throw");
			Tooltip.SetDefault("Picks up a hit enemy\n" + 
                "Does not work on enemies immune to knockback");
		}
		public override void SetDefaults()
		{
			item.damage = 24;
			item.melee = true;
			item.width = 30;
			item.height = 26;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = 5;
			item.knockBack = 0;
			item.channel = true;
            item.value = 100000;
            item.rare = 2;
            item.noMelee = true;
			item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
			item.shoot = mod.ProjectileType("SucculentThrow");
			item.shootSpeed = 11f;
		}
	}
}

