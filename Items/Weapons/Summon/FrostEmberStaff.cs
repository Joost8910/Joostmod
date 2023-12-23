using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Minions;
using JoostMod.Items.Weapons.Magic;

namespace JoostMod.Items.Weapons.Summon
{
    public class FrostEmberStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frostfire Staff");
            Tooltip.SetDefault("Summons an Frostfire Ember to fight for you");
        }
        public override void SetDefaults()
        {
            Item.damage = 6;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 10;
            Item.width = 36;
            Item.height = 36;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = 500;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item44;
            Item.shoot = ModContent.ProjectileType<FrostEmberMinion>();
            Item.shootSpeed = 7f;
            Item.buffType = ModContent.BuffType<Buffs.FrostEmberMinion>();
            Item.buffTime = 3600;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
        }
        public override bool CanShoot(Player player) => player.altFunctionUse != 2;
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<EmberStaff>()
                .AddIngredient(ItemID.IceTorch, 99)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}


