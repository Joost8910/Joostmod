using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Melee
{
    public class Whirlwind : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Whirlwind");
            Tooltip.SetDefault("Increases defense by 20 while in use");
        }
        public override void SetDefaults()
        {
            Item.damage = 44;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 84;
            Item.height = 58;
            Item.noMelee = true;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 5;
            Item.useStyle = 3;
            Item.knockBack = 4;
            Item.value = 300000;
            Item.rare = ItemRarityID.Orange;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.Whirlwind>();
            Item.shootSpeed = 8f;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(255, 128, 0);
                }
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 0.01f, -8f, type, damage, knockback, player.whoAmI);
            return false;
        }

    }
}

