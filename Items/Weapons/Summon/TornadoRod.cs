using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Minions;

namespace JoostMod.Items.Weapons.Summon
{
    public class TornadoRod : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tornado Rod");
            Tooltip.SetDefault("Summons a miniature tornado to fight for you");
        }
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 10;
            Item.width = 36;
            Item.height = 36;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = 225000;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item44;
            Item.shoot = ModContent.ProjectileType<WindMinion>();
            Item.shootSpeed = 7f;
            Item.buffType = ModContent.BuffType<Buffs.WindMinionBuff>();
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
                .AddIngredient<Materials.TinyTwister>(50)
                .AddRecipeGroup(nameof(ItemID.CobaltBar), 4)
                .AddRecipeGroup(nameof(ItemID.MythrilBar), 4)
                .AddRecipeGroup(nameof(ItemID.AdamantiteBar), 4)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }

    }
}


