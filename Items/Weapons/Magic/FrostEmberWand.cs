using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Magic;

namespace JoostMod.Items.Weapons.Magic
{
    public class FrostEmberWand : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frostfire Wand");
            Tooltip.SetDefault("Fires a small frostflame");
        }
        public override void SetDefaults()
        {
            Item.damage = 6;
            Item.DamageType = DamageClass.Magic;
            Item.width = 24;
            Item.height = 24;
            Item.mana = 3;
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = 50;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<FrostFlame>();
            Item.shootSpeed = 9f;
        }
        public override void HoldItem(Player player)
        {
            if (Main.rand.NextBool(player.itemAnimation > 0 ? 40 : 80))
            {
                Dust.NewDust(new Vector2(player.itemLocation.X + 16f * player.direction, player.itemLocation.Y - 14f * player.gravDir), 4, 4, 67);
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<EmberWand>()
                .AddIngredient(ItemID.IceTorch, 99)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

    }
}


