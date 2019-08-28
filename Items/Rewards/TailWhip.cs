using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
	public class TailWhip : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tail Whip");
            Tooltip.SetDefault("Envenoms struck targets");
		}
		public override void SetDefaults()
		{
			item.damage = 31;
			item.melee = true;
			item.noMelee = true;
			item.scale = 1f;
			item.noUseGraphic = true;
			item.width = 30;
			item.height = 32;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = 5;
			item.knockBack = 4.5f;
			item.value = 80000;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.channel = true;
			item.shoot = mod.ProjectileType("TailWhip");
			item.shootSpeed = 20f;
            item.useTurn = true;
		}
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[item.shoot] > 0)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            speedX += player.velocity.X;
            speedY += player.velocity.Y;
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
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

