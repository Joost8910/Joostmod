using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles;
using JoostMod.Items.Tools.Hammers;

namespace JoostMod.Items.Tools
{
    public class LongPole : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Long Pole");
        }
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 130;
            Item.noMelee = true;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.autoReuse = true;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = 20000;
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<VaultPoleLong>();
            Item.shootSpeed = 1;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<VaultingPole>()
                .AddIngredient<VaultingPole>()
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
    }
}

