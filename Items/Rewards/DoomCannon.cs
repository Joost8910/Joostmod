using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
	public class DoomCannon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Doom Cannon");
			Tooltip.SetDefault("Charge up a deadly skull shot\n" + "Max charge tunnels through and destroys tiles\n" + "Right click to cancel the shot");
		}
		public override void SetDefaults()
		{
			item.damage = 60;
			item.ranged = true;
			item.width = 76;
			item.height = 46;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 5;
			item.knockBack = 8f;
			item.value = 70000;
			item.rare = 3;
			item.UseSound = SoundID.Item73;
			item.autoReuse = true;
			item.noUseGraphic = true;
			item.channel = true;
			item.noMelee = true;
			item.shoot = mod.ProjectileType("DoomCannon");
			item.shootSpeed = 1f;
		}
        public override bool CanUseItem(Player player)
        {
            if (player.controlUseTile)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
        public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.overrideColor = new Color(230, 204, 128);
				}
			}
		}
	}
}

