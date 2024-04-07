using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;
using JoostMod.Items.Legendaries;

namespace JoostMod.Items.Legendaries.Weps
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
            Item.damage = 100;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 64;
            Item.height = 48;
            Item.useTime = 4;
            Item.useAnimation = 4;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = 300000;
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item23;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.BoookBulletHell>();
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Bullet;
            Item.noUseGraphic = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IllegalGunParts)
                .AddIngredient<EvilStone>()
                .AddIngredient<SkullStone>()
                .AddIngredient<JungleStone>()
                .AddIngredient<InfernoStone>()
                .AddTile<Tiles.ShrineOfLegends>()
                .Register();
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(0, 255, (int)(51 + Main.DiscoG * 0.75f));
                }
            }
        }
        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            return false;
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage *= JoostGlobalItem.LegendaryDamage() * 0.04f;
        }
        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<JoostPlayer>().legendOwn = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() < 0.5f && !player.ItemAnimationJustStarted;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = ModContent.ProjectileType<Projectiles.Ranged.BoookBulletHell>();
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
        public override bool Shoot(Player player, ref Vector2 position, ref float velocity.X, ref float velocity.Y, ref int type, ref int damage, ref float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 32;
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
			float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
			double startAngle = Math.Atan2(velocity.X, velocity.Y)- spread/2;
			double deltaAngle = spread/16f;
			double offsetAngle;
			int dir = arrow ? player.itemAnimation : 15 - player.itemAnimation;
            Main.PlaySound(2, player.Center, arrow ? 102 : 11);
			offsetAngle = startAngle + deltaAngle * dir;
            Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockback, player.whoAmI);

            dir = arrow ? (player.itemAnimation-1) : 15 - (player.itemAnimation-1);
            offsetAngle = startAngle + deltaAngle * dir;
            Projectile.NewProjectile(source, position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), type, damage, knockback, player.whoAmI);
			return false;
		}
        */
    }
}
