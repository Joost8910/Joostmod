using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Ammo
{
	public class HungeringArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hungering Arrow");
			Tooltip.SetDefault("A homing arrow with a 35% chance to pierce enemies");
		}
		public override void SetDefaults()
		{
			Item.maxStack = 1;
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 8;
			Item.width = 18;
			Item.height = 44;
			Item.consumable = false;
			Item.knockBack = 0f;
			Item.value = 300000;
			Item.rare = ItemRarityID.Orange;
			Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.HungeringArrow>();
			Item.shootSpeed = 7f;
			Item.ammo = AmmoID.Arrow;
		}
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(255, 128, 0);
                }
            }
        }
	}
}

