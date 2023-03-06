using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
	public class EyeballStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eyeball Staff>();
			Tooltip.SetDefault("Rapidly shoots eyeballs");
		}
		public override void SetDefaults()
		{
			Item.damage = 12;
			Item.DamageType = DamageClass.Magic;
            Item.mana = 20;
			Item.width = 42;
			Item.height = 42;
			Item.useTime = 8;
			Item.useAnimation = 32;
			Item.useStyle = ItemUseStyleID.Shoot;
            Item.staff[Item.type] = true;
			Item.noMelee = true; 
			Item.knockBack = 1;
            Item.value = 35000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item8;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("Eyeball").Type;
			Item.shootSpeed = 6f;
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position.X + Main.rand.Next(-30, 30), position.Y + Main.rand.Next(-30, 30), velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
            return false;
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


