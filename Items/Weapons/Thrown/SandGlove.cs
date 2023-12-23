using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Thrown;

namespace JoostMod.Items.Weapons.Thrown
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class SandGlove : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sandstorm Glove");
            Tooltip.SetDefault("Rapidly throws sand\nUses sand blocks as ammo");
        }
        public override void SetDefaults()
        {
            Item.damage = 28;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 28;
            Item.height = 30;
            Item.useTime = 7;
            Item.useAnimation = 7;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 4;
            Item.value = 225000;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item19;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.SandBallGun;
            Item.shootSpeed = 12.5f;
            Item.useAmmo = AmmoID.Sand;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            float spread = 7.5f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double baseAngle = Math.Atan2(velocity.X, velocity.Y);
            double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
            velocity.X = baseSpeed * (float)Math.Sin(randomAngle);
            velocity.Y = baseSpeed * (float)Math.Cos(randomAngle);
            if (type == ProjectileID.SandBallGun)
            {
                type = ModContent.ProjectileType<SandBlock>();
            }
            if (type == ProjectileID.EbonsandBallGun)
            {
                type = ModContent.ProjectileType<EbonSandBlock>();
            }
            if (type == ProjectileID.PearlSandBallGun)
            {
                type = ModContent.ProjectileType<PearlSandBlock>();
            }
            if (type == ProjectileID.CrimsandBallGun)
            {
                type = ModContent.ProjectileType<CrimSandBlock>();
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.DesertCore>()
                .AddRecipeGroup(nameof(ItemID.AdamantiteBar), 8)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}


