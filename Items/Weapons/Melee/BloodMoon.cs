using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons.Melee
{
    public class BloodMoon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Moon");
        }
        public override void SetDefaults()
        {
            Item.damage = 77;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.width = 30;
            Item.height = 32;
            Item.useTime = 44;
            Item.useAnimation = 44;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 9;
            Item.value = 54000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.channel = true;
            Item.useTurn = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Melee.BloodMoon>();
            Item.shootSpeed = 15f;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position.Y -= Item.scale * 38 - 38;
            damage /= 2;
        }
        public override bool MeleePrefix()
        {
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Sunfury)
                .AddIngredient(ItemID.BlueMoon)
                .AddIngredient<TheRose>()
                .AddIngredient(ItemID.TheMeatball)
                .AddTile(TileID.DemonAltar)
                .Register();
        }

    }
}

