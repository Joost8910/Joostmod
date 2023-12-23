using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Magic;

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
			Item.shoot = ModContent.ProjectileType<Eyeball>();
			Item.shootSpeed = 6f;
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 pos = new(position.X + Main.rand.Next(-30, 31), position.Y + Main.rand.Next(-30, 31));
            if (Collision.SolidCollision(pos - new Vector2(8, 8), 16, 16))
			{
				pos = position + new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10));
            }
            Projectile.NewProjectile(source, pos, velocity, type, damage, knockback, player.whoAmI);
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


