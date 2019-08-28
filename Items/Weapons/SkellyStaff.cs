using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class SkellyStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Skeleton Staff");
			Tooltip.SetDefault("Summons a mini Skeleton to fight for you\n" + 
			"Mini Skeletons have a 20% chance to throw an empowered bone");
		}
		public override void SetDefaults()
		{
			item.damage = 17;
			item.summon = true;
			item.mana = 10;
			item.width = 48;
			item.height = 48;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.noMelee = true; 
			item.knockBack = 4;
			item.value = 25000;
			item.rare = 3;
			item.UseSound = SoundID.Item44;
			item.shoot = mod.ProjectileType("SkellyMinion");
			item.shootSpeed = 7f;
			item.buffType = mod.BuffType("SkellyMinion");
			item.buffTime = 3600;
		}
				public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
            position = Main.MouseWorld;
			return player.altFunctionUse != 2;
		}
		
		public override bool UseItem(Player player)
		{
			if(player.altFunctionUse == 2)
			{
				player.MinionNPCTargetAim();
			}
			return base.UseItem(player);
		}
	}
}


