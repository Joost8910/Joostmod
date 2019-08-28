using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class PumpkinStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pumpkin Staff");
			Tooltip.SetDefault("Creates a swirling shield of pumpkins\n" + "Right click to send the pumpkins outwards");
		}
		public override void SetDefaults()
		{
			item.damage = 38;
			item.summon = true;
			item.mana = 15;
			item.width = 50;
			item.height = 48;
			item.useTime = 4;
			item.useAnimation = 48;
			item.useStyle = 1;
			item.knockBack = 8;
			item.value = 120000;
			item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.noMelee = true;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Pumpkin");
			item.shootSpeed = 11f;
		}
public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			if(player.altFunctionUse == 2)
			{
				for (int l = 0; l < 200; l++)
				{
					Projectile p = Main.projectile[l];
					if (p.type == type && p.active && p.owner == player.whoAmI)
					{
						p.ai[1] = 1f;
					}
				}
			}
			return player.altFunctionUse != 2;
		}		
	}
}


