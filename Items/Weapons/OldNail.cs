using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class OldNail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Old Nail");
			Tooltip.SetDefault("'Years of age and wear have blunted its blade'");
		}
		public override void SetDefaults()
		{
			item.damage = 9;
			item.melee = true;
			item.width = 30;
			item.height = 30;
			item.noMelee = true;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 5;
			item.autoReuse = true;
			item.knockBack = 3;
			item.value = 8000;
			item.rare = 0;
			item.UseSound = SoundID.Item1;
			item.noUseGraphic = true;
			item.channel = true;
			item.shoot = mod.ProjectileType("OldNail");
			item.shootSpeed = 6f;
		}
		public override bool CanUseItem(Player player)
        {
           return player.ownedProjectileCounts[item.shoot] < 1;
		}
	}
}

