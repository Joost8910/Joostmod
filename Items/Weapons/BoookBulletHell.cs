using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Weapons
{
	public class BoookBulletHell : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Boook's Bullet Hell");
			Tooltip.SetDefault("'Crazy Gun Contraption of the legendary Boook'\n" +
			"Does more damage as you kill bosses throughout the game\n" +
			"Fires a crazy amount of bullets\n" + 
            "Right click to fire arrows\n" + 
            "Does 20% reduced damage when using Chlorophyte Bullets");
		}
		public override void SetDefaults()
		{
			item.damage = 100;
			item.ranged = true;
			item.width = 56;
			item.height = 48;
			item.useTime = 2;
			item.useAnimation = 16;
			item.reuseDelay = 12;
			item.useStyle = 5;
			item.noMelee = true; 
			item.knockBack = 5;
            item.value = 200000;
            item.rare = 9;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
			item.shoot = 10; 
			item.shootSpeed = 10f;
			item.useAmmo = AmmoID.Bullet;
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IllegalGunParts);
            recipe.AddIngredient(null, "EvilStone");
            recipe.AddIngredient(null, "SkullStone");
            recipe.AddIngredient(null, "JungleStone");
            recipe.AddIngredient(null, "InfernoStone");
            recipe.AddTile(null, "ShrineOfLegends");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.overrideColor = new Color(0, 255, (int)(51 + (Main.DiscoG * 0.5f)));
                }
			}
		}
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            mult *= JoostGlobalItem.LegendaryDamage() * 0.04f;
        }
        public override void UpdateInventory (Player player)
		{
			player.GetModPlayer<JoostPlayer>().legendOwn = true;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(0, 0);
		}
		public override bool ConsumeAmmo(Player player)
		{
			return player.itemAnimation < item.useAnimation / 2;
		}
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.UseSound = SoundID.Item102;
                item.shoot = 1;
                item.useAmmo = AmmoID.Arrow;
            }
            else
            {
                item.UseSound = SoundID.Item11;
                item.shoot = 10;
                item.useAmmo = AmmoID.Bullet;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 32;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            if (type == ProjectileID.ChlorophyteBullet)
            {
                damage = (int)(damage * 0.8f);
            }
            float spread = 180f * 0.0174f;
			float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
			double startAngle = Math.Atan2(speedX, speedY)- spread/2;
			double deltaAngle = spread/16f;
			double offsetAngle;
			int dir = player.altFunctionUse == 2 ? player.itemAnimation : 15 - player.itemAnimation;
            Main.PlaySound(2, player.Center, player.altFunctionUse == 2 ? 102 : 11);
				offsetAngle = startAngle + deltaAngle * dir;
            Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockBack, player.whoAmI);

            dir = player.altFunctionUse == 2 ? (player.itemAnimation-1) : 15 - (player.itemAnimation-1);
            offsetAngle = startAngle + deltaAngle * dir;
            Projectile.NewProjectile(position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), type, damage, knockBack, player.whoAmI);
			return false;
		}
	}
}
