using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Pets
{
	public class StormyCollar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flower Collar");
			Tooltip.SetDefault("In loving memory of Stormy");
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.ZephyrFish);
			Item.shoot = Mod.Find<ModProjectile>("Stormy").Type;
			Item.buffType = Mod.Find<ModBuff>("Stormy").Type;
            Item.rare = ItemRarityID.Green;
            Item.value = 0;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line in list)
            {
                if (line.Mod == "Terraria" && line.Name == "ItemName")
                {
                    line.OverrideColor = new Color(107, 173, 120);
                }
                if (line.Mod == "Terraria" && line.Name == "Price")
                {
                    line.Text = "Priceless";
                }
            }
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600, true);
			}
		}
	}
}