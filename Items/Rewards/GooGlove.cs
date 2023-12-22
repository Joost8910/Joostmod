using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using JoostMod.Projectiles.Thrown;

namespace JoostMod.Items.Rewards
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class GooGlove : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gooey Glove");
			Tooltip.SetDefault("Flings multiple globules of bouncy pink gel");
		}
		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.DamageType = DamageClass.Throwing;
			Item.width = 32;
			Item.height = 34;
			Item.useTime = 43;
			Item.useAnimation = 43;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
            Item.noUseGraphic = true;
			Item.knockBack = 2;
			Item.value = 10000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<PinkGoo>(); 
			Item.shootSpeed = 9.5f;
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
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int numberProjectiles = 2 + Main.rand.Next(3); 
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(30)); 
				float scale = 1f - (Main.rand.NextFloat() * .3f);
				perturbedSpeed = perturbedSpeed * scale; 
				Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
			}
			return false; 
		}
	}
}
