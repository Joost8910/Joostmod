using JoostMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons.Melee
{
    public class TerraFury : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Fury");
        }
        public override void SetDefaults()
        {
            Item.damage = 164;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.width = 60;
            Item.height = 64;
            Item.useTime = 44;
            Item.useAnimation = 44;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 10;
            Item.value = 1000000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.channel = true;
            Item.useTurn = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.TerraFury>();
            Item.shootSpeed = 28f;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position.Y -= Item.scale * 42 - 42;
            damage /= 2;
            if (player.altFunctionUse == 2)
                type = ModContent.ProjectileType<TerraFury2>();
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool MeleePrefix()
        {
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<TrueHallowedFlail>()
                .AddIngredient<TrueNightsFury>()
                .AddTile(TileID.MythrilAnvil)
                .Register();
            CreateRecipe()
                .AddIngredient<TrueHallowedFlail>()
                .AddIngredient<TrueBloodMoon>()
                .AddTile(TileID.MythrilAnvil)
                .Register();

        }

    }
}

