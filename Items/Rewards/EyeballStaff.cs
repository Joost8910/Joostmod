using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
	public class EyeballStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eyeball Staff");
			Tooltip.SetDefault("Rapidly shoots eyeballs");
		}
		public override void SetDefaults()
		{
			item.damage = 12;
			item.magic = true;
            item.mana = 20;
			item.width = 42;
			item.height = 42;
			item.useTime = 8;
			item.useAnimation = 32;
			item.useStyle = 5;
            Item.staff[item.type] = true;
			item.noMelee = true; 
			item.knockBack = 1;
            item.value = 35000;
            item.rare = 3;
            item.UseSound = SoundID.Item8;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Eyeball");
			item.shootSpeed = 6f;
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X + Main.rand.Next(-30, 30), position.Y + Main.rand.Next(-30, 30), speedX, speedY, type, damage, knockBack, player.whoAmI);
            return false;
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


