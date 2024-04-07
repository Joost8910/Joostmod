using JoostMod.DamageClasses;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Ammo
{
    public class SoulArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Arrow");
            Tooltip.SetDefault("Has a slight homing effect");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.DamageType = ModContent.GetInstance<MagicRangedHybrid>();
            Item.damage = 14;
            Item.width = 30;
            Item.height = 60;
            Item.consumable = true;
            Item.knockBack = 2.5f;
            Item.value = 150;
            Item.rare = ItemRarityID.Yellow;
            Item.shoot = ModContent.ProjectileType<Projectiles.Hybrid.SoulArrow>();
            Item.shootSpeed = 9f;
            Item.ammo = AmmoID.Arrow;
        }
        public override void AddRecipes()
        {
            CreateRecipe(75)
                .AddIngredient(ItemID.SpectreBar)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}

