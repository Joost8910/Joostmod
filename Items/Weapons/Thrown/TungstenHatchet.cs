using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Thrown
{
    public class TungstenHatchet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tungsten Hatchet");
            Tooltip.SetDefault("'On a chain!'\n" + "Stacks up to 3");
        }
        public override void SetDefaults()
        {
            Item.damage = 11;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.width = 26;
            Item.height = 22;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4;
            Item.value = 1000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.TungstenHatchet>();
            Item.shootSpeed = 12f;
            Item.maxStack = 3;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[Item.shoot] + player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Thrown.TungstenHatchet2>()] >= Item.stack)
            {
                return false;
            }
            else return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.TungstenBar, 2)
                .AddIngredient(ItemID.Chain)
                .AddTile(TileID.Anvils)
                .Register();
        }

    }
}

