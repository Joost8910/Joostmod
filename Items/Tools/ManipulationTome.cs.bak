using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles;

namespace JoostMod.Items.Tools
{
    public class ManipulationTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tome of Manipulation");
            Tooltip.SetDefault("Allows you to pick up and move friendly NPCs\n" + "Right click while holding the NPC to rapidly damage the NPC");
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.noMelee = true;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.reuseDelay = 5;
            Item.autoReuse = true;
            Item.channel = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<Manipulation>();
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
            damage = 25;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Book)
            .AddIngredient(ItemID.FallenStar)
            .AddIngredient(ItemID.GoldBar)
            .AddTile(TileID.WorkBenches)
            .Register();
            CreateRecipe()
            .AddIngredient(ItemID.Book)
            .AddIngredient(ItemID.FallenStar)
            .AddIngredient(ItemID.PlatinumBar)
            .AddTile(TileID.WorkBenches)
            .Register();
        }

    }
}

