using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class RoseWeave : ModItem
	{public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rose Weave");
			Tooltip.SetDefault("Showers thorns in the air");
		}
		public override void SetDefaults()
		{
			item.damage = 42;
			item.thrown = true;
			item.width = 66;
			item.height = 66;
			item.useTime = 33;
			item.useAnimation = 33;
			item.useStyle = 1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.knockBack = 8;
			item.value = 300000;
			item.rare = 7;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("ThornShower");
			item.shootSpeed = 10f;
		}

	}
}
