using JoostMod.Projectiles.Minions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Summon
{
    public class CactusStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactus Staff");
            Tooltip.SetDefault("Summons a Cactuar to fight for you");
        }
        public override void SetDefaults()
        {
            Item.damage = 150;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 10;
            Item.width = 58;
            Item.height = 58;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 7;
            Item.value = 10000000;
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item44;
            Item.shoot = ModContent.ProjectileType<Cactuar>();
            Item.shootSpeed = 10f;
            Item.buffType = ModContent.BuffType<Buffs.CactuarBuff>();  //The buff added to player after used the item
            Item.buffTime = 3600;               //The duration of the buff, here is 60 seconds
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
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.Cactustoken>()
                .Register();
        }
    }
}
