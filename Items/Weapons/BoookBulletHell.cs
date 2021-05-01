using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;

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
            "Right click to spin the barrel\n" + 
            "Deals 40% reduced damage when using homing ammunition\n" +
            "50% chance to not consume ammo");
		}
		public override void SetDefaults()
		{
			item.damage = 100;
			item.ranged = true;
			item.width = 64;
			item.height = 48;
			item.useTime = 4;
			item.useAnimation = 4;
			item.useStyle = 5;
			item.noMelee = true; 
			item.knockBack = 3;
            item.value = 300000;
            item.rare = 9;
			item.UseSound = SoundID.Item23;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("BoookBulletHell"); 
			item.shootSpeed = 10f;
			item.useAmmo = AmmoID.Bullet;
            item.noUseGraphic = true;
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
					line2.overrideColor = new Color(0, 255, (int)(51 + (Main.DiscoG * 0.75f)));
                }
			}
		}
        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            return false;
        }
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            mult *= JoostGlobalItem.LegendaryDamage() * 0.04f;
        }
        public override void UpdateInventory (Player player)
		{
			player.GetModPlayer<JoostPlayer>().legendOwn = true;
		}
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() < 0.5f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            type = mod.ProjectileType("BoookBulletHell");
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
        /*
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(0, 0);
        }
		public override bool ConsumeAmmo(Player player)
		{
			return player.itemAnimation < item.useAnimation / 2;
		}
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 0)
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
            if (ProjectileID.Sets.Homing[type])
            {
                damage = (int)(damage * 0.7f);
            }
            bool arrow = item.useAmmo == AmmoID.Arrow;
            damage = (int)(damage * (arrow ? player.arrowDamage : player.bulletDamage));
            float spread = 180f * 0.0174f;
			float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
			double startAngle = Math.Atan2(speedX, speedY)- spread/2;
			double deltaAngle = spread/16f;
			double offsetAngle;
			int dir = arrow ? player.itemAnimation : 15 - player.itemAnimation;
            Main.PlaySound(2, player.Center, arrow ? 102 : 11);
			offsetAngle = startAngle + deltaAngle * dir;
            Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockBack, player.whoAmI);

            dir = arrow ? (player.itemAnimation-1) : 15 - (player.itemAnimation-1);
            offsetAngle = startAngle + deltaAngle * dir;
            Projectile.NewProjectile(position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), type, damage, knockBack, player.whoAmI);
			return false;
		}
        */
    }
}
