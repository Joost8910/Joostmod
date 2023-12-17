using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Minions;

namespace JoostMod.Items.Weapons.Summon
{
    public class HarpyStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Harpy Rod");
            Tooltip.SetDefault("Summons a miniature Harpy to fight for you");
        }
        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 10;
            Item.width = 34;
            Item.height = 34;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = 25000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item44;
            Item.shoot = ModContent.ProjectileType<HarpyMinion>();
            Item.shootSpeed = 7f;
            Item.buffType = ModContent.BuffType<Buffs.HarpyMinionBuff>();
            Item.buffTime = 3600;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position = Main.MouseWorld;
            return player.altFunctionUse != 2;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim(false);
            }
            return base.UseItem(player);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ShadowScale)
                .AddIngredient(ItemID.Feather, 12)
                .AddTile(TileID.Anvils)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.TissueSample)
                .AddIngredient(ItemID.Feather, 12)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}


