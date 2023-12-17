using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
	public class TheHive : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Hive");
            Tooltip.SetDefault("Rapidly spews bees that deal one-third damage");
		}
		public override void SetDefaults()
		{
			Item.damage = 25;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.noMelee = true;
			Item.scale = 1f;
			Item.noUseGraphic = true;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 19;
			Item.useAnimation = 19;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 6.5f;
			Item.value = 55000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.channel = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.Melee.TheHive>();
			Item.shootSpeed = 10f;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(230, 204, 128);
                }
            }
        }

    }
}

