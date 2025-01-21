using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;
using JoostMod.Items.Legendaries;
using JoostMod.Projectiles.Magic;
using Terraria.Audio;

namespace JoostMod.Items.Legendaries.Weps
{
    public class BrokenBulletHell : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Boook's Bullet Hell");
            /* Tooltip.SetDefault("'Crazy Gun Contraption of the legendary Boook'\n" +
            "Does more damage as you kill bosses throughout the game\n" +
            "Fires a crazy amount of bullets\n" +
            "Right click to spin the barrel\n" +
            "Deals 40% reduced damage when using homing ammunition\n" +
            "50% chance to not consume ammo"); */
            ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<BoookBulletHell>();
        }
        public override void SetDefaults()
        {
            Item.damage = 4;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 64;
            Item.height = 48;
            Item.useTime = 4;
            Item.useAnimation = 28;
            Item.reuseDelay = 9;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = 0;
            Item.rare = ItemRarityID.Gray;
            Item.UseSound = SoundID.Item7;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Bullet;
            Item.noUseGraphic = false;
        }
        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            return false;
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() < 0.5f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-13, -6);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 offSet = new Vector2(46, velocity.X > 0 ? -7 : 7);
            Vector2 muzzleOffset = offSet.RotatedBy(velocity.ToRotation());
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            float spread = MathHelper.ToRadians(270f);
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            int dir = ((velocity.X > 0 ? player.itemAnimationMax - player.itemAnimation : 
                player.itemAnimation - player.itemTime) / player.itemTime);
            offsetAngle = startAngle + deltaAngle * dir;
            SoundEngine.PlaySound(SoundID.Item41, position);
            Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockback, player.whoAmI);
            
            return false;

        }
    }
}
