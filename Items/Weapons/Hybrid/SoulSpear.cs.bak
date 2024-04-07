using JoostMod.DamageClasses;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons.Hybrid
{
    public class SoulSpear : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Spear");
            Tooltip.SetDefault("Fire a piercing soul spear\n" + "Goes through blocks for a short distance");
        }
        public override void SetDefaults()
        {
            Item.damage = 225;
            Item.DamageType = ModContent.GetInstance<MagicThrowingHybrid>();
            Item.mana = 20;
            Item.width = 36;
            Item.height = 36;
            Item.useTime = 25;
            Item.useAnimation = 50;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 6.5f;
            Item.value = 500000;
            Item.rare = ItemRarityID.Yellow;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item8;
            Item.shoot = ModContent.ProjectileType<Projectiles.Hybrid.SoulSpear>();
            Item.shootSpeed = 15f;
        }
        public override void HoldItem(Player player)
        {
            if (player.itemAnimation > 1 && player.itemAnimation > Item.useTime)
            {
                int dustType = 92;
                Vector2 pos = player.MountedCenter + new Vector2(6 * player.direction - 9, -16);
                int dustIndex = Dust.NewDust(pos, 18, 18, dustType);
                Dust dust = Main.dust[dustIndex];
                dust.noGravity = true;
            }
            base.HoldItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.itemAnimation > Item.useTime)
            {
                SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_book_staff_cast_2"), player.Center); // 203
                return false;
            }
            SoundEngine.PlaySound(SoundID.Item28, player.Center);
            return true;
        }
        public override bool RangedPrefix()
        {
            return Main.rand.NextBool(2);
        }
        public override bool MagicPrefix()
        {
            return true;
        }
    }
}


