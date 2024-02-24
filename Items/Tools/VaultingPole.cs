using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles;

namespace JoostMod.Items.Tools
{
    public class VaultingPole : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vaulting Pole");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 76;
            Item.noMelee = true;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.autoReuse = true;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<VaultPole>();
            Item.shootSpeed = 1;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
    }
}

