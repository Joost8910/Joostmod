using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Minions;

namespace JoostMod.Items.Weapons.Summon
{
    public class IceXStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Core-X Staff");
            Tooltip.SetDefault("Summons an SA-X Core-X to fight for you");
        }
        public override void SetDefaults()
        {
            Item.damage = 300;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 10;
            Item.width = 64;
            Item.height = 64;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 9;
            Item.value = 10000000;
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item44;
            Item.shoot = ModContent.ProjectileType<IceXMinion>();
            Item.shootSpeed = 10f;
            Item.buffType = ModContent.BuffType<Buffs.IceXMinionBuff>();
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
                .AddIngredient<Materials.IceCoreX>()
                .Register();
        }

    }
}


