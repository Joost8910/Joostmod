using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
	public class ObservantStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Observant Staff");
			Tooltip.SetDefault("Summons an ICU to fight for you");
		}
		public override void SetDefaults()
		{
			item.damage = 8;
			item.summon = true;
			item.mana = 10;
			item.width = 36;
			item.height = 36;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.noMelee = true; 
			item.knockBack = 0;
            item.value = 40000;
            item.rare = 3;
            item.UseSound = SoundID.Item44;
			item.shoot = mod.ProjectileType("ICUMinion");
			item.shootSpeed = 7f;
			item.buffType = mod.BuffType("ICUMinion");
			item.buffTime = 3600;
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
        public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			position = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
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


