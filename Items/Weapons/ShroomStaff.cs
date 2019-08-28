using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class ShroomStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shroom Staff");
			Tooltip.SetDefault("Summons a giant mushroom that creates homing spores\n" + 
			"The mushroom knocks enemies upwards as it sprouts");
		}
		public override void SetDefaults()
		{
			item.damage = 35;
			item.summon = true;
			item.mana = 15;
			item.width = 44;
			item.height = 44;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = 1;
			item.noMelee = true; 
			item.knockBack = 10;
			item.value = 75000;
			item.rare = 7;
			item.UseSound = SoundID.Item44;
			item.shoot = mod.ProjectileType("ShroomSentry");
			item.sentry = true;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			position = Main.MouseWorld - new Vector2(0, 50);
			speedY = 48;
			return true;
		}
	}
}


