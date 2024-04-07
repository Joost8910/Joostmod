using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Fishhooks;

namespace JoostMod.Items.Tools.Rods
{
    public class DukeFishRod : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Duke Fishrod");
        }
        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 36;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = 500000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<DukeFishHook2>();
            Item.shootSpeed = 18f;
            Item.fishingPole = 75;
        }

    }
}

