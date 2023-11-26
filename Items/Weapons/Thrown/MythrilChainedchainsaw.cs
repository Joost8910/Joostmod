using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Thrown
{
    public class MythrilChainedchainsaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mythril Chained-Chainsaw");
            Tooltip.SetDefault("'On a chain!'\n" + "Stacks up to 4");
        }
        public override void SetDefaults()
        {
            Item.damage = 29;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.width = 14;
            Item.height = 54;
            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("MythrilChainedchainsaw").Type;
            Item.shootSpeed = 14f;
            Item.maxStack = 4;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[Item.shoot] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("MythrilChainedchainsaw2").Type] >= Item.stack)
            {
                return false;
            }
            else return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.MythrilBar, 2)
                .AddIngredient(ItemID.Chain)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

    }
}
