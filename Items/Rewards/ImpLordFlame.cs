using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
	public class ImpLordFlame : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lord's Flame");
            Tooltip.SetDefault("Hold attack to charge a bigger fireball");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 8));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
		public override void SetDefaults()
		{
			item.damage = 60;
            item.mana = 10;
            item.magic = true;
			item.noMelee = true;
			item.scale = 1f;
			item.noUseGraphic = true;
			item.width = 26;
			item.height = 26;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 3.5f;
			item.value = 80000;
			item.rare = 3;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.channel = true;
			item.shoot = mod.ProjectileType("ImpLordFlame");
			item.shootSpeed = 12f;
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

