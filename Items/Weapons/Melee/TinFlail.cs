using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons.Melee
{
    public class TinFlail : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tin Flail");
        }
        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.scale = 0.825f;
            Item.noUseGraphic = true;
            Item.width = 30;
            Item.height = 32;
            Item.useTime = 44;
            Item.useAnimation = 44;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 3;
            Item.value = 1000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.channel = true;
            Item.useTurn = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.TinFlail>();
            Item.shootSpeed = 8.5f;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position.Y -= Item.scale * 30 - 30;
            damage /= 2;
        }

        public override bool MeleePrefix()
        {
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.TinBar, 8)
                .AddTile(TileID.Anvils)
                .Register();
        }

    }
}

