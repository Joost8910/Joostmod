//TODO: Change up the method for throwing the giant shuriken, the dodge jump makes aiming a slow multihit frustrating
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;
using JoostMod.Items.Legendaries;
using JoostMod.Projectiles.Thrown;

namespace JoostMod.Items.Legendaries.Weps
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class BrokenGlove : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gnunderson's Glove");
            /* Tooltip.SetDefault("'Glove of the legendary Gnunderson'\n" +
            "Does more damage as you kill bosses throughout the game\n" +
            "Rapidly throws shurikens\n" +
            "Right click for an evasive jump that throws many shurikens\n" +
            "Hold UP during the jump to throw a giant shuriken\n" +
            "(4 Second Cooldown"); */
            ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<GnunderGlove>();
        }
        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 30;
            Item.height = 28;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3;
            Item.rare = ItemRarityID.Gray;
            Item.UseSound = SoundID.Item7;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<Gnunderken>();
            Item.shootSpeed = 8f;
            Item.value = 0;
        }
        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            return false;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 15f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double baseAngle = Math.Atan2(velocity.X, velocity.Y);
            double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
            velocity.X = baseSpeed * (float)Math.Sin(randomAngle);
            velocity.Y = baseSpeed * (float)Math.Cos(randomAngle);
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 1);
            return false;
        }

    }
}

