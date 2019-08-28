using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class PlagueOfToads : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Plague of Toads");
			Tooltip.SetDefault("Summons multiple toads to chase down enemies");
		}
		public override void SetDefaults()
		{
			item.damage = 45;
			item.summon = true;
			item.mana = 12;
			item.maxStack = 1;
			item.consumable = false;
			item.width = 50;
			item.height = 50;
			item.useTime = 35;
			item.useAnimation = 35;
			item.useStyle = 5;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.knockBack = 3;
			item.value = 100000;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Toad");
			item.shootSpeed = 8f;
		}
		    public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.overrideColor = new Color(255, 128, 0);
            }
        }
    }
		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float spread = 90f * 0.0174f;
			float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
			double baseAngle = Math.Atan2(speedX, speedY);
			    for (int i = 0; i < 4;i++ )
				{
					double randomAngle = baseAngle+(Main.rand.NextFloat()-0.5f)*spread;
					speedX = baseSpeed*(float)Math.Sin(randomAngle);
					speedY = baseSpeed*(float)Math.Cos(randomAngle);
  
					Terraria.Projectile.NewProjectile(position.X, position.Y, speedX,speedY, type, damage, knockBack, player.whoAmI);
				}	
			Main.PlaySound(29, (int)player.position.X, (int)player.position.Y, 13);
			return false;
		}
	}
}

