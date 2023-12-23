using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Minions;

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
			Item.shoot = ModContent.ProjectileType<ICUMinion>();
			Item.shootSpeed = 7f;
			Item.buffType = ModContent.BuffType<Buffs.ICUMinion>();
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

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
        }
        public override bool CanShoot(Player player)
        {
            return player.altFunctionUse != 2;
        }
		
	}
}


