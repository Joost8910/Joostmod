using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Thrown
{
    public class AdamantiteChainedchainsaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Adamantite Chained-Chainsaw");
            Tooltip.SetDefault("'On a chain!'\n" + "Stacks up to 4");
        }
        public override void SetDefaults()
        {
            Item.damage = 33;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.width = 14;
            Item.height = 56;
            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5;
            Item.value = 12000;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Thrown.AdamantiteChainedchainsaw>();
            Item.shootSpeed = 16f;
            Item.maxStack = 4;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[Item.shoot] >= Item.stack)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.AdamantiteBar, 2)
                .AddIngredient(ItemID.Chain)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

    }
}

