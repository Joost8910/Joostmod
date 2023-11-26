using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

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
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 7.5f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double baseAngle = Math.Atan2(velocity.X, velocity.Y);
            double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
            velocity.X = baseSpeed * (float)Math.Sin(randomAngle);
            velocity.Y = baseSpeed * (float)Math.Cos(randomAngle);
            if (type == ProjectileID.SandBallGun)
            {
                type = Mod.Find<ModProjectile>("SandBlock").Type;
            }
            if (type == ProjectileID.EbonsandBallGun)
            {
                type = Mod.Find<ModProjectile>("EbonSandBlock").Type;
            }
            if (type == ProjectileID.PearlSandBallGun)
            {
                type = Mod.Find<ModProjectile>("PearlSandBlock").Type;
            }
            if (type == ProjectileID.CrimsandBallGun)
            {
                type = Mod.Find<ModProjectile>("CrimSandBlock").Type;
            }
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.DesertCore>()
                .AddRecipeGroup("JoostMod:AnyAdamantite", 8)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}


