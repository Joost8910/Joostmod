using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
	public class ObservantStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Observant Staff>();
			Tooltip.SetDefault("Summons an ICU to fight for you");
		}
		public override void SetDefaults()
		{
			Item.damage = 8;
			Item.DamageType = DamageClass.Summon;
			Item.mana = 10;
			Item.width = 36;
			Item.height = 36;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true; 
			Item.knockBack = 0;
            Item.value = 40000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item44;
			Item.shoot = Mod.Find<ModProjectile>("ICUMinion").Type;
			Item.shootSpeed = 7f;
			Item.buffType = Mod.Find<ModBuff>("ICUMinion").Type;
			Item.buffTime = 3600;
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
        public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			position = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
			return player.altFunctionUse != 2;
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			if(player.altFunctionUse == 2)
			{
				player.MinionNPCTargetAim(false);
			}
			return base.UseItem(player);
		}
	}
}


