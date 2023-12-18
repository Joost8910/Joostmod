using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Ranged
{
    public class NapalmLauncher : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Napalm Launcher");
            Tooltip.SetDefault("Uses napalms for ammo\n" +
                "25% chance to not consume ammo");
        }
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 64;
            Item.height = 64;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 225000;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.Napalm>();
            Item.shootSpeed = 8f;
            Item.useAmmo = ModContent.ItemType<Ammo.Napalm>();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 0);
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() > .25f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Materials.FireEssence>(50)
                .AddRecipeGroup(nameof(ItemID.CobaltBar), 4)
                .AddRecipeGroup(nameof(ItemID.MythrilBar), 4)
                .AddRecipeGroup(nameof(ItemID.AdamantiteBar), 4)
                .AddTile<Tiles.ElementalForge>()
                .Register();
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage.CombineWith(player.rocketDamage);
        }
    }
}


