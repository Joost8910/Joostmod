using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Summon
{
    public class PlagueOfToads : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plague of Toads");
            Tooltip.SetDefault("Summons multiple toads to chase down enemies");
        }
        public override void SetDefaults()
        {
            Item.damage = 44;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 20;
            Item.maxStack = 1;
            Item.consumable = false;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 3;
            Item.value = 300000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("Toad").Type;
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
            float spread = 90f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double baseAngle = Math.Atan2(velocity.X, velocity.Y);
            for (int i = 0; i < 4; i++)
            {
                double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
                velocity.X = baseSpeed * (float)Math.Sin(randomAngle);
                velocity.Y = baseSpeed * (float)Math.Cos(randomAngle);

                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
            }
            SoundEngine.PlaySound(SoundID.Zombie13, player.position);
            return false;
        }
    }
}

