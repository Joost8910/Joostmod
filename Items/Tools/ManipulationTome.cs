using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

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
            Item.shoot = Mod.Find<ModProjectile>("Manipulation").Type;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position = Main.MouseWorld;
            damage = 25;
            return true;
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

